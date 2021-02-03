using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimationPage : ContentPage
    {
        string username;
        string userpassword;

        public AnimationPage(string userName, string userPassword)
        {
            InitializeComponent();

            SfBusyIndicator busyIndicator = new SfBusyIndicator()
            {
                AnimationType = AnimationTypes.Ball,
                ViewBoxWidth = 100,
                ViewBoxHeight = 100,
                Title = "Loading...",
                BackgroundColor = (Color)Application.Current.Resources["backgroundcolor"],
                TextColor = (Color)Application.Current.Resources["buttonscolor"]
            };

            username = userName;
            userpassword = userPassword;


            this.Content = busyIndicator;
            loginProcess();
        }

        public async void loginProcess()
        {
            try
            {
                //searches the azure database for the worker logging in and sends back his email and password to the phone. 
                //passwords are currently not encrypted. Functionality will be added later
                var user = (await App.mobileService.GetTable<Worker>().Where(u => u.WorkerUserName == username).ToListAsync()).FirstOrDefault();



                if (user != null)
                {
                    //checks the password
                    if (user.WorkerPassword == userpassword)
                    {
                        //sets the user and the boss for when pulling infomation later on
                        App.user = user;
                        App.boss = (await App.mobileService.GetTable<Worker>().Where(u => u.WorkerEmail == App.user.EmployerEmail).ToListAsync()).FirstOrDefault();

                        //grabs and sets the workerlist and splashpageview list in app.cs
                        
                        await FormModel.FullAzureDatabaseQuery();

                        if (string.IsNullOrEmpty(App.user.CompanyName))
                        {
                            //on first login it checks to see if information is filled out on the user
                            await Navigation.PushAsync(new userDetails());
                        }
                        else
                        {
                            //checks the users fields to see what type of worker they are (boss, supervisor, worker) and sends them to those page groups
                            if (App.boss == null)
                            {
                                await Navigation.PushAsync(new OwnerPages.OwnerSplashPage());
                            }
                            else if (user.IsSupervisor.Equals(true))
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
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Username/Password is wrong", "OK");
                        await Navigation.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Username/Password is wrong", "OK");
                    await Navigation.PopAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Oops no connection. {ex}", "OK");
            }
            catch (NullReferenceException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
            }
        }

    }
}