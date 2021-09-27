using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using teste.aiko.ViewModel;
using System.Linq;

namespace teste.aiko.Filtros
{
    public class ValidadorCamposOutputFiltro : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validadorCampo =  new BadRequestObjectResult(new ValidadorCamposOutput(context.ModelState.SelectMany(sm => sm.Value.Errors).Select(s => s.ErrorMessage)));
                context.Result = new BadRequestObjectResult(validadorCampo);
            }
        }
    }
}
