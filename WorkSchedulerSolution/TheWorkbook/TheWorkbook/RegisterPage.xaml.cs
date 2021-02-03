
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Helper;
using TheWorkbook.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public DataTable returnedSelectTable { get; set; } = new DataTable { };

        #region
        public RegisterPage()
        {
            InitializeComponent();

            var assemble = typeof(LoginPage);

            headerImage.Source = ImageSource.FromResource("TheWorkbook.Assets.Images.headerbackground.png", assemble);
            curvedMask.Source = ImageSource.FromResource("TheWorkbook.Assets.Images.curvedmask.png", assemble);

        }
        #endregion

        #region function button
        private async void registerButton_Pressed(object sender, EventArgs e)
        {
            bool passwordMatches = false;
            bool validUserName = true;

            bool validEmail = false;


            try
            {
                if (userEmail.Text.ToString().Equals(userEmail2.Text.ToString()))
                {
                    if (!FormModel.IsValidEmail(userEmail.Text))
                    {
                        userEmail.TextColor = Color.Red;
                        userEmail2.TextColor = Color.Red;
                
                        await Application.Current.MainPage.DisplayAlert("Error", "Valid User Email Required", "OK");
                    }
                    else
                    {
                        validEmail = true;
                    }
                }
                else
                {
                    userEmail2.TextColor = Color.Red;
                    userEmail.TextColor = Color.Red;
                    await Application.Current.MainPage.DisplayAlert("Error", "Email must match", "OK");
                }

                if (userPass.Text.ToString().Equals(userPass2.Text.ToString()))
                {
                    if (userPass.Text.Length >= 8)
                    {
                        passwordMatches = true;
                    }
                    else
                    {
                        userPass2.TextColor = Color.Red;
                        userPass.TextColor = Color.Red;
                        await Application.Current.MainPage.DisplayAlert("Error", "User password must be more than 8 characters", "OK");
                    }
                }
                else
                {

                    userPass2.TextColor = Color.Red;
                    userPass.TextColor = Color.Red;
                    await Application.Current.MainPage.DisplayAlert("Error", "User password must match", "OK");

                }

                if (userName.Text.Length < 6)
                {
                    validUserName = false;
                    userName.TextColor = Color.Red;
                    await Application.Current.MainPage.DisplayAlert("Error", "User password must be more than 6 characters", "OK");

                }

                var user = (await App.mobileService.GetTable<Worker>().Where(u => u.WorkerUserName == userName.Text).ToListAsync()).FirstOrDefault();



                if (user != null)
                {
                    validUserName = false;
                    userName.TextColor = Color.Red;
                    await Application.Current.MainPage.DisplayAlert("Error", "Username taken", "OK");
                }
                


                if (passwordMatches && validUserName && validEmail)
                {
                    Worker worker = new Worker()
                    {
                        WorkerUserName = userName.Text,
                        WorkerPassword = userPass.Text,
                        WorkerEmail = userEmail.Text
                    };

                    await App.mobileService.GetTable<Worker>().InsertAsync(worker);

                    statusLabel.IsEnabled = true;
                    statusLabel.Text = "Registration succeeded";
                    App.user = worker;
                    await Application.Current.MainPage.DisplayAlert("Success", "Registration Succeeded", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    statusLabel.IsEnabled = true;
                    statusLabel.Text = "Registration failed";
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
            }

           

            //this.Content = registerPageContent;
        }
        #endregion
    }
}

