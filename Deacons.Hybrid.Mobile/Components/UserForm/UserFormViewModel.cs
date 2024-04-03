using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deacons.Hybrid.Mobile.Components.UserForm
{
    public class UserFormViewModel
    {
        public UserFormViewModel()
        {
            this.UserFormModel = new UserFormModel();
        }

        public UserFormModel UserFormModel { get; set; }
    }
}
