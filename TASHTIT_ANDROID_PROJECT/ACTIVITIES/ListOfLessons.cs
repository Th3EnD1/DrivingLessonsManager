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
using TASHTIT_ANDROID_PROJECT.ADAPTERS;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "ListOfLessons")]
    public class ListOfLessons : Activity
    {
        private ListView lv;
        private Lessons lessons;
        private LessonsAdapter adapter;
        private Button btnAddNewLesson;
        private int position;

        public void SetViews()
        {

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            
        }
    }
}