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
    [Activity(Label = "PickTeacher")]
    public class PickTeacher : Activity
    {
        private TextView txtHeader;
        private ListView lvTeachersOrStudents;
        private ArrayAdapter<string> adapter;
        private Teachers teachers;

        private void SetViews()
        {
            txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);
            lvTeachersOrStudents = FindViewById<ListView>(Resource.Id.lvTeachersOrStudents);

            lvTeachersOrStudents.ItemClick += LvTeachersOrStudents_ItemClick;
        }

        private void LvTeachersOrStudents_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

            //alertDiag.SetTitle("Confirm Choose");
            //alertDiag.SetMessage("Are you sure you want to pick '" + teachers[e.Position].Name + "' ?");

            //alertDiag.SetCancelable(true);

            //alertDiag.SetPositiveButton("Choose", (EventHandler<DialogClickEventArgs>)((senderAlert, args)
            //=> {
            //    Teacher teacher = teachers[e.Position];

            //    if (teacher.Id != 0)
            //        teacher.EntityStatus = EntityStatus.DELETED;
            //    else
            //        teachers.Remove(teacher);

            //    teachers.Save();

            //    RefreshListView();

            //    alertDiag.Dispose();
            //}));

            //alertDiag.SetNegativeButton("Cancel", (senderAlert, args)
            //=> {
            //    alertDiag.Dispose();
            //});

            //Dialog diag = alertDiag.Create();
            //diag.Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListViewOfTeacherOrStudents);
            SetViews();

            RefreshListView();
        }

        private void RefreshListView()
        {
            adapter = new ArrayAdapter<string>(this, Resource.Layout.OneItemPick);
            lvTeachersOrStudents.Adapter = adapter;
        }
    }
}