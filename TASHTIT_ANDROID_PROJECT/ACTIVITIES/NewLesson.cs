using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MODEL;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HELPER;
using Android.Icu.Text;
using static Android.App.DatePickerDialog;
using static Android.App.TimePickerDialog;
using Java.Util;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "NewLesson")]
    public class NewLesson : Activity,IOnDateSetListener,IOnTimeSetListener
    {
        private Button btnSave, btnCancel, btnDate, btnTime;
        private TextView txtDate, txtTime, txtHeader;
        private DatePickerDialog datePicker;
        private Lesson lesson;
        private LessonTypes lessonTypes;
        private Spinner spnType;
        private bool isNew;
        private const int DATE_DIALOG = 0;
        private const int TIME_DIALOG = 1;
        private int day, month, year, hour, minuts;
        private DateTime date, time;
        private Diaries diaries;
        private Diary diary;
        private CheckBox cbPaid;

        public void SetViews()
        {
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            btnDate = FindViewById<Button>(Resource.Id.btnDate);
            btnTime = FindViewById<Button>(Resource.Id.btnTime);
            txtDate = FindViewById<TextView>(Resource.Id.txtDate);
            txtTime = FindViewById<TextView>(Resource.Id.txtTime);
            txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);
            spnType = FindViewById<Spinner>(Resource.Id.spnType);
            cbPaid = FindViewById<CheckBox>(Resource.Id.cbPaid);

            btnTime.Click += BtnTime_Click;
            btnDate.Click += BtnDate_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            spnType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.type_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnType.Adapter = adapter;
        }

        private void BtnTime_Click(object sender, EventArgs e)
        {
            ShowDialog(TIME_DIALOG);
        }

        private void BtnDate_Click(object sender, EventArgs e)
        {
            ShowDialog(DATE_DIALOG);
        }

        protected override Dialog OnCreateDialog(int id)
        {
            DateTime today = DateTime.Today;
            switch (id)
            {
                case DATE_DIALOG:
                    {
                        return new DatePickerDialog(this, this, today.Year, today.Month, today.Day);
                    }
                default:
                    break;
            }
            switch (id)
            {
                case TIME_DIALOG:
                    {
                        return new TimePickerDialog(this, this, hour, minuts, true);
                    }
                default:
                    break;
            }
            return null;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Lesson lesson = new Lesson();

            if (!isNew)
                lesson.Id = this.lesson.Id;

            lesson.Paid = cbPaid.Checked;

            lesson.Date = date;

            lesson.Time = time;

            lesson.StudentNo = MainActivity.student.Id;

            lesson.LessonTypeNo = (int)spnType.SelectedItemId;

            //switch(lesson.LessonTypeNo)
            //{
            //    case 0: { lesson.Cost = MainActivity.teacher.Cost; } break;
            //    case 1: { lesson.Cost = MainActivity.teacher.Cost * 1.5; } break;
            //    case 2: { lesson.Cost = MainActivity.teacher.Cost * 2; } break;
            //    case 3: { lesson.Cost = MainActivity.teacher.Cost * 3; } break;
            //    case 4: { lesson.Cost = MainActivity.teacher.Cost; } break;
            //    case 5: { lesson.Cost = MainActivity.teacher.Cost; } break;
            //    default: { lesson.Cost = MainActivity.teacher.Cost; } break;
            //}

            diaries = diaries.SelectAll(MainActivity.student);

            if (diaries.Count == 0)
            {
                diary = new Diary();
                diary.Date = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
                diary.LessonType = spnType.SelectedItem.ToString(); 
                diary.StudentName = MainActivity.student.Name;

                diaries.Add(diary);
                diaries.Insert(diary);
            }

            Intent intent = new Intent();

            intent.PutExtra("LESSON", Serializer.ObjectToByteArray(lesson));

            SetResult(Result.Ok, intent);


            Finish();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok)
            {
                base.OnActivityResult(requestCode, resultCode, data);
            }
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Current type: {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewLesson);
            SetViews();

            diaries = new Diaries();
            diaries = diaries.SelectAll();

            lessonTypes = new LessonTypes();

            if (Intent.Extras != null)
            {
                // Lessonבדיקה אם הגיע 
                if (Intent.Extras.ContainsKey("LESSON"))
                {
                    // Lessonחילוץ ה-
                    // "דה-סריאליזציה"
                    lesson = Serializer.ByteArrayToObject(Intent.GetByteArrayExtra("LESSON")) as Lesson;

                    // השמת הערכים לעריכה בשדות הקלט

                    spnType.SetSelection(lesson.LessonTypeNo);

                    txtDate.Text = lesson.Date.ToShortDateString();  ///  יכול להיות שיציג חודשים וימים במהופך
                                                                     /// במיקרה כזה יש להרכיב מחרוזת תאריך בטאופן ידני
                    txtTime.Text = lesson.Time.ToShortTimeString();

                    isNew = false;
                }
                else
                {
                    isNew = true;
                }
            }
            else
            {
                isNew = true;
            }

            // קביעת הכותרת המתאימה
            if (!isNew)
            {
                txtHeader.Text = "Edit Lesson";
            }
            else
            {
                txtHeader.Text = "New A New Lesson";
            }
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            this.year = year;
            this.month = monthOfYear;
            this.day = dayOfMonth;
            date = new DateTime(year, month, day);
            Toast.MakeText(this, "You have selected: " + day + "/" + (month + 1) + "/" + year, ToastLength.Short).Show();
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minuteOfHour)
        {
            hour = hourOfDay;
            minuts = minuteOfHour;

            SimpleDateFormat timeFormat = new SimpleDateFormat("hh:mm:aa");
            time = new DateTime(date.Year, date.Month, date.Day, hour, minuts, 0);
            Toast.MakeText(this, "You have selected: " + time.ToShortTimeString(), ToastLength.Short).Show();
        }
    }
}