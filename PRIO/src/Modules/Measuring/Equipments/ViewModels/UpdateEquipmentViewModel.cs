using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.Equipments.ViewModels
{
    public class UpdateEquipmentViewModel
    {
        [StringLength(120, ErrorMessage = "TagEquipment cannot exceed 120 characters.")]
        public string? TagEquipment { get; set; }
        [StringLength(120, ErrorMessage = "TagMeasuringPoint cannot exceed 120 characters.")]
        public string? TagMeasuringPoint { get; set; }
        [StringLength(120, ErrorMessage = "SerieNumber cannot exceed 120 characters.")]
        public string? SerieNumber { get; set; }
        [StringLength(10, ErrorMessage = "Type cannot exceed 10 characters.")]
        public string? Type { get; set; }
        [StringLength(120, ErrorMessage = "TypeEquipment cannot exceed 120 characters.")]
        public string? TypeEquipment { get; set; }
        [StringLength(120, ErrorMessage = "Model cannot exceed 120 characters.")]
        public string? Model { get; set; }
        public bool? HasSeal { get; set; }
        public bool? MVS { get; set; }
        [StringLength(120, ErrorMessage = "CommunicationProtocol cannot exceed 120 characters.")]
        public string? CommunicationProtocol { get; set; }
        [StringLength(120, ErrorMessage = "TypePoint cannot exceed 120 characters.")]
        public string? TypePoint { get; set; }
        [StringLength(10, ErrorMessage = "ChannelNumber cannot exceed 10 characters.")]
        public string? ChannelNumber { get; set; }
        public bool? InOperation { get; set; }
        [StringLength(120, ErrorMessage = "Fluid cannot exceed 120 characters.")]
        public string? Fluid { get; set; }
        public Guid? MeasuringId { get; set; }
        public string? Description { get; set; }
    }
}
