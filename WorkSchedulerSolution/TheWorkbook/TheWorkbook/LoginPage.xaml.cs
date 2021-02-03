using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Helper;
using TheWorkbook.Model;
using Xamarin.Forms;

namespace TheWorkbook
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        public DataTable returnedSelectTable { get; set; } = new DataTable { };

        #region constructors and onappearing
        public LoginPage()
        {
            InitializeComponent();

            var assemble = typeof(LoginPage);

            //Calls images for application. 
            headerImage.Source = ImageSource.FromResource("TheWorkbook.Assets.Images.headerbackground.png", assemble);
            curvedMask.Source = ImageSource.FromResource("TheWorkbook.Assets.Images.curvedmask.png", assemble);
        }

        protected override void OnAppearing()
        {
            //right before displaying the page the user information stored from the registration is passed to the text boxes.
            //should put them into a sqlite db to have them reappear and maybe auto-login after the first login. Would need a logout button. 
            base.OnAppearing();
            if (App.user != null)
            {
                userName.Text = App.user.WorkerUserName;
                userPass.Text = App.user.WorkerPassword;
            }
        }
        #endregion

        #region navigation buttons
        public async void LoginButton_Pressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AnimationPage(userName.Text, userPass.Text));
        }

        //2 pages where you can register. One registers as a boss and one as an employee.
        private void newEmployeeButton_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterEmployeePage());
        }

        private void registerBusinessButton_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
    #endregion
}
