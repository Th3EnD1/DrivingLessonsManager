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
using MODEL;

namespace TASHTIT_ANDROID_PROJECT
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/car" )]
    public class MainActivity : AppCompatActivity
    {
        private Button btnStudentLogin, btnTeacherLogin, btnStudentRegister, btnTeacherRegister;
        private TextView txtLoggedAs;
        public static Teacher teacher;
        public static Student student;

        public void SetViews()
        {
            btnStudentLogin = FindViewById<Button>(Resource.Id.btnStudentLogin);
            btnStudentRegister = FindViewById<Button>(Resource.Id.btnStudentRegister);
            btnTeacherLogin = FindViewById<Button>(Resource.Id.btnTeacherLogin);
            btnTeacherRegister = FindViewById<Button>(Resource.Id.btnTeacherRegister);
            txtLoggedAs = FindViewById<TextView>(Resource.Id.txtLoggedAs);

            btnStudentLogin.Click += BtnStudentLogin_Click;
            btnStudentRegister.Click += BtnStudentRegister_Click;
            btnTeacherLogin.Click += BtnTeacherLogin_Click;
            btnTeacherRegister.Click += BtnTeacherRegister_Click;
        }

        private void BtnTeacherRegister_Click(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(this, typeof(TeacherActivityRegister)));
        }

        private void BtnStudentRegister_Click(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(this, typeof(StudentActivityRegister)));
        }

        private void BtnTeacherLogin_Click(object sender, System.EventArgs e)
        {
            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
            View mView = layoutInflaterAndroid.Inflate(Resource.Layout.LoginDialog, null);
            Android.Support.V7.App.AlertDialog.Builder alertDialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertDialogBuilder.SetView(mView);

            var etEmail = mView.FindViewById<EditText>(Resource.Id.etEmail);
            var etPassword = mView.FindViewById<EditText>(Resource.Id.etPassword);

            alertDialogBuilder.SetCancelable(false)
            .SetPositiveButton("Login", delegate
            {
                Teachers teachers = new Teachers();
                teachers = teachers.SelectAll();
                Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this);
                bool exists = false;
                Teacher teacherTest = new Teacher();
                for (int i = 0; i < teachers.Count; i++)
                {
                    if (etEmail.Text == teachers[i].Email)
                    {
                        if (etPassword.Text == teachers[i].Psw)
                        {
                            teacherTest = teachers[i];
                            exists = true;
                        }
                        else
                            exists = false;
                    }
                    else
                        exists = false;
                }
                if (exists == true)
                {
                    Toast.MakeText(this, "Logged in as " + teacherTest.Name + ".", ToastLength.Short).Show();
                    MainActivity.teacher = teacherTest;
                    txtLoggedAs.Text = "Logged as " + MainActivity.teacher.Name;
                    alertDialogBuilder.Dispose();
                    StartActivity(new Intent(this, typeof(TeacherActivity)));
                }
                else
                {
                    alertDialog.SetTitle("Login Error");
                    alertDialog.SetMessage("The email or the password are incorrect. Please check if you have entered the correct email and password and try again.");
                    alertDialog.SetNeutralButton("OK", delegate
                    {
                        alertDialog.Dispose();
                    });
                    alertDialog.Show();
                    exists = false;
                }
            })
            .SetNegativeButton("Cancel", delegate
            {
                alertDialogBuilder.Dispose();
            });

            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertDialogBuilder.Create();
            alertDialogAndroid.Show();
        }

        private void BtnStudentLogin_Click(object sender, System.EventArgs e)
        {
            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
            View mView = layoutInflaterAndroid.Inflate(Resource.Layout.LoginDialog, null);
            Android.Support.V7.App.AlertDialog.Builder alertDialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertDialogBuilder.SetView(mView);

            var etEmail = mView.FindViewById<EditText>(Resource.Id.etEmail);
            var etPassword = mView.FindViewById<EditText>(Resource.Id.etPassword);

            alertDialogBuilder.SetCancelable(false)
            .SetPositiveButton("Login", delegate
            {
                Students students = new Students();
                students = students.SelectAll();
                Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this);
                bool exists = false;
                Student studentTest = new Student();
                for (int i = 0; i < students.Count; i++)
                {
                    if (etEmail.Text == students[i].Email)
                    {
                        if (etPassword.Text == students[i].Psw)
                        {
                            studentTest = students[i];
                            exists = true;
                        }
                        else
                            exists = false;
                    }
                    else
                        exists = false;
                }
                if (exists == true)
                {
                    Toast.MakeText(this, "Logged in as " + studentTest.Name + ".", ToastLength.Short).Show();
                    MainActivity.student = studentTest;
                    txtLoggedAs.Text = "Logged as " + MainActivity.student.Name;
                    alertDialogBuilder.Dispose();
                    StartActivity(new Intent(this, typeof(StudentActivity)));
                }
                else
                {
                    alertDialog.SetTitle("Login Error");
                    alertDialog.SetMessage("The email or the password are incorrect. Please check if you have entered the correct email and password and try again.");
                    alertDialog.SetNeutralButton("OK", delegate
                    {
                        alertDialog.Dispose();
                    });
                    alertDialog.Show();
                    exists = false;
                }
            })
            .SetNegativeButton("Cancel", delegate
            {
                alertDialogBuilder.Dispose();
            });

            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertDialogBuilder.Create();
            alertDialogAndroid.Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SetViews();

            Students students = new Students();
            students = students.SelectAll();
            //if (students.Count > 0)
            //    MainActivity.student = students[0];
            //teacher.Cost = 140;

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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Toast.MakeText(this, "OnRequestPermissionsResult", ToastLength.Long).Show();

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}