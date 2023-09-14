using AutoMapper;
using OfficeOpenXml;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Dtos;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.BTPS.ViewModels;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Http.Services;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.Http.Services
{
    public class BTPService
    {
        private readonly IMapper _mapper;
        private readonly IBTPRepository _BTPRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly WellProductionService _wellProductionService;

        public BTPService(IMapper mapper, IBTPRepository btpRepository, IWellRepository wellRepository, IProductionRepository productionRepository, WellProductionService wellProductionService)
        {
            _mapper = mapper;
            _BTPRepository = btpRepository;
            _wellRepository = wellRepository;
            _productionRepository = productionRepository;
            _wellProductionService = wellProductionService;
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
        public async Task<BTPCreateDTO> createBTP(CreateBTPViewModel body, User user)
        {
            var BTPexists = await _BTPRepository.GetByNameOrContent(body.Name, body.FileContent);
            if (BTPexists is not null)
            {
                if (BTPexists.Name == body.Name)
                    throw new ConflictException("Nome de BTP já existe.");
                if (BTPexists.FileContent == body.FileContent)
                    throw new ConflictException("Modelo de BTP já existe.");
            }
            var BTPType = await _BTPRepository.GetTypeAsync(body.Type);
            if (BTPType is null)
                throw new NotFoundException("Não existe este Tipo de modelo.");

            var create = new BTP
            {
                BTPSheet = body.BTPSheet,
                FileContent = body.FileContent,
                CellBSW = body.CellBSW,
                CellBTPNumber = body.CellBTPNumber,
                CellDuration = body.CellDuration,
                CellFinalDate = body.CellFinalDate,
                CellInitialDate = body.CellInitialDate,
                CellMPointGas = body.CellMPointGas,
                CellMPointOil = body.CellMPointOil,
                CellMPointWater = body.CellMPointWater,
                CellPotencialOil = body.CellPotencialOil,
                CellPotencialGas = body.CellPotencialGas,
                CellPotencialLiquid = body.CellPotencialLiquid,
                CellPotencialWater = body.CellPotencialWater,
                CellRGO = body.CellRGO,
                CellWellAlignmentData = body.CellWellAlignmentData,
                CellWellAlignmentHour = body.CellWellAlignmentHour,
                CellWellName = body.CellWellName,
                Mutable = true,
                Name = body.Name,
                IsActive = true,
                Type = body.Type,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await _BTPRepository.AddBTPAsync(create);
            await _BTPRepository.SaveChangesAsync();

            var BTPDTO = _mapper.Map<BTP, BTPCreateDTO>(create);

            return BTPDTO;

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
            var btpsDTO = _mapper.Map<WellTests, BTPDataDTO>(BTPdata);

            return btpsDTO;
        }
        public async Task<ValidateDataBTPDTO?> ValidateImportFiles(RequestWellTestXls body, User user)
        {
            var BTP = await _BTPRepository.GetByIdAsync(body.BTPId) ?? throw new NotFoundException("BTP não encontrado.");
            var splitName = body.FileName.Split('.');

            if (splitName.Length != 2)
                throw new ConflictException("Nome do arquivo inválido");

            if (body.FileName.EndsWith(".xlsx") is false && body.FileName.EndsWith(".xlsm") is false)
                throw new BadRequestException("O arquivo deve ter a extensão .xlsx ou .xlsm", status: "Error");

            if (body.Type != BTP.Type)
                throw new ConflictException($"O modelo do arquivo não corresponde ao tipo {body.Type}");

            var well = await _wellRepository.GetByIdAsync(body.WellId) ?? throw new NotFoundException("Poço não encontrado.");

            var contentBase64 = body.ContentBase64?.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "").Replace("data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            using var stream = new MemoryStream(Convert.FromBase64String(contentBase64!));
            using ExcelPackage package = new(stream);
            var workbook = package.Workbook;

            if (workbook.Worksheets[BTP.BTPSheet] is null)
                throw new NotFoundException("Arquivo não corresponde ao modelo.");

            var worksheet = BTP.BTPSheet is not null ? workbook.Worksheets[BTP.BTPSheet] : workbook.Worksheets[0];

            List<string> erros = new List<string>();
            object initialDateValue = worksheet.Cells[BTP.CellInitialDate].Value;

            if (!(initialDateValue is DateTime) && !(initialDateValue is double))
            {
                erros.Add("Erro: Valor da célula para data inicial não é uma data na célula " + BTP.CellInitialDate);
            }

            object finalDateValue = worksheet.Cells[BTP.CellFinalDate].Value;

            if (!(finalDateValue is DateTime) && !(finalDateValue is string) && !(finalDateValue is double))
            {
                erros.Add("Erro: Valor da célula para data final não é um data na célula " + BTP.CellFinalDate);
            }

            object DurationValue = worksheet.Cells[BTP.CellDuration].Value;
            if (!(DurationValue is DateTime) && !(DurationValue is double))
            {
                erros.Add("Erro: Valor da célula para duração não é uma hora na célula " + BTP.CellDuration);
            }
            object btpNumberValue = worksheet.Cells[BTP.CellBTPNumber].Value;
            if (btpNumberValue is null)
            {
                erros.Add("Erro: Não existe valor na célula para número do boletim " + BTP.CellBTPNumber);
            }

            object? wellNameValue = worksheet.Cells[BTP.CellWellName].Value;
            if (!(wellNameValue is string))
            {
                erros.Add("Erro: Valor da célula para nome do poço não é um texto na célula " + BTP.CellWellName);
            }

            object? wellAlignmentHourValue = worksheet.Cells[BTP.CellWellAlignmentHour].Value;
            if (!(wellAlignmentHourValue is double) && !(wellAlignmentHourValue is DateTime))
            {
                erros.Add("Erro: Valor da célula para hora do alinhamento do poço não é uma hora na célula " + BTP.CellWellAlignmentHour);
            }


            object? wellAlignmentDataValue = worksheet.Cells[BTP.CellWellAlignmentData].Value;

            if (!(wellAlignmentDataValue is DateTime) && !(wellAlignmentDataValue is double))
            {
                erros.Add("Erro: Valor da célula para data do alinhamento do poço não é um data na célula " + BTP.CellWellAlignmentData);
            }

            if (!(BTP.CellPotencialLiquid == ""))
            {
                object? CellPotencialLiquidValue = worksheet.Cells[BTP.CellPotencialLiquid].Value;
                if (!(CellPotencialLiquidValue is double))
                {
                    erros.Add("Erro: Valor da célula para potencial do liquido não é um número na célula " + BTP.CellPotencialLiquid);
                }
            };
            object? potencialOilValue = worksheet.Cells[BTP.CellPotencialOil].Value;
            if (!(potencialOilValue is double))
            {
                erros.Add("Erro: Valor da célula para potencial do óleo não é um número na célula " + BTP.CellPotencialOil);
            }

            object? potencialGasValue = worksheet.Cells[BTP.CellPotencialGas].Value;
            if (!(potencialGasValue is double))
            {
                erros.Add("Erro: Valor da célula para potencial do gás não é um número na célula " + BTP.CellPotencialGas);
            }

            object? potencialWaterValue = worksheet.Cells[BTP.CellPotencialWater].Value;
            if (!(potencialWaterValue is double))
            {
                erros.Add("Erro: Valor da célula para potencial da água não é um número na célula " + BTP.CellPotencialWater);
            }

            string[] parts = BTP.CellMPointOil.Split(',');
            List<string> concatenatedValues = new List<string>();
            if (parts.Length > 1)
            {
                foreach (string part in parts)
                {

                    object? MPointOilValue = worksheet.Cells[part].Value;
                    if (!(part is string))
                    {
                        erros.Add("Erro: Valor da célula para ponto de medição do óleo não é uma texto na célula " + part);
                    }
                    else if (MPointOilValue != null)
                    {
                        concatenatedValues.Add(MPointOilValue.ToString());
                    }
                    else
                    {
                        erros.Add("Erro: Valor da célula para ponto de medição do óleo é nulo na célula " + part);
                    }
                }
            }
            else
            {
                object? MPointOilValue = worksheet.Cells[BTP.CellMPointOil].Value;
                if (!(parts[0] is string))
                {
                    erros.Add("Erro: Valor da célula para ponto de medição do óleo não é um texto na célula " + BTP.CellMPointOil);
                }
                else
                {
                    if (MPointOilValue is not null)
                    {
                        concatenatedValues.Add(MPointOilValue.ToString());
                    }
                    else
                    {
                        erros.Add("Erro: Valor da célula para ponto de medição do óleo não é um texto na célula " + BTP.CellMPointOil);
                    }
                }
            }
            string concatenatedString = string.Join(", ", concatenatedValues);

            object? MPointGasValue = worksheet.Cells[BTP.CellMPointGas].Value;
            if (!(MPointGasValue is string))
            {
                erros.Add("Erro: Valor da célula para ponto de medição do gás não é um texto na célula " + BTP.CellMPointGas);
            }

            object? MPointWaterValue = worksheet.Cells[BTP.CellMPointWater].Value;
            if (!(MPointWaterValue is string))
            {
                erros.Add("Erro: Valor da célula para ponto de medição da água não é um texto na célula " + BTP.CellMPointWater);
            }

            object? RGOValue = worksheet.Cells[BTP.CellRGO].Value;
            if (!(RGOValue is double))
            {
                erros.Add("Erro: Valor da célula para RGO não é um número na célula " + BTP.CellRGO);
            }

            object? BSWValue = worksheet.Cells[BTP.CellBSW].Value;
            if (!(BSWValue is double))
            {
                erros.Add("Erro: Valor da célula para BSW não é um número na célula " + BTP.CellBSW);
            }

            var convertBtp = btpNumberValue?.ToString();
            DateTime? finalDateTest = DateTime.MinValue;
            DateTime? inicialDateTest = DateTime.MinValue;
            DateTime? alignDate = DateTime.MinValue;
            if (initialDateValue is not null && finalDateValue is not null)
            {
                finalDateTest = finalDateValue is string ? DateTime.Parse(finalDateValue.ToString()) : finalDateValue is DateTime ? (DateTime)finalDateValue : finalDateValue is double ? DateTime.FromOADate((double)finalDateValue) : null;
                inicialDateTest = initialDateValue is string ? DateTime.Parse(initialDateValue.ToString()) : initialDateValue is DateTime ? (DateTime)initialDateValue : initialDateValue is double ? DateTime.FromOADate((double)initialDateValue) : null;


                if (finalDateTest is null || inicialDateTest is null)
                {
                    erros.Add("Erro: Data inicial e final do teste estão em branco ou não foram encontradas.");
                }
                else
                {
                    DateTime? finalDate = (DateTime)finalDateTest;

                    DateTime? initialDate = (DateTime)inicialDateTest;
                    if (initialDate > finalDate)
                    {
                        erros.Add("Erro: Data inicial do teste não pode ser maior do que a data final do teste.");
                    }
                    try
                    {
                        DateTime? applicationDate = DateTime.Parse(body.ApplicationDate);
                        if (finalDate.Value.Date > applicationDate)
                        {
                            erros.Add("Erro: Data final do teste não pode ser maior do que a data de aplicação do teste.");
                        }
                    }
                    catch
                    {
                        throw new NotFoundException("Data da Aplicação não é valida");
                    }
                    if (wellAlignmentDataValue is not null)
                    {
                        alignDate = wellAlignmentDataValue is DateTime ? (DateTime)wellAlignmentDataValue : wellAlignmentDataValue is double ? DateTime.FromOADate((double)wellAlignmentDataValue) : null;
                        if (alignDate is not null)
                        {
                            if (alignDate.Value.Date > initialDate.Value.Date)
                            {

                                erros.Add("Erro: Data do alinhamento do poço não pode ser maior do que a data inicial do teste.");

                            }
                            else if (alignDate.Value.Date == initialDate.Value.Date)
                            {
                                if (wellAlignmentHourValue is DateTime)
                                {
                                    DateTime? alignHour = (DateTime)wellAlignmentHourValue;
                                    if (alignHour.Value.Hour > initialDate.Value.Hour)
                                    {
                                        erros.Add("Erro: Hora do alinhamento do poço não pode ser maior do que a hora inicial do teste.");
                                    }
                                }
                                else if (wellAlignmentHourValue is double)
                                {
                                    {
                                        string? align = ConvertDoubleToTimeSpan(worksheet.Cells[BTP.CellWellAlignmentHour].Value.ToString());
                                        DateTime? alignHour = DateTime.Parse(align);
                                        if (alignHour.Value.TimeOfDay > initialDate.Value.TimeOfDay)
                                        {
                                            erros.Add("Erro: Hora do alinhamento do poço não pode ser maior do que a hora inicial do teste.");
                                        }
                                    }
                                }
                            }

                        }

                        else
                        {
                            erros.Add("Erro: Data do alinhamento do poço não é uma data valida.");

                        }

                    }
                }
            }
            else
            {
                erros.Add("Erro: Data inicial e final do teste estão em branco ou não foram encontradas.");
            }

            if (erros.Count > 0)
            {
                throw new BadRequestException("Arquivo inválido.", erros);
            }

            //Verify Well
            string? message = well.Name == worksheet.Cells[BTP.CellWellName].Value.ToString() ? "Sucesso: Nome do poço encontrado corresponde ao xls" : throw new ConflictException($"O poço {worksheet.Cells[BTP.CellWellName].Value} do arquivo {body.FileName} não corresponde ao poço {well.Name} selecionado para o teste.");

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
            var rgoCheck = decimal.TryParse(worksheet.Cells[BTP.CellRGO].Value.ToString(), out var rgoDecimal);
            decimal rgoDecimalFormated = Math.Round(rgoDecimal, 5, MidpointRounding.AwayFromZero);
            var bswCheck = decimal.TryParse(worksheet.Cells[BTP.CellBSW].Value.ToString(), out var bswDecimal);
            decimal bswDecimalFormated = Math.Round(bswDecimal, 5, MidpointRounding.AwayFromZero);

            if (oilCheck is false || gasCheck is false || waterCheck is false || rgoCheck is false || bswCheck is false)
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

            var data = new WellTests
            {
                Id = Guid.NewGuid(),
                BTPId = body.BTPId,
                Filename = body.FileName,
                Type = body.Type,
                IsValid = body.IsValid,
                ApplicationDate = body.ApplicationDate,
                PotencialOil = oilDecimalFormated,
                PotencialOilPerHour = oilPerHourDecimalFormated,
                PotencialGas = gasDecimalFormated,
                PotencialGasPerHour = gasPerHourDecimalFormated,
                PotencialWater = waterDecimalFormated,
                PotencialWaterPerHour = waterPerHourDecimalFormated,
                InitialDate = inicialDateTest is not null ? inicialDateTest.ToString() : DateTime.MinValue.ToString(),
                FinalDate = finalDateTest is not null ? finalDateTest.ToString() : DateTime.MinValue.ToString(),
                BTPNumber = convertBtp,
                MPointGas = worksheet.Cells[BTP.CellMPointGas].Value.ToString(),
                MPointOil = concatenatedString,
                MPointWater = worksheet.Cells[BTP.CellMPointWater].Value.ToString(),
                BSW = bswDecimalFormated,
                RGO = rgoDecimalFormated,
                WellAlignmentData = alignDate is not null ? alignDate.ToString().Split(" ")[0] : DateTime.MinValue.ToString(),
                WellName = worksheet.Cells[BTP.CellWellName].Value.ToString(),
                BTPSheet = BTP.BTPSheet,
                Well = well,
                BTPBase64 = content
            };

            //Trated Duration
            string[] duration = worksheet.Cells[BTP.CellDuration].Value.ToString().Split(' ');
            if (duration.Length == 2)
            {
                if (duration[1] is null)
                    throw new ConflictException("Conteúdo da célula duração é inválido");
                string valorTempo = duration[1];
                data.Duration = valorTempo;
            }
            else if (duration.Length == 1)
            {
                string valorTempo = duration[0];
                data.Duration = valorTempo;
                if (worksheet.Cells[BTP.CellDuration].Value is double)
                {
                    var attData = ConvertDoubleToTimeSpan(worksheet.Cells[BTP.CellDuration].Value.ToString());
                    data.Duration = attData.ToString();

                }
            }

            //Trated aligmentHour
            if (wellAlignmentHourValue is double)
            {
                string? align = ConvertDoubleToTimeSpan(worksheet.Cells[BTP.CellWellAlignmentHour].Value.ToString());
                data.WellAlignmentHour = align;

            }
            else if (wellAlignmentHourValue is DateTime)
            {
                var splitAlignHour = wellAlignmentHourValue.ToString().Split(" ");
                var splitAlignHourSecondPosition = splitAlignHour[1];
                data.WellAlignmentHour = splitAlignHourSecondPosition;
            }

            if (BTP.CellPotencialLiquid == "")
            {
                decimal liquidDecimalFormated = oilDecimalFormated + waterDecimalFormated;
                decimal liquidPerHourDecimalFormated = oilPerHourDecimalFormated + waterPerHourDecimalFormated;

                data.PotencialLiquid = liquidDecimalFormated;
                data.PotencialLiquidPerHour = liquidPerHourDecimalFormated;

            }
            else
            {
                var liquidCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialLiquid].Value.ToString(), out var liquidDecimal);
                decimal liquidDecimalFormated = Math.Round(liquidDecimal, 5, MidpointRounding.AwayFromZero);
                decimal liquidPerHourDecimalFormated = Math.Round(liquidDecimal / 24, 5, MidpointRounding.AwayFromZero);

                if (liquidCheck is false)
                {
                    throw new ConflictException("Dados decimais não podem ser convertidos.");
                }
                data.PotencialLiquid = liquidDecimalFormated;
                data.PotencialLiquidPerHour = liquidPerHourDecimalFormated;
            }
            var validate = new ValidateBTP
            {
                Id = Guid.NewGuid(),
                WellId = well.Id,
                BTPId = BTP.Id,
                DataId = data.Id,
                ContentId = content.Id,
                Filename = body.FileName,
                IsValid = body.IsValid,
                ApplicationDate = body.ApplicationDate,
                content = body.ContentBase64,
            };

            await _BTPRepository.AddBTPValidateAsync(validate);

            var BTPdataDTO = _mapper.Map<WellTests, BTPDataDTO>(data);
            var createDataDTO = new ValidateDataBTPDTO
            {
                Message = message,
                Data = BTPdataDTO
            };
            await _BTPRepository.SaveChangesAsync();
            return createDataDTO;
        }
        public async Task<ValidateDataBTPDTO> PostImportFiles(ImportViewModel body, User user)
        {
            var Validate = await _BTPRepository.GetValidate(body.Validate.WellId, body.Validate.BTPId, body.Validate.ContentId, body.Validate.DataId) ?? throw new NotFoundException("Validação não autorizada.");
            var well = await _wellRepository.GetByIdAsync(body.Validate.WellId) ?? throw new NotFoundException("Poço não encontrado.");
            var BTP = await _BTPRepository.GetByIdAsync(body.Validate.BTPId) ?? throw new NotFoundException("BTP não encontrado");

            var foundDate = await _BTPRepository.GetByWellAndDateXls(body.Validate.WellId, body.Data.FinalDate);
            if (foundDate is not null)
                throw new ConflictException("Já existe um teste para este poço nesta data");

            var foundApllicationDate = await _BTPRepository.GetByWellAndApplicationDateXls(body.Validate.WellId, body.Data.ApplicationDate);
            if (foundApllicationDate is not null)
                throw new ConflictException("Já existe um teste para este poço nesta data de aplicação");

            if (body.Data.Filename.EndsWith(".xlsx") is false && body.Data.Filename.EndsWith(".xlsm") is false)
                throw new BadRequestException("O arquivo deve ter a extensão .xlsx ou .xlsm", status: "Error");
            if (body.Data.Type != BTP.Type)
                throw new ConflictException($"O modelo do arquivo não corresponde ao tipo {body.Data.Type}");
            if (body.Data.Filename != Validate.Filename)
                throw new ConflictException($"O modelo do arquivo não corresponde ao Filenmame {body.Data.Filename}");
            if (body.Data.ApplicationDate != Validate.ApplicationDate)
                throw new ConflictException($"O modelo do arquivo não corresponde a data de aplicação {body.Data.ApplicationDate}");
            if (body.Data.IsValid != Validate.IsValid)
                throw new ConflictException($"O modelo do arquivo não corresponde a validade {body.Data.IsValid}");

            var contentBase64 = Validate.content.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            using var stream = new MemoryStream(Convert.FromBase64String(contentBase64!));
            using ExcelPackage package = new(stream);
            var workbook = package.Workbook;
            var worksheet = BTP.BTPSheet is not null ? workbook.Worksheets[BTP.BTPSheet] : workbook.Worksheets[0];

            //Trated aligmentHour
            object? wellAlignmentHourValue = worksheet.Cells[BTP.CellWellAlignmentHour].Value;
            if (wellAlignmentHourValue is double)
            {
                string? align = ConvertDoubleToTimeSpan(worksheet.Cells[BTP.CellWellAlignmentHour].Value.ToString());
                if (align != body.Data.WellAlignmentHour)
                    throw new ConflictException("Horário do alinhamento do poço não corresponde a validação");
            }
            else if (wellAlignmentHourValue is DateTime)
            {
                var splitAlignHour = wellAlignmentHourValue.ToString().Split(" ");
                var splitAlignHourSecondPosition = splitAlignHour[1];
                if (splitAlignHourSecondPosition != body.Data.WellAlignmentHour)
                    throw new ConflictException("Horário do alinhamento do poço não corresponde a validação");
            }

            //Trated Duration
            string[] duration = worksheet.Cells[BTP.CellDuration].Value.ToString().Split(' ');
            string valorTempo = "";
            if (duration.Length == 2)
            {
                if (duration[1] is null)
                    throw new ConflictException("Conteúdo da célula duração é inválido");
                valorTempo = duration[1];
            }
            else if (duration.Length == 1)
            {
                valorTempo = duration[0];
                if (worksheet.Cells[BTP.CellDuration].Value is double)
                {
                    var attData = ConvertDoubleToTimeSpan(worksheet.Cells[BTP.CellDuration].Value.ToString());
                    valorTempo = attData;
                }
            }
            var finalDateValue = worksheet.Cells[BTP.CellFinalDate].Value;
            var initialDateValue = worksheet.Cells[BTP.CellInitialDate].Value;
            var wellAlignmentDataValue = worksheet.Cells[BTP.CellWellAlignmentData].Value;
            DateTime? finalDateTest = finalDateValue is string ? DateTime.Parse(finalDateValue.ToString()) : finalDateValue is DateTime ? (DateTime)finalDateValue : finalDateValue is double ? DateTime.FromOADate((double)finalDateValue) : null;
            DateTime? initialDateTest = initialDateValue is string ? DateTime.Parse(initialDateValue.ToString()) : initialDateValue is DateTime ? (DateTime)initialDateValue : initialDateValue is double ? DateTime.FromOADate((double)initialDateValue) : null;
            DateTime? alignDate = wellAlignmentDataValue is DateTime ? (DateTime)wellAlignmentDataValue : wellAlignmentDataValue is double ? DateTime.FromOADate((double)wellAlignmentDataValue) : null;

            string? message = well.Name == worksheet.Cells[BTP.CellWellName].Value.ToString() ? "Sucess: Nome do poço encontrado corresponde ao xls" : throw new ConflictException($"O poço {worksheet.Cells[BTP.CellWellName].Value.ToString()} do arquivo {body.Data.Filename} não corresponde ao poço {well.Name} selecionado para o teste.");

            var oilCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialOil].Value.ToString(), out var oilDecimal);
            decimal oilDecimalFormated = Math.Round(oilDecimal, 5, MidpointRounding.AwayFromZero);
            decimal oilPerHourDecimalFormated = Math.Round(oilDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var gasCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialGas].Value.ToString(), out var gasDecimal);
            decimal gasDecimalFormated = Math.Round(gasDecimal, 5, MidpointRounding.AwayFromZero);
            decimal gasPerHourDecimalFormated = Math.Round(gasDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var waterCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialWater].Value.ToString(), out var waterDecimal);
            decimal waterDecimalFormated = Math.Round(waterDecimal, 5, MidpointRounding.AwayFromZero);
            decimal waterPerHourDecimalFormated = Math.Round(waterDecimal / 24, 5, MidpointRounding.AwayFromZero);
            var rgoCheck = decimal.TryParse(worksheet.Cells[BTP.CellRGO].Value.ToString(), out var rgoDecimal);
            decimal rgoDecimalFormated = Math.Round(rgoDecimal, 5, MidpointRounding.AwayFromZero);
            var bswCheck = decimal.TryParse(worksheet.Cells[BTP.CellBSW].Value.ToString(), out var bswDecimal);
            decimal bswDecimalFormated = Math.Round(bswDecimal, 5, MidpointRounding.AwayFromZero);

            if (oilCheck is false || gasCheck is false || waterCheck is false || rgoCheck is false || bswCheck is false)
                throw new ConflictException("Dados decimais não podem ser convertidos.");

            var _validatePotenialOil = body.Data.PotencialOil == oilDecimalFormated;
            var _validatePotencialGas = body.Data.PotencialGas == gasDecimalFormated;
            var _validatePotencialWater = body.Data.PotencialWater == waterDecimalFormated;
            var _validateDuration = body.Data.Duration == valorTempo;
            var _validateInitialDate = body.Data.InitialDate == initialDateTest.ToString();
            var _validateFinalDate = body.Data.FinalDate == finalDateTest.ToString();
            var _validateBTPNumber = body.Data.BTPNumber == worksheet.Cells[BTP.CellBTPNumber].Value.ToString();
            var _validateMPointGas = body.Data.MPointGas == worksheet.Cells[BTP.CellMPointGas].Value.ToString();
            var _validateMPointWater = body.Data.MPointWater == worksheet.Cells[BTP.CellMPointWater].Value.ToString();
            var _validateBsw = body.Data.BSW == bswDecimalFormated;
            var _validateRGO = body.Data.RGO == rgoDecimalFormated;
            var _validateWellAlignDate = body.Data.WellAlignmentData == alignDate.ToString().Split(" ")[0];
            var _validateWellName = body.Data.WellName == worksheet.Cells[BTP.CellWellName].Value.ToString();
            var _validateBTPSheet = body.Data.BTPSheet == BTP.BTPSheet;

            string[] parts = BTP.CellMPointOil.Split(',');
            List<string> concatenatedValues = new List<string>();
            if (parts.Length > 1)
            {
                foreach (string part in parts)
                {
                    object? MPointOilValue = worksheet.Cells[part].Value;
                    concatenatedValues.Add(MPointOilValue.ToString());
                }
            }
            else
            {
                object? MPointOilValue = worksheet.Cells[parts[0]].Value;
                concatenatedValues.Add(MPointOilValue.ToString());
            }
            string concatenatedString = string.Join(", ", concatenatedValues);
            var _validateMPointOil = body.Data.MPointOil == concatenatedString;

            if (_validatePotenialOil is false || _validatePotencialGas is false || _validatePotencialWater is false || _validateDuration is false || _validateInitialDate is false || _validateFinalDate is false || _validateBTPNumber is false || _validateMPointGas is false || _validateMPointOil is false || _validateMPointWater is false || _validateBsw is false || _validateRGO is false || _validateWellAlignDate is false || _validateWellName is false || _validateBTPSheet is false)
            {
                throw new ConflictException("Dados diferentes da importação");
            }

            var content = new BTPBase64
            {
                Id = body.Validate.ContentId,
                Filename = body.Data.Filename,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Type = body.Data.Type,
                FileContent = Validate.content,
                IsActive = true,
                User = user
            };

            var data = new WellTests
            {
                Id = body.Validate.DataId,
                BTPId = body.Validate.BTPId,
                Type = body.Data.Type,
                Filename = body.Data.Filename,
                IsValid = body.Data.IsValid,
                ApplicationDate = body.Data.ApplicationDate,
                PotencialOil = oilDecimalFormated,
                PotencialOilPerHour = oilPerHourDecimalFormated,
                PotencialGas = gasDecimalFormated,
                PotencialGasPerHour = gasPerHourDecimalFormated,
                PotencialWater = waterDecimalFormated,
                PotencialWaterPerHour = waterPerHourDecimalFormated,
                Duration = valorTempo,
                InitialDate = initialDateTest is not null ? initialDateTest.ToString() : DateTime.MinValue.ToString(),
                FinalDate = finalDateTest is not null ? finalDateTest.ToString() : DateTime.MinValue.ToString(),
                BTPNumber = worksheet.Cells[BTP.CellBTPNumber].Value.ToString(),
                MPointGas = worksheet.Cells[BTP.CellMPointGas].Value.ToString(),
                MPointOil = worksheet.Cells[BTP.CellMPointOil].Value.ToString(),
                MPointWater = worksheet.Cells[BTP.CellMPointWater].Value.ToString(),
                BSW = bswDecimalFormated,
                RGO = rgoDecimalFormated,
                WellAlignmentData = alignDate is not null ? alignDate.ToString() : DateTime.MinValue.ToString(),
                WellName = worksheet.Cells[BTP.CellWellName].Value.ToString(),
                BTPSheet = BTP.BTPSheet,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Well = well,
                BTPBase64 = content,
                IsActive = body.Data.IsValid == false ? false : true,
            };
            if (BTP.CellPotencialLiquid != "" && BTP.CellPotencialLiquid != null)
            {
                var liquidCheck = decimal.TryParse(worksheet.Cells[BTP.CellPotencialLiquid].Value.ToString(), out var liquidDecimal);
                decimal liquidDecimalFormated = Math.Round(liquidDecimal, 5, MidpointRounding.AwayFromZero);
                decimal liquidPerHourDecimalFormated = Math.Round(liquidDecimal / 24, 5, MidpointRounding.AwayFromZero);

                data.PotencialLiquid = liquidDecimalFormated;
                data.PotencialLiquidPerHour = liquidPerHourDecimalFormated;
            }
            else
            {
                data.PotencialLiquid = oilDecimalFormated + waterDecimalFormated;
                data.PotencialLiquidPerHour = oilPerHourDecimalFormated + waterPerHourDecimalFormated;
            }

            //Trated aligmentHour
            if (wellAlignmentHourValue is double)
            {
                string? align = ConvertDoubleToTimeSpan(worksheet.Cells[BTP.CellWellAlignmentHour].Value.ToString());
                data.WellAlignmentHour = align;

            }
            else if (wellAlignmentHourValue is DateTime)
            {
                var splitAlignHour = wellAlignmentHourValue.ToString().Split(" ");
                var splitAlignHourSecondPosition = splitAlignHour[1];
                data.WellAlignmentHour = splitAlignHourSecondPosition;
            }


            var listWellTests = await _BTPRepository.ListBTPSDataActiveByWellId(body.Validate.WellId);
            DateTime applicationDateFromBody = DateTime.Parse(body.Data.ApplicationDate);

            if (listWellTests.Count != 0)
            {
                var greaterThanDate = listWellTests.LastOrDefault(x => DateTime.Parse(x.ApplicationDate) > applicationDateFromBody);
                var previousDate = listWellTests.FirstOrDefault(x => DateTime.Parse(x.ApplicationDate) < applicationDateFromBody);

                if (DateTime.Parse(listWellTests[0].ApplicationDate) < applicationDateFromBody)
                {
                    DateTime applicationDateFromBodya = DateTime.Parse(body.Data.ApplicationDate);
                    DateTime FinalnewDate = applicationDateFromBody.AddDays(-1);
                    DateTime today = DateTime.Today;

                    listWellTests[0].FinalApplicationDate = FinalnewDate.ToString();
                    listWellTests[0].IsActive = today <= FinalnewDate;

                    _BTPRepository.Update(listWellTests[0]);

                    data.IsActive = today >= applicationDateFromBodya;
                }
                else if (DateTime.Parse(listWellTests[0].ApplicationDate) == applicationDateFromBody)
                {
                    throw new ConflictException("Já existe uma aplicação de teste para essa data.");
                }
                else
                {
                    DateTime FinalnewDate = DateTime.Parse(greaterThanDate.ApplicationDate).AddDays(-1);
                    DateTime today = DateTime.Today;
                    data.FinalApplicationDate = FinalnewDate.ToString();
                    data.IsActive = false;

                    if (previousDate is not null)
                    {
                        DateTime previousFinalNewDate = DateTime.Parse(data.ApplicationDate).AddDays(-1);
                        previousDate.FinalApplicationDate = previousFinalNewDate.ToString();
                        previousDate.IsActive = false;

                        _BTPRepository.Update(previousDate);
                    }
                }
            }

            await _BTPRepository.AddBTPAsync(data);
            await _BTPRepository.AddBTPBase64Async(content);

            var BTPdataDTO = _mapper.Map<WellTests, BTPDataDTO>(data);

            await _BTPRepository.RemoveValidate(Validate);
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
            var btpsDTO = _mapper.Map<List<WellTests>, List<BTPDataDTO>>(BTPs);

            return btpsDTO;
        }
        public async Task<BTPDataDTO> UpdateByDataId(Guid dataId)
        {
            var BTPData = await _BTPRepository.GetByDataIdAsync(dataId);

            if (BTPData is null)
                throw new NotFoundException("Teste de poço não encontrado.");

            if (BTPData.IsValid is false)
                throw new NotFoundException("Teste de poço está invalidado.");

            var listWellTests = await _BTPRepository.ListBTPSDataActiveByWellId(BTPData.Well.Id);
            DateTime applicationDateFromBody = DateTime.Parse(BTPData.ApplicationDate);

            List<DateTime> listDates = new();
            List<DateTime> listExistingDates = new();
            if (listWellTests.Count != 0)
            {
                var greaterThanDate = listWellTests.OrderBy(x => DateTime.Parse(x.ApplicationDate)).LastOrDefault(x => DateTime.Parse(x.ApplicationDate) > applicationDateFromBody);
                var previousDate = listWellTests.OrderBy(x => DateTime.Parse(x.ApplicationDate)).FirstOrDefault(x => DateTime.Parse(x.ApplicationDate) < applicationDateFromBody);
                if (previousDate is not null && greaterThanDate is null)
                {
                    for (DateTime data = DateTime.Parse(previousDate.ApplicationDate); data <= DateTime.Today; data = data.AddDays(1))
                    {
                        listDates.Add(data);
                    }

                    BTPData.IsActive = false;
                    BTPData.IsValid = false;
                    BTPData.FinalApplicationDate = null;

                    previousDate.FinalApplicationDate = null;
                    previousDate.IsActive = true;

                    _BTPRepository.Update(previousDate);

                }
                else if (previousDate is not null && greaterThanDate is not null)
                {
                    for (DateTime data = DateTime.Parse(BTPData.ApplicationDate); data <= DateTime.Parse(greaterThanDate.ApplicationDate).AddDays(-1); data = data.AddDays(1))
                    {
                        listDates.Add(data);
                    }

                    DateTime previousFinalNewDate = DateTime.Parse(greaterThanDate.ApplicationDate).AddDays(-1);

                    BTPData.IsActive = false;
                    BTPData.IsValid = false;
                    BTPData.FinalApplicationDate = null;

                    previousDate.FinalApplicationDate = previousFinalNewDate.ToString();


                    _BTPRepository.Update(previousDate);
                }
                else
                {
                    throw new ConflictException("Só existe este BTP para este poço.");
                }
            }
            foreach (var date in listDates)
            {
                var production = await _productionRepository.GetExistingByDate(date);
                if (production is not null)
                    listExistingDates.Add(production.MeasuredAt);
            }

            BTPData.IsValid = false;

            _BTPRepository.Update(BTPData);
            await _BTPRepository.SaveChangesAsync();

            foreach (var dateProduction in listExistingDates)
            {
                var production = await _productionRepository.GetExistingByDate(dateProduction);
                if (production is not null && production.WellProductions is not null && production.WellProductions.Count > 0)
                {

                    await _wellProductionService.ReAppropriateWithWellTest(production.Id);
                }
            }


            var BTPDataDTO = _mapper.Map<WellTests, BTPDataDTO>(BTPData);


            return BTPDataDTO;
        }
        public async Task<List<BTPDataDTO>> GetBTPDataByWellId(Guid wellId)
        {
            var BTPs = await _BTPRepository.GetAllBTPsDataByWellIdAsync(wellId);
            var btpsDTO = _mapper.Map<List<WellTests>, List<BTPDataDTO>>(BTPs);

            return btpsDTO;
        }
        public async Task<BTPDataDTO> GetBTPDataByDataId(Guid dataId)
        {
            var BTPs = await _BTPRepository.GetAllBTPsDataByDataIdAsync(dataId);
            var btpsDTO = _mapper.Map<WellTests, BTPDataDTO>(BTPs);

            return btpsDTO;
        }
        private static string ConvertDoubleToTimeSpan(string valorDaCelula)
        {
            bool checkAlignHour = decimal.TryParse(valorDaCelula, out var valor);
            if (checkAlignHour is true)
            {
                decimal x = valor * 24 * 60;
                int horas = (int)x / 60;
                int minutos = (int)x % 60;
                int segundos = (int)(x * 60) % 60;
                if (segundos == 59)
                {
                    segundos = 0;
                    minutos += 1;

                    if (minutos == 60)
                    {
                        minutos = 0;
                        horas += 1;
                    }
                }
                TimeSpan horaMinuto = new TimeSpan(horas, minutos, segundos);
                string? align = horaMinuto.ToString();
                return align;
            }
            else
            {
                throw new ConflictException("Dados decimais não podem ser convertidos.");
            }
        }
    }
}