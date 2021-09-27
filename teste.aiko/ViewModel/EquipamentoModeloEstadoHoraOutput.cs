using System;

namespace teste.aiko.ViewModel
{
    public class EquipamentoModeloEstadoHoraOutput
    {
        public Guid Id { get; set; }

        public Guid IdEquipamento { get; set; }

        public Guid IdEstado { get; set; }

        public float Valor { get; set; }

        public string Modelo { get; set; }

        public string Estado { get; set; }

        public EquipamentoModeloEstadoHoraOutput(Guid id, Guid idEquipamento, Guid idEstado, float valor, string modelo, string estado)
        {
            Id = id;
            IdEquipamento = idEquipamento;
            IdEstado = idEstado;
            Valor = valor;
            Modelo = modelo;
            Estado = estado;
        }
    }
}
