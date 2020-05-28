using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

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
    [Activity(Label = "StudentActivity")]
    public class StudentActivity : AppCompatActivity
    {
        private Button btnList, btnCheckList, btnStudentLogin, btnTeacherLogin, btnInfo;
        private TextView txtLoggedAs;
        public static Teacher teacher;
        public static Student student;
        public static Students students;
        private Intent intent;
        IList<string> cbGetList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.StudentLayout);
            SetViews();

            students = new Students();
            students = students.SelectAll();

            txtLoggedAs.Text = "Logged as " + MainActivity.student.Name;

            cbGetList = new List<string>();

            if (MainActivity.student.Cb1 != null)
            {
                cbGetList.Add(MainActivity.student.Cb1);
                cbGetList.Add(MainActivity.student.Cb2);
                cbGetList.Add(MainActivity.student.Cb3);
                cbGetList.Add(MainActivity.student.Cb4);
                cbGetList.Add(MainActivity.student.Cb5);
                cbGetList.Add(MainActivity.student.Cb6);
                cbGetList.Add(MainActivity.student.Cb7);
                cbGetList.Add(MainActivity.student.Cb8);
            }
        }

        public void SetViews()
        {
            btnList = FindViewById<Button>(Resource.Id.btnList);
            btnCheckList = FindViewById<Button>(Resource.Id.btnCheckList);
            btnStudentLogin = FindViewById<Button>(Resource.Id.btnStudentLogin);
            btnTeacherLogin = FindViewById<Button>(Resource.Id.btnTeacherLogin);
            btnInfo = FindViewById<Button>(Resource.Id.btnInfo);
            txtLoggedAs = FindViewById<TextView>(Resource.Id.txtLoggedAs);

            btnList.Click += BtnList_Click;
            btnCheckList.Click += BtnCheckList_Click;
            btnStudentLogin.Click += BtnStudentLogin_Click;
            btnTeacherLogin.Click += BtnTeacherLogin_Click;
            btnInfo.Click += BtnInfo_Click;
        }

        private void BtnInfo_Click(object sender, EventArgs e)
        {
            Teachers teachers = new Teachers();
            teachers = teachers.SelectAll();
            if (MainActivity.student.TeacherId == teachers.SelectPicked(MainActivity.student.TeacherId).Id)
            {
                StartActivity(new Intent(this, typeof(Info)));
            }
            else
            {
                LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
                View mView = layoutInflaterAndroid.Inflate(Resource.Layout.LoginDialog, null);
                Android.Support.V7.App.AlertDialog.Builder alertDialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertDialogBuilder.SetView(mView);
                Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertDialog.SetTitle("Pick a teacher");
                alertDialog.SetMessage("You must pick a teacher first before viewing the info about your lessons, " + MainActivity.student.Name + ".");
                alertDialog.SetPositiveButton("Pick Teacher", delegate
                {
                    StartActivity(new Intent(this, typeof(PickTeacher)));
                    alertDialog.Dispose();
                })
                .SetNegativeButton("Cancel", delegate
                {
                    alertDialogBuilder.Dispose();
                });
                alertDialog.Show();
            }
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
                    if (exists == false)
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

        private void BtnCheckList_Click(object sender, EventArgs e)
        {
            intent = new Intent(this, typeof(WhatsNewActivity));
            if (MainActivity.student.Cb1 != null)
            {
                cbGetList[0] = MainActivity.student.Cb1;
                cbGetList[1] = MainActivity.student.Cb2;
                cbGetList[2] = MainActivity.student.Cb3;
                cbGetList[3] = MainActivity.student.Cb4;
                cbGetList[4] = MainActivity.student.Cb5;
                cbGetList[5] = MainActivity.student.Cb6;
                cbGetList[6] = MainActivity.student.Cb7;
                cbGetList[7] = MainActivity.student.Cb8;

                intent.PutStringArrayListExtra("CHECK", cbGetList);
            }
            StartActivityForResult(intent, 4);
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ListOfLessons));
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

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 4)
                if (resultCode == Result.Ok)
                {
                    cbGetList = data.GetStringArrayListExtra("CHECK");
                    MainActivity.student.Cb1 = cbGetList[0];
                    MainActivity.student.Cb2 = cbGetList[1];
                    MainActivity.student.Cb3 = cbGetList[2];
                    MainActivity.student.Cb4 = cbGetList[3];
                    MainActivity.student.Cb5 = cbGetList[4];
                    MainActivity.student.Cb6 = cbGetList[5];
                    MainActivity.student.Cb7 = cbGetList[6];
                    MainActivity.student.Cb8 = cbGetList[7];
                    students.Update(MainActivity.student);
                }
        }
    }
}