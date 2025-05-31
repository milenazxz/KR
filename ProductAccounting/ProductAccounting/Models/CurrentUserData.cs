using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Models
{
    public class CurrentUserData
    {
        private string _name;
        private string _role;
        public CurrentUserData(string name, string role) 
        {
            this._name = name;
            this._role = role;
        }
        public string GetRole() 
        {
            return this._role;
        }

    }
}
