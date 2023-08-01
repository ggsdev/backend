using AutoMapper;
using OfficeOpenXml;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Dtos;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.BTPS.ViewModels;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.Http.Services
{
    public class BTPService
    {
        private readonly IMapper _mapper;
        private readonly IBTPRepository _BTPRepository;

        public BTPService(IMapper mapper, IBTPRepository btpRepository)
        {
            _mapper = mapper;
            _BTPRepository = btpRepository;
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
        public async Task<BTPData> GetImportFiles(RequestWellTestXls body, User user)
        {

            var BTP = await _BTPRepository.GetByIdAsync(body.BTPId);
            if (BTP is null)
                throw new NotFoundException("BTP não encontrado.");

            if (body.FileName.EndsWith(".xlsx") is false)
                throw new BadRequestException("O arquivo deve ter a extensão .xlsx", status: "Error");

            var contentBase64 = body.ContentBase64?.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            using var stream = new MemoryStream(Convert.FromBase64String(contentBase64!));
            using ExcelPackage package = new(stream);
            var workbook = package.Workbook;
            var worksheet = BTP.BTPSheet is not null ? workbook.Worksheets[BTP.BTPSheet] : workbook.Worksheets[0];

            double fractionOfHour = double.Parse(worksheet.Cells[BTP.CellWellAlignmentHour].Value.ToString());
            TimeSpan timeSpan = TimeSpan.FromHours(fractionOfHour);

            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;

            string time = $"{hours:D2}:{minutes:D2}";


            var data = new BTPData
            {
                Filename = body.FileName,
                PotencialGas = worksheet.Cells[BTP.CellPotencialGas].Value.ToString(),
                PotencialOil = worksheet.Cells[BTP.CellPotencialOil].Value.ToString(),
                PotencialWater = worksheet.Cells[BTP.CellPotencialWater].Value.ToString(),
                Duration = worksheet.Cells[BTP.CellDuration].Value.ToString(),
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
                WellAlignmentHour = worksheet.Cells[BTP.CellWellAlignmentHour].Value.ToString(),
                WellName = worksheet.Cells[BTP.CellWellName].Value.ToString(),
                BTPSheet = BTP.BTPSheet,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            return data;
        }
    }
}