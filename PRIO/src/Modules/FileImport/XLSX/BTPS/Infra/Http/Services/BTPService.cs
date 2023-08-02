﻿using AutoMapper;
using OfficeOpenXml;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Dtos;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.BTPS.ViewModels;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.Http.Services
{
    public class BTPService
    {
        private readonly IMapper _mapper;
        private readonly IBTPRepository _BTPRepository;
        private readonly IWellRepository _wellRepository;

        public BTPService(IMapper mapper, IBTPRepository btpRepository, IWellRepository wellRepository)
        {
            _mapper = mapper;
            _BTPRepository = btpRepository;
            _wellRepository = wellRepository;
        }

        public async Task<List<BTPDTO>> Get()
        {
            var BTPs = await _BTPRepository.GetAllBTPsAsync();
            var btpsDTO = _mapper.Map<List<BTP>, List<BTPDTO>>(BTPs);

            return btpsDTO;
        }
        public async Task<List<BTPDTO>> GetByType(string type)
        {
            var BTPs = await _BTPRepository.GetAllBTPsByTypeAsync(type);
            var btpsDTO = _mapper.Map<List<BTP>, List<BTPDTO>>(BTPs);

            return btpsDTO;
        }
        public async Task<BTPBase64DTO> GetById(Guid id)
        {
            var BTP = await _BTPRepository.GetByIdAsync(id);
            if (BTP is null)
                throw new NotFoundException("BTP não encontrado.");
            var btpsDTO = _mapper.Map<BTP, BTPBase64DTO>(BTP);

            return btpsDTO;
        }
        public async Task<ValidateDataBTPDTO> GetImportFiles(RequestWellTestXls body, User user)
        {
            var BTP = await _BTPRepository.GetByIdAsync(body.BTPId) ?? throw new NotFoundException("BTP não encontrado.");

            if (body.FileName.EndsWith(".xlsx") is false)
                throw new BadRequestException("O arquivo deve ter a extensão .xlsx", status: "Error");

            if (body.Type != BTP.Type)
                throw new ConflictException($"O modelo do arquivo não corresponde ao tipo {body.Type}");

            var well = await _wellRepository.GetByIdAsync(body.WellId) ?? throw new NotFoundException("Poço não encontrado.");

            var contentBase64 = body.ContentBase64?.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            using var stream = new MemoryStream(Convert.FromBase64String(contentBase64!));
            using ExcelPackage package = new(stream);
            var workbook = package.Workbook;
            var worksheet = BTP.BTPSheet is not null ? workbook.Worksheets[BTP.BTPSheet] : workbook.Worksheets[0];

            //Trated aligmentHour
            string valorDaCelula = worksheet.Cells[BTP.CellWellAlignmentHour].Value.ToString();
            bool checkAlignHour = decimal.TryParse(valorDaCelula, out var valor);
            string? align = "";
            if (checkAlignHour is true)
            {
                decimal x = valor * 24 * 60;
                int horas = (int)x / 60;
                int minutos = (int)x % 60;
                TimeSpan horaMinuto = new TimeSpan(horas, minutos, 0);
                align = horaMinuto.ToString();
            }

            //Trated Duration
            string[] duration = worksheet.Cells[BTP.CellDuration].Value.ToString().Split(' ');
            string valorTempo = duration[1];

            //Verify Well
            string? message = well.Name == worksheet.Cells[BTP.CellWellName].Value.ToString() ? "Sucess: Nome do poço encontrado corresponde ao xls" : "Warning: Nome do poço encontrado não corresponde ao xls";

            //Datas per hour
            var oilPerHourCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialOil].Value.ToString(), out var oilPerHourOut);
            decimal? oilPerHour = oilPerHourCheck is true ? oilPerHourOut / 24 : null;

            var gasPerHourCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialGas].Value.ToString(), out var gasPerHourOut);
            decimal? gaslPerHour = gasPerHourCheck is true ? gasPerHourOut / 24 : null;

            var waterPerHourCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialWater].Value.ToString(), out var waterPerHourOut);
            decimal? waterPerHour = waterPerHourCheck is true ? waterPerHourOut / 24 : null;

            var liquidPerHourCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialLiquid].Value.ToString(), out var liquidPerHourOut);
            decimal? liquidPerHour = liquidPerHourCheck is true ? liquidPerHourOut / 24 : null;

            var content = new BTPBase64
            {
                Id = Guid.NewGuid(),
                Filename = body.FileName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Type = body.Type,
                FileContent = body.ContentBase64,
                IsActive = true,
                User = user
            };
            var data = new BTPData
            {
                Id = Guid.NewGuid(),
                Filename = body.FileName,
                Type = body.Type,
                IsValid = body.isValid,
                ApplicationDate = body.applicationDate,
                PotencialLiquid = worksheet.Cells[BTP.CellPotencialLiquid].Value.ToString(),
                PotencialLiquidPerHour = liquidPerHour != null ? liquidPerHour.ToString() : "0",
                PotencialOil = worksheet.Cells[BTP.CellPotencialOil].Value.ToString(),
                PotencialOilPerHour = oilPerHour != null ? oilPerHour.ToString() : "0",
                PotencialGas = worksheet.Cells[BTP.CellPotencialGas].Value.ToString(),
                PotencialGasPerHour = gaslPerHour != null ? gaslPerHour.ToString() : "0",
                PotencialWater = worksheet.Cells[BTP.CellPotencialWater].Value.ToString(),
                PotencialWaterPerHour = waterPerHour != null ? waterPerHour.ToString() : "0",
                Duration = valorTempo,
                InitialDate = worksheet.Cells[BTP.CellInitialDate].Value.ToString(),
                FinalDate = worksheet.Cells[BTP.CellFinalDate].Value.ToString(),
                BTPNumber = worksheet.Cells[BTP.CellBTPNumber].Value.ToString(),
                MPointGas = worksheet.Cells[BTP.CellMPointGas].Value.ToString(),
                MPointOil = worksheet.Cells[BTP.CellMPointOil].Value.ToString(),
                MPointWater = worksheet.Cells[BTP.CellMPointWater].Value.ToString(),
                BSW = worksheet.Cells[BTP.CellBSW].Value.ToString(),
                RGO = worksheet.Cells[BTP.CellRGO].Value.ToString(),
                WellAlignmentData = worksheet.Cells[BTP.CellWellAlignmentData].Value.ToString(),
                WellAlignmentHour = align,
                WellName = worksheet.Cells[BTP.CellWellName].Value.ToString(),
                BTPSheet = BTP.BTPSheet,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                BTPBase64 = content,
                Well = well
            };

            var BTPdataDTO = _mapper.Map<BTPData, BTPDataDTO>(data);
            var createDataDTO = new ValidateDataBTPDTO
            {
                Message = message,
                Data = BTPdataDTO
            };

            return createDataDTO;
        }
        public async Task<ValidateDataBTPDTO> PostImportFiles(RequestWellTestXls body, User user)
        {
            var BTP = await _BTPRepository.GetByIdAsync(body.BTPId) ?? throw new NotFoundException("BTP não encontrado.");

            if (body.FileName.EndsWith(".xlsx") is false)
                throw new BadRequestException("O arquivo deve ter a extensão .xlsx", status: "Error");

            if (body.Type != BTP.Type)
                throw new ConflictException($"O modelo do arquivo não corresponde ao tipo {body.Type}");

            var well = await _wellRepository.GetByIdAsync(body.WellId) ?? throw new NotFoundException("Poço não encontrado.");

            var contentBase64 = body.ContentBase64?.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            using var stream = new MemoryStream(Convert.FromBase64String(contentBase64!));
            using ExcelPackage package = new(stream);
            var workbook = package.Workbook;
            var worksheet = BTP.BTPSheet is not null ? workbook.Worksheets[BTP.BTPSheet] : workbook.Worksheets[0];

            //Trated aligmentHour
            string valorDaCelula = worksheet.Cells[BTP.CellWellAlignmentHour].Value.ToString();
            bool checkAlignHour = decimal.TryParse(valorDaCelula, out var valor);
            string? align = "";
            if (checkAlignHour is true)
            {
                decimal x = valor * 24 * 60;
                int horas = (int)x / 60;
                int minutos = (int)x % 60;
                TimeSpan horaMinuto = new TimeSpan(horas, minutos, 0);
                align = horaMinuto.ToString();
            }

            //Trated Duration
            string[] duration = worksheet.Cells[BTP.CellDuration].Value.ToString().Split(' ');
            string valorTempo = duration[1];

            //Verify Well
            string? message = well.Name == worksheet.Cells[BTP.CellWellName].Value.ToString() ? "Sucess: Nome do poço encontrado corresponde ao xls" : "Warning: Nome do poço encontrado não corresponde ao xls";

            var content = new BTPBase64
            {
                Id = Guid.NewGuid(),
                Filename = body.FileName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Type = body.Type,
                FileContent = body.ContentBase64,
                IsActive = true,
                User = user
            };
            var data = new BTPData
            {
                Filename = body.FileName,
                Type = body.Type,
                PotencialGas = worksheet.Cells[BTP.CellPotencialGas].Value.ToString(),
                PotencialOil = worksheet.Cells[BTP.CellPotencialOil].Value.ToString(),
                PotencialWater = worksheet.Cells[BTP.CellPotencialWater].Value.ToString(),
                Duration = valorTempo,
                InitialDate = worksheet.Cells[BTP.CellInitialDate].Value.ToString(),
                FinalDate = worksheet.Cells[BTP.CellFinalDate].Value.ToString(),
                BTPNumber = worksheet.Cells[BTP.CellBTPNumber].Value.ToString(),
                MPointGas = worksheet.Cells[BTP.CellMPointGas].Value.ToString(),
                MPointOil = worksheet.Cells[BTP.CellMPointOil].Value.ToString(),
                MPointWater = worksheet.Cells[BTP.CellMPointWater].Value.ToString(),
                BSW = worksheet.Cells[BTP.CellBSW].Value.ToString(),
                RGO = worksheet.Cells[BTP.CellRGO].Value.ToString(),
                PotencialLiquid = worksheet.Cells[BTP.CellPotencialLiquid].Value.ToString(),
                WellAlignmentData = worksheet.Cells[BTP.CellWellAlignmentData].Value.ToString(),
                WellAlignmentHour = align,
                WellName = worksheet.Cells[BTP.CellWellName].Value.ToString(),
                BTPSheet = BTP.BTPSheet,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                BTPBase64 = content,
                Well = well
            };

            var BTPdataDTO = _mapper.Map<BTPData, BTPDataDTO>(data);
            var createDataDTO = new ValidateDataBTPDTO
            {
                Message = message,
                Data = BTPdataDTO
            };

            return createDataDTO;
        }
    }
}