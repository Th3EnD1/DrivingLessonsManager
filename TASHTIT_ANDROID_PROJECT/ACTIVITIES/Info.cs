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
    [Activity(Label = "Info")]
    public class Info : Activity
    {
        private TextView txtLessonsDone, txtMoneyPaid, txtLessonsLeft, txtMoneyLeft;
        private Student student;
        private Lessons paidLessons, notPaidLessons, lessonsLeft;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            student = MainActivity.student;
            paidLessons = paidLessons.SelectPaidLessons(student);
            notPaidLessons = notPaidLessons.SelectNotPaidLessons(student);
            lessonsLeft = lessonsLeft.SelectLessonsLeft(student);
            SetViews();
        }

        private void SetViews()
        {
            txtLessonsDone = FindViewById<TextView>(Resource.Id.txtLessonsDone);
            txtMoneyPaid = FindViewById<TextView>(Resource.Id.txtMoneyPaid);
            txtLessonsLeft = FindViewById<TextView>(Resource.Id.txtLessonsLeft);
            txtMoneyLeft = FindViewById<TextView>(Resource.Id.txtMoneyLeft);

            LessonsDone();
            PaidLessons();
            LessonsLeft();
            MoneyLeft();
        }

        private void LessonsDone()
        {
            txtLessonsDone.Text = paidLessons.Count + " lessons.";
        }

        private void PaidLessons()
        {
            txtMoneyPaid.Text = (paidLessons.Count * MainActivity.teacher.Cost) + " Shekels.";
        }

        private void LessonsLeft()
        {
            txtLessonsLeft.Text = lessonsLeft.Count + " more lessons left.";
        }

        private void MoneyLeft()
        {
            txtMoneyLeft.Text = (notPaidLessons.Count * MainActivity.teacher.Cost) + " more lessons left.";
        }
    }
}