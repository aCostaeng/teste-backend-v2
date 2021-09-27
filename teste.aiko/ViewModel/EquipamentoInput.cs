using System;
using System.ComponentModel.DataAnnotations;

namespace teste.aiko.ViewModel
{
    
    public class EquipamentoInput
    {
        
        [Required(ErrorMessage = "O campo Id do Modelo é obrigatório")]
        public Guid EquipamentoModeloId { get; set; }

       
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
    }
}
