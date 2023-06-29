using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.Equipments.ViewModels
{
    public class CreateEquipmentViewModel
    {
        [Required(ErrorMessage = "TagEquipment is a required field.")]
        [StringLength(120, ErrorMessage = "TagEquipment cannot exceed 120 characters.")]
        public string? TagEquipment { get; set; }
        [Required(ErrorMessage = "TagMeasuringPoint is a required field.")]
        [StringLength(120, ErrorMessage = "TagMeasuringPoint cannot exceed 120 characters.")]
        public string? TagMeasuringPoint { get; set; }
        public string? MeasuringPointName { get; set; }
        [Required(ErrorMessage = "SerieNumber is a required field.")]
        [StringLength(120, ErrorMessage = "SerieNumber cannot exceed 120 characters.")]
        public string? SerieNumber { get; set; }
        [Required(ErrorMessage = "Type is a required field.")]
        [StringLength(10, ErrorMessage = "Type cannot exceed 10 characters.")]
        public string? Type { get; set; }

        public string? TypeEquipment { get; set; }

        public string? Model { get; set; }
        [Required(ErrorMessage = "HasSeal is a required field.")]
        public bool? HasSeal { get; set; }

        public bool? MVS { get; set; }

        public string? CommunicationProtocol { get; set; }

        public string? TypePoint { get; set; }

        public string? ChannelNumber { get; set; }
        [Required(ErrorMessage = "InOperation is a required field.")]
        public bool? InOperation { get; set; }
        [Required(ErrorMessage = "Fluid is a required field.")]
        [StringLength(120, ErrorMessage = "Fluid cannot exceed 120 characters.")]
        public string? Fluid { get; set; }
        [Required(ErrorMessage = "InstallationId is a required field.")]
        public Guid? InstallationId { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? Description { get; set; }
    }
}
