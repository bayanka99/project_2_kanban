using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class UserController
    {
        private UserDalController userDal = new UserDalController();
        //Field:
        private Boolean ispersictent;

        private Dictionary<string , User> users=new Dictionary<string, User>();
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //Methods:
        internal virtual void loadData()
        {
            List<UserDTO> userDTOs = userDal.SelectAllUsers();
            foreach (UserDTO user in userDTOs)
            {
                if (!users.ContainsKey(user.email))
                {
                    User dalUser = new User(user);

                    users[dalUser.email] = dalUser;
                }

            }
        }

        /// <summary>
        /// this fun delete the data from the DALayer
        /// </summary>
        internal void deleteData()
        {
            userDal.DeleteData();
            users = new Dictionary<string, User>();
        }
        /*
        This function checks if there is any user connected
        Input:string userEmail - The user that try to register/login/logout
        Output:None
        */
        internal virtual void isAnotherUserLogedIn(string userEmail)
        {
            foreach (string email in users.Keys)
            {
                if (email != userEmail && users[email].checkIfLoggedIn())
                {
                    throw new ArgumentException("Another user is sigend in!, you can't register if he is signed in");
                }
            }
        }

        /*
        This function checks if the user is connected
        Input:
            1)string userEmail - The user that try to register/login/logout
            2) bool exceptedExists - is user exists (true) or opisate
        Output:None
        */
        internal void isUserLogedIn(string email, bool exceptedLoggedIn)
        {
            isEmailExists(email, true);
            if (users[email].checkIfLoggedIn())
            {
                if (!exceptedLoggedIn)
                {
                    throw new ArgumentException("The user is already logged in!");
                }
            }
            else
            {
                if (exceptedLoggedIn)
                {
                    throw new ArgumentException("The user is not logged in!");
                }
            }
        }

        /*
        This function checks if the string input is vaild
        Input:
            1) string str - the string that tested
            2) string varName
        Output:None
        */
        private void validInput(string str, string varName)
        {
            if (str == null | str == "")
            {
                throw new ArgumentException(varName + " is null or emptysapce, please change!");
            }
        }

        /*
        This function checks if the user exists and returns an error as expected
        Input:
            1) string email - The email checked
            2) bool exceptedExists - is user exists (true) or opisate
        Output:None
        */
        internal virtual void isEmailExists(string email, bool exceptedExists)
        {
            if (users.ContainsKey(email))
            {
                if (!exceptedExists)
                {
                    throw new ArgumentException("This email already exists, please change email");
                }
            }
            else
            {
                if (exceptedExists)
                {
                    throw new ArgumentException("Email does not exist!");
                }
            }
        }

        /*
        This function creates a new user and checks if it is valid
        Input:
            1) string email
            2) string password
        Output:User - The user that create if the input is valid
        */
        private User newUserValidation(string email, string password)
        {
            User newUser = new User(email, password);
            return newUser;
        }

        /*
        This function performs the entire registration process
        Input:
            1) string email
            2) string password
        Output:None
        */
        public void Register(String email,String password)
        {
            if (!ispersictent)
                loadData();
            validInput(email, "Email");
            validInput(password, "Password");
            isAnotherUserLogedIn(email);
            isEmailExists(email, false);
            IsValidEmailAddress(email);
            validateMailRules(email);
            dbInsert(email,password);

            users[email] = newUserValidation(email, password);
            log.Info("Created new User");
        }
        internal virtual void dbInsert(string email,string password)
        {
            DataAccessLayer.DTOs.UserDTO userDTO = new DataAccessLayer.DTOs.UserDTO(email, password);
            userDal.Insert(userDTO);
        }
        /*
 This function checks if the email is valid
 Input:None
 Output:bool - is valid email
 */
        internal virtual bool validateMailRules(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException("This is not a proper email");
            }

        }
        internal virtual bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                Regex rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                if (rx.IsMatch(emailaddress))
                    return true;
                throw new ArgumentException("This is not a proper email");
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message+ "::IsValidEmailAddress error");
            }
        }
        /*
        This function performs the entire login process
        Input:
            1) string email
            2) string password
        Output:None
        */
        public void Login(string email, string password)
        {
            if (!ispersictent)
                loadData();
            validInput(email, "Email");
            validInput(password, "Password");
            isAnotherUserLogedIn(email);
            User user = getUser(email);
            user.login(password);
            log.Info(email + " logged in succesfully");
            
        }

        /*
        This function performs the entire logout process
        Input:string email
        Output:None
        */
        public void Logout(string email)
        {
            validInput(email, "Email");
            isAnotherUserLogedIn(email);
            User user = getUser(email);
            user.logout();
            log.Info(email + " loggout in succesfully");
        }

        /*
        This function returns the user according to the email entered
        Input:string email
        Output:None
        */
        internal User getUser(String email)
        {
            validInput(email, "Email");
            isEmailExists(email, true);
            return users[email];
        }
     
    }
}
