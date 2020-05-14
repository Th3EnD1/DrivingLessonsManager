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
using static Android.Provider.Settings;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "PickTeacher")]
    public class PickTeacher : Activity
    {
        private TextView txtHeader;
        private ListView lvTeachers;
        private ArrayAdapter adapter;
        private Teachers teachers;
        int position;

        private void SetViews()
        {
            txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);
            lvTeachers = FindViewById<ListView>(Resource.Id.lvTeachersOrStudents);

            lvTeachers.ItemClick += LvTeachersOrStudents_ItemClick;
        }

        private void LvTeachersOrStudents_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

            alertDiag.SetTitle("Confirm Choose");
            alertDiag.SetMessage("Are you sure you want to pick '" + teachers[e.Position].Name + "' ?");

            alertDiag.SetCancelable(true);

            alertDiag.SetPositiveButton("Choose", (EventHandler<DialogClickEventArgs>)((senderAlert, args)
            =>
            {
                MainActivity.student.TeacherId = teachers[e.Position].Id;
                alertDiag.Dispose();
            }));

            alertDiag.SetNegativeButton("Cancel", (senderAlert, args)
            =>
            {
                alertDiag.Dispose();
            });

            Dialog diag = alertDiag.Create();
            diag.Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListViewOfTeacherOrStudents);
            SetViews();
            teachers = new Teachers();
            teachers = teachers.SelectAll();

            RefreshListView();
            position = -1;
        }

        private void RefreshListView()
        {
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, teachers);
            lvTeachers.Adapter = adapter;
        }
    }
}