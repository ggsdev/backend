using AutoMapper;
using PRIO.src.Modules.Balance.Balance.Dtos;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Balance.Injection.Dtos;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Menus.Dtos;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Dtos;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Measuring.Dtos;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._001;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._002;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._003;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._039;
using PRIO.src.Modules.FileImport.XML.NFSMS.Dtos;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Dtos;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Dtos;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Dtos;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Dtos;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Dtos;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Comments.Dtos;
using PRIO.src.Modules.Measuring.Comments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Dtos;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Dtos;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Dtos;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Dtos;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Dtos;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Dtos;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Modules.PI.Dtos;
using PRIO.src.Modules.PI.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Dtos;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Dtos.UserDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using System.Globalization;

namespace PRIO.src.Shared.Utils.MappingProfiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            #region 039
            CreateMap<BSW, Bsw>()
            .ForMember(dest => dest.DHA_FALHA_BSW_039, opt => opt.MapFrom(src =>
            string.IsNullOrEmpty(src.DHA_FALHA_BSW_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_FALHA_BSW_039, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            CreateMap<CALIBRACAO, Calibration>()
            .ForMember(dest => dest.DHA_FALHA_CALIBRACAO_039, opt => opt.MapFrom(src =>
            string.IsNullOrEmpty(src.DHA_FALHA_CALIBRACAO_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_FALHA_CALIBRACAO_039, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));

            CreateMap<DADOS_BASICOS_039, Measurement>()
            .ForMember(dest => dest.DHA_OCORRENCIA_039, opt => opt.MapFrom(src =>
            string.IsNullOrEmpty(src.DHA_OCORRENCIA_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_OCORRENCIA_039, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)))
             .ForMember(dest => dest.DHA_DETECCAO_039, opt => opt.MapFrom(src =>
            string.IsNullOrEmpty(src.DHA_DETECCAO_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_DETECCAO_039, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)))
              .ForMember(dest => dest.DHA_RETORNO_039, opt => opt.MapFrom(src =>
            string.IsNullOrEmpty(src.DHA_RETORNO_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_RETORNO_039, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));

            CreateMap<VOLUME, Volume>();
            //.ForMember(dest => dest.DHA_MEDICAO_039, opt => opt.MapFrom(src =>
            //    string.IsNullOrEmpty(src.DHA_MEDICAO_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_MEDICAO_039, "dd/MM/yyyy 00:00:00", CultureInfo.InvariantCulture)));


            CreateMap<Measurement, _039DTO>();
            CreateMap<_039DTO, Measurement>();
            CreateMap<Measurement, Client039DTO>();
            CreateMap<Volume, VolumeDto>();
            CreateMap<Calibration, CalibrationDto>();
            CreateMap<Bsw, BswDto>();





            CreateMap<Measurement, Client001DTO>();
            CreateMap<Measurement, Client002DTO>();
            #endregion

            #region 001
            CreateMap<_001PMO, Measurement>();
            CreateMap<Measurement, _001DTO>();
            CreateMap<_001DTO, Measurement>();

            #endregion

            #region 002
            CreateMap<_002PMGL, Measurement>();
            CreateMap<Measurement, _002DTO>();
            CreateMap<_002DTO, Measurement>();
            CreateMap<Client002DTO, Measurement>();
            CreateMap<Client001DTO, Measurement>();
            CreateMap<Client003DTO, Measurement>();
            CreateMap<Client039DTO, Measurement>();
            CreateMap<MeasurementHistory, MeasurementHistoryDto>()
                .ForMember(dest => dest.ImportId, opt => opt.MapFrom(src =>
                src.Id));

            #endregion

            #region 003
            CreateMap<_003PMGD, Measurement>();
            CreateMap<Measurement, _003DTO>();
            CreateMap<_003DTO, Measurement>();
            CreateMap<Measurement, Client003DTO>();

            #endregion

            #region Control Access
            CreateMap<User, UserDTO>();
            CreateMap<User, UserHistoryDTO>();
            CreateMap<Menu, MenuParentDTO>();
            CreateMap<Menu, MenuChildrenDTO>();
            CreateMap<User, ProfileDTO>();
            CreateMap<User, UserWithPermissionsDTO>();
            CreateMap<UserPermission, UserPermissionParentDTO>();
            CreateMap<UserPermission, UserPermissionChildrenDTO>();
            CreateMap<UserOperation, UserOperationsDTO>();
            CreateMap<UserPermissionParentDTO, UserPermissionChildrenDTO>();
            CreateMap<GroupPermission, GroupPermissionsDTO>();
            CreateMap<Group, GroupDTO>();
            CreateMap<GroupPermission, GroupPermissionChildrenDTO>();
            CreateMap<GroupPermission, GroupPermissionParentDTO>();
            CreateMap<GroupPermissionParentDTO, GroupPermissionChildrenDTO>();
            CreateMap<Group, GroupWithGroupPermissionDTO>();
            CreateMap<GroupPermission, GroupPermissionsWithoutMenusDTO>();
            CreateMap<Menu, MenuDTO>();
            CreateMap<GroupOperation, UserGroupOperationDTO>();
            CreateMap<GroupOperation, GroupOperationDTO>();
            CreateMap<User, UserGroupDTO>();
            CreateMap<Group, GroupWithMenusDTO>();
            CreateMap<Group, GroupHistoryDTO>();
            CreateMap<Reservoir, ReservoirWithZoneDTO>();
            CreateMap<InstallationsAccess, InstallationAccessDTO>();
            #endregion

            #region Hierachy
            CreateMap<Cluster, ClusterDTO>();
            CreateMap<Cluster, ClusterHistoryDTO>();
            CreateMap<Cluster, ClusterWithInstallationsDTO>();
            CreateMap<Cluster, ClusterWithoutInstallationsDTO>();

            CreateMap<Installation, InstallationDTO>();
            CreateMap<Installation, CreateUpdateInstallationDTO>();
            CreateMap<Installation, InstallationHistoryDTO>();
            CreateMap<Installation, InstallationWithoutClusterDTO>();
            CreateMap<InstallationWithoutClusterDTO, Installation>();
            CreateMap<Installation, InstallationWithFieldsEquipmentsDTO>();
            CreateMap<Installation, InstallationWithAttributesDTO>();

            CreateMap<Field, FieldDTO>();
            CreateMap<Field, FieldWithoutWellDTO>();
            CreateMap<Field, CreateUpdateFieldDTO>();
            CreateMap<Field, FieldHistoryDTO>();
            CreateMap<Field, FieldWithoutInstallationDTO>();
            CreateMap<Field, FieldWithZonesAndWellsDTO>();

            CreateMap<Zone, ZoneDTO>();
            CreateMap<Zone, CreateUpdateZoneDTO>();
            CreateMap<Zone, ZoneHistoryDTO>();
            CreateMap<Zone, ZoneWithoutFieldDTO>();
            CreateMap<Zone, ZoneWithReservoirsDTO>();

            CreateMap<Reservoir, ReservoirDTO>();
            CreateMap<Reservoir, CreateUpdateReservoirDTO>();
            CreateMap<Reservoir, ReservoirHistoryDTO>();
            CreateMap<Reservoir, ReservoirWithoutZoneDTO>();
            CreateMap<Reservoir, ReservoirWithCompletionsDTO>();

            CreateMap<Well, WellDTO>();
            CreateMap<Well, WellHistoryDTO>();
            CreateMap<Well, CreateUpdateWellDTO>()
                .ForMember(dest => dest.WaterDepth, opt => opt.MapFrom(src => TruncateTwoDecimals(src.WaterDepth)));

            CreateMap<Well, WellWithoutFieldDTO>()
                .ForMember(dest => dest.WaterDepth, opt => opt.MapFrom(src => TruncateTwoDecimals(src.WaterDepth)));

            CreateMap<Well, WellWithoutCompletionDTO>()
                .ForMember(dest => dest.WaterDepth, opt => opt.MapFrom(src => TruncateTwoDecimals(src.WaterDepth)));

            CreateMap<Well, WellWithFieldAndCompletionsDTO>().ForMember(dest => dest.WaterDepth, opt => opt.MapFrom(src => TruncateTwoDecimals(src.WaterDepth)));

            CreateMap<Completion, CompletionDTO>();
            CreateMap<Completion, CompletionHistoryDTO>();
            CreateMap<Completion, CompletionWithouWellDTO>();
            CreateMap<Completion, CompletionWithoutReservoirDTO>();
            CreateMap<Completion, CreateUpdateCompletionDTO>();
            CreateMap<Completion, CompletionWithWellAndReservoirDTO>();


            CreateMap<SystemHistory, ImportHistoryDTO>()
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FieldsChanged));
            #endregion

            #region Measuring
            CreateMap<MeasuringEquipment, MeasuringEquipmentDTO>();
            CreateMap<MeasuringEquipment, MeasuringEquipmentHistoryDTO>();
            CreateMap<MeasuringEquipment, MeasuringEquipmentWithoutInstallationDTO>();
            CreateMap<MeasuringPoint, MeasuringPointDTO>();
            CreateMap<MeasuringPoint, MeasuringPointHistoryDTO>();
            CreateMap<MeasuringPoint, MeasuringPointWithoutInstallationDTO>();
            CreateMap<OilVolumeCalculation, OilVolumeCalculationDTO>();
            CreateMap<Section, SectionWithEquipmentDTO>();
            CreateMap<DrainVolume, DrainVolumeWithEquipmentDTO>();
            CreateMap<TOGRecoveredOil, TOGRecoveredOilWithEquipmentDTO>();
            CreateMap<DOR, DORWithEquipmentDTO>();
            CreateMap<NFSM, NFSMGetAllDto>();

            CreateMap<MeasurementHistory, MeasurementHistoryWithMeasurementsDto>()
                .ForMember(dest => dest.ImportId, opt => opt.MapFrom(src =>
                src.Id));

            CreateMap<GasVolumeCalculation, GasVolumeCalculationDto>();
            CreateMap<AssistanceGas, AssistanceGasDto>();
            CreateMap<ExportGas, ExportGasDto>();
            CreateMap<HighPressureGas, HighPressureGasDto>();
            CreateMap<HPFlare, HPFlareDto>();
            CreateMap<ImportGas, ImportGasDto>();
            CreateMap<LowPressureGas, LowPressureGasDto>();
            CreateMap<LPFlare, LPFlareDto>();
            CreateMap<PilotGas, PilotGasDto>();
            CreateMap<PurgeGas, PurgeGasDto>();
            #endregion

            CreateMap<BTP, BTPDTO>();
            CreateMap<BTP, BTPCreateDTO>();
            CreateMap<BTP, BTPBase64DTO>();
            CreateMap<BTPBase64, BTPBase64RelationWellTestDTO>();
            CreateMap<BTPBase64, BTPBase64DTO>();
            CreateMap<WellTests, BTPDataDTO>();//TEM QUE CONSERTAR AQ
            //.ForPath(dest => dest.BTPBase64.Name, opt => opt.MapFrom(src => src.Filename));


            CreateMap<FieldFR, FRFieldDTO>();

            CreateMap<CommentInProduction, CreateUpdateCommentDto>();
            CreateMap<Production, ProductionDto>();
            CreateMap<WellEvent, EventWithReasonDTO>();
            CreateMap<EventReason, EventReasonDTO>();
            CreateMap<EventReason, ReasonDetailedDto>()
                .ForMember(dest => dest.Downtime, opt => opt.MapFrom(src =>
                src.Interval))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.HasValue ? src.EndDate.Value.ToString("dd/MM/yyyy HH:mm") : null))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("dd/MM/yyyy HH:mm")));

            CreateMap<Modules.PI.Infra.EF.Models.Attribute, AttributeDTO>();
            CreateMap<Element, ElementDTO>();
            CreateMap<Field, FieldWithInjectionsValuesDTO>();
            CreateMap<Well, WellsWithInjectionsValuesDTO>();
            CreateMap<WellsValues, WellValuesDTO>();
            CreateMap<Value, ValueWithInjecctionDTO>()
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.Attribute.Name))
                .ForMember(dest => dest.Parameter, opt => opt.MapFrom(src => src.Attribute.Element.Parameter));

            CreateMap<InjectionWaterWell, InjectionWaterWellDTO>();
            CreateMap<InjectionGasWell, InjectionGasWellDTO>();
            CreateMap<WellSensor, SensorsDTO>();
            CreateMap<ManualWellConfiguration, ManualConfigDTO>();
            CreateMap<ManualWellConfiguration, ManualConfigWithListsDTO>();
            CreateMap<Modules.PI.Infra.EF.Models.Attribute, AttributeWithInjectionDTO>();
            CreateMap<InjectivityIndex, InjectivityIndexDTO>();
            CreateMap<ProductivityIndex, ProductivityIndexDTO>();
            CreateMap<BuildUp, BuildUpDTO>();
            CreateMap<WellSensor, WellSensorDTO>();
            CreateMap<WellsValues, WellValueDTO>();
            CreateMap<Value, ValueSensorDTO>();
            CreateMap<FieldsBalance, FieldBalanceDto>();
            CreateMap<FieldsBalance, FieldBalanceWithParameterDTO>();
            CreateMap<FieldsBalance, FieldsBalanceDTO>()
                .ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.FieldProduction.FieldId));
        }

        private static decimal? TruncateTwoDecimals(decimal? value)
        {
            if (value.HasValue)
            {
                int scale = 2;

                decimal truncatedValue = decimal.Truncate(value.Value * (decimal)Math.Pow(10, scale)) / (decimal)Math.Pow(10, scale);

                return truncatedValue;
            }

            return null;
        }
    }
}
