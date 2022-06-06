using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SNS.ECommerce.Data;
using SNS.ECommerce.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace SNS.ECommerce.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _dbContext;

        public AccountService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> LogIn(string email, string password)
        {
            var data = await _dbContext.Users.SingleOrDefaultAsync(c => c.Email == email && c.Password == password);

            if (data != null)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
    }
}
