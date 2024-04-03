using Syncfusion.Maui.DataForm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
namespace Deacons.Hybrid.Mobile.Components.UserForm
{
    public class UserFormModel
    {
        public UserFormModel() 
        {

            //this.ProfileImage = new Image();
            //this.FirstName = string.Empty;
            //this.LastName = string.Empty;
            //this.Mobile = string.Empty;
            //this.Event = string.Empty;
            //this.EventDate = DateTime.Now;
            //this.EventEndDate = DateTime.Now;
            //this.StartTime = new TimeSpan();
            //this.EndTime = new TimeSpan();
            //this.State = string.Empty;
            //this.Address = string.Empty;
            //this.City = string.Empty;
            //this.Zip = string.Empty;
        }

        [DataFormDisplayOptions(ColumnSpan = 2, ShowLabel = false)]
        public string ProfileImage { get; set; }

        [Display(Prompt = "First name")]
        [Required(ErrorMessage = "Name should not be empty")]
        public string Name { get; set; }

        [Display(Prompt = "Last name")]
        public string LastName { get; set; }

        [Display(Prompt = "Mobile")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public double? Mobile { get; set; }

        [Display(Prompt = "Landline")]
        public double? Landline { get; set; }

        [Display(Prompt = "Address")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        public string Address { get; set; }

        [Display(Prompt = "City")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        public string City { get; set; }

        [Display(Prompt = "State")]
        public string State { get; set; }

        [Display(Prompt = "Zip code")]
        [DataFormDisplayOptions(ShowLabel = false)]
        public double? ZipCode { get; set; }

        [Display(Prompt = "Email")]
        public string Email { get; set; }
    }
}
