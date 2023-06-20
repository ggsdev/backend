using AutoMapper;
using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Menus.Dtos;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.FileContent._001;
using PRIO.src.Modules.FileImport.XML.FileContent._002;
using PRIO.src.Modules.FileImport.XML.FileContent._003;
using PRIO.src.Modules.FileImport.XML.FileContent._039;
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
using PRIO.src.Modules.Measuring.Equipments.Dtos;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
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
            CreateMap<Installation, InstallationHistoryDTO>();

            CreateMap<Field, FieldDTO>();
            CreateMap<Field, CreateUpdateFieldDTO>();
            CreateMap<Field, FieldHistoryDTO>();

            CreateMap<Zone, ZoneDTO>();
            CreateMap<Zone, CreateUpdateZoneDTO>();
            CreateMap<Zone, ZoneHistoryDTO>();

            CreateMap<Reservoir, ReservoirDTO>();
            CreateMap<Reservoir, CreateUpdateReservoirDTO>();
            CreateMap<Reservoir, ReservoirHistoryDTO>();

            CreateMap<Well, WellDTO>()
                .ForMember(dest => dest.WaterDepth, opt => opt.MapFrom(src => TruncateTwoDecimals(src.WaterDepth)))
                .ForMember(dest => dest.TopOfPerforated, opt => opt.MapFrom(src => TruncateTwoDecimals(src.TopOfPerforated)))
                .ForMember(dest => dest.BaseOfPerforated, opt => opt.MapFrom(src => TruncateTwoDecimals(src.BaseOfPerforated)));
            CreateMap<Well, WellHistoryDTO>();
            CreateMap<Well, CreateUpdateWellDTO>();

            CreateMap<Completion, CompletionDTO>();
            CreateMap<Completion, CompletionHistoryDTO>();
            CreateMap<Completion, CompletionWithouWellDTO>();

            CreateMap<User, UserDTO>();

            CreateMap<MeasuringEquipment, MeasuringEquipmentDTO>();
            CreateMap<MeasuringEquipment, MeasuringEquipmentHistoryDTO>();

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
