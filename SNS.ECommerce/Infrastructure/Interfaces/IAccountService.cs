using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SNS.ECommerce.Infrastructure.Interfaces
{
    public interface IAccountService
    {
        Task<string> LogIn(string email, string password);
    }
}
