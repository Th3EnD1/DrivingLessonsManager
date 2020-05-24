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
    [Activity(Label = "Info")]
    public class Info : AppCompatActivity
    {
        private TextView txtLessonsDone, txtMoneyPaid, txtLessonsLeft, txtMoneyLeft;
        private Student student;
        //private Lessons paidLessons, notPaidLessons, lessonsLeft;
        private Lessons lessons;
        private Teacher teacher;
        private Teachers teachers;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.info);
            student = MainActivity.student;
            lessons = new Lessons();
            lessons = lessons.SelectAll();
            teacher = new Teacher();
            teachers = new Teachers();
            teachers = teachers.SelectAll();
            teacher = teachers.SelectPicked(student.TeacherId);
            //paidLessons = new Lessons();
            //paidLessons = paidLessons.SelectPaidLessons(student);
            //notPaidLessons = new Lessons();
            //notPaidLessons = notPaidLessons.SelectNotPaidLessons(student);
            //lessonsLeft = new Lessons();
            //lessonsLeft = lessonsLeft.SelectLessonsLeft(student);
            SetViews();
            LessonsDone();
            PaidLessons();
            LessonsLeft();
            MoneyLeft();
        }

        private void SetViews()
        {
            txtLessonsDone = FindViewById<TextView>(Resource.Id.txtLessonsPaid);
            txtMoneyPaid = FindViewById<TextView>(Resource.Id.txtMoneyPaid);
            txtLessonsLeft = FindViewById<TextView>(Resource.Id.txtLessonsLeft);
            txtMoneyLeft = FindViewById<TextView>(Resource.Id.txtMoneyLeft);
        }

        private void LessonsDone()
        {
            lessons = lessons.SelectPaidLessons(student);
            txtLessonsDone.Text ="" + lessons.Count.ToString() + " lessons.";
            lessons = lessons.SelectAll();
        }

        private void PaidLessons()
        {
            lessons = lessons.SelectPaidLessons(student);
            txtMoneyPaid.Text = (lessons.Count * teacher.Cost).ToString() + " Shekels.";
            lessons = lessons.SelectAll();
        }

        private void LessonsLeft()
        {
            lessons = lessons.SelectLessonsLeft(student);
            txtLessonsLeft.Text = lessons.Count.ToString() + " more lessons left.";
            lessons = lessons.SelectAll();
        }

        private void MoneyLeft()
        {
            lessons = lessons.SelectNotPaidLessons(student);
            txtMoneyLeft.Text = (lessons.Count * teacher.Cost).ToString() + " Shekels.";
            lessons = lessons.SelectAll();
        }
    }
}