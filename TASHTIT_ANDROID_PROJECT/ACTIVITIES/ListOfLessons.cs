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
        private Button btnAddNewLesson;
        private Lessons lessons;
        private LessonsAdapter adapter;
        int position;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            lessons = new Lessons();
            //lessons = lessons.SelectAll();
            lessons = lessons.SelectLessonsForStudent(MainActivity.student);
            SetContentView(Resource.Layout.ListOfLessons);
            SetViews();
            lvLessons.Adapter = adapter;

            RefreshListView();

            Global.HideKeyboard(this, true);
            position = -1;
        }

        private void SetViews()
        {
            lvLessons  = FindViewById<ListView>(Resource.Id.lvLessons);
            btnAddNewLesson = FindViewById<Button>(Resource.Id.btnAddNewLesson);
            lvLessons.ItemClick += LvLessons_ItemClick;
            lvLessons.ItemLongClick += LvLessons_ItemLongClick;
            btnAddNewLesson.Click += BtnAddNewLesson_Click;
        }

        private void BtnAddNewLesson_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(NewLesson));
            StartActivityForResult(intent, 0);
            RefreshListView();
        }

        private void RefreshListView()
        {
            adapter = new LessonsAdapter(this, Resource.Layout.OneItem, lessons);
            lvLessons.Adapter = adapter;
        }

        protected override void OnActivityResult(int RequestCode, [GeneratedEnum] Result resultCode, Intent data) 
        {
            Lesson lesson = Serializer.ByteArrayToObject(data.GetByteArrayExtra("LESSON")) as Lesson;
            lessons.Add(lesson);
            lessons.InsertDb(lesson);
            RefreshListView();
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
                {
                    lesson.EntityStatus = EntityStatus.DELETED;
                    lessons.DeleteDb(lesson);
                }
                else
                {
                    lessons.Remove(lesson);
                    lessons.DeleteDb(lesson);
                }

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
    }
}