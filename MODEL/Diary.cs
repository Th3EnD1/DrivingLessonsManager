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

namespace MODEL
{
    public class Diary : BaseEntity
    {
        private string studentName;
        private string lessonType;
        private DateTime date;
        private bool paid;

        public Diary() { }

        public Diary(string studentName, string lessonType, DateTime date, bool paid)
        {
            this.studentName = studentName;
            this.lessonType = lessonType;
            this.date = date;
            this.paid = paid;
        }

        public string StudentName { get => studentName; set => studentName = value; }
        public string LessonType { get => lessonType; set => lessonType = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool Paid { get => paid; set => paid = value; }

        public override bool Equals(object obj)
        {
            return obj is Diary diary &&
                   base.Equals(obj) &&
                   studentName == diary.studentName &&
                   lessonType == diary.lessonType &&
                   date == diary.date &&
                   paid == diary.paid;
        }
    }
}