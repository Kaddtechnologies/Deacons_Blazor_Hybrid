#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace Deacons.Hybrid.Mobile.SfDataForm
{
    using ColorHelper;
    using Microsoft.Maui;
    using Deacons.Hybrid.Mobile.DataForm;
    using Deacons.Hybrid.Mobile.DataForm.Common;
    using Deacons.Hybrid.Mobile.DataForm.EventRegistration.Model;
    using Syncfusion.Maui.Core;
    using Syncfusion.Maui.Core.Internals;
    using Syncfusion.Maui.DataForm;
    using Syncfusion.Maui.Inputs;
    using System;
    using Deacons.Hybrid.Shared.Models;

    /// <summary>
    /// Represents a behavior class for event registration sample.
    /// </summary>
    public class EventRegistrationBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm? dataForm;
       
        /// <summary>
        /// Holds the clear button instance.
        /// </summary>
        private Button? clearButton;

        /// <summary>
        /// Holds the submit button instance.
        /// </summary>
        private Button? submitButton;
        private SfBusyIndicator? busyIndicator;

        private SfAutocomplete? autoComplete; 

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            try
            {

                base.OnAttachedTo(bindable);
                DataFormCustomItem customItem = new DataFormCustomItem();
                autoComplete = new SfAutocomplete();
                autoComplete.TextSearchMode = AutocompleteTextSearchMode.Contains;
                autoComplete.FilterBehavior = new CustomFiltering();
                autoComplete.HeightRequest = 50;
                autoComplete.WidthRequest =
                autoComplete.MaxDropDownHeight = 250;
                autoComplete.Placeholder = "Find an Address";
                customItem.FieldName = nameof(EventRegistrationModel.Address);
                customItem.EditorView = autoComplete;

                DataFormCustomItem customAvatarItem = new DataFormCustomItem();
                var avatarView = new SfAvatarView();
                avatarView.VerticalOptions = LayoutOptions.Center;
                avatarView.HorizontalOptions = LayoutOptions.Center;
                avatarView.ContentType = ContentType.AvatarCharacter;
                avatarView.BackgroundColor = Color.Parse("Purple");
                avatarView.WidthRequest = 50;
                avatarView.HeightRequest = 50;
                avatarView.CornerRadius = 25;
                avatarView.AvatarCharacter = AvatarCharacter.Avatar8;
                customAvatarItem.FieldName = "Profile Image";
                customAvatarItem.EditorView = avatarView;


                this.dataForm = bindable.Content.FindByName<SfDataForm>("eventForm");
                dataForm.ItemsSourceProvider = new ItemsSourceProvider();

                if (this.dataForm != null)
                {
                    this.dataForm.Items.Add(customAvatarItem);
                    this.dataForm.RegisterEditor("State", DataFormEditorType.AutoComplete);
                    //this.dataForm.RegisterEditor(nameof(EventRegistrationModel.Address), autoComplete);
                    this.dataForm.RegisterEditor(nameof(EventRegistrationModel.Mobile), DataFormEditorType.MaskedText);
                    this.dataForm.RegisterEditor(nameof(EventRegistrationModel.StartTime), DataFormEditorType.Time);
                    this.dataForm.RegisterEditor(nameof(EventRegistrationModel.EndTime), DataFormEditorType.Time);
                    this.dataForm.RegisterEditor(nameof(EventRegistrationModel.State), DataFormEditorType.ComboBox);                    
                    this.dataForm.RegisterEditor(nameof(EventRegistrationModel.Zip), DataFormEditorType.MaskedText);
                    this.dataForm.RegisterEditor(nameof(EventRegistrationModel.Address), DataFormEditorType.AutoComplete);
                    this.dataForm.ItemManager = new DataFormItemManagerEditorExt();
                    this.dataForm.GenerateDataFormItem += OnGenerateDataFormItem;
                }

                this.clearButton = bindable.Content.FindByName<Button>("clearButton");
                this.submitButton = bindable.Content.FindByName<Button>("submitButton");
                this.busyIndicator = bindable.Content.FindByName<SfBusyIndicator>("busyIndicator");
                

                if (this.clearButton != null)
                {
                    this.clearButton.Clicked += OnClearButtonClicked;
                }

                if (this.submitButton != null)
                {
                    this.submitButton.Clicked += OnSubmitButtonClicked;
                }

                if(this.autoComplete != null)
                {
                    //this.autoComplete.AddKeyboardListener()
                }
              
            }
            catch (Exception ex)
            {

                App.Current!.MainPage!.DisplayAlert("Loading Error", "There was an error loading the page" + ex.Message, "OK");
            }
        }
      
        /// <summary>
        /// Invokes on clear button click.
        /// </summary>
        /// <param name="sender">The clear button.</param>
        /// <param name="e">The event arguments.</param>
        private void OnClearButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null)
            {
                this.dataForm.DataObject = new EventRegistrationModel();   
            }
        }
        public class DataFormItemManagerEditorExt : DataFormItemManager

        {

            public override void InitializeDataEditor(DataFormItem dataFormItem, View editor)

            {

                if (editor is SfAutocomplete autoComplete)

                {

                    autoComplete.FilterBehavior = new CustomFiltering();

                }

            }

        }


        /// <summary>
        /// Invokes on submit button click.
        /// </summary>
        /// <param name="sender">The submit button.</param>
        private async void OnSubmitButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (this.dataForm.Validate())
                {
                    EventRegistrationModel eventRegistration = (EventRegistrationModel)this.dataForm.DataObject;
                    CalendarEventsModel model = ParseEventRegistration(eventRegistration);
                    string  returnn = await _calendarEventsService.CreateNewEvent(model);
                    if (returnn.Contains(model.EventName!))
                    {
                        this.busyIndicator!.IsRunning = false;
                        await App.Current.MainPage.DisplayAlert("Success", @$"Event {returnn} Created Successfully", "OK");
                        this.dataForm.DataObject = new EventRegistrationModel();

                    }
                    else
                    {
                        this.busyIndicator!.IsRunning = false;
                        await App.Current.MainPage.DisplayAlert("Signup Error", "There was an error creating a new event", "OK");

                    }
                   
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
            }
        }

        private CalendarEventsModel ParseEventRegistration(EventRegistrationModel eventRegistration)
        {
            var bgColor = ColorGenerator.GetRandomColor<HEX>().Value;
            bgColor = @$"0xFF{ bgColor}";
            //var startTime = new TimeSpan(7, 0, 0);
            //var endTime = new TimeSpan(8, 0, 0);
            var startTime = eventRegistration.StartTime;   // 11:25 AM and 46 seconds
            var endTime = eventRegistration.EndTime;
            var startDate = new DateTime(eventRegistration.EventDate.Year, eventRegistration.EventDate.Month, eventRegistration.EventDate.Day); // October 21, 2015
            var endDate = new DateTime(eventRegistration.EventEndDate.Year, eventRegistration.EventEndDate.Month, eventRegistration.EventEndDate.Day); // October 21, 2015
            var startDateTime = startDate.Add(startTime);
            var endDateTime = endDate.Add(endTime);

            var model = new CalendarEventsModel();
            model.CalendarId = Guid.NewGuid();
            model.EventName = eventRegistration.Event;
            model.Organizer = eventRegistration.FirstName + " " + eventRegistration.LastName;
            model.StartDate = startDateTime;
            model.EndDate = endDateTime;
            model.BackgroundColor = bgColor.ToString();
            model.Notes = eventRegistration.EventDetails;
            model.EventAddress = $@"{eventRegistration.Address} {eventRegistration.City} {eventRegistration.State} {eventRegistration.Zip}";           
            model.IsAllDay = false;
            return model;
        }

        //private void handleAddressChange(google.maps.places.PlaceResult)
        //{
        //    console.log(place);
        //    var addressModel =  getAddressObject(Google.Api.  place.address_components);
        //    this.dataForm.DataObject["streetAddress"] = addressModel["home"] + " " + addressModel["street"];
        //    this.dataForm.DataObject.get("city").setValue(addressModel["city"]);
        //    this.dataForm.DataObject.get("state").setValue(addressModel["region"]);
        //    this.dataForm.DataObject.get("zip").setValue(addressModel["postal_code"]);
        //    this.dataForm.DataObject.get("clientAddressCounty").setValue(addressModel["county"]);
        //}


        /// <summary>
        /// Invokes on each data form item generation.
        /// </summary>
        /// <param name="sender">The data form.</param>
        /// <param name="e">The event arguments.</param> 
        private void OnGenerateDataFormItem(object? sender, GenerateDataFormItemEventArgs e)
        {
            try
            {
                if (e.DataFormItem != null)
                {
                    if (e.DataFormItem.FieldName == nameof(EventRegistrationModel.Mobile) && e.DataFormItem is DataFormMaskedTextItem mobile)
                    {
                        mobile.Mask = "###-###-####";
                        mobile.Keyboard = Keyboard.Numeric;
                    }
                    if (e.DataFormItem.FieldName == nameof(EventRegistrationModel.Address) && e.DataFormItem is DataFormAutoCompleteItem address)
                    {
                        address.MaxDropDownHeight = 300;                      
                        address.PlaceholderText = "Type an Address";
                        address.TextSearchMode = DataFormTextSearchMode.Contains;
                     
                    }                  
                    else if ((e.DataFormItem.FieldName == nameof(EventRegistrationModel.State)) && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
                    {
                        comboBoxItem.MaxDropDownHeight = 200;
                        comboBoxItem.TextSearchMode = DataFormTextSearchMode.StartsWith;
                       


                    }
                    else if ((e.DataFormItem.FieldName == nameof(EventRegistrationModel.EventDate) || e.DataFormItem.FieldName == nameof(EventRegistrationModel.EventEndDate)) && e.DataFormItem is DataFormDateItem dateItem)
                    {
                        dateItem.MinimumDisplayDate = DateTime.Now.Date;
                    }
                    else if (e.DataFormItem.FieldName == nameof(EventRegistrationModel.Zip) && e.DataFormItem is DataFormMaskedTextItem zipCode)
                    {
                        zipCode.Mask = "#####-####";
                        zipCode.Keyboard = Keyboard.Numeric;
                    }
                }
            }
            catch (Exception ex)
            {

                App.Current!.MainPage!.DisplayAlert("Loading Error", "There was an error loading the page" + ex.Message, "OK");
            }
        }

        /// <inheritdoc/>
        /// 

   
        protected override void OnDetachingFrom(SampleView grid)
        {
            try
            {
                base.OnDetachingFrom(grid);
                if (this.dataForm != null)
                {
                    this.dataForm.GenerateDataFormItem -= OnGenerateDataFormItem;
                }

                if (this.clearButton != null)
                {
                    this.clearButton.Clicked -= OnClearButtonClicked;
                }

                if (this.submitButton != null)
                {
                    this.submitButton.Clicked -= OnSubmitButtonClicked;
                }
            }
            catch (Exception ex)
            {

                App.Current!.MainPage!.DisplayAlert("Loading Error", "There was an error detaching the page" + ex.Message, "OK");
            }
        }
    }
}