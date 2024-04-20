using SampleBrowser.Maui.Base;
using Syncfusion.Licensing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Deacons.Hybrid.Mobile
{
    public static class BaseConfig
    {
        public static bool IsIndividualSB = false;

        public static Assembly? AssemblyName = null;

        public static Dictionary<string, Stream> AvailableControlStreamList = new Dictionary<string, Stream>();

        public static SBDevice RunTimeDevicePlatform { get; set; }

        public static SBLayout RunTimeDeviceLayout { get; set; }

        public static Page MainPageInit(Assembly appInfo)
        {
            GetDeviceInfo();
            AssemblyName = appInfo;
            string name = appInfo.GetName().Name;
            RegisterLicense(appInfo, name);
            //Stream manifestResourceStream = appInfo.GetManifestResourceStream(name + ".ControlList.xml");
            //if (manifestResourceStream != null)
            //{
            //    XDocument xDocument = XDocument.Load(manifestResourceStream);
            //    foreach (XElement item in from AllProduct in xDocument.Descendants("Assemblies")
            //                              select (AllProduct))
            //    {
            //        string text = item.Attribute("QualifiedInfo")?.Value;
            //        string text2 = item.Attribute("Prefix")?.Value;
            //        foreach (XElement item2 in from product in xDocument.Descendants("Assembly")
            //                                   select (product))
            //        {
            //            string text3 = item2.Attribute("Name")?.Value;
            //            Type type = Type.GetType(text2 + ".ControlConfig," + text2 + "," + text);
            //            if (type != null)
            //            {
            //                Stream manifestResourceStream2 = type.GetTypeInfo().Assembly.GetManifestResourceStream(name + "." + "SamplesList.xml");
            //                if (manifestResourceStream2 != null && !string.IsNullOrEmpty(text3))
            //                {
            //                    AvailableControlStreamList.Add(text3, manifestResourceStream2);
            //                }
            //            }
            //       }
             //  }
           // }

            MainPageiOS mainPageiOS = new MainPageiOS();
            NavigationPage.SetHasNavigationBar(mainPageiOS, value: false);
            return new NavigationPage(mainPageiOS)
            {
                BindingContext = PopulateViewModel()
            };
        }

        //
        // Summary:
        //     Register the license for Syncfusion controls
        public static void RegisterLicense(Assembly? assembly, string? name)
        {
            if (!(assembly != null) || string.IsNullOrEmpty(name))
            {
                return;
            }

            string text = "SyncfusionLicense.txt";
            string licenseKey = string.Empty;
            using Stream stream = assembly.GetManifestResourceStream(name + "." + text);
            if (stream != null)
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    licenseKey = streamReader.ReadToEnd();
                }

                SyncfusionLicenseProvider.RegisterLicense(licenseKey);
            }
        }

        public static void GetDeviceInfo()
        {
            RunTimeDevicePlatform = SBDevice.iOS;
            RunTimeDeviceLayout = SBLayout.Mobile;
        }

        public static SamplesViewModel PopulateViewModel()
        {
            return new SamplesViewModel();
        }
    }
}
