
using Syncfusion.Maui.DataForm;

namespace Deacons.Hybrid.Mobile.Components.UserForm
{
    public class UserFormBehavior: Behavior<ContentPage>
    {
        /// </summary>
        public SfDataForm? dataForm;
        
        /// <summary>
        /// Holds the save button instance.
        /// </summary>
        public Button? saveButton;

        /// <inheritdoc/>
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.FindByName<SfDataForm>("userForm");

            if (this.dataForm != null)
            {
                dataForm.RegisterEditor(nameof(UserFormModel.Mobile), new NumericEditor(dataForm));
                dataForm.RegisterEditor(nameof(UserFormModel.ZipCode), new NumericEditor(dataForm));
                dataForm.RegisterEditor(nameof(UserFormModel.Landline), new NumericEditor(dataForm));
                dataForm.ValidateForm += this.OnDataFormValidateForm;
                dataForm.ValidateProperty += this.OnDataFormValidateProperty;
            }

            this.saveButton = bindable.FindByName<Button>("saveButton");
            if (this.saveButton != null)
            {
                this.saveButton.Clicked += OnSaveButtonClicked;
            }
        }

        /// <summary>
        /// Invokes on data form item validation.
        /// </summary>
        /// <param name="sender">The data form.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDataFormValidateProperty(object? sender, DataFormValidatePropertyEventArgs e)
        {
            if (e.PropertyName == nameof(UserFormModel.Mobile) && !e.IsValid)
            {
                e.ErrorMessage = e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()) ? "Please enter the mobile number" : "Invalid mobile number";
            }
        }

        /// <summary>
        /// Invokes on manual data form validation.
        /// </summary>
        /// <param name="sender">The data form.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnDataFormValidateForm(object? sender, DataFormValidateFormEventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (e.ErrorMessage != null && e.ErrorMessage.Count > 0)
                {
                    if (e.ErrorMessage.Count == 2)
                    {
                        await App.Current.MainPage.DisplayAlert("", "Please enter the contact name and mobile number", "OK");
                    }
                    else
                    {
                        if (e.ErrorMessage.ContainsKey(nameof(UserFormModel.Name)))
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please enter the contact name", "OK");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please enter the mobile number", "OK");
                        }
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Contact saved", "OK");
                }
            }
        }

        /// <summary>
        /// Invokes on save button click.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSaveButtonClicked(object? sender, EventArgs e)
        {
            this.dataForm?.Validate();
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.dataForm != null)
            {
                this.dataForm.ValidateForm -= this.OnDataFormValidateForm;
                this.dataForm.ValidateProperty -= this.OnDataFormValidateProperty;
                this.dataForm = null;
            }

            if (this.saveButton != null)
            {
                this.saveButton.Clicked -= OnSaveButtonClicked;
                this.saveButton = null;
            }
        }
    }
}
