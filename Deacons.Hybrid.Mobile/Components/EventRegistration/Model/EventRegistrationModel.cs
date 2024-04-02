namespace Deacons.Hybrid.Mobile.SfDataForm
{
    using Syncfusion.Maui.DataForm;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents the model class for event registration.
    /// </summary>
    public class EventRegistrationModel
    {
        public EventRegistrationModel()
        {
            this.ProfileImage = new Image();
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Mobile = string.Empty;
            this.Event = string.Empty;
            this.EventDate = DateTime.Now;
            this.EventEndDate = DateTime.Now;
            this.StartTime = new TimeSpan();
            this.EndTime = new TimeSpan();
            this.State = string.Empty;
            this.Address = string.Empty;
            this.City = string.Empty;
            this.Zip = string.Empty;
        }

        [Display(Name = "Profile Image", Prompt = "Profile Image")]
        public Image ProfileImage { get; set; }

        [Display(Name = "First Name", Prompt = "First name")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name", Prompt = "Last name")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [Display(Prompt = "Organizer Phone")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        public string Mobile{ get; set; }

        [Display(Name = "Event Start Date")]
        [Required(ErrorMessage = "Please enter a Date")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Event End Date")]
        [Required(ErrorMessage = "Please enter a Date")]
        public DateTime EventEndDate { get; set; }

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "Please enter event start time")]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        [Required(ErrorMessage = "Please enter event end time")]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Event or Activity Name", Prompt = "Name your event")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        [Required(ErrorMessage = "Please select an event")]
        public string Event { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Event or Activity Details", Prompt = "Describe your event")]
        [DataFormDisplayOptions(ColumnSpan = 2, RowSpan = 2)]
        [Required(ErrorMessage = "Please add event details")]
        public string? EventDetails { get; set; }

        [Display(Prompt = "Enter Event Address", Name = "Address")]
        [DataFormDisplayOptions(ColumnSpan = 2, RowSpan = 1)]
        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [Display(Prompt = "Event City", Name = "City")]
        [Required(ErrorMessage = "Please enter your city")]
        public string City { get; set; }

        [Display(Prompt = "Event State", Name = "State")]
        [Required(ErrorMessage = "Please enter your state")]
        public string State { get; set; }

        [Display(Prompt = "Event  Zipcode", Name = "Zip code")]
        [Required(ErrorMessage = "Please enter your zip code")]
        public string Zip { get; set; }       

    }
}