using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace teste.aiko.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class EquipamentoEstadoHistoricoOutput
    {
       
        public Guid Id { get; set; }
        
        public string Data { get; set; }
        
        public string Equipamento { get; set; }

        public string Estado { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <param name="estado"></param>
        /// <param name="equipamentoEstadoId"></param>
        public EquipamentoEstadoHistoricoOutput(Guid id, string data, string equipamento, string estado)
        {
            Id = id;
            Data = data;
            Estado = estado;
            Equipamento = equipamento;
        }
    }
}
