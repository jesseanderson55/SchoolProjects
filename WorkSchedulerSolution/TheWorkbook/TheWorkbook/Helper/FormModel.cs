using SQLite;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;

namespace TheWorkbook
{
    public static class FormModel
    {

        #region field validators
        //uses .net mail.mailaddress method to check and see if an email is good. 
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //using regex, checks the phone number for US correct format
        public static bool IsPhoneNumber(string number)
        {
            //This regex number is too much and terribly confusing. It accounts for international numbers also which isnt necessary. Could delete but keeping in case we do need it later.
            //return Regex.Match(number, @"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$").Success;
            if (number != null)
            {
                return Regex.Match(number, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").Success;
            }
            return false;
        }
        #endregion

        #region Query for the azureDB, also writes to SQLite
        //method to run whenever repopulating from the azure database is necessary
        public async static System.Threading.Tasks.Task FullAzureDatabaseQuery()
        {
            try
            {
                //pulls from the azure db if the user has a boss or if it doesnt. Grabs the whole worker list and the splashpage view set-up in azure
                if (App.boss != null)
                {
                    App.WorkerList = await App.mobileService.GetTable<Worker>().Where(u => u.EmployerEmail == App.boss.WorkerEmail).ToListAsync();
                    App.Tools = await App.mobileService.GetTable<Tool>().Where(u => u.EmployeeID == App.boss.ID).ToListAsync();
                    App.Customers = await App.mobileService.GetTable<Customer>().Where(u => u.EmployeeID == App.boss.ID).ToListAsync();
                    App.Tasks = await App.mobileService.GetTable<Model.Task>().Where(u => u.EmployeeID == App.boss.ID).ToListAsync();
                    App.Requests = await App.mobileService.GetTable<Request>().Where(u => u.OwnerID == App.boss.ID).ToListAsync();
                    App.MessageList = await App.mobileService.GetTable<Message>().Where(u => u.OwnerID == App.boss.ID).ToListAsync();
                    App.timeClockRecords = await App.mobileService.GetTable<TimeClockRecord>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();

                }
                else
                {
                    App.WorkerList = await App.mobileService.GetTable<Worker>().Where(u => u.EmployerEmail == App.user.WorkerEmail).ToListAsync();
                    App.Tools = await App.mobileService.GetTable<Tool>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                    App.Customers = await App.mobileService.GetTable<Customer>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                    App.Tasks = await App.mobileService.GetTable<Model.Task>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                    App.Requests = await App.mobileService.GetTable<Request>().Where(u => u.OwnerID == App.user.ID).ToListAsync();
                    App.MessageList = await App.mobileService.GetTable<Message>().Where(u => u.OwnerID == App.user.ID).ToListAsync();
                    App.timeClockRecords = await App.mobileService.GetTable<TimeClockRecord>().Where(u => u.OwnerID == App.user.ID).ToListAsync();

                }

                //populates jobfunctions based on the tasks associated with the company owner
                if (App.Tasks.Count > 0)
                {
                    App.JobFunctions.Clear();

                    foreach (Model.Task item in App.Tasks)
                    {
                        var tempJobFunctionsList = await App.mobileService.GetTable<JobFunction>().Where(u => u.TaskID == item.ID).ToListAsync();
                        App.JobFunctions.AddRange(tempJobFunctionsList);
                    }
                }

                if (App.Tools.Count > 0)
                {
                    App.Labors.Clear();

                    foreach (Tool item in App.Tools)
                    {
                        var tempLaborList = await App.mobileService.GetTable<Labor>().Where(u => u.ToolID == item.ID).ToListAsync();
                        App.Labors.AddRange(tempLaborList);
                    }
                }

                //populates jobs and notes based on the customers associated with the company owner
                if (App.Customers.Count > 0)
                {
                    App.Jobs.Clear();

                    foreach (Customer item in App.Customers)
                    {
                        var tempJobTable = await App.mobileService.GetTable<Job>().Where(u => u.CustomerID == item.ID).ToListAsync();
                        App.Jobs.AddRange(tempJobTable);
                    }


                    foreach (Job item in App.Jobs)
                    {
                        var tempNoteTable = await App.mobileService.GetTable<Note>().Where(u => u.JobID == item.ID).ToListAsync();
                        App.Notes.AddRange(tempNoteTable);

                    }

                    //creates a joined subclass list for a listview
                    if (App.Jobs.Count > 0)
                    {
                        App.CustomerJobs = App.Customers.Join(App.Jobs,
                            cu => cu.ID,
                            jo => jo.CustomerID,
                            (Customer, Job) => new CustomerJob()
                            {
                                ID = Customer.ID,
                                CustomerName = Customer.CustomerName,
                                JobName = Job.JobName,
                                JobID = Job.ID

                            }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok");
                // App.online = false;
            }


            //it then takes that information and writes it into the sqlite worker db on the device in case of no connectivity. 
            //I found some answers to shortening this using generic methods and interfaces but I wasnt able to figure it out
            #region Write to the SQLite DB what was downloaded
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {

                conn.CreateTable<Worker>();

                if (App.WorkerList != null)
                {
                    foreach (Worker item in App.WorkerList)
                    {
                        var workerMatch = conn.Table<Worker>().Where(u => u.ID == item.ID).Any();

                        if (workerMatch)
                        {
                            conn.Update(item);
                        }
                        else
                        {
                            conn.Insert(item);
                        }

                    }
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Labor>();

                if (App.Labors != null)
                {
                    foreach (var item in App.Labors)
                    {
                        var match = conn.Table<Labor>().Where(u => u.ID == item.ID).Any();

                        if (match)
                        {
                            conn.Update(item);
                        }
                        else
                        {
                            conn.Insert(item);
                        }
                    }
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Tool>();

                if (App.Tools != null)
                {
                    foreach (var item in App.Tools)
                    {
                        var match = conn.Table<Tool>().Where(u => u.ID == item.ID).Any();

                        if (match)
                        {
                            conn.Update(item);
                        }
                        else
                        {
                            conn.Insert(item);
                        }

                    }
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Customer>();

                if (App.Customers != null)
                {
                    foreach (var item in App.Customers)
                    {
                        var match = conn.Table<Customer>().Where(u => u.ID == item.ID).Any();

                        if (match)
                        {
                            conn.Update(item);
                        }
                        else
                        {
                            conn.Insert(item);
                        }
                    }
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Model.Task>();

                if (App.Tasks != null)
                {
                    foreach (var item in App.Tasks)
                    {
                        var match = conn.Table<Model.Task>().Where(u => u.ID == item.ID).Any();

                        if (match)
                        {
                            conn.Update(item);
                        }
                        else
                        {
                            conn.Insert(item);
                        }
                    }
                }
            }
            #endregion
        }

        public async static System.Threading.Tasks.Task LaborAndTasksAzureDatabaseQuery()
        {
            try
            {
                //pulls from the azure db if the user has a boss or if it doesnt. Grabs the whole worker list and the splashpage view set-up in azure
                if (App.boss != null)
                {
                    App.Tools = await App.mobileService.GetTable<Tool>().Where(u => u.EmployeeID == App.boss.ID).ToListAsync();
                    App.Tasks = await App.mobileService.GetTable<Model.Task>().Where(u => u.EmployeeID == App.boss.ID).ToListAsync();
                }
                else
                {
                    App.Tools = await App.mobileService.GetTable<Tool>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                    App.Tasks = await App.mobileService.GetTable<Model.Task>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                }

                //populates jobfunctions based on the tasks associated with the company owner
                if (App.Tasks.Count > 0)
                {
                    App.JobFunctions.Clear();

                    foreach (Model.Task item in App.Tasks)
                    {
                        var tempJobFunctionsList = await App.mobileService.GetTable<JobFunction>().Where(u => u.TaskID == item.ID).ToListAsync();
                        App.JobFunctions.AddRange(tempJobFunctionsList);
                    }
                }

                if (App.Tools.Count > 0)
                {
                    App.Labors.Clear();

                    foreach (Tool item in App.Tools)
                    {
                        var tempLaborList = await App.mobileService.GetTable<Labor>().Where(u => u.ToolID == item.ID).ToListAsync();
                        App.Labors.AddRange(tempLaborList);
                    }
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok");
                // App.online = false;
            }
        }


        public async static System.Threading.Tasks.Task JobsCustomersAzureDatabaseQuery()
        {
            try
            {
                //pulls from the azure db if the user has a boss or if it doesnt. Grabs the whole worker list and the splashpage view set-up in azure
                if (App.boss != null)
                {
                    App.Customers = await App.mobileService.GetTable<Customer>().Where(u => u.EmployeeID == App.boss.ID).ToListAsync();
                }
                else
                {
                    App.Customers = await App.mobileService.GetTable<Customer>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                }



                //populates jobs and notes based on the customers associated with the company owner
                if (App.Customers.Count > 0)
                {
                    App.Jobs.Clear();

                    foreach (Customer item in App.Customers)
                    {
                        var tempJobTable = await App.mobileService.GetTable<Job>().Where(u => u.CustomerID == item.ID).ToListAsync();
                        App.Jobs.AddRange(tempJobTable);
                    }


                    foreach (Job item in App.Jobs)
                    {
                        var tempNoteTable = await App.mobileService.GetTable<Note>().Where(u => u.JobID == item.ID).ToListAsync();
                        App.Notes.AddRange(tempNoteTable);

                    }

                    //creates a joined subclass list for a listview
                    if (App.Jobs.Count > 0)
                    {
                        App.CustomerJobs = App.Customers.Join(App.Jobs,
                            cu => cu.ID,
                            jo => jo.CustomerID,
                            (Customer, Job) => new CustomerJob()
                            {
                                ID = Customer.ID,
                                CustomerName = Customer.CustomerName,
                                JobName = Job.JobName,
                                JobID = Job.ID

                            }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok");
                //App.online = false;
            }


            //it then takes that information and writes it into the sqlite worker db on the device in case of no connectivity. 
            //I found some answers to shortening this using generic methods and interfaces but I wasnt able to figure it out
            //setup for future offline capability
            #region Write to the SQLite DB what was downloaded
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Customer>();

                if (App.Customers != null)
                {
                    foreach (var item in App.Customers)
                    {
                        var match = conn.Table<Customer>().Where(u => u.ID == item.ID).Any();

                        if (match)
                        {
                            conn.Update(item);
                        }
                        else
                        {
                            conn.Insert(item);
                        }
                    }
                }
            }
            #endregion
        }

        public async static System.Threading.Tasks.Task TaskToolsAzureDatabaseQuery()
        {
            try
            {
                //pulls from the azure db if the user has a boss or if it doesnt. Grabs the whole worker list and the splashpage view set-up in azure
                if (App.boss != null)
                {
                    App.Tools = await App.mobileService.GetTable<Tool>().Where(u => u.EmployeeID == App.boss.ID).ToListAsync();
                    App.Tasks = await App.mobileService.GetTable<Model.Task>().Where(u => u.EmployeeID == App.boss.ID).ToListAsync();
                }
                else
                {
                    App.Tools = await App.mobileService.GetTable<Tool>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                    App.Tasks = await App.mobileService.GetTable<Model.Task>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok");
                //App.online = false;
            }


            //it then takes that information and writes it into the sqlite worker db on the device in case of no connectivity. 
            //I found some answers to shortening this using generic methods and interfaces but I wasnt able to figure it out
            #region Write to the SQLite DB what was downloaded
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Tool>();

                if (App.Tools != null)
                {
                    foreach (var item in App.Tools)
                    {
                        var match = conn.Table<Tool>().Where(u => u.ID == item.ID).Any();

                        if (match)
                        {
                            conn.Update(item);
                        }
                        else
                        {
                            conn.Insert(item);
                        }

                    }
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Model.Task>();

                if (App.Tasks != null)
                {
                    foreach (var item in App.Tasks)
                    {
                        var match = conn.Table<Model.Task>().Where(u => u.ID == item.ID).Any();

                        if (match)
                        {
                            conn.Update(item);
                        }
                        else
                        {
                            conn.Insert(item);
                        }
                    }
                }
            }
            #endregion
        }

        public async static System.Threading.Tasks.Task MessagingAzureDatabaseQuery()
        {
            try
            {
                //pulls from the azure db if the user has a boss or if it doesnt. Grabs the whole worker list and the splashpage view set-up in azure
                if (App.boss != null)
                {
                    App.MessageList = await App.mobileService.GetTable<Message>().Where(u => u.OwnerID == App.boss.ID).ToListAsync();
                }
                else
                {
                    App.MessageList = await App.mobileService.GetTable<Message>().Where(u => u.OwnerID == App.user.ID).ToListAsync();
                }
                App.MessageList.OrderBy(Message => Message.DateTimeSent);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok");
                //App.online = false;
            }
        }

        public async static System.Threading.Tasks.Task TimeClockAzureDatabaseQuery()
        {
            try
            {
                //pulls from the azure db if the user has a boss or if it doesnt. Grabs the whole worker list and the splashpage view set-up in azure
                if (App.boss != null)
                {
                    App.timeClockRecords = await App.mobileService.GetTable<TimeClockRecord>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                }
                else
                {
                    App.timeClockRecords = await App.mobileService.GetTable<TimeClockRecord>().Where(u => u.OwnerID == App.user.ID).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok");
            }
        }
        #endregion
    }
}
