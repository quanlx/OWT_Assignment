using System.Linq;
using WebOWT.Models.EntityDataModels;

namespace WebOWT.Services
{
    public interface IUserService
    {
        User Login(string username, string password);
    }

    public class UserService : IUserService
    {
        private OWT_SSEContext _context;

        public UserService(OWT_SSEContext context)
        {
            _context = context;
        }
        public User Login(string username, string password)
        {
            return _context.Users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
