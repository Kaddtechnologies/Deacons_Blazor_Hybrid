using Deacons.Hybrid.Shared.Models;
using Deacons.Hybrid.Shared.Utility;
using DevExpress.Maui.Controls;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Mail;
namespace Deacons.Hybrid.Mobile.Components.UserForm;

public partial class UserForm : ContentPage
{
    IDialogService _dialogService;
    HttpClient _httpClient;
    User ProfileUser { get; set; } = new();
    List<User> UsersList { get; set; } = new();
    List<User> SelectedUsers { get; set; } = new();
    List<User> DummySkeletonUsers = new List<User>(new User[3]);
    List<string> DeaconTitles { get; set; } = new();
    List<DeaconTitleModel> DeaconPositions { get; set; }

    public string imgSrc = "https://pottershousedeacons.blob.core.windows.net/imagescontainer/avatars/default_avatar.jpg";
    bool Loading = true;
    public UserForm(string userId = "")	{
		InitializeComponent();
        _httpClient = UtilityService.GetService<HttpClient>();
        _dialogService = UtilityService.GetService<IDialogService>();
        dataForm.DataObject = new UserFormViewModel();
       // dataForm.PickerSourceProvider = new ComboBoxDataProvider();
        if (!string.IsNullOrEmpty(userId) )
        {
            SetAndPatchAsync(userId);
        }
    }

    private async Task SetAndPatchAsync(string userId)
    {
        DeaconTitles.AddRange(new[] { "Yokefellow", "Deacon", "D.I.T", "Administrator", "All" });

        var response = await _httpClient.GetAsync(@$"api/Users/GetUserById?id={userId}");
        if (response.IsSuccessStatusCode)
        {
            ProfileUser = await response.Content.ReadFromJsonAsync<User>();
           if(!string.IsNullOrEmpty(ProfileUser.FirstName)) 
            {
                var userFormViewModel = new UserFormViewModel();
                userFormViewModel.UserFormModel.FirstName = ProfileUser.FirstName;
                userFormViewModel.UserFormModel.LastName = ProfileUser.LastName;
                userFormViewModel.UserFormModel.Address = ProfileUser.Address;
                userFormViewModel.UserFormModel.City = ProfileUser.City;
                userFormViewModel.UserFormModel.ZipCode = ProfileUser.Zip;
                userFormViewModel.UserFormModel.State = ProfileUser.State;
                userFormViewModel.UserFormModel.PhotoPath = ProfileUser.AvatarUrl;
                userFormViewModel.UserFormModel.Phone = ProfileUser.PhoneNumber;
                userFormViewModel.UserFormModel.TitleId = ProfileUser.TitleId;
                dataForm.DataObject = userFormViewModel.UserFormModel;
            }
        }
        response = await _httpClient.GetAsync("api/Users/GetDeaconTitleList");
        if (response.IsSuccessStatusCode)
        {
            DeaconPositions = await response.Content.ReadFromJsonAsync<List<DeaconTitleModel>>();
        }
        Loading = false;
    }

    public void ValidateCustomerProperties(object sender, DevExpress.Maui.DataForm.DataFormPropertyValidationEventArgs e)
    {
        if (e.PropertyName == "Email" && e.NewValue != null)
        {
            MailAddress res;
            if (!MailAddress.TryCreate((string)e.NewValue, out res))
            {
                e.HasError = true;
                e.ErrorText = "Invalid email";
            }
        }
    }
    public async void GoBack()
    {
        await Navigation.PopAsync();
    }
    public void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        dataForm.Commit();
    }

    public void ImageTapped(object sender, EventArgs e)
    {
        bottomSheet.State = BottomSheetState.HalfExpanded;
    }

    public async void DeletePhotoClicked(object sender, EventArgs args)
    {
        await Dispatcher.DispatchAsync(() => {
            bottomSheet.State = BottomSheetState.Hidden;
            editControl.IsVisible = false;
            preview.Source = null;
        });
    }

    public async void SelectPhotoClicked(object sender, EventArgs args)
    {
        var photo = await MediaPicker.PickPhotoAsync();
        await ProcessResult(photo);
        editControl.IsVisible = true;
    }

    public async void TakePhotoClicked(object sender, EventArgs args)
    {
        if (!MediaPicker.Default.IsCaptureSupported)
            return;

        var photo = await MediaPicker.Default.CapturePhotoAsync();
        await ProcessResult(photo);
        editControl.IsVisible = true;
    }

    public async Task ProcessResult(FileResult result)
    {
        await Dispatcher.DispatchAsync(() => {
            bottomSheet.State = BottomSheetState.Hidden;
        });


        if (result == null)
            return;

        ImageSource imageSource = null;
        if (System.IO.Path.IsPathRooted(result.FullPath))
            imageSource = ImageSource.FromFile(result.FullPath);
        else
        {
            var stream = await result.OpenReadAsync();
            imageSource = ImageSource.FromStream(() => stream);
        }
        var editorPage = new ImageEditView(imageSource);
        await Navigation.PushAsync(editorPage);

        var cropResult = await editorPage.WaitForResultAsync();
        if (cropResult != null)
            preview.Source = cropResult;

        editorPage.Handler.DisconnectHandler();
    }
}