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
using MODEL;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "Student_Activity")]
    public class Student_Activity : Activity
    {
        private EditText etName;
        private EditText etTz;
        private EditText etPhone;
        private EditText etEmail;
        private EditText etPassword;
        private Button btnSave;
        private Button btnCancel;
        private Student student;
        private Students students;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.student_layout);
            SetViews();

            students = new Students();
            students = students.SelectAll();
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

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            student.Name = etName.Text;
            student.Tz = etTz.Text;
            student.Phone = etPhone.Text;
            student.Email = etEmail.Text;
            student.Psw = etPassword.Text;


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

            Finish();
        }
    }
}