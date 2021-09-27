using System;

namespace teste.aiko.ViewModel
{
    
    public class EquipamentoPosicoesHistoricoInput
    {
        public Guid IdEquipamnto { get; set; }

        public float Lat { get; set; }

        public float Lon { get; set; }

        public DateTime Data { get; set; }
    }
}
