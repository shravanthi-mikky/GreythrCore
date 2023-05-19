using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class ProfileModel
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public long PrimaryContact { get; set; }
        public string CompanyEmail { get; set; }
        public string Password { get; set; }
    }

    public class ProfileModel2
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public long PrimaryContact { get; set; }
        public string CompanyEmail { get; set; }
        public string Password { get; set; }
    }
}
