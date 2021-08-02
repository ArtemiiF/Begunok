﻿using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using YAssistant.Droid.IoC;
using Android.Views;
using System.Linq;
using YAssistant.View;
using System;

namespace YAssistant.Droid
{
    [Activity(Label = "YAssistant", Icon = "@mipmap/icon",
        Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation |
        ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new PlatformModule()));

            AndroidX.AppCompat.Widget.Toolbar toolbar =
            this.FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Console.WriteLine("BackMenu");
            if (item.ItemId == 16908332)
            {
                var page = Xamarin.Forms.Application.
                           Current.MainPage.Navigation.
                           NavigationStack.LastOrDefault();

                if (!(page is ICustomBackButton currentpage))
                {
                    return base.OnOptionsItemSelected(item);
                }
                if (currentpage?.CustomBackButtonAction != null)
                {
                    currentpage?.CustomBackButtonAction.Invoke();
                }
                return false;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnBackPressed()
        {
            Console.WriteLine("Back");
            var page = Xamarin.Forms.Application.
            Current.MainPage.Navigation.
            NavigationStack.LastOrDefault();

            ICustomBackButton currentpage = page as ICustomBackButton;

            if (currentpage == null)
            {
                base.OnBackPressed();
            }

            if (currentpage?.CustomBackButtonAction != null)
            {
                currentpage?.CustomBackButtonAction.Invoke();
            }
            else
            {
                base.OnBackPressed();
            }
        }

    }
}