using Syncfusion.Blazor.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deacons.Hybrid.Shared.Utility
{
    public static class ToasterManger
    {
        public static string insertContent = "Record has been added successfully.";
        public static string updateContent = "Record has been updated successfully.";

        public static ToastModel Success = new ToastModel() { Title = "Success!", Content = "", CssClass = "e-toast-success", Icon = "e-success toast-icons" };
        public static ToastModel Error = new ToastModel { Title = "Error!", Content = "", CssClass = "e-toast-danger", Icon = "e-error toast-icons" };
        public static void UpdateSuccessContent(string newContent)
        {
            Success.Content = newContent;

        }
        public static void UpdateErrorContent(string newContent)
        {
            Error.Content = newContent;

        }

    }
}
