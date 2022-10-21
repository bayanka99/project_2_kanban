using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntroSE.ForumSystem.DataAccessLayer
{
    internal class User
    {
        JsonController dalController;
        private readonly string _email;
        public string Email { get => _email; }
        private string _password;
        public string Password { get => _password; set => _password = value; }

        internal User(string Email, string Password)
        {
            this.dalController = new JsonController();
            this._email = Email;
            this._password = Password;
        }

        public void Save()
        {
            dalController.Write(Email, ToJson());
        }

        private string ToJson()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }


    }
}