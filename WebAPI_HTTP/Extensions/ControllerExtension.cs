using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_HTTP.Extensions
{
    public static class ControllerExtension
    {
        public static ActionResult ValidateModel(this ControllerBase controllerBase)
        {
            if (!controllerBase.ModelState.IsValid) { 
                
                var errors = controllerBase.ModelState.Values.SelectMany(x => x.Errors).ToList();

                return controllerBase.BadRequest(new { Errors = errors });
            }

            //nema validaciski greski
            return null;
        }
    }
}
