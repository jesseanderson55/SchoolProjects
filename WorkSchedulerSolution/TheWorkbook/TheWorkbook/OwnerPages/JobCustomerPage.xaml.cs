using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.OwnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobCustomerPage : ContentPage
    {
        Customer selectedCustomer = new Customer();

        #region Constructor
        public JobCustomerPage()
        {
            InitializeComponent();
            Title = "Customer";
            CustomerListView.ItemsSource = App.Customers;
        }
        #endregion

        #region Functional Button
        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            bool validEntry = true;

            Customer newCustomer = new Customer();


            if (string.IsNullOrEmpty(customerNameEntry.Text))
            {
                validEntry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter customer name.", "OK");
            }


            if (!FormModel.IsPhoneNumber(customerPhoneEntry.Text))
            {
                validEntry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter valid phone number.", "OK");
            }
            if (customerPhone2Entry.Text != null)
            {
                if (!FormModel.IsPhoneNumber(customerPhone2Entry.Text))
                {
                    validEntry = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "Please enter valid 2nd phone number", "OK");
                }
            }
          
            if (!FormModel.IsValidEmail(customerEmailEntry.Text))
            {
                validEntry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter valid email address.", "OK");
            }

            if (string.IsNullOrEmpty(streetEntry.Text))
            {
                validEntry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter street.", "OK");
            }

            if (string.IsNullOrEmpty(cityAddressEntry.Text))
            {
                validEntry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter city.", "OK");
            }

            if (string.IsNullOrEmpty(stateAddressEntry.Text))
            {
                validEntry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter state.", "OK");
            }

            if (string.IsNullOrEmpty(zipAddressEntry.Text))
            {
                validEntry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter zipcode.", "OK");
            }

            if (validEntry)
            {
                try
                {
                    if (App.user != null)
                    {

                        CustomerListView.ItemsSource = null;

                        newCustomer.EmployeeID = App.user.ID;
                        newCustomer.CustomerName = customerNameEntry.Text;
                        newCustomer.CustomerPhone = customerPhoneEntry.Text;
                        newCustomer.CustomerPhone2 = customerPhone2Entry.Text;
                        newCustomer.CustomerEmail = customerEmailEntry.Text;
                        newCustomer.CustomerStreetAddress = streetEntry.Text;
                        newCustomer.CustomerCity = cityAddressEntry.Text;
                        newCustomer.CustomerState = stateAddressEntry.Text;
                        newCustomer.CustomerZipCode = zipAddressEntry.Text;

                        var existsAlready = App.Customers.Where(u => u.CustomerName == newCustomer.CustomerName).FirstOrDefault();

                        if (existsAlready != null)
                        {
                            newCustomer.ID = existsAlready.ID;
                            await App.mobileService.GetTable<Customer>().UpdateAsync(newCustomer);
                            await FormModel.JobsCustomersAzureDatabaseQuery();

                            await Application.Current.MainPage.DisplayAlert("Success", $"{newCustomer.CustomerName} updated.", "OK");
                        }
                        else
                        {
                            await App.mobileService.GetTable<Customer>().InsertAsync(newCustomer);
                            await FormModel.JobsCustomersAzureDatabaseQuery();

                            await Application.Current.MainPage.DisplayAlert("Success", $"{newCustomer.CustomerName} saved.", "OK");
                        }

                    }
                }
                catch (Exception ex)
                { 
                    await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
                }
                selectedCustomer = null;
                
            }
            CustomerListView.ItemsSource = App.Customers;

            CustomerListView.RefreshView();

        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (selectedCustomer != null)
                {
                    await App.mobileService.GetTable<Customer>().DeleteAsync(selectedCustomer);
                    await FormModel.JobsCustomersAzureDatabaseQuery();
                    await Application.Current.MainPage.DisplayAlert("Success", $"{selectedCustomer.CustomerName} deleted.", "OK");
                    App.Customers.Remove(selectedCustomer);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
            }

            CustomerListView.SelectedItem = null;


            CustomerListView.ItemsSource = App.Customers;

            CustomerListView.RefreshView();
        }

        private void CustomerListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            selectedCustomer = e.ItemData as Customer;

            customerNameEntry.Text = selectedCustomer.CustomerName;
            customerPhoneEntry.Text = selectedCustomer.CustomerPhone;
            customerPhone2Entry.Text = selectedCustomer.CustomerPhone2;
            customerEmailEntry.Text = selectedCustomer.CustomerEmail;
            streetEntry.Text = selectedCustomer.CustomerStreetAddress;
            cityAddressEntry.Text = selectedCustomer.CustomerCity;
            stateAddressEntry.Text = selectedCustomer.CustomerState;
            zipAddressEntry.Text = selectedCustomer.CustomerZipCode;
        }
        #endregion

        #region Navigation Buttons
        private void cancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void NoticesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.NoticePage());
        }
        #endregion
    }
}