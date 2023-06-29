﻿using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.Equipments.ViewModels
{
    public class CreateEquipmentViewModel
    {
        [Required(ErrorMessage = "TagEquipment is a required field.")]
        [StringLength(60, ErrorMessage = "TagEquipment cannot exceed 60 characters.")]
        public string? TagEquipment { get; set; }
        [Required(ErrorMessage = "TagMeasuringPoint is a required field.")]
        [StringLength(60, ErrorMessage = "TagMeasuringPoint cannot exceed 60 characters.")]
        public string? TagMeasuringPoint { get; set; }
        [Required(ErrorMessage = "SerieNumber is a required field.")]
        [StringLength(60, ErrorMessage = "SerieNumber cannot exceed 60 characters.")]
        public string? SerieNumber { get; set; }
        [Required(ErrorMessage = "Type is a required field.")]
        [StringLength(60, ErrorMessage = "Type cannot exceed 60 characters.")]
        public string? Type { get; set; }
        [Required(ErrorMessage = "TypeEquipment is a required field.")]
        [StringLength(60, ErrorMessage = "TypeEquipment cannot exceed 60 characters.")]
        public string? TypeEquipment { get; set; }
        [Required(ErrorMessage = "Model is a required field.")]
        [StringLength(60, ErrorMessage = "Model cannot exceed 60 characters.")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "HasSeal is a required field.")]
        public bool? HasSeal { get; set; }
        [Required(ErrorMessage = "MVS is a required field.")]
        public bool? MVS { get; set; }
        [Required(ErrorMessage = "CommunicationProtocol is a required field.")]
        [StringLength(60, ErrorMessage = "CommunicationProtocol cannot exceed 60 characters.")]
        public string? CommunicationProtocol { get; set; }
        [Required(ErrorMessage = "TypePoint is a required field.")]
        [StringLength(60, ErrorMessage = "TypePoint cannot exceed 60 characters.")]
        public string? TypePoint { get; set; }
        [Required(ErrorMessage = "ChannelNumber is a required field.")]
        [StringLength(60, ErrorMessage = "ChannelNumber cannot exceed 60 characters.")]
        public string? ChannelNumber { get; set; }
        [Required(ErrorMessage = "InOperation is a required field.")]
        public bool? InOperation { get; set; }
        [Required(ErrorMessage = "Fluid is a required field.")]
        [StringLength(60, ErrorMessage = "Fluid cannot exceed 60 characters.")]
        public string? Fluid { get; set; }
        [Required(ErrorMessage = "InstallationId is a required field.")]
        public Guid? InstallationId { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? Description { get; set; }
    }
}
