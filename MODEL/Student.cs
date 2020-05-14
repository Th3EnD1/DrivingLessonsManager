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
using SQLite;

namespace MODEL
{
    [Table("Students")]
    public class Student : Teacher
    {
        private string tz;
        private int teacherId;

        public Student()
        {

        }

        public Student(string tz, int teacherId)
        {
            this.tz = tz;
            this.teacherId = teacherId;
        }



        public string Tz { get => tz; set => tz = value; }
        public int TeacherId { get => teacherId; set => teacherId = value; }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   tz == student.tz &&
                   teacherId == student.teacherId;
        }

        public static bool operator ==(Student left, Student right)
        {
            return EqualityComparer<Student>.Default.Equals(left, right);
        }

        public static bool operator !=(Student left, Student right)
        {
            return !(left == right);
        }
    }
}