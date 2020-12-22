using EReceipt.Utils;
using EReceipt.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace EReceipt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        protected UserData CurrentUser;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            CurrentUser = AuthHelper.GetUserData(HttpContext.User.Claims.ToList());
        }
    }
}
