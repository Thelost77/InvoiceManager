using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace InvoiceManager.Models
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}