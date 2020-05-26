using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MODEL;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "Settings")]
    public class Settings : AppCompatActivity
    {
        private EditText txtTime, txtPrice;
        private Button btnApply;
        private Teachers teachers; 
        private Teacher teacher;

        public void SetViews()
        {
            txtTime = FindViewById<EditText>(Resource.Id.txtTime);
            txtPrice = FindViewById<EditText>(Resource.Id.txtPrice);
            btnApply = FindViewById<Button>(Resource.Id.btnApply);

            btnApply.Click += BtnApply_Click;

            txtTime.Text = MainActivity.teacher.MinutsOfLesson.ToString();
            txtPrice.Text = MainActivity.teacher.Cost.ToString();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            Intent.PutExtra("Time", txtTime.Text);
            Intent.PutExtra("Price", txtPrice.Text);
            teacher = teachers.SelectPicked(MainActivity.student.TeacherId);
            teachers = teachers.SelectAll();
            teachers.Update(teacher);
            //.Cost = int.Parse(txtPrice.Text);
            MainActivity.teacher.MinutsOfLesson = int.Parse(txtTime.Text);
            Toast.MakeText(this, "Saved successfully!", ToastLength.Short).Show();
            Finish();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LessonSettings);
            SetViews();
            teachers = new Teachers();
            teachers = teachers.SelectAll();
            teacher = new Teacher();
        }
    }
}