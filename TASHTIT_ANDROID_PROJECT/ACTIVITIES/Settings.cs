using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "Settings")]
    public class Settings : Activity
    {
        private EditText txtTime, txtPrice;
        private Button btnApply;

        public void SetViews()
        {
            txtTime = FindViewById<EditText>(Resource.Id.txtTime);
            txtPrice = FindViewById<EditText>(Resource.Id.txtPrice);
            btnApply = FindViewById<Button>(Resource.Id.btnApply);

            btnApply.Click += BtnApply_Click;

            txtTime.Text = Intent.GetStringExtra("Time");
            txtPrice.Text = Intent.GetStringExtra("Price");
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            Intent.PutExtra("Time", txtTime.Text);
            Intent.PutExtra("Price", txtPrice.Text);
            Toast.MakeText(this, "Saved successfully!", ToastLength.Short).Show();
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LessonSettings);
            SetViews();
        }
    }
}