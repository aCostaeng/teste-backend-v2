using System;
using System.Collections.Generic;

#nullable disable

namespace teste.aiko.Modelos
{
    public partial class EquipmentState
    {
        public EquipmentState()
        {
            EquipmentModelStateHourlyEarnings = new HashSet<EquipmentModelStateHourlyEarning>();
            EquipmentStateHistories = new HashSet<EquipmentStateHistory>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual ICollection<EquipmentModelStateHourlyEarning> EquipmentModelStateHourlyEarnings { get; set; }
        public virtual ICollection<EquipmentStateHistory> EquipmentStateHistories { get; set; }
    }
}
