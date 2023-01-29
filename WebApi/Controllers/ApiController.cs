using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ApiController :ControllerBase
{

    [NonAction]
    public List<string> GetModelErrors()
    {
        var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();
        return errors.Select(x => x.ErrorMessage).ToList();
    }
}