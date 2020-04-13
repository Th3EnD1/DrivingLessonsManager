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

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "NewLesson")]
    public class NewLesson : Activity
    {
        private Button btnSave, btnCancel, btnDate, btnTime;
        private TextView txtDate, txtTime, txtHeader;
        private DatePickerDialog datePicker;
        private Lesson lesson;
        private LessonTypes lessonTypes;
        private Spinner spnType;
        private bool isNew;
        private int hour;
        private int minute;
        const int TIME_DIALOG_ID = 0;

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

            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;

            UpdateDisplay();

            btnDate.Click += BtnDate_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            btnTime.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);

            spnType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.type_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnType.Adapter = adapter;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void UpdateDisplay()
        {
            string time = string.Format("{0}:{1}", hour, minute.ToString().PadLeft(2, '0'));
            txtTime.Text = time;
        }

        // Create a Method TimePickerCallback   

        private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            hour = e.HourOfDay;
            minute = e.Minute;
            UpdateDisplay();
        }

        // Create a Method OnCreateDialog   

        protected override Dialog OnCreateDialog(int id)
        {
            if (id == TIME_DIALOG_ID)
                return new TimePickerDialog(this, TimePickerCallback, hour, minute, true);

            return null;
        }

        private void PerformDatePicker()
        {
            DateTime today = DateTime.Today;

            datePicker = new DatePickerDialog(this, //Context 
            OnDateClick, // ***
            today.Year,  // שנה
            today.Month - 1, //חודש
            today.Day);  // שנה
            datePicker.Show();
        }

        private void OnDateClick(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            txtDate.Text =
            e.Date.Day + "/" +
            e.Date.Month + "/" +
            e.Date.Year;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // פירוק התאריך ל-3 מחרוזות 
            string[] dateParts = txtDate.Text.Split(new char[] { '/', '.', '-', ' ' });

            Lesson lesson = new Lesson();

            lesson.Date = new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]));

            lesson.Time = lesson.Date.ToShortTimeString();

            lesson.Paid = false;

            lesson.LessonTypeNo = (int)spnType.SelectedItemId;

            Intent intent = new Intent();

            intent.PutExtra("LESSON", Serializer.ObjectToByteArray(lesson));

            SetResult(Result.Ok, intent);

            if (!isNew)
                lesson.Id = this.lesson.Id;

            Finish();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok)
            {
                base.OnActivityResult(requestCode, resultCode, data);
            }
        }

        private void BtnDate_Click(object sender, EventArgs e)
        {
            PerformDatePicker();
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

            lessonTypes = new LessonTypes();

            if (Intent.Extras != null)
            {
                // Taskבדיקה אם הגיע 
                if (Intent.Extras.ContainsKey("LESSON"))
                {
                    // Taskחילוץ ה-
                    // "דה-סריאליזציה"
                    lesson = Serializer.ByteArrayToObject(Intent.GetByteArrayExtra("LESSON")) as Lesson;

                    // השמת הערכים לעריכה בשדות הקלט

                    spnType.SetSelection(lesson.LessonTypeNo);

                    txtDate.Text = lesson.Date.ToShortDateString();  ///  יכול להיות שיציג חודשים וימים במהופך
                                                                     /// במיקרה כזה יש להרכיב מחרוזת תאריך בטאופן ידני
                    txtTime.Text = lesson.Date.ToShortTimeString();

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
    }
}