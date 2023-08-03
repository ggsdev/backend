using AutoMapper;
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
        public async Task<BTPDataDTO> GetByDate(string date, Guid wellId)
        {
            var BTPdata = await _BTPRepository.GetByDateAsync(date, wellId);
            if (BTPdata is null)
                throw new NotFoundException("Dados do BTP não encontrado.");
            var btpsDTO = _mapper.Map<BTPData, BTPDataDTO>(BTPdata);

            return btpsDTO;
        }
        public async Task<ValidateDataBTPDTO> ValidateImportFiles(RequestWellTestXls body, User user)
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

            //ToDecimal
            var oilCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialOil].Value.ToString(), out var oilDecimal);
            decimal oilDecimalFormated = Math.Round(oilDecimal, 5, MidpointRounding.AwayFromZero);
            decimal oilPerHourDecimalFormated = Math.Round(oilDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var gasCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialGas].Value.ToString(), out var gasDecimal);
            decimal gasDecimalFormated = Math.Round(gasDecimal, 5, MidpointRounding.AwayFromZero);
            decimal gasPerHourDecimalFormated = Math.Round(gasDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var waterCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialWater].Value.ToString(), out var waterDecimal);
            decimal waterDecimalFormated = Math.Round(waterDecimal, 5, MidpointRounding.AwayFromZero);
            decimal waterPerHourDecimalFormated = Math.Round(waterDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var liquidCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialLiquid].Value.ToString(), out var liquidDecimal);
            decimal liquidDecimalFormated = Math.Round(liquidDecimal, 5, MidpointRounding.AwayFromZero);
            decimal liquidPerHourDecimalFormated = Math.Round(liquidDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var rgoCheck = decimal.TryParse(worksheet.Cells[BTP.CellRGO].Value.ToString(), out var rgoDecimal);
            decimal rgoDecimalFormated = Math.Round(rgoDecimal, 5, MidpointRounding.AwayFromZero);
            var bswCheck = decimal.TryParse(worksheet.Cells[BTP.CellBSW].Value.ToString(), out var bswDecimal);
            decimal bswDecimalFormated = Math.Round(bswDecimal, 5, MidpointRounding.AwayFromZero);

            if (oilCheck is false || gasCheck is false || waterCheck is false || liquidCheck is false || rgoCheck is false || bswCheck is false)
            {
                throw new ConflictException("Dados decimais não podem ser convertidos.");
            }

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
                IsValid = body.IsValid,
                ApplicationDate = body.ApplicationDate,
                PotencialLiquid = liquidDecimalFormated,
                PotencialLiquidPerHour = liquidPerHourDecimalFormated,
                PotencialOil = oilDecimalFormated,
                PotencialOilPerHour = oilPerHourDecimalFormated,
                PotencialGas = gasDecimalFormated,
                PotencialGasPerHour = gasPerHourDecimalFormated,
                PotencialWater = waterDecimalFormated,
                PotencialWaterPerHour = waterPerHourDecimalFormated,
                Duration = valorTempo,
                InitialDate = worksheet.Cells[BTP.CellInitialDate].Value.ToString(),
                FinalDate = worksheet.Cells[BTP.CellFinalDate].Value.ToString(),
                BTPNumber = worksheet.Cells[BTP.CellBTPNumber].Value.ToString(),
                MPointGas = worksheet.Cells[BTP.CellMPointGas].Value.ToString(),
                MPointOil = worksheet.Cells[BTP.CellMPointOil].Value.ToString(),
                MPointWater = worksheet.Cells[BTP.CellMPointWater].Value.ToString(),
                BSW = bswDecimalFormated,
                RGO = rgoDecimalFormated,
                WellAlignmentData = worksheet.Cells[BTP.CellWellAlignmentData].Value.ToString(),
                WellAlignmentHour = align,
                WellName = worksheet.Cells[BTP.CellWellName].Value.ToString(),
                BTPSheet = BTP.BTPSheet,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Well = well,
                BTPBase64 = content
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

            //ToDecimal
            var oilCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialOil].Value.ToString(), out var oilDecimal);
            decimal oilDecimalFormated = Math.Round(oilDecimal, 5, MidpointRounding.AwayFromZero);
            decimal oilPerHourDecimalFormated = Math.Round(oilDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var gasCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialGas].Value.ToString(), out var gasDecimal);
            decimal gasDecimalFormated = Math.Round(gasDecimal, 5, MidpointRounding.AwayFromZero);
            decimal gasPerHourDecimalFormated = Math.Round(gasDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var waterCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialWater].Value.ToString(), out var waterDecimal);
            decimal waterDecimalFormated = Math.Round(waterDecimal, 5, MidpointRounding.AwayFromZero);
            decimal waterPerHourDecimalFormated = Math.Round(waterDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var liquidCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialLiquid].Value.ToString(), out var liquidDecimal);
            decimal liquidDecimalFormated = Math.Round(liquidDecimal, 5, MidpointRounding.AwayFromZero);
            decimal liquidPerHourDecimalFormated = Math.Round(liquidDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var rgoCheck = decimal.TryParse(worksheet.Cells[BTP.CellRGO].Value.ToString(), out var rgoDecimal);
            decimal rgoDecimalFormated = Math.Round(rgoDecimal, 5, MidpointRounding.AwayFromZero);
            var bswCheck = decimal.TryParse(worksheet.Cells[BTP.CellBSW].Value.ToString(), out var bswDecimal);
            decimal bswDecimalFormated = Math.Round(bswDecimal, 5, MidpointRounding.AwayFromZero);

            if (oilCheck is false || gasCheck is false || waterCheck is false || liquidCheck is false || rgoCheck is false || bswCheck is false)
            {
                throw new ConflictException("Dados decimais não podem ser convertidos.");
            }

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
                IsValid = body.IsValid,
                ApplicationDate = body.ApplicationDate,
                PotencialLiquid = liquidDecimalFormated,
                PotencialLiquidPerHour = liquidPerHourDecimalFormated,
                PotencialOil = oilDecimalFormated,
                PotencialOilPerHour = oilPerHourDecimalFormated,
                PotencialGas = gasDecimalFormated,
                PotencialGasPerHour = gasPerHourDecimalFormated,
                PotencialWater = waterDecimalFormated,
                PotencialWaterPerHour = waterPerHourDecimalFormated,
                Duration = valorTempo,
                InitialDate = worksheet.Cells[BTP.CellInitialDate].Value.ToString(),
                FinalDate = worksheet.Cells[BTP.CellFinalDate].Value.ToString(),
                BTPNumber = worksheet.Cells[BTP.CellBTPNumber].Value.ToString(),
                MPointGas = worksheet.Cells[BTP.CellMPointGas].Value.ToString(),
                MPointOil = worksheet.Cells[BTP.CellMPointOil].Value.ToString(),
                MPointWater = worksheet.Cells[BTP.CellMPointWater].Value.ToString(),
                BSW = bswDecimalFormated,
                RGO = rgoDecimalFormated,
                WellAlignmentData = worksheet.Cells[BTP.CellWellAlignmentData].Value.ToString(),
                WellAlignmentHour = align,
                WellName = worksheet.Cells[BTP.CellWellName].Value.ToString(),
                BTPSheet = BTP.BTPSheet,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Well = well,
                BTPBase64 = content
            };
            await _BTPRepository.AddBTPAsync(data);
            await _BTPRepository.AddBTPBase64Async(content);

            var BTPdataDTO = _mapper.Map<BTPData, BTPDataDTO>(data);

            await _BTPRepository.SaveChangesAsync();

            var createDataDTO = new ValidateDataBTPDTO
            {
                Message = message,
                Data = BTPdataDTO
            };

            return createDataDTO;
        }
        public async Task<List<BTPDataDTO>> GetBTPData()
        {
            var BTPs = await _BTPRepository.GetAllBTPsDataAsync();
            var btpsDTO = _mapper.Map<List<BTPData>, List<BTPDataDTO>>(BTPs);

            return btpsDTO;
        }
        public async Task<List<BTPDataDTO>> GetBTPDataByWellId(Guid wellId)
        {
            var BTPs = await _BTPRepository.GetAllBTPsDataByWellIdAsync(wellId);
            var btpsDTO = _mapper.Map<List<BTPData>, List<BTPDataDTO>>(BTPs);

            return btpsDTO;
        }
    }
}