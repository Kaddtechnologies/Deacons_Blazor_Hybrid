using DevExpress.Maui.Controls;
using System.Net.Mail;

namespace Deacons.Hybrid.Mobile.Components.UserForm;

public partial class UserForm : ContentPage
{
	public UserForm()
	{
		InitializeComponent();
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