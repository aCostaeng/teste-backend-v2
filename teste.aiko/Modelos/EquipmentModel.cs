using System;
using System.Collections.Generic;

#nullable disable

namespace teste.aiko.Modelos
{
    public partial class EquipmentModel
    {
        public EquipmentModel()
        {
            Equipment = new HashSet<Equipment>();
            EquipmentModelStateHourlyEarnings = new HashSet<EquipmentModelStateHourlyEarning>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<EquipmentModelStateHourlyEarning> EquipmentModelStateHourlyEarnings { get; set; }
    }
}
