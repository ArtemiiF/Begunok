using BegunokApp.Models;
using BegunokApp.Droid.Services;
using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using BegunokApp.DB;
using BegunokApp.ViewModel;

namespace BegunokApp.Droid.Models
{
    public class VisualBegunok : Begunok, IBegunok
    {
        public event BegunokHandler BegunokNotify;
        public VisualBegunok() : base()
        {
            System.Diagnostics.Debug.WriteLine("*\n*\n*\nBegunok Created\n*\n*\n*\n");
            Activities = new ObservableCollection<IActivity>();
            MessagingCenter.Subscribe<BegunokTimerService>(this, "TimerUpdate", MessageHandler);
            SetStartPositionOfBegunokVizualization();

            if (AndroidServiceHandler.IsRunning)
            {
                System.Diagnostics.Debug.WriteLine($"Load from DB");
                SetBegunokData();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"First Load");
                ActivityCount = 0;
            }
        }

        ~VisualBegunok()
        {

        }

        private void MessageHandler(BegunokTimerService obj)
        {
            SetBegunokData();
            BegunokNotify?.Invoke("TimerUpdate");
        }

        //Здесь запускается foregroundservice
        public void StartBegunok()
        {         
            AndroidServiceHandler.StartService<BegunokTimerService>(Android.App.Application.Context);
        }

        private void SetBegunokData()
        {
            SetBegunokDataFromDatabase();
            System.Diagnostics.Debug.WriteLine("posOfBegunokVizualization:" + Xamarin.Essentials.Preferences.Get("posOfBegunokVizualization", 0));
        }

        private void SetStartPositionOfBegunokVizualization()
        {
            if (AndroidServiceHandler.IsRunning)
            {
                return;
            }

            Xamarin.Essentials.DisplayInfo screenInfo = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;
            //App.Current.Properties["posOfBegunokVizualization"] = Convert.ToInt32(screenInfo.Width / screenInfo.Density - 0.5f) / 2;
            Xamarin.Essentials.Preferences.Set("posOfBegunokVizualization", Convert.ToInt32(screenInfo.Width / screenInfo.Density - 0.5f) / 2);
        }

        public override void AddActivity(string activityName, TimeSpan activityTime, Color activityColor)
        {
            base.AddActivity(activityName, activityTime, activityColor);
            App.Database.SaveItem(new BegunokDB(
                new Activity(activityName, activityTime, activityColor)));

            BegunokNotify?.Invoke("AddActivity");
        }

        public override void DeleteActivity(int id)
        {
            if (IsRunning)
            {
                App.Current.MainPage.DisplayAlert("Warning", "You can't delete activity while begunok is running", "Ok");
                return;
            }

            base.DeleteActivity(id);
            
            int indexOfDeleteitemDB = App.Database.DeleteItem(id);
          
            BegunokNotify?.Invoke("AddActivity");
            System.Diagnostics.Debug.WriteLine($"{Activities[id].Name} is deleted. Also id:{indexOfDeleteitemDB} of DB");
            return;
        }

        public override void ClearBegunok()
        {
            base.ClearBegunok();
            App.Database.RefreshTable();    
            AndroidServiceHandler.StopService<BegunokTimerService>(Android.App.Application.Context);
            SetStartPositionOfBegunokVizualization();
            System.Diagnostics.Debug.WriteLine("Clear begunok");
        }
    }
}