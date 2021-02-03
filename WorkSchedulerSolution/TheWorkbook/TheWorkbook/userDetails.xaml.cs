using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Helper;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class userDetails : ContentPage
    {
        #region constructor and onappearing
        public userDetails()
        {
            InitializeComponent();
            if (App.boss != null)
            {
                companyName.Text = App.boss.CompanyName;
                companyName.IsReadOnly = true;
                companyName.TextColor = Color.Gray;
            }
            if (!string.IsNullOrEmpty(App.user.CompanyName))
            {
                companyName.Text = App.user.CompanyName;
                titleLabel.Text = "Profile Page";
            }
            if (!string.IsNullOrEmpty(App.user.WorkerLastName))
            {
                lastName.Text = App.user.WorkerLastName;
            }
            if (!string.IsNullOrEmpty(App.user.WorkerPhone))
            {
                phoneNumber.Text = App.user.WorkerPhone;
            }
            if (!string.IsNullOrEmpty(App.user.StreetAddress))
            {
                streetAddress.Text = App.user.StreetAddress;
            }
            if (!string.IsNullOrEmpty(App.user.CityAddress))
            {
                cityAddress.Text = App.user.CityAddress;
            }
            if (!string.IsNullOrEmpty(App.user.StateAddress))
            {
                stateAddress.Text = App.user.StateAddress;
            }
            if (!string.IsNullOrEmpty(App.user.ZipCode))
            {
                zipCode.Text = App.user.ZipCode;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (string.IsNullOrEmpty(App.user.CompanyName))
            {
                await Application.Current.MainPage.DisplayAlert("Welcome", "Thank you for using TimeTrimmer. Please complete these additional details before you use this application.", "OK");
            }
            
        }
        #endregion

        #region function button
        private async void saveInfoButton_Pressed(object sender, EventArgs e)
        {
            if (FormModel.IsPhoneNumber(phoneNumber.Text))
            {
                if (companyName.Text.Length > 49)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Company Name is too long", "Ok");
                }
                else
                {
                    if (!(string.IsNullOrEmpty(companyName.Text) || string.IsNullOrEmpty(streetAddress.Text) ||
                    string.IsNullOrEmpty(stateAddress.Text) || string.IsNullOrEmpty(zipCode.Text) ||
                    string.IsNullOrEmpty(lastName.Text)))
                    {
                        try
                        {
                            App.user.CompanyName = companyName.Text;
                            App.user.WorkerPhone = phoneNumber.Text;
                            App.user.StreetAddress = streetAddress.Text;
                            App.user.CityAddress = cityAddress.Text;
                            App.user.StateAddress = stateAddress.Text;
                            App.user.ZipCode = zipCode.Text;
                            App.user.WorkerLastName = lastName.Text;
                            await App.mobileService.GetTable<Worker>().UpdateAsync(App.user);

                            if (App.boss == null)
                            {
                                await Navigation.PushAsync(new OwnerPages.OwnerSplashPage());
                            }
                            else if (App.user.IsSupervisor.Equals(true))
                            {
                                App.user.CompanyName = App.boss.CompanyName;
                                await Navigation.PushAsync(new SupervisorPages.SupervisorSplashPage());
                            }
                            else
                            {
                                App.user.CompanyName = App.boss.CompanyName;
                                await Navigation.PushAsync(new WorkerPages.WorkerSplashPage());
                            }
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Please fill all fields.", "OK");
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please use valid phone number.", "OK");
            }
        }
        #endregion
    }
}