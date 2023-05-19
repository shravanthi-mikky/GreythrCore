using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL iUserRL;
        public UserBL(IUserRL iUserRL)
        {
            this.iUserRL = iUserRL;
        }

        //public ProfileModel UserLogin(LoginModel loginModel)
        //{
        //    try
        //    {
        //        return iUserRL.UserLogin(loginModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}


        public string Login(LoginModel loginModel)
        {
            try
            {
                return iUserRL.Login(loginModel);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public ProfileModel2 Register(ProfileModel2 registrationModel)
        {
            try
            {
                return iUserRL.Register(registrationModel);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
    }


}
