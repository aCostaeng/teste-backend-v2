using System;
using System.ComponentModel.DataAnnotations;

namespace teste.aiko.ViewModel
{
    
    public class EquipamentoOutput
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public Guid EquipamentoModeloId { get; set; }

        
        public EquipamentoOutput(Guid id, string nome, string modelo)
        {
            Id = id;
            Nome = nome;
            Modelo = modelo;
        }
    }
}
