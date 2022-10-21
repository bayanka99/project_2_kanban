using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class UserDTO : DTO
    {

        public const string UsersemailColumnName = "email";
        public const string UserspasswordColumnName = "password";
        private string Email;
        public string email
        {
            get => Email; set { Email = value; dc.Update(Id, UsersemailColumnName, value); }
        }
        private string Password;
        public string password
        {
            get => Password; set { Password = value; dc.Update(Id, UserspasswordColumnName, value); }
        }




        public UserDTO(string email, string password) : base(new UserDalController())
        {
           
            Email = email;
            Password = password;
        }



    }

}



        
        
    




