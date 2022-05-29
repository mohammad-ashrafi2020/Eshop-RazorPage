using Eshop.RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Eshop.RazorPage.Infrastructure.RazorUtils;


[ValidateAntiForgeryToken]
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
    protected IActionResult RedirectAndShowAlert(ApiResult result, IActionResult redirectPath, IActionResult errorRedirectTo)
    {

        var model = JsonConvert.SerializeObject(result);
        HttpContext.Response.Cookies.Append("SystemAlert", model);
        if (result.IsSuccess == false)
            return errorRedirectTo;
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


    public async Task<ContentResult> AjaxTryCatch(Func<Task<ApiResult>> func,
           bool isSuccessReloadPage = true,
           bool isErrorReloadPage = false,
           bool checkModelState = true)
    {
        try
        {
            var isPost = PageContext.HttpContext.Request.Method == "POST";
            if (isPost && !ModelState.IsValid && checkModelState)
            {
                var errors = JoinErrors();
                var modelError = new AjaxResult()
                {
                    Status = AppStatusCode.ServerError,
                    Title = "عملیات ناموفق",
                    Message = errors,
                    IsReloadPage = isErrorReloadPage,
                };
                var jsonResult = JsonConvert.SerializeObject(modelError);
                return Content(jsonResult);
            }

            var res = await func().ConfigureAwait(false);
            var model = new AjaxResult()
            {
                Status = res.MetaData.AppStatusCode,
                Title = null,
                Message = res.MetaData.Message
            };
            switch (res.MetaData.AppStatusCode)
            {
                case AppStatusCode.Success:
                    {
                        model.IsReloadPage = isSuccessReloadPage;
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                case AppStatusCode.ServerError:
                    {
                        model.IsReloadPage = isErrorReloadPage;

                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                case AppStatusCode.NotFound:
                    {
                        model.IsReloadPage = isErrorReloadPage;
                        model.Title ??= "نتیجه ای یافت نشد";
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                case AppStatusCode.BadRequest:
                    {
                        model.IsReloadPage = isErrorReloadPage;
                        model.Title ??= "اطلاعات نامعتبر است";
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                default:
                    {
                        model.IsReloadPage = isSuccessReloadPage;
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
            }
        }
        catch (Exception ex)
        {
            var res = ApiResult.Error(ex.Message);
            var model = new AjaxResult()
            {
                Status = res.MetaData.AppStatusCode,
                Title = null,
                Message = res.MetaData.Message,
                IsReloadPage = isErrorReloadPage
            };
            var jsonResult = JsonConvert.SerializeObject(model);
            return Content(jsonResult);
        }
    }

    public async Task<ContentResult> AjaxTryCatch<T>(Func<Task<ApiResult<T>>> func,
        bool isSuccessReloadPage = false,
        bool isErrorReloadPage = false)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = JoinErrors();
                var modelError = new AjaxResult()
                {
                    Status = AppStatusCode.BadRequest,
                    Title = "عملیات ناموفق",
                    Message = errors,
                    IsReloadPage = isErrorReloadPage,
                    Data = default(T)
                };
                var jsonResult = JsonConvert.SerializeObject(modelError);
                return Content(jsonResult);
            }

            var res = await func().ConfigureAwait(false);
            var model = new AjaxResult()
            {
                Status = res.MetaData.AppStatusCode,
                Title = null,
                IsReloadPage = isSuccessReloadPage,
                Message = res.MetaData.Message,
                Data = res.Data
            };
            switch (res.MetaData.AppStatusCode)
            {
                case AppStatusCode.Success:
                    {
                        model.IsReloadPage = isSuccessReloadPage;
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                case AppStatusCode.ServerError:
                    {
                        model.IsReloadPage = isErrorReloadPage;

                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                case AppStatusCode.NotFound:
                    {
                        model.IsReloadPage = isErrorReloadPage;
                        model.Title ??= "نتیجه ای یافت نشد";
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                case AppStatusCode.BadRequest:
                    {
                        model.IsReloadPage = isErrorReloadPage;
                        model.Title ??= "اطلاعات نامعتبر است";
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                default:
                    {
                        model.IsReloadPage = isSuccessReloadPage;
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
            }
        }
        catch (Exception ex)
        {
            var res = ApiResult.Error(ex.Message);
            var model = new AjaxResult()
            {
                Status = res.MetaData.AppStatusCode,
                Title = null,
                Message = res.MetaData.Message,
                IsReloadPage = isErrorReloadPage
            };
            var jsonResult = JsonConvert.SerializeObject(model);
            return Content(jsonResult);
        }
    }
    public class AjaxResult
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public bool IsReloadPage { get; set; } = false;
        public object Data { get; set; }
        public AppStatusCode Status { get; set; }
    }
}