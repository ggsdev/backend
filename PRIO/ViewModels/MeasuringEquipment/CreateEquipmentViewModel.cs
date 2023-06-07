using PRIO.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.MeasuringEquipment
{
    public class CreateEquipmentViewModel
    {
        [Required(ErrorMessage = "TagEquipment name is a required field.")]
        public string? TagEquipment { get; set; }
        [Required(ErrorMessage = "TagMeasuringPoint name is a required field.")]
        public string? TagMeasuringPoint { get; set; }
        [Required(ErrorMessage = "SerieNumber name is a required field.")]
        public string? SerieNumber { get; set; }
        [Required(ErrorMessage = "Type name is a required field.")]
        public string? Type { get; set; }
        [Required(ErrorMessage = "TypeEquipment name is a required field.")]
        public string? TypeEquipment { get; set; }
        [Required(ErrorMessage = "Model name is a required field.")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "HasSeal name is a required field.")]
        public bool? HasSeal { get; set; }
        [Required(ErrorMessage = "MVS name is a required field.")]
        public bool? MVS { get; set; }
        [Required(ErrorMessage = "CommunicationProtocol name is a required field.")]
        public string? CommunicationProtocol { get; set; }
        [Required(ErrorMessage = "TypePoint name is a required field.")]
        public string? TypePoint { get; set; }
        [Required(ErrorMessage = "ChannelNumber name is a required field.")]
        public string? ChannelNumber { get; set; }
        [Required(ErrorMessage = "InOperation name is a required field.")]
        public bool? InOperation { get; set; }
        public string? Fluid { get; set; }
        public Guid? InstallationId { get; set; }
        public User? User { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? Description { get; set; }
    }
}
