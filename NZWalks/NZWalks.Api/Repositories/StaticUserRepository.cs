
using NZWalks.Api.Models.Domain;
using System.Data;
using System.Net.Mail;

namespace NZWalks.Api.Repositories
{
    public class StaticUserRepository : IUserRepository

    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                FirstName ="readonly ",LastName="User",EmailAddress="readonly@gmail.com",Id=Guid.NewGuid(),
                UserName="readonly@gmail.com",Password ="ReadOnly123",
                Roles = new List<String>{"reader"}

            },
            new User ()
            {
                FirstName ="Read Write  ",LastName="User ",EmailAddress="readonly@gmail.com",Id=Guid.NewGuid(),
                UserName="readwrite@gmail.com",Password ="ReadWriteOnly123",
                Roles = new List<String>{"reader"}

            },

        };
        public  async Task<bool> AuthenticateAsync(string userName, string Password)
        {
            var user =  Users.Find(x => x.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase) && x.Password == Password);
            if(user != null)
            {
                return true;
            }
            return false;
        }   

    }
}
