using Android;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Content;
using Android.Provider;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using TASHTIT_ANDROID_PROJECT.ACTIVITIES;

namespace TASHTIT_ANDROID_PROJECT
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/car" )]
    public class MainActivity : AppCompatActivity
    {
        private Button btnList, btnSettings, btnCheckList;

        public void SetViews()
        {
            btnList = FindViewById<Button>(Resource.Id.btnList);
            btnSettings = FindViewById<Button>(Resource.Id.btnSettings);
            btnCheckList = FindViewById<Button>(Resource.Id.btnCheckList);

            btnList.Click += BtnList_Click;
            btnSettings.Click += BtnSettings_Click;
            btnCheckList.Click += BtnCheckList_Click;
        }

        private void BtnCheckList_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(WhatsNewActivity));
            StartActivity(intent);
        }

        private void BtnSettings_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ACTIVITIES.Settings));
            StartActivity(intent);
        }

        private void BtnList_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ListOfLessons));
            StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SetViews();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.mnuList:
                    {
                        StartActivity(new Intent(this, typeof(ListViewOfLessons)));
                        break;
                    }

                case Resource.Id.mnuSettings:
                    {
                        StartActivity(new Intent(this, typeof(ACTIVITIES.Settings)));
                        break;
                    }

                case Resource.Id.mnuWhatsLeft:
                    {
                        StartActivity(new Intent(this, typeof(WhatsNewActivity)));
                        break;
                    }

                case Resource.Id.mnuExit:
                    {
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Toast.MakeText(this, "OnRequestPermissionsResult", ToastLength.Long).Show();

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}