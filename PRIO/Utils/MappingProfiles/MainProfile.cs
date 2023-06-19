using AutoMapper;
using PRIO.DTOS.ControlAccessDTOS;
using PRIO.DTOS.FileImportDTOS.XMLFilesDTOS;
using PRIO.DTOS.HierarchyDTOS.ClusterDTOS;
using PRIO.DTOS.HierarchyDTOS.CompletionDTOS;
using PRIO.DTOS.HierarchyDTOS.FieldDTOS;
using PRIO.DTOS.HierarchyDTOS.InstallationDTOS;
using PRIO.DTOS.HierarchyDTOS.MeasuringEquipment;
using PRIO.DTOS.HierarchyDTOS.ReservoirDTOS;
using PRIO.DTOS.HierarchyDTOS.WellDTOS;
using PRIO.DTOS.HierarchyDTOS.ZoneDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.DTOS.MenuDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.Files.XML._001;
using PRIO.Files.XML._002;
using PRIO.Files.XML._003;
using PRIO.Files.XML._039;
using PRIO.Models.HierarchyModels;
using PRIO.Models.MeasurementModels;
using PRIO.Models.UserControlAccessModels;
using System.Globalization;

namespace PRIO.Utils.MappingProfiles
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

            CreateMap<VOLUME, Volume>()
            .ForMember(dest => dest.DHA_MEDICAO_039, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.DHA_MEDICAO_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_MEDICAO_039, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            CreateMap<Measurement, _039DTO>();
            #endregion

            #region 001
            CreateMap<_001PMO, Measurement>();
            CreateMap<Measurement, _001DTO>();
            #endregion

            #region 002
            CreateMap<_002PMGL, Measurement>();
            CreateMap<Measurement, _002DTO>();
            #endregion

            #region 003
            CreateMap<_003PMGD, Measurement>();
            CreateMap<Measurement, _003DTO>();
            #endregion

            CreateMap<Cluster, ClusterDTO>();
            CreateMap<Cluster, Cluster>();
            CreateMap<Cluster, ClusterHistoryDTO>();

            CreateMap<Installation, InstallationDTO>();
            CreateMap<Installation, CreateUpdateInstallationDTO>();

            CreateMap<Field, FieldDTO>();

            CreateMap<Zone, ZoneDTO>();
            CreateMap<Zone, CreateUpdateZoneDTO>();

            CreateMap<Reservoir, ReservoirDTO>();

            CreateMap<Well, WellDTO>()
                .ForMember(dest => dest.WaterDepth, opt => opt.MapFrom(src => TruncateTwoDecimals(src.WaterDepth)))
                .ForMember(dest => dest.TopOfPerforated, opt => opt.MapFrom(src => TruncateTwoDecimals(src.TopOfPerforated)))
                .ForMember(dest => dest.BaseOfPerforated, opt => opt.MapFrom(src => TruncateTwoDecimals(src.BaseOfPerforated)));

            CreateMap<Completion, CompletionDTO>();

            CreateMap<User, UserDTO>();

            CreateMap<MeasuringEquipment, MeasuringEquipmentDTO>();
            CreateMap<Menu, MenuParentDTO>();
            CreateMap<Menu, MenuChildrenDTO>();
            CreateMap<User, ProfileDTO>();
            CreateMap<UserPermission, UserPermissionParentDTO>();
            CreateMap<UserPermission, UserPermissionChildrenDTO>();
            CreateMap<UserOperation, UserOperationsDTO>();
            CreateMap<UserPermissionParentDTO, UserPermissionChildrenDTO>();
            CreateMap<GroupPermission, GroupPermissionsDTO>();
            CreateMap<Group, GroupDTO>();
            CreateMap<Menu, MenuDTO>();
            CreateMap<GroupOperation, UserGroupOperationDTO>();
            CreateMap<User, UserGroupDTO>();
            CreateMap<Group, GroupWithMenusDTO>();

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
