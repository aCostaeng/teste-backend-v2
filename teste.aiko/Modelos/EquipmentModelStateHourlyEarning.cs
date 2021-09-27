using System;
using System.Collections.Generic;

#nullable disable

namespace teste.aiko.Modelos
{
    public partial class EquipmentModelStateHourlyEarning
    {
        public Guid EquipmentModelId { get; set; }
        public Guid EquipmentStateId { get; set; }
        public float Value { get; set; }
        public Guid Id { get; set; }

        public virtual EquipmentModel EquipmentModel { get; set; }
        public virtual EquipmentState EquipmentState { get; set; }
    }
}
