using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace MotionSerialTalker
{
    class AzureMobileServicesClient
    {
        // this is the constructor method (snippet is ctro) called when using "new"
        public AzureMobileServicesClient()
        {

        }
        public MobileServiceClient ConnectAzureMobileServicesClient()
        {
            MobileServiceClient client = new MobileServiceClient("https://solarcontrol.azure-mobile.net/", "ZBaAvirmKzAGFXrHmjfRHSyrdCVdek84");
            return client;
        }

        public async void UpdateAzureTable(string time, string id)
        {
            //// using a class and object instance
            //AzureMobileServicesClient azm = new AzureMobileServicesClient();
            //MobileServiceClient newclient = azm.ConnectAzureMobileServicesClient();
            //newclient.GetTable<TodoItem>();
            //azm.todoTable = newclient.GetTable<TodoItem>();

            //TodoItem newItem = new TodoItem();
            //newItem.Text = time;
            //newItem.Complete = false;
            //newItem.Id = id;
            //// update the record specified by unique id
            //try
            //{
            //    await azm.todoTable.UpdateAsync(newItem);
            //}

            //catch (Exception e)
            //{
            //    e.ToString();
            //}

            // using a class and object instance
            AzureMobileServicesClient azm = new AzureMobileServicesClient();
            MobileServiceClient newclient = azm.ConnectAzureMobileServicesClient();
            newclient.GetTable<TodoItem>();
            azm.todoTable = newclient.GetTable<TodoItem>();

            TodoItem newItem = new TodoItem();
            newItem.Text = time;
            newItem.Complete = false;
            newItem.Id = id;
            // update the record specified by unique id
            try
            {
                await azm.todoTable.UpdateAsync(newItem);
            }

            catch (Exception e)
            {
                e.ToString();
            }
        }

        // This is a FIELD initializer. For Objects that are an instance of this class 
        // fields will "remember" the values as long as the object is alive
        // public fields should start with CAPITAL letter: PublicField
        // private fields and local variables or parameters to a method should start with lower-case and underscore if you like: _privateField        

        public IMobileServiceTable<TodoItem> todoTable;

    }
}
