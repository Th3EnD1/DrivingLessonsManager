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
using HELPER;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "ListOfLessons")]
    public class ListOfLessons : Activity
    {
        private ListView lvLessons;
        //private EditText etLesson;
        //private ImageButton btnOk;
        //private ImageButton btnCancel;
        //private TextView txtHeader;
        private Button btnAddNewLesson;

        private Lessons lessons;
        private LessonsAdapter adapter;

        int position;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            lessons = lessons.SelectAll();
            SetContentView(Resource.Layout.ListOfLessons);

            SetViews();

            //txtHeader.Text = "Lessons list";
            //etLesson.Hint = "New Lesson";
            //etCity.InputType = Android.Text.InputTypes.ClassNumber;

            //lessons = new Lessons();
            //lessons = lessons.SelectAll();

            adapter = new LessonsAdapter(this, Resource.Layout.OneItem, lessons);
            lvLessons.Adapter = adapter;

            RefreshListView();

            Global.HideKeyboard(this, true);
            position = -1;
        }

        private void SetViews()
        {
            lvLessons  = FindViewById<ListView>(Resource.Id.lvLessons);
            //etLesson    = FindViewById<EditText>(Resource.Id.etLesson);
            //btnOk     = FindViewById<ImageButton>(Resource.Id.btnOk);
            //btnCancel = FindViewById<ImageButton>(Resource.Id.btnCancel);
            //txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);
            btnAddNewLesson = FindViewById<Button>(Resource.Id.btnAddNewLesson);

            lvLessons.ItemClick += LvLessons_ItemClick;
            lvLessons.ItemLongClick += LvLessons_ItemLongClick;

            btnAddNewLesson.Click += BtnAddNewLesson_Click;

            //btnOk.Click += BtnOk_Click;
            //btnCancel.Click += BtnCancel_Click;
        }

        private void BtnAddNewLesson_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(NewLesson));
            StartActivityForResult(intent, 0);
        }

        private void RefreshListView()
        {
            //adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lessons.Select(lesson => lesson.Id.ToString()).OrderBy(name => name).ToList());
            adapter = new LessonsAdapter(this, Resource.Layout.OneItem, lessons);
            lvLessons.Adapter = adapter;
        }

        private void LvLessons_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            position = e.Position;
            Intent intent = new Intent(this, typeof(NewLesson));
            intent.PutExtra("LESSON", Serializer.ObjectToByteArray(lessons[e.Position]));
            StartActivityForResult(intent, 0);
            //etLesson.Text = lessons[position].Id.ToString();
        }

        private void LvLessons_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

            alertDiag.SetTitle("Confirm delete");
            alertDiag.SetMessage("Once '" + lessons[e.Position].Id.ToString() + "' deleted the move cannot be undone");

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

        //private void BtnOk_Click(object sender, EventArgs e)
        //{
        //    bool isNew = position == -1;
        //    bool dataSetChanged = false;

        //    Global.HideKeyboard(this);

        //    //if (etLesson.Text != "")
        //    //{
        //    //    if (!etLesson.Text.StartsWith('0') || etLesson.Text.Length > 3)
        //    //    {
        //    //        Global.ToastCenteredText(this, "Area code should follow \n 1. Start with '0' \n 2. Maximum 3 digits", ToastLength.Long);
        //    //    }
        //        //else
        //        Lesson lesson = new Lesson();
        
        //        if (isNew)
        //        {
        //            if (!lessons.Exists(lesson))
        //            {
        //                lesson.EntityStatus = EntityStatus.ADDED;
        //                lessons.Add(lesson);
        //                dataSetChanged = true;
        //            }
        //        }
        //        else
        //        {
        //            lesson.Id = lessons[position].Id;
        //            lesson.EntityStatus = lessons[position].EntityStatus;

        //            if (lesson.Id != 0)
        //                lesson.EntityStatus = EntityStatus.MODIFIED;

        //            if (!lessons.Exists(lesson, true))
        //            {
        //                lessons[position] = lesson;
        //                dataSetChanged = true;
        //            }
        //        }

        //        if (dataSetChanged)
        //        {
        //          //etLesson.Text = "";
        //            position = -1;

        //            lessons.Sort();

        //            RefreshListView();
        //        }
        //        else
        //        {
        //            Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

        //            alertDiag.SetTitle("Exists");
        //            alertDiag.SetMessage(lesson.Id + " already exists");

        //            alertDiag.SetCancelable(true);

        //            alertDiag.SetPositiveButton("OK", (senderAlert, args)
        //            =>
        //            {
        //                alertDiag.Dispose();
        //            });

        //            Dialog diag = alertDiag.Create();
        //            diag.Show();
        //        }
        //}

        //private void BtnCancel_Click(object sender, EventArgs e)
        //{
        //    //etLesson.Text = "";
        //    position = -1;
        //}

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //    lessons.Save();
        //}
    }
}