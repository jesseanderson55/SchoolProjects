using Syncfusion.XForms.Chat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.WorkerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoticePage : ContentPage
    {
        #region Properties
        public ObservableCollection<object> Messages = new ObservableCollection<object>() { };
        Author currentUser = new Author();
        bool ToSupervisor = false;
        #endregion

        #region Constructor, on appearing, populator
        public NoticePage()
        {
            InitializeComponent();

            Title = "Notices";

            currentUser.Name = App.user.WorkerLastName;

            sfChat.CurrentUser = currentUser;
            sfChat.MessageShape = MessageShape.DualTearDrop;
            sfChat.ShowOutgoingMessageAvatar = false;
            sfChat.ShowIncomingMessageAvatar = false;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await RetreiveMessages(ToSupervisor);
        }

        private async System.Threading.Tasks.Task RetreiveMessages(bool forSupervisor)
        {
            await FormModel.MessagingAzureDatabaseQuery();
            var messageList = sfChat.Messages.ToList();

            if (App.MessageList.Count > 0)
            {
                foreach (Message messageItem in App.MessageList)
                {
                    Worker tempSender;
                    if (messageItem.SenderID == App.boss.ID)
                    {
                        tempSender = App.boss;
                    }
                    else
                    {
                        tempSender = App.WorkerList.Where(u => u.ID == messageItem.SenderID).FirstOrDefault();
                    }
                    TextMessage textMessage = new TextMessage();

                    //create a textmessage and populate it with the message info from app.messagelist
                    if (messageItem.SenderID == App.user.ID)
                    {
                        textMessage.Author = sfChat.CurrentUser;
                        textMessage.Text = messageItem.TextMessage;
                        textMessage.DateTime = messageItem.DateTimeSent;
                    }
                    else
                    {
                        textMessage.Author = new Author() { Name = tempSender.WorkerLastName };
                        textMessage.Text = messageItem.TextMessage;
                        textMessage.DateTime = messageItem.DateTimeSent;
                    }

                    bool itemAlreadyAdded = false;

                    bool linqCheckOfOCMessageLists = Messages.Where(u => (u as TextMessage).DateTime == textMessage.DateTime).Any();
                    if (linqCheckOfOCMessageLists == true)
                    {
                        itemAlreadyAdded = true;
                    }

                    //checks the list in messaging to see if this message already exists
                    foreach (var item in messageList)
                    {
                        if ((item as TextMessage).DateTime == textMessage.DateTime)
                        {
                            itemAlreadyAdded = true;
                        }
                    }

                    //if it already exists dont add it to a list
                    if (!itemAlreadyAdded)
                    {
                        if (messageItem.ToSupervisors != true)
                        {
                            Messages.Add(textMessage);
                        }
                    }
                }
                sfChat.Messages = Messages;
            }
        }
        #endregion

        #region Functional Buttons
        private async void Refresh_Clicked(object sender, EventArgs e)
        {
            await RetreiveMessages(ToSupervisor);
        }

        private async void sfChat_SendMessage(object sender, SendMessageEventArgs e)
        {
            e.Handled = true;

            Message message = new Message()
            {
                OwnerID = App.boss.ID,
                SenderID = App.user.ID,
                DateTimeSent = e.Message.DateTime,
                TextMessage = e.Message.Text,
                ToSupervisors = false
            };

            try
            {
                await App.mobileService.GetTable<Message>().InsertAsync(message);
                await RetreiveMessages(ToSupervisor);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
            }
        }
        #endregion
    }
}