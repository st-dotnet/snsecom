using Microsoft.AspNetCore.Mvc;
using SNS.ECommerce.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace SNS.ECommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<bool> LogIn(string email, string password)
        {
            var response = await _accountService.LogIn(email, password);
            
            if(response == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}