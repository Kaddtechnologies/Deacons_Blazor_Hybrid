using System.Collections.Generic;

namespace Deacons.Hybrid.Shared.Models
{
    public class Email
    {
        public string emailaddress { get; set; }
    }

    public class Group
    {
        public string GroupName { get; set; }
        public List<Email> Emails { get; set; }
    }

    public class UsersOrGroups
    {
        public List<Group> Groups { get; set; }
    }

}
