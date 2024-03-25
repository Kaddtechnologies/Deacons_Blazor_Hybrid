namespace Deacons.Hybrid.Shared.DBModels
{
    public class TeamUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public Guid UserId { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }   
        public string PhoneNumber { get; set; }
        public string DeaconTitle { get; set; }
        public string DeaconPosition { get; set; }
        public string TeamName { get; set; }
            
    }
}
