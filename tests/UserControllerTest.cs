using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using Moq;
using NUnit.Framework;

namespace Tests
{
    class UserControllerTest
    {
        IntroSE.Kanban.Backend.BusinessLayer.UserController uc;
        Mock<UserController> mockUserControllerA;
        Mock<User> mockUserA;
        Mock<User> mockUserB;

        [SetUp]
        public void SetUp()
        {
            uc = new UserController();
            mockUserControllerA = new Mock<UserController>();
            mockUserA = new Mock<User>();
            mockUserB = new Mock<User>();
            mockUserControllerA.Object.Register("email@gmail.com", "Passw0rd!");
            mockUserControllerA.Object.Logout("email@gmail.com");
        }
        [TearDown]
        public void Dispose()
        {
            mockUserControllerA.Object.deleteData();
        }
        [Test]
        public void Register_leagalLogin_success()
        {
            //arrenge
            string email = "email";
            string password = "Passw0rd!";

            mockUserControllerA.Setup(m => m.IsValidEmailAddress(email)).Returns(true);
            mockUserControllerA.Setup(m => m.validateMailRules(email)).Returns(true);
            mockUserControllerA.Setup(m => m.isEmailExists(email,true)).Verifiable();
            mockUserControllerA.Setup(m => m.loadData()).Verifiable();
            mockUserControllerA.Setup(m => m.dbInsert(email,password)).Verifiable();

            
            //mockUserA.Setup(m => m.checkIfLoggedIn()).Returns(true);
            //act
            mockUserControllerA.Object.Register(email, "Passw0rd!");
            string result = mockUserControllerA.Object.getUser(email).email;

            //assert
            Assert.AreEqual(email, result, "email is not found in user controller or db");

        }
        [Test]
        public void Login_leagalLogin_success()
        {
            //arrenge
            string email = "email@gmail.com";
            string password = "Passw0rd!";

            mockUserControllerA.Setup(m => m.IsValidEmailAddress(email)).Returns(true);
            mockUserControllerA.Setup(m => m.validateMailRules(email)).Returns(true);
            mockUserControllerA.Setup(m => m.isEmailExists(email, true)).Verifiable();
            mockUserControllerA.Setup(m => m.dbInsert(email, password)).Verifiable();
            mockUserControllerA.Setup(m => m.isAnotherUserLogedIn(email)).Verifiable();


            //mockUserA.Setup(m => m.checkIfLoggedIn()).Returns(true);
            //act
            mockUserControllerA.Object.Login(email, "Passw0rd!");
            bool result = mockUserControllerA.Object.getUser(email).checkIfLoggedIn();

            //assert
            Assert.AreEqual(true, result, "email is not found in user controller or db");

        }

    }
    

}
