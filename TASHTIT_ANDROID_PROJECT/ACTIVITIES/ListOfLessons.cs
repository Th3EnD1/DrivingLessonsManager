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
using Android.Support.V7.App;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "ListOfLessons")]
    public class ListOfLessons : AppCompatActivity
    {
        private ListView lvLessons;
        private Button btnAddNewLesson;
        private Lessons lessons;
        private LessonsAdapter adapter;
        private Teachers teachers;
        private Teacher teacher;
        private Diary diary;
        private Diaries diaries;
        int position;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            lessons = new Lessons();
            lessons = lessons.SelectLessonsForStudent(MainActivity.student);

            teachers = new Teachers();
            teacher = new Teacher();
            teacher = teachers.SelectPicked(MainActivity.student.TeacherId);

            diaries = new Diaries();
            diaries = diaries.SelectAll(MainActivity.student);
            diary = new Diary();

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
            StartActivityForResult(intent, 5);
            RefreshListView();
        }

        private void RefreshListView()
        {
            adapter = new LessonsAdapter(this, Resource.Layout.OneItem, lessons);
            lvLessons.Adapter = adapter;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data) 
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 5)
            {
                if (resultCode == Result.Ok)
                {
                    if (data.Extras != null)
                    {
                        if (data.Extras.ContainsKey("LESSON"))
                        {
                            Lesson lesson = Serializer.ByteArrayToObject(data.GetByteArrayExtra("LESSON")) as Lesson;
                            if (lesson.Time >= teacher.StartHour && lesson.Time <= teacher.EndHour)
                            {
                                lessons.Add(lesson);
                                lessons.InsertDb(lesson);
                                RefreshListView();

                                if (diaries.Count == 0)
                                {
                                    diary.Date = new DateTime(lesson.Date.Year, lesson.Date.Month, lesson.Date.Day, lesson.Time.Hour, lesson.Time.Minute, 0);
                                    switch(lesson.LessonTypeNo)
                                    {
                                        case 0: { diary.LessonType = LessonTypeEnum.Regular.ToString(); }break;
                                        case 1: { diary.LessonType = LessonTypeEnum.OneAndHalf.ToString(); } break;
                                        case 2: { diary.LessonType = LessonTypeEnum.Double.ToString(); } break;
                                        case 3: { diary.LessonType = LessonTypeEnum.Triple.ToString(); } break;
                                        case 4: { diary.LessonType = LessonTypeEnum.InTest.ToString(); } break;
                                        case 5: { diary.LessonType = LessonTypeEnum.OutTest.ToString(); } break;
                                        default: { diary.LessonType = LessonTypeEnum.Regular.ToString(); } break;
                                    }
                                    diary.StudentName = MainActivity.student.Name;
                                    diaries.Add(diary);
                                    diaries.Insert(diary);
                                }
                            }
                        }
                    }
                }
            }
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