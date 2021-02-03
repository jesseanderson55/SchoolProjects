using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.OwnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobAllPage : ContentPage
    {
        CustomerJob jobselected = new CustomerJob();

        #region constructor, on appearing, populator
        public JobAllPage()
        {
            InitializeComponent();
            if (App.Jobs.Count != 0)
            {
                PopulateViews();
            }

            this.Title = "All Jobs";

            if (App.Customers.Count == 0)
            {
                newJobButton.IsEnabled = false;
                newJobButton.IsVisible = false;

                deleteJobButton.IsEnabled = false;
                deleteJobButton.IsVisible = false;

            }
            else
            {
                newJobButton.IsEnabled = true;
                newJobButton.IsVisible = true;

                deleteJobButton.IsEnabled = true;
                deleteJobButton.IsVisible = true;
                listOfCustomers.ItemsSource = App.Customers;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await FormModel.JobsCustomersAzureDatabaseQuery();

        }

        private void PopulateViews()
        {
            AllJobsListView.ItemsSource = null;

            AllJobsListView.ItemsSource = App.CustomerJobs;

            AllJobsListView.RefreshView();
        }
        #endregion
        
        #region navigation buttons
        private void newCustomerButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.JobCustomerPage());
        }

        private void newCustomer_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.JobCustomerPage());
        }

        private void NoticesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.NoticePage());
        }
        #endregion

        #region overlay buttons
        private void newJobButton_Clicked(object sender, EventArgs e)
        {
            overlay.IsEnabled = true;
            overlay.IsVisible = true;
            overlayNewJob.IsVisible = true;
            background.IsEnabled = false;
            overlayNewJob.IsEnabled = true;
        }

        private void cancelNewJob_Clicked(object sender, EventArgs e)
        {
            listOfCustomers.SelectedItem = null;
            listOfCustomers.IsEnabled = true;
            overlay.IsEnabled = false;
            overlay.IsVisible = false;
            overlayNewJob.IsVisible = false;
            background.IsEnabled = true;
            overlayNewJob.IsEnabled = false;

            cityAddressEntry.Text = string.Empty;
            jobDiscountEntry.Text = string.Empty;
            jobNameEntry.Text = string.Empty;
            stateAddressEntry.Text = string.Empty;
            streetAddressEntry.Text = string.Empty;
            zipAddressEntry.Text = string.Empty;
        }
        #endregion

        #region Functional Buttons
        private async void deleteJobButton_Pressed(object sender, EventArgs e)
        {
            if (jobselected != null)
            {
                var tempJobFromSelected = App.Jobs.Where(u => u.ID == jobselected.JobID).FirstOrDefault();
                await App.mobileService.GetTable<Job>().DeleteAsync(tempJobFromSelected);
                App.Jobs.Remove(tempJobFromSelected);
                await FormModel.JobsCustomersAzureDatabaseQuery();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select a job to delete.", "OK");
            }

            PopulateViews();
        }

        private async void saveNewJob_Clicked(object sender, EventArgs e)
        {
            bool validEntry = true;

            if (listOfCustomers.SelectedItem == null)
            {
                validEntry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please select a customer.", "OK");
            }

            if (string.IsNullOrEmpty(jobNameEntry.Text))
            {
                validEntry = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter street.", "OK");
            }

            if (string.IsNullOrEmpty(streetAddressEntry.Text))
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

            int discountReturned = 0;
            if (!string.IsNullOrEmpty(jobDiscountEntry.Text))
            {
                if (int.TryParse(jobDiscountEntry.Text, out int result))
                {
                    discountReturned = result;
                }
                else
                {
                    validEntry = false;
                }
            }


            if (validEntry)
            {
                Job job = new Job()
                {
                    JobDiscount = discountReturned,
                    JobName = jobNameEntry.Text,
                    JobStreetAddress = streetAddressEntry.Text,
                    JobCity = cityAddressEntry.Text,
                    JobState = stateAddressEntry.Text,
                    JobZipCode = zipAddressEntry.Text,
                    CustomerID = (listOfCustomers.SelectedItem as Customer).ID
                };

                try
                {
                    Job existsAlready = new Job();
                    if (App.Jobs != null)
                    {
                        existsAlready = App.Jobs.Where(u => u.JobName == job.JobName).FirstOrDefault();
                    }

                    if (existsAlready != null)
                    {
                        job.ID = existsAlready.ID;
                        await App.mobileService.GetTable<Job>().UpdateAsync(job);
                        await FormModel.JobsCustomersAzureDatabaseQuery();

                        await Application.Current.MainPage.DisplayAlert("Success", $"{job.JobName} updated.", "OK");
                    }
                    else
                    {
                        await App.mobileService.GetTable<Job>().InsertAsync(job);
                        await FormModel.JobsCustomersAzureDatabaseQuery();

                        await Application.Current.MainPage.DisplayAlert("Success", $"{job.JobName} saved.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
                }

                if (validEntry == false)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Save failed", "OK");
                }
                else
                {
                    cityAddressEntry.Text = string.Empty;
                    jobDiscountEntry.Text = string.Empty;
                    jobNameEntry.Text = string.Empty;
                    stateAddressEntry.Text = string.Empty;
                    streetAddressEntry.Text = string.Empty;
                    zipAddressEntry.Text = string.Empty;
                    listOfCustomers.SelectedItem = null;
                    listOfCustomers.IsEnabled = true;

                    overlay.IsEnabled = false;
                    overlay.IsVisible = false;
                    overlayNewJob.IsVisible = false;
                    background.IsEnabled = true;
                    overlayNewJob.IsEnabled = false;
                }
                PopulateViews();
            }
        }

        private void AllJobsListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            jobselected = e.ItemData as CustomerJob;
        }

        private void scheduleButton_Pressed(object sender, EventArgs e)
        {
            if (jobselected.ID != null)
            {
                var tempJobFromSelected = App.Jobs.Where(u => u.ID == jobselected.JobID).FirstOrDefault();
                Navigation.PushAsync(new OwnerPages.JobSchedulePage(tempJobFromSelected));
            }
        }

        private async void editJobButton_Clicked(object sender, EventArgs e)
        {

            if (jobselected != null)
            {
                var tempJobFromSelected = App.Jobs.Where(u => u.ID == jobselected.JobID).FirstOrDefault();

                overlay.IsEnabled = true;
                overlay.IsVisible = true;
                overlayNewJob.IsVisible = true;
                background.IsEnabled = false;
                overlayNewJob.IsEnabled = true;

                if (tempJobFromSelected.JobDiscount > 0)
                {
                    jobDiscountEntry.Text = tempJobFromSelected.JobDiscount.ToString();
                }
                jobNameEntry.Text = tempJobFromSelected.JobName;
                streetAddressEntry.Text = tempJobFromSelected.JobStreetAddress;
                cityAddressEntry.Text = tempJobFromSelected.JobCity;
                stateAddressEntry.Text = tempJobFromSelected.JobState;
                zipAddressEntry.Text = tempJobFromSelected.JobZipCode;
                

                App.Customers = await App.mobileService.GetTable<Customer>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                listOfCustomers.ItemsSource = App.Customers;
                var tempCustomerForSelection = App.Customers.Where(u => u.ID == tempJobFromSelected.CustomerID).FirstOrDefault();
                listOfCustomers.SelectedItem = tempCustomerForSelection;
                listOfCustomers.IsEnabled = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Select a job to edit.", "OK");
            }
        }

        private void customerAddressInfoCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (listOfCustomers.SelectedItem != null)
            {
                if (e.Value == true)
                {
                    streetAddressEntry.Text = (listOfCustomers.SelectedItem as Customer).CustomerStreetAddress;
                    cityAddressEntry.Text = (listOfCustomers.SelectedItem as Customer).CustomerCity;
                    stateAddressEntry.Text = (listOfCustomers.SelectedItem as Customer).CustomerState;
                    zipAddressEntry.Text = (listOfCustomers.SelectedItem as Customer).CustomerZipCode;
                }
                else
                {
                    streetAddressEntry.Text = string.Empty;
                    cityAddressEntry.Text = string.Empty;
                    stateAddressEntry.Text = string.Empty;
                    zipAddressEntry.Text = string.Empty;
                }
            }
        }
        #endregion
    }
}