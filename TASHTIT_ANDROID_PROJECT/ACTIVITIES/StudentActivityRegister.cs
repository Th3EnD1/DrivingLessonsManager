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
using HELPER;
using MODEL;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "Student_Activity")]
    public class StudentActivityRegister : AppCompatActivity
    {
        private EditText etName;
        private EditText etTz;
        private EditText etPhone;
        private EditText etEmail;
        private EditText etPassword;
        private Button btnSave;
        private Button btnCancel;
        private Button btnPick;
        private Student student;
        private Teacher TeacherForPick;
        private Students students;
        private int teacherId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.student_register);
            SetViews();

            students = new Students();
            students = students.SelectAll();
            TeacherForPick = new Teacher();
        }

        public void SetViews()
        {
            etName = FindViewById<EditText>(Resource.Id.etName);
            etTz = FindViewById<EditText>(Resource.Id.etTz);
            etPhone = FindViewById<EditText>(Resource.Id.etPhone);
            etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnPick = FindViewById<Button>(Resource.Id.btnPick);

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            btnPick.Click += BtnPick_Click;
        }

        private void BtnPick_Click(object sender, EventArgs e)
        {
            StartActivityForResult(new Intent(this, typeof(PickTeacher)), 3);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 3)
            {
                if (resultCode == Result.Ok)
                {
                    if (data.Extras != null)
                    {
                        // Studentבדיקה אם הגיע 
                        //if (data.Extras.ContainsKey("TEACHER"))
                        //{
                            // Studentחילוץ ה-
                            // "דה-סריאליזציה"
                            //TeacherForPick = Serializer.ByteArrayToObject(data.GetByteArrayExtra("TEACHER")) as Teacher;
                        //}

                        if (data.Extras.ContainsKey("TEACHERID"))
                        {
                            teacherId = data.GetIntExtra("TEACHERID", 0);
                        }
                    }
                }
            }
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
            Student student = new Student();
            student.Name = etName.Text;
            student.Tz = etTz.Text;
            student.Phone = etPhone.Text;
            student.Email = etEmail.Text;
            student.Psw = etPassword.Text;
            //student.PickedTeacher = TeacherForPick;
            student.TeacherId = teacherId;
            //student.Cb1 = "false";
            //student.Cb2 = "false";
            //student.Cb3 = "false";
            //student.Cb4 = "false";
            //student.Cb5 = "false";
            //student.Cb6 = "false";
            //student.Cb7 = "false";
            //student.Cb8 = "false";


            if (!students.Exists(student))
            {
                students.Add(student);
                 students.Insert(student);
                MainActivity.student = student;
            }
            else
            {
                Toast.MakeText(this, "The ID number is already in use by another student!", ToastLength.Short).Show();
            }

            if (CheckPermission(Manifest.Permission.Vibrate))
            {
                Vibrator vibrator = (Vibrator)this.ApplicationContext.GetSystemService(Context.VibratorService);
                vibrator.Vibrate(1000);
            }
            StartActivity(new Intent(this, typeof(StudentActivity)));
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