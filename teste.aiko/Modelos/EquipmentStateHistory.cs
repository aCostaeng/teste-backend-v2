using System;
using System.Collections.Generic;

#nullable disable

namespace teste.aiko.Modelos
{
    public partial class EquipmentStateHistory
    {
        public Guid EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public Guid EquipmentStateId { get; set; }
        public Guid Id { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual EquipmentState EquipmentState { get; set; }
    }
}
