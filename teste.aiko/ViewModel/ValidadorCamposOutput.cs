using System.Collections.Generic;

namespace teste.aiko.ViewModel
{
    public class ValidadorCamposOutput
    {
        public IEnumerable<string> Erros { get; private set; }

        public ValidadorCamposOutput(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}
