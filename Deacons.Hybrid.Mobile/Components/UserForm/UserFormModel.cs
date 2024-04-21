using Deacons.Hybrid.Shared.Models;
using DevExpress.Maui.DataForm;
using Microsoft.Maui;
using Syncfusion.Maui.DataForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataFormDisplayOptionsAttribute = DevExpress.Maui.DataForm.DataFormDisplayOptionsAttribute;
namespace Deacons.Hybrid.Mobile.Components.UserForm
{
    public class UserFormModel: BindableBase
    {
        Guid userId;
        string firstName;
        string lastName;
        string address;
        string city;
        StateList state;
        string email;
        string zipcode;
        string phone;
        string photoPath  = "https://pottershousedeacons.blob.core.windows.net/imagescontainer/avatars/1000006725.jpg";
        Guid? titleId;

        public Guid UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                RaisePropertyChanged();
            }
        }
        [DataFormComboBoxEditor(Placeholder = "Deacon Title")]
        [DataFormDisplayOptions(GroupName = "Title")]
        [Required(ErrorMessage = "Deacon Title cannot be empty")]
        public Guid? TitleId
        {
            get
            {
                return titleId;
            }
            set
            {
                titleId = value;
                RaisePropertyChanged();
            }
        }
        [DataFormTextEditor(Placeholder = "First Name")]
        [DevExpress.Maui.DataForm.DataFormDisplayOptions(GroupName = "Name")]
        [Required(ErrorMessage = "First Name cannot be empty")]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                RaisePropertyChanged();
            }
        }
        [DataFormTextEditor(Placeholder = "Last Name")]
        [DataFormDisplayOptions(GroupName = "Name")]
        [Required(ErrorMessage = "Last Name cannot be empty")]
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                RaisePropertyChanged();
            }
        }       

        [DataFormTextEditor(Placeholder = "Address")]
        [DataFormDisplayOptions(GroupName = "Address")]
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                RaisePropertyChanged();
            }
        }

        [DataFormTextEditor(Placeholder = "City")]
        [DataFormDisplayOptions(GroupName = "Address")]
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
                RaisePropertyChanged();
            }
        }
        [DataFormComboBoxEditor(Placeholder = "State")]
        [DataFormDisplayOptions(GroupName = "Address")]
        public StateList State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                RaisePropertyChanged();
            }
        }

        [DataFormMaskedEditor(Mask = "00000", Keyboard = "Numeric", Placeholder = "Zip")]
        [DataFormDisplayOptions(GroupName = "Address")]
        public string ZipCode
        {
            get
            {
                return zipcode;
            }
            set
            {
                zipcode = value;
                RaisePropertyChanged();
            }
        }

        [DataFormTextEditor(Placeholder = "Phone")]
        [DataFormDisplayOptions(GroupName = "Contact Information")]
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
                RaisePropertyChanged();
            }
        }

        [DataFormTextEditor(Placeholder = "Email")]
        [DataFormDisplayOptions(GroupName = "Contact Information")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                RaisePropertyChanged();
            }
        }
        [DataFormDisplayOptionsAttribute(SkipAutoGenerating = true)]
        public string PhotoPath
        {
            get
            {
                return photoPath;
            }
            set
            {
                photoPath = value;
                RaisePropertyChanged();
            }
        }
    }
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum StateList
    {
        [Description("Alabama")]
        AL,
        [Description("Alaska")]
        AK,
        [Description("Arkansas")]
        AR,
        [Description("Arizona")]
        AZ,
        [Description("California")]
        CA,
        [Description("Colorado")]
        CO,
        [Description("Connecticut")]
        CT,
        [Description("D.C.")]
        DC,
        [Description("Delaware")]
        DE,
        [Description("Florida")]
        FL,
        [Description("Georgia")]
        GA,
        [Description("Hawaii")]
        HI,
        [Description("Iowa")]
        IA,
        [Description("Idaho")]
        ID,
        [Description("Illinois")]
        IL,
        [Description("Indiana")]
        IN,
        [Description("Kansas")]
        KS,
        [Description("Kentucky")]
        KY,
        [Description("Louisiana")]
        LA,
        [Description("Massachusetts")]
        MA,
        [Description("Maryland")]
        MD,
        [Description("Maine")]
        ME,
        [Description("Michigan")]
        MI,
        [Description("Minnesota")]
        MN,
        [Description("Missouri")]
        MO,
        [Description("Mississippi")]
        MS,
        [Description("Montana")]
        MT,
        [Description("North Carolina")]
        NC,
        [Description("North Dakota")]
        ND,
        [Description("Nebraska")]
        NE,
        [Description("New Hampshire")]
        NH,
        [Description("New Jersey")]
        NJ,
        [Description("New Mexico")]
        NM,
        [Description("Nevada")]
        NV,
        [Description("New York")]
        NY,
        [Description("Oklahoma")]
        OK,
        [Description("Ohio")]
        OH,
        [Description("Oregon")]
        OR,
        [Description("Pennsylvania")]
        PA,
        [Description("Rhode Island")]
        RI,
        [Description("South Carolina")]
        SC,
        [Description("South Dakota")]
        SD,
        [Description("Tennessee")]
        TN,
        [Description("Texas")]
        TX,
        [Description("Utah")]
        UT,
        [Description("Virginia")]
        VA,
        [Description("Vermont")]
        VT,
        [Description("Washington")]
        WA,
        [Description("Wisconsin")]
        WI,
        [Description("West Virginia")]
        WV,
        [Description("Wyoming")]
        WY
    }
}
