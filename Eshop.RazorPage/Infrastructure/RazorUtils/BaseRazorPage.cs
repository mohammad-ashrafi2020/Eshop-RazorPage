using Eshop.RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Eshop.RazorPage.Infrastructure.RazorUtils;

public class BaseRazorPage : PageModel
{
    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        if (context.HandlerMethod.MethodInfo.Name == "OnPost")
            if (context.ModelState.IsValid == false)
            {
                var modelStateErrors = JoinErrors();
                var model = JsonConvert.SerializeObject(ApiResult.Error(modelStateErrors));
                HttpContext.Response.Cookies.Append("SystemAlert", model);
                context.Result = Page();
            }

        base.OnPageHandlerExecuting(context);
    }

    protected IActionResult RedirectAndShowAlert(ApiResult result, IActionResult redirectPath)
    {
        var model = JsonConvert.SerializeObject(result);
        HttpContext.Response.Cookies.Append("SystemAlert", model);
        if (result.IsSuccess == false)
            return Page();
        return redirectPath;
    }
    //protected IActionResult RedirectAndShowAlert(ApiResult<long> result, IActionResult redirectPath)
    //{
    //    var model = JsonConvert.SerializeObject(result);
    //    HttpContext.Response.Cookies.Append("SystemAlert", model);
    //    if (result.IsSuccess == false)
    //        return Page();
    //    return redirectPath;
    //}

    protected void SuccessAlert()
    {
        var model = JsonConvert.SerializeObject(ApiResult.Success());
        HttpContext.Response.Cookies.Append("SystemAlert", model);
    }
    protected void SuccessAlert(string message)
    {
        var model = JsonConvert.SerializeObject(ApiResult.Success(message));
        HttpContext.Response.Cookies.Append("SystemAlert", model);
    }
    protected void ErrorAlert()
    {
        var model = JsonConvert.SerializeObject(ApiResult.Error());
        HttpContext.Response.Cookies.Append("SystemAlert", model);
    }
    protected void ErrorAlert(string message)
    {
        var model = JsonConvert.SerializeObject(ApiResult.Error(message));
        HttpContext.Response.Cookies.Append("SystemAlert", model);
    }

    protected string JoinErrors()
    {
        var errors = new Dictionary<string, List<string>>();

        if (!ModelState.IsValid)
        {
            if (ModelState.ErrorCount > 0)
            {
                for (int i = 0; i < ModelState.Values.Count(); i++)
                {
                    var key = ModelState.Keys.ElementAt(i);
                    var value = ModelState.Values.ElementAt(i);

                    if (value.ValidationState == ModelValidationState.Invalid)
                    {
                        errors.Add(key, value.Errors.Select(x => string.IsNullOrEmpty(x.ErrorMessage) ? x.Exception?.Message : x.ErrorMessage).ToList());
                    }
                }
            }
        }

        var error = string.Join("<br/>", errors.Select(x =>
        {
            return $"{string.Join(" - ", x.Value)}";
        }));
        return error;
    }
}