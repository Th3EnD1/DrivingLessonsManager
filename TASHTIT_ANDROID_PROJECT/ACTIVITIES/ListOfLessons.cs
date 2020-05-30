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
        private Lessons lessons, allLessonsOfTeacher;
        private LessonsAdapter adapter;
        private Teachers teachers;
        private Teacher teacher;
        private Student student;
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

            allLessonsOfTeacher = new Lessons();
            student = MainActivity.student;
            allLessonsOfTeacher = allLessonsOfTeacher.SelectLessonsForTeacher(student, teacher.Id);

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

                            Lesson l = new Lesson();

                            bool possible = true;

                            if (lesson.Time.Hour == teacher.StartHour.Hour)//Same strating hour
                            {
                                if (lesson.Time.Minute >= teacher.StartHour.Minute)//After starting
                                    possible = true;
                                else
                                    possible = false;
                            }
                            else//Not same hour
                            {
                                if (lesson.Time.Hour < teacher.StartHour.Hour)//Before teacher starts working
                                    possible = false;
                                else//At least one hour after teacher starts working
                                    possible = true;
                            }
                            if (possible == true)//After starting
                            {
                                if (lesson.Time.Hour == teacher.EndHour.Hour)//Same ending hour
                                {
                                    if (lesson.Time.Minute < teacher.EndHour.Minute)//Same hour but before ending
                                    {
                                        if ((60 - teacher.MinutsOfLesson) < lesson.Time.Minute)//Not enough time for lesson
                                            possible = false;
                                        else//Enough time for lesson
                                            possible = true;
                                    }
                                    else//Same hour but after ending
                                        possible = false;
                                }
                                else
                                {
                                    if (lesson.Time.Hour > teacher.EndHour.Hour)//After ending
                                        possible = false;
                                    else//At least one hour before ending
                                    {
                                        if (teacher.EndHour.Hour - 1 == lesson.Time.Hour)//One hour before ending
                                        {
                                            if ((60 - teacher.MinutsOfLesson) < lesson.Time.Minute)//Not enough time for lesson
                                                possible = false;
                                            else
                                                possible = true;
                                        }
                                        else//More than one hour before ending
                                            possible = true;
                                    }
                                }
                            }
                            if (possible == true)//If lesson is in working time and enough time before ending
                            {
                                if (allLessonsOfTeacher.Count > 0)//There are lessons for the teacher
                                {
                                    for (int i = 0; i < allLessonsOfTeacher.Count; i++)//Checks each lesson in the teacher's lessons list
                                    {
                                        l = allLessonsOfTeacher[i];//l is the lesson from the list that is in checking right now
                                        if (possible == true)
                                        {
                                            if (lesson.Date <= l.Date)//The date of the lesson is before or at the same date as the checked lesson from the teacher's list
                                            {
                                                if (lesson.Date == l.Date)//The date is at the same date as the checked lesson from the teacher's list
                                                {
                                                    if (l.Time.Hour == lesson.Time.Hour)//Same date and hour
                                                    {
                                                        if (l.Time.Minute == lesson.Time.Minute)//Both at the exact same time
                                                            possible = false;
                                                        else//Same date and hour but not minutes
                                                        {
                                                            if ((l.Time.Minute + teacher.MinutsOfLesson) <= lesson.Time.Minute)//The lesson is exactely after the previous one has done or after it has done at the same hour
                                                                possible = true;
                                                            else//The lesson is before the prevoius one has done
                                                                possible = false;
                                                        }
                                                    }
                                                }
                                                else//The checked lesson from the teacher's list is sometime after the lesson
                                                {
                                                    if ((l.Date.Day - 1) == lesson.Date.Day)//The checked lesson from the teacher's list is the day after the lesson
                                                    {
                                                        if (lesson.Time.Hour == 23)//If it's the last hour of the day (23:xx or 11PM)
                                                        {
                                                            if (lesson.Time.Minute + teacher.MinutsOfLesson <= 60)//The lesson will end before the end of the day
                                                                possible = true;
                                                            else//The lesson will continue to the next day (same day as the checked lesson from the teacher's list)
                                                            {
                                                                int difference = 60 - lesson.Time.Minute;//The difference is now the time that the lesson will be for the next day
                                                                if (l.Time.Hour == 0)//The checked lesson from the teacher's list is at the first hour of the day (00:xx or 12 AM)
                                                                {
                                                                    if (l.Time.Minute - difference >= 0)//The checked lesson from the teacher's list will start the exact time the previous lesson has ended or after it has ended
                                                                        possible = true;
                                                                    else//The checked lesson from the teacher's list will start before the previous lesson has ended
                                                                        possible = false;
                                                                }
                                                                else//The checked lesson from the teacher's list is sometime after 01:00 or 1AM
                                                                    possible = true;
                                                            }

                                                        }
                                                        else//If it's before 23:00 or 11PM
                                                            possible = true;
                                                    }
                                                    else//The checked lesson from the teacher's list is more than a day after the lesson
                                                        possible = true;
                                                }
                                            }
                                            else//The date of the lesson is after the checked lesson from the teacher's list
                                            {
                                                if ((lesson.Date.Day - 1) == l.Date.Day)//The checked lesson from the teacher's list is the day before the lesson
                                                {
                                                    if (l.Time.Hour == 23)//If it's the last hour of the day (23:xx or 11PM)
                                                    {
                                                        if (l.Time.Minute + teacher.MinutsOfLesson <= 60)//The checked lesson from the teacher's list will end before the end of the day
                                                            possible = true;
                                                        else//The checked lesson from the teacher's list will continue to the next day (same day as the lesson)
                                                        {
                                                            int difference = 60 - l.Time.Minute;//The difference is now the time that the lesson from the teacher's list will be for the next day
                                                            if (lesson.Time.Hour == 0)//The lesson is at the first hour of the day (00:xx or 12 AM)
                                                            {
                                                                if (lesson.Time.Minute - difference >= 0)//The lesson will start the exact time the previous lesson has ended or after it has ended
                                                                    possible = true;
                                                                else//The lesson will start before the previous lesson has ended
                                                                    possible = false;
                                                            }
                                                            else//The lesson is sometime after 01:00 or 1AM
                                                                possible = true;
                                                        }
                                                    }
                                                    else//If it's before 23:00 or 11PM
                                                        possible = true;
                                                }
                                                else//The lesson is more than a day after the checked lesson from the teacher's list
                                                    possible = true;
                                            }
                                        }
                                    }
                                }
                                else//There are no lessons for the teacher
                                    possible = true;
                            }

                            if (possible == true)//If there is no problem at all about the date and the time of the lesson
                            {
                                lessons.Add(lesson);
                                lessons.InsertDb(lesson);
                                RefreshListView();

                                if (diaries.Count == 0)
                                {
                                    diary.Date = new DateTime(lesson.Date.Year, lesson.Date.Month, lesson.Date.Day, lesson.Time.Hour, lesson.Time.Minute, 0);
                                    switch (lesson.LessonTypeNo)
                                    {
                                        case 0: { diary.LessonType = LessonTypeEnum.Regular.ToString(); } break;
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
                            else//If there is a problem with the date or the time of the lesson
                            {
                                Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this);
                                alertDialog.SetTitle("Error while trying to add new lesson");
                                alertDialog.SetMessage("There is a problem with the date or the time of the lesson. please choose a different date or time for the new lesson.");
                                alertDialog.SetNeutralButton("OK", delegate
                                {
                                    alertDialog.Dispose();
                                });
                                alertDialog.Show();
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.mnuMainActivity:
                    {
                        StartActivity(new Intent(this, typeof(MainActivity)));
                        break;
                    }

                case Resource.Id.mnuExit:
                    {
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}