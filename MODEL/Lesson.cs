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
        private int studentNo;
        private int lessonTypeNo;
        private int categoryNo;
        private DateTime date;
        private bool paid;
        private string details;

        public Lesson() { }

        public Lesson(int lessonTypeNo, DateTime date, bool paid = false)
        {
            this.date = date;
            this.lessonTypeNo = lessonTypeNo;
            this.paid = paid;
        }

        public int LessonTypeNo { get => lessonTypeNo; set => lessonTypeNo = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool Paid { get => paid; set => paid = value; }
        public string Details { get => details; set => details = value; }

        public override bool Equals(object obj)
        {
            return obj is Lesson lesson &&
                   base.Equals(obj) &&
                   studentNo == lesson.studentNo &&
                   lessonTypeNo == lesson.lessonTypeNo &&
                   categoryNo == lesson.categoryNo &&
                   date == lesson.date &&
                   paid == lesson.paid &&
                   details == lesson.details;
        }
    }

    //public enum LessonType
    //{
    //    Regular,
    //    OneAndHalf,
    //    Double,
    //    Triple,
    //    InTest,
    //    OutTest
    //}
}