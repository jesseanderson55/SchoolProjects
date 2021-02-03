using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Helper;
using TheWorkbook.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.OwnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeAddEditPage : ContentPage
    {
        Worker selectedWorker;
        bool addNewRecordPage;

        public EmployeeAddEditPage()
        {
            InitializeComponent();
            deleteEmployeeButton.IsVisible = false;
            deleteEmployeeButton.IsEnabled = false;
            selectedWorker = null;
            addNewRecordPage = true;
            this.Title = "Add Employee";
        }

        public EmployeeAddEditPage(Worker itemSelected)
        {
            InitializeComponent();
            selectedWorker = itemSelected;
            deleteEmployeeButton.IsVisible = true;
            deleteEmployeeButton.IsEnabled = true;
            addNewRecordPage = false;
            if (itemSelected != null)
            {
                employeeName.Text = itemSelected.WorkerLastName;
                employeeEmail.Text = itemSelected.WorkerEmail;
                employeePhone.Text = itemSelected.WorkerPhone;
                employeeHourly.Text = itemSelected.WorkerHourly.ToString();
                employeeAddress.Text = itemSelected.StreetAddress;
                employeeCity.Text = itemSelected.CityAddress;
                employeeState.Text = itemSelected.StateAddress;
                employeeZip.Text = itemSelected.ZipCode;
                checkBox.IsChecked = itemSelected.IsSupervisor;
            }
            this.Title = "Update Employee";
        }


        private async void deleteEmployeeButton_Pressed(object sender, EventArgs e)
        {
            try
            {
                await App.mobileService.GetTable<Worker>().DeleteAsync(selectedWorker);
                await FormModel.FullAzureDatabaseQuery();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
            }

            await Navigation.PushAsync(new EmployeePage());
        }

        private async void saveEmployeeButton_Pressed(object sender, EventArgs e)
        {
            bool validentry = true;

            
            if (!FormModel.IsPhoneNumber(employeePhone.Text))
            {
                validentry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter valid phone number", "OK");
            }
            if (!FormModel.IsValidEmail(employeeEmail.Text))
            {
                validentry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter valid email address", "OK");
            }
            if (!int.TryParse(employeeHourly.Text, out int output))
            {
                validentry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter valid hourly wage", "OK");
            }
            if (employeeZip.Text.Length < 5 || employeeZip.Text.Length > 9 || !int.TryParse(employeeZip.Text, out int output2))
            {
                validentry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter valid zip with no dashes or spaces", "OK");
            }

            if (selectedWorker != null && validentry == true)
            {
                try
                {
                    selectedWorker.WorkerLastName = employeeName.Text;
                    selectedWorker.WorkerEmail = employeeEmail.Text;
                    selectedWorker.WorkerPhone = employeePhone.Text;
                    selectedWorker.WorkerHourly = int.Parse(employeeHourly.Text);
                    selectedWorker.StreetAddress = employeeAddress.Text;
                    selectedWorker.CityAddress = employeeCity.Text;
                    selectedWorker.StateAddress = employeeState.Text;
                    selectedWorker.ZipCode = employeeZip.Text;
                    selectedWorker.IsSupervisor = (bool)checkBox.IsChecked;
                    if (addNewRecordPage)
                    {
                        await App.mobileService.GetTable<Worker>().InsertAsync(selectedWorker);
                        await FormModel.FullAzureDatabaseQuery();

                        await Application.Current.MainPage.DisplayAlert("Success", $"{selectedWorker.WorkerLastName} inserted.", "OK");
                    }
                    else
                    {
                        await App.mobileService.GetTable<Worker>().UpdateAsync(selectedWorker);
                        await FormModel.FullAzureDatabaseQuery();

                        await Application.Current.MainPage.DisplayAlert("Success", $"{selectedWorker.WorkerLastName} updated.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
                }

                await Navigation.PushAsync(new EmployeePage());

            }
        }


        private void crewButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.CrewPage());
        }

        private async void callButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open(selectedWorker.WorkerPhone);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
                await Application.Current.MainPage.DisplayAlert("Error", $"Phone Number not found. {anEx}", "OK");

            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                await Application.Current.MainPage.DisplayAlert("Error", $"Calling not supported. {ex}", "OK");

            }
            catch (Exception ex)
            {
                // Other error has occurred.
                await Application.Current.MainPage.DisplayAlert("Error", $"Unexpected Error. {ex}", "OK");
            }
        }
    }
}