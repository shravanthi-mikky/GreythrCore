using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        //public ProfileModel UserLogin(LoginModel loginModel);

        public string Login(LoginModel loginModel);
        public ProfileModel2 Register(ProfileModel2 registrationModel);
    }
}
