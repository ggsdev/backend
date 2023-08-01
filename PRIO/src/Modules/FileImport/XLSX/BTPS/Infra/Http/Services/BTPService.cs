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
            var worksheet = workbook.Worksheets
                .FirstOrDefault(x => x.Name.ToLower().Trim() == "BTP");
            worksheet ??= workbook.Worksheets[0];

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
            };

            Console.WriteLine(data.PotencialGas);
            Console.WriteLine(data.PotencialOil);
            Console.WriteLine(data.PotencialWater);
            Console.WriteLine(data.Duration);
            Console.WriteLine(data.InitialDate);
            Console.WriteLine(data.FinalDate);
            Console.WriteLine(data.BTPNumber);

            return data;
        }
    }
}