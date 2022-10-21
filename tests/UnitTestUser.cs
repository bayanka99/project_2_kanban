using IntroSE.Kanban.Backend.BusinessLayer;
using NUnit.Framework;
using System;

namespace tests
{
    public class Tests
    {
        UserController uc;

        [SetUp]
        public void Setup()
        {
            uc = new UserController();
        }

        [Test]
        public void Registar_ValidInputs_Success()
        {
            //arrenge

            //act
            uc.Register("bill2@gmail.com", "Passw0rd");

            //assert
            Assert.AreEqual("bill2@gmail.com",uc.getUser("bill2@gmail.com").email,"email is not found in user controller or db");

        }
    }
}