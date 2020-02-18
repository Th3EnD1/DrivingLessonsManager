using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MODEL
{
    [Table ("Lessons")]
    public class Lesson : BaseEntity
    {
        private LessonType lessonType;
        private DateTime date;
        private bool paid;
        private string details;

        public Lesson() { }

        public Lesson(LessonType lessonType, DateTime date, bool paid = false)
        {
            this.date = date;
            this.lessonType = lessonType;
            this.paid = paid;
        }

        public LessonType LessonType { get => lessonType; set => lessonType = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool Paid { get => paid; set => paid = value; }
        public string Details { get => details; set => details = value; }

        public override bool Equals(object obj)
        {
            return obj is Lesson lesson &&
                   base.Equals(obj) &&
                   lessonType == lesson.lessonType &&
                   date == lesson.date &&
                   paid == lesson.paid &&
                   details == lesson.details;
        }
    }

    public enum LessonType
    {
        Regular,
        OneAndHalf,
        Double,
        Triple,
        InTest,
        OutTest
    }
}