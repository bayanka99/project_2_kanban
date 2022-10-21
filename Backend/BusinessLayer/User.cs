using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests")]

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    static class UserConstants
    {
        public const int PASSWORD_MIN_LENGTH = 4;
        public const int PASSWORD_MAX_LENGTH = 20;
    }
    class User
    {


        //fields
        public string email { get; set; }
        private string password { get; set; }
        private bool isLoggedIn;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        //Constructor
        public User(string email, string password)
        {
           
            
            this.password = password;
            this.email = email;


            
            this.isLoggedIn = true;
        }
        internal User(UserDTO userDTO)
        {
            this.password = userDTO.password;
            this.email = userDTO.email;
            this.isLoggedIn = false;

        }
        //Methods:

        /*
        This function checks if the user is connected
        Input:bool - expect the user to be logged in
        Output:None
        */
        private void isLogIn(bool expectLoggedIn)
        {
            if (this.isLoggedIn)
            {
                if(!expectLoggedIn)
                {
                    throw new ArgumentException("The user is logged in!");
                }
            }
            else
            {
                if (expectLoggedIn)
                {
                    throw new ArgumentException("User not logged in Please log in before!");
                }
            }
        }

        /*
        This function changes the user's password
        Input:None
        Output:None
        */
        public void changePassword(String newPassword)
        {
            isLogIn(false);
            if (validatePasswordRules(newPassword))
            {
                this.password = newPassword;
                log.Info("Password changed successfully for " + this.email);
            }
            else
            {
                throw new ArgumentException("new password does not match password rules, please enter legal password");
            }

        }

        /*
        This function login the user
        Input:string password 
        Output:None
        */
        public void login(string password)
        {
            isLogIn(false);
            validatePasswordMatch(password);
            this.isLoggedIn = true;
        }

        /*
        This function disconnects the user
        Input:None
        Output:None
        */
        public void logout()
        {
            isLogIn(true);
            this.isLoggedIn = false;
            log.Info(this.email + " logged out");
        }

        /*
        This function checks whether the password is correct
        Input:string password - The password being checked
        Output:bool - is correct password
        */
        public bool validatePasswordMatch(String password)
        {
            if (!this.password.Equals(password))
            {
                throw new ArgumentException("The password you entered is incorrect!");
            }
            return true;
        }

 

        /*
        This function checks if the password is valid
        Input:None
        Output:bool - is valid password
        */
        internal bool validatePasswordRules(String password)
        {
            String[] cyberilligalpass = { "123456", "123456789", "qwerty", "password", "1111111", "12345678", "abc123", "1234567", "password1", "12345", "1234567890", "123123", "0", "Iloveyou", "1234", "1q2w3e4r5t", "Qwertyuiop", "123", "Monkey", "Dragon" };
            bool hasUpper = false, hasLower = false, hasNumber = false;
            if (password.Length < UserConstants.PASSWORD_MIN_LENGTH | password.Length > UserConstants.PASSWORD_MAX_LENGTH)
            {//check password length
                throw new ArgumentException("This is not a proper password");
            }
            if (cyberilligalpass.Contains(password)){
                throw new ArgumentException("This is not a proper password");
            }
            foreach (char c in password)
            {
                if (c <= 'Z' & c >= 'A')
                {
                    hasUpper = true;
                }
                else if (c <= 'z' & c >= 'a')
                {
                    hasLower = true;
                }
                else if (c <= '9' & c >= '0')
                {
                    hasNumber = true;
                }
            }
            if (hasUpper & hasLower & hasNumber)
            {
                return true;
            }
            throw new ArgumentException("This is not a proper password");

        }

        /*
        This function checks if the user is logged in
        Input:None
        Output:bool - is logged in
        */
        internal bool checkIfLoggedIn()
        {
            return this.isLoggedIn;
        }
    }
}
