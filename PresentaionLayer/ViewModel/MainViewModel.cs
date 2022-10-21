﻿
using Presentation.Model;
using System;

namespace PresentaionLayer
{
    class MainViewModel : NotifiableObject
    {
        public BackendController Controller { get; private set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                this._username = value;
                RaisePropertyChanged("Username");
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                this._password = value;
                RaisePropertyChanged("Password");
            }
        }
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                this._message = value;
                RaisePropertyChanged("Message");
            }
        }

        public UserModel Login()
        {
            Message = "";
            try
            {
                return Controller.Login(Username, Password);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }
        public UserModel Register()
        {
            Message = "";
            try
            {
                Controller.Register(Username, Password);
                Message = "Registered successfully";
                UserModel um = new UserModel(Controller, Username);
                return um;

            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }

        

        public MainViewModel()
        {
            this.Controller = new BackendController();
            this.Username = "bayan@hotmail.com";
            this.Password = "abc123";
        }

    }
}
