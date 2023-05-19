using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        //public ProfileModel UserLogin(LoginModel loginModel);

        public string Login(LoginModel loginModel);
        public ProfileModel2 Register(ProfileModel2 registrationModel);
    }
}
