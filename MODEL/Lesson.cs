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
    [Serializable]
    public class Lesson : BaseEntity
    {
        private int studentNo;
        private int lessonTypeNo;
        private int categoryNo;
        private DateTime date;
        private bool paid;
        private string details;

        public Lesson() { }

        public Lesson(int studentNo, int lessonTypeNo, int categoryNo, DateTime date, bool paid, string details)
        {
            this.studentNo = studentNo;
            this.lessonTypeNo = lessonTypeNo;
            this.categoryNo = categoryNo;
            this.date = date;
            this.paid = paid;
            this.details = details;
        }

        public int LessonTypeNo { get => lessonTypeNo; set => lessonTypeNo = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool Paid { get => paid; set => paid = value; }
        public string Details { get => details; set => details = value; }
        public int StudentNo { get => studentNo; set => studentNo = value; }
        public int CategoryNo { get => categoryNo; set => categoryNo = value; }

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

    public enum LessonTypeEnum
    {
        Regular,
        OneAndHalf,
        Double,
        Triple,
        InTest,
        OutTest
    }
}