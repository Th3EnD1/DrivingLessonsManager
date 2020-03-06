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
using HELPER;
using MODEL;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "ListViewOfLessons")]
    public class ListViewOfLessons : Activity
    {
        private ListView lvLessons;
        private EditText etLesson;
        private ImageButton btnOk;
        private ImageButton btnCancel;
        private TextView txtHeader;

        private Lessons lessons;
        private ArrayAdapter<string> adapter;

        int position = -1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListOfLessons);

            SetViews();

            txtHeader.Text = "Lessons list";
            etLesson.Hint = "New Lesson";

            lessons = new Lessons();
            lessons = lessons.SelectAll();

            RefreshListView();

            Global.HideKeyboard(this, true);
        }

        private void SetViews()
        {
            lvLessons  = FindViewById<ListView>(Resource.Id.lvLessons);
            //etCity    = FindViewById<EditText>(Resource.Id.etCity);
            btnOk     = FindViewById<ImageButton>(Resource.Id.btnAddNewLesson);
            //btnCancel = FindViewById<ImageButton>(Resource.Id.btnCancel);
            //txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);

            lvLessons.ItemClick += LvCities_ItemClick;
            lvLessons.ItemLongClick += LvCities_ItemLongClick;

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void RefreshListView()
        {
            //            citiesList = new List<string>();

            //            foreach (City c in cities)
            //                citiesList.Add(c.Name);

            //            //List<string> orderedNames = people.Select(person => person.FirstName).OrderBy(name => name).ToList();

            ////            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, citiesList);
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lessons.Select(lesson => lesson.Id.ToString()).OrderBy(Id => Id).ToList());
            lvLessons.Adapter = adapter;
        }

        private void LvCities_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            position = e.Position;
            etLesson.Text = lessons[position].Id.ToString();
        }

        private void LvCities_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

            alertDiag.SetTitle("Confirm delete");
            alertDiag.SetMessage("Once '" + lessons[e.Position].Id + "' deleted the move cannot be undone");

            alertDiag.SetCancelable(true);

            alertDiag.SetPositiveButton("Delete", (EventHandler<DialogClickEventArgs>)((senderAlert, args)
            => {
                Lesson lesson = lessons[e.Position];

                if (lesson.Id != 0)
                    lesson.EntityStatus = EntityStatus.DELETED;
                else
                    lessons.Remove(lesson);

                lessons.Save();

                RefreshListView();

                alertDiag.Dispose();
            }));

            alertDiag.SetNegativeButton("Cancel", (senderAlert, args)
            => {
                alertDiag.Dispose();
            });

            Dialog diag = alertDiag.Create();
            diag.Show();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            bool isNew = position == -1;
            bool dataSetChanged = false;

            Global.HideKeyboard(this);

            if (etLesson.Text != "")
            {
                Lesson lesson = new Lesson();

                if (isNew)
                {
                    if (!lessons.Exists(lesson))
                    {
                        lesson.EntityStatus = EntityStatus.ADDED;
                        lessons.Add(lesson);
                        dataSetChanged = true;
                    }
                }
                else
                {
                    lesson.Id = lessons[position].Id;
                    lesson.EntityStatus = lessons[position].EntityStatus;

                    if (lesson.Id != 0)
                        lesson.EntityStatus = EntityStatus.MODIFIED;

                    if (!lessons.Exists(lesson, true))
                    {
                        lessons[position] = lesson;
                        dataSetChanged = true;
                    }
                }

                if (dataSetChanged)
                {
                    etLesson.Text = "";
                    position = -1;

                    lessons.Sort();

                    RefreshListView();
                }
                else
                {
                    Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

                    alertDiag.SetTitle("Exists");
                    alertDiag.SetMessage(lesson.Id + " already exists");

                    alertDiag.SetCancelable(true);

                    alertDiag.SetPositiveButton("OK", (senderAlert, args)
                    =>
                    {
                        alertDiag.Dispose();
                    });

                    Dialog diag = alertDiag.Create();
                    diag.Show();
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            etLesson.Text = "";
            position = -1;
        }

        protected override void OnStop()
        {
            base.OnStop();
            lessons.Save();
        }
    }
}