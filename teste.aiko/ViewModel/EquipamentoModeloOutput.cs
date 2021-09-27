using System;
using System.ComponentModel.DataAnnotations;

namespace teste.aiko.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class EquipamentoModeloOutput
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Nome { get; set; }

    }
}
