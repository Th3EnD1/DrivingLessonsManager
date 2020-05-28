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
        private DateTime date;
        private DateTime time;
        private bool paid;
        private string details;
        private double cost;

        public Lesson(int studentNo, int lessonTypeNo, DateTime date, DateTime time, bool paid, string details, double cost)
        {
            this.studentNo = studentNo;
            this.lessonTypeNo = lessonTypeNo;
            this.date = date;
            this.time = time;
            this.paid = paid;
            this.details = details;
            this.cost = cost;
        }

        public Lesson() { }

        public int LessonTypeNo { get => lessonTypeNo; set => lessonTypeNo = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool Paid { get => paid; set => paid = value; }
        public string Details { get => details; set => details = value; }
        public int StudentNo { get => studentNo; set => studentNo = value; }
        public DateTime Time { get => time; set => time = value; }
        public double Cost { get => cost; set => cost = value; }

        public override bool Equals(object obj)
        {
            return obj is Lesson lesson &&
                   base.Equals(obj) &&
                   studentNo == lesson.studentNo &&
                   lessonTypeNo == lesson.lessonTypeNo &&
                   date == lesson.date &&
                   time == lesson.time &&
                   paid == lesson.paid &&
                   details == lesson.details &&
                   cost == lesson.cost;
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