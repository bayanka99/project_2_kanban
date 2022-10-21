using System.Reflection;
using System;
using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;
using log4net.Config;
using System.IO;



namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private readonly UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        //Constructor
        public UserService()
        {
            userController = new UserController();
            // Load configuration
            //Right click on log4net.config file and choose Properties. 
            //Then change option under Copy to Output Directory build action into Copy if newer or Copy always.
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Starting log!");
        }
        ///<summary>This method loads the data from the persistance.
        ///You should call this function when the program starts. </summary>
        public Response LoadData()
        {
            try
            {
                userController.loadData();
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);

                return new Response("LoadData failed!  " + e.Message);
            }
        }

        ///<summary>Removes all persistent data.</summary>
        public Response DeleteData()
        {
            try
            {
                userController.deleteData();
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);

                return new Response("Delete Data failed!  " + e.Message);
            }

        }



        ///<summary>This method registers a new user to the system.</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        ///<returns cref="Response">The response of the action</returns>
        public Response Register(string email, string password)
        {
            try
            {
                userController.Register(email, password);
                User user = new User(email);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {log.Info(e.Message);

                return new Response("registeration failed!  " + e.Message);
            }
        }

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            try
            {
                userController.Login(email, password);
                User user = new User(email);
                Response<User> ret = Response<User>.FromValue(user);
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response<User> ret = Response<User>.FromError("log in failed!  " + e.Message);
                return ret;
            }
        }

        // <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            try
            {
                userController.Logout(email);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {log.Info(e.Message);

                Response ret = new Response("user not logged out::"+e.Message);
                return ret;
            }
        }

        /*
        This function returns the UserController
        Input:None
        Output:UserController
        */
        internal UserController sendUC()
        {
            return userController;
        }








    }
}
