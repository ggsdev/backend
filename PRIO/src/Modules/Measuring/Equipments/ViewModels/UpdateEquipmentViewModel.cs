using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.Equipments.ViewModels
{
    public class UpdateEquipmentViewModel
    {
        [StringLength(60, ErrorMessage = "TagEquipment cannot exceed 60 characters.")]
        public string? TagEquipment { get; set; }
        [StringLength(60, ErrorMessage = "TagMeasuringPoint cannot exceed 60 characters.")]
        public string? TagMeasuringPoint { get; set; }
        [StringLength(60, ErrorMessage = "SerieNumber cannot exceed 60 characters.")]
        public string? SerieNumber { get; set; }
        [StringLength(60, ErrorMessage = "Type cannot exceed 60 characters.")]
        public string? Type { get; set; }
        [StringLength(60, ErrorMessage = "TypeEquipment cannot exceed 60 characters.")]
        public string? TypeEquipment { get; set; }
        [StringLength(60, ErrorMessage = "Model cannot exceed 60 characters.")]
        public string? Model { get; set; }
        public bool? HasSeal { get; set; }
        public bool? MVS { get; set; }
        [StringLength(60, ErrorMessage = "CommunicationProtocol cannot exceed 60 characters.")]
        public string? CommunicationProtocol { get; set; }
        [StringLength(60, ErrorMessage = "TypePoint cannot exceed 60 characters.")]
        public string? TypePoint { get; set; }
        [StringLength(60, ErrorMessage = "ChannelNumber cannot exceed 60 characters.")]
        public string? ChannelNumber { get; set; }
        public bool? InOperation { get; set; }
        [StringLength(60, ErrorMessage = "Fluid cannot exceed 60 characters.")]
        public string? Fluid { get; set; }
        public Guid? MeasuringId { get; set; }
        public string? Description { get; set; }
    }
}
