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
    [Activity(Label = "TeacherActivity")]
    public class TeacherActivity : AppCompatActivity
    {
        private Button btnSettings, btnStudentLogin, btnTeacherLogin;
        private TextView txtLoggedAs;
        public static Teacher teacher;
        public static Student student;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TeacherLayout);
            SetViews();

            Students students = new Students();
            students = students.SelectAll();
        }

        public void SetViews()
        {
            btnSettings = FindViewById<Button>(Resource.Id.btnSettings);
            btnStudentLogin = FindViewById<Button>(Resource.Id.btnStudentLogin);
            btnTeacherLogin = FindViewById<Button>(Resource.Id.btnTeacherLogin);
            txtLoggedAs = FindViewById<TextView>(Resource.Id.txtLoggedAs);

            btnSettings.Click += BtnSettings_Click;
            btnStudentLogin.Click += BtnStudentLogin_Click;
            btnTeacherLogin.Click += BtnTeacherLogin_Click;
        }

        private void BtnTeacherLogin_Click(object sender, EventArgs e)
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

        private void BtnStudentLogin_Click(object sender, EventArgs e)
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

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ACTIVITIES.Settings));
            StartActivity(intent);
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
                        StartActivity(new Intent(this, typeof(ListOfLessons)));
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

                case Resource.Id.mnuTeacher:
                    {
                        StartActivity(new Intent(this, typeof(TeacherActivityRegister)));
                        break;
                    }

                case Resource.Id.mnuStudent:
                    {
                        StartActivity(new Intent(this, typeof(StudentActivityRegister)));
                        break;
                    }

                case Resource.Id.mnuPickTeacher:
                    {
                        StartActivity(new Intent(this, typeof(PickTeacher)));
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