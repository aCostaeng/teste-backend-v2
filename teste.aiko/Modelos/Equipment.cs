using System;
using System.Collections.Generic;

#nullable disable

namespace teste.aiko.Modelos
{
    public partial class Equipment
    {
        public Equipment()
        {
            EquipmentPositionHistories = new HashSet<EquipmentPositionHistory>();
            EquipmentStateHistories = new HashSet<EquipmentStateHistory>();
        }

        public Guid Id { get; set; }
        public Guid EquipmentModelId { get; set; }
        public string Name { get; set; }

        public virtual EquipmentModel EquipmentModel { get; set; }
        public virtual ICollection<EquipmentPositionHistory> EquipmentPositionHistories { get; set; }
        public virtual ICollection<EquipmentStateHistory> EquipmentStateHistories { get; set; }
    }
}
