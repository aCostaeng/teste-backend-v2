using System;

namespace teste.aiko.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class EquipamentoPosicoesHistoricoOutput
    {
        public Guid ? Id { get; set; }
        public Guid IdEquipamento { get; set; }

        public string Equipamento { get; set; }

        public string Data { get; set; }
      
        public float Lat { get; set; }
        
        public float Lon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEquipamento"></param>
        /// <param name="equipamento"></param>
        /// <param name="data"></param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        public EquipamentoPosicoesHistoricoOutput(Guid id, Guid idEquipamento, string equipamento, string data,  float lat, float lon)
        {
            Id = id;
            IdEquipamento = idEquipamento;
            Equipamento = equipamento;
            Data = data;
            Lat = lat;
            Lon = lon;
        }
    }
}
