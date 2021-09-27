using System;

namespace teste.aiko.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class EquipamentoEstadoOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Cor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <param name="cor"></param>
        public EquipamentoEstadoOutput(Guid id, string nome, string cor)
        {
            this.Id = id;
            this.Nome = nome;
            this.Cor = cor;
        }
    }
}
