using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MODEL;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "TeacherActivity")]
    public class TeacherActivityRegister : AppCompatActivity
    {
        private EditText etName;
        private EditText etPhone;
        private EditText etEmail;
        private EditText etPassword;
        private EditText etStart;
        private EditText etEnd;
        private Button btnSave;
        private Button btnCancel;
        private Teacher teacher;
        private Teachers teachers;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.teacher_register);
            SetViews();

            teachers = new Teachers();
            teachers = teachers.SelectAll();
        }

        private void SetViews()
        {
            etName = FindViewById<EditText>(Resource.Id.etName);
            etPhone = FindViewById<EditText>(Resource.Id.etPhone);
            etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            etStart = FindViewById<EditText>(Resource.Id.etStart);
            etEnd = FindViewById<EditText>(Resource.Id.etEnd);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Finish();
        }

        /// <summary>
        /// Checks if is there a permission
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        private bool CheckPermission(string permission)
        {
            const int PERMISSION_REQUEST_CODE = 1;

            if (ContextCompat.CheckSelfPermission(this, permission) == Android.Content.PM.Permission.Denied)
                ActivityCompat.RequestPermissions(this, new string[] { permission }, PERMISSION_REQUEST_CODE);
            return ContextCompat.CheckSelfPermission(this, permission) == Android.Content.PM.Permission.Granted;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Teacher teacher = new Teacher();
            teacher.Name = etName.Text;
            teacher.Phone = etPhone.Text;
            teacher.Email = etEmail.Text;
            teacher.Psw = etPassword.Text;
            teacher.StartHour = Convert.ToDateTime(etStart.Text);
            teacher.EndHour = Convert.ToDateTime(etEnd.Text);


            if (!teachers.Exists(teacher))
            {
                teachers.Add(teacher);
                teachers.Insert(teacher);
                MainActivity.teacher = teacher;
            }
            else
            {
                Toast.MakeText(this, "The email is already in use by another teacher!", ToastLength.Short).Show();
            }

            if (CheckPermission(Manifest.Permission.Vibrate))
            {
                Vibrator vibrator = (Vibrator)this.ApplicationContext.GetSystemService(Context.VibratorService);
                vibrator.Vibrate(1000);
            }
            StartActivity(new Intent(this, typeof(TeacherActivity)));
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
                case Resource.Id.mnuMainActivity:
                    {
                        StartActivity(new Intent(this, typeof(MainActivity)));
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
    }
}