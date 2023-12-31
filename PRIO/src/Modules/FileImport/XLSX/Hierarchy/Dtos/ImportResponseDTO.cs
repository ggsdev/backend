﻿using Newtonsoft.Json;

namespace PRIO.src.Modules.FileImport.XLSX.Hierarchy.Dtos
{
    public class ImportResponseDTO
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("productionId")]
        public Guid ProductionId { get; set; }
    }

}
