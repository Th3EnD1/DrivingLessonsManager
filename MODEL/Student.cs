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
    [Serializable]
    public class Student : Teacher
    {
        private string tz;
        private int teacherId;
        private string cb1;
        private string cb2;
        private string cb3;
        private string cb4;
        private string cb5;
        private string cb6;
        private string cb7;
        private string cb8;

        public Student()
        {

        }

        public Student(string tz, int teacherId, string cb1, string cb2, string cb3, string cb4, string cb5, string cb6, string cb7, string cb8)
        {
            this.tz = tz;
            this.teacherId = teacherId;
            this.cb1 = cb1;
            this.cb2 = cb2;
            this.cb3 = cb3;
            this.cb4 = cb4;
            this.cb5 = cb5;
            this.cb6 = cb6;
            this.cb7 = cb7;
            this.cb8 = cb8;
        }



        public string Tz { get => tz; set => tz = value; }
        public int TeacherId { get => teacherId; set => teacherId = value; }
        public string Cb1 { get => cb1; set => cb1 = value; }
        public string Cb2 { get => cb2; set => cb2 = value; }
        public string Cb3 { get => cb3; set => cb3 = value; }
        public string Cb4 { get => cb4; set => cb4 = value; }
        public string Cb5 { get => cb5; set => cb5 = value; }
        public string Cb6 { get => cb6; set => cb6 = value; }
        public string Cb7 { get => cb7; set => cb7 = value; }
        public string Cb8 { get => cb8; set => cb8 = value; }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   base.Equals(obj) &&
                   tz == student.tz &&
                   teacherId == student.teacherId &&
                   cb1 == student.cb1 &&
                   cb2 == student.cb2 &&
                   cb3 == student.cb3 &&
                   cb4 == student.cb4 &&
                   cb5 == student.cb5 &&
                   cb6 == student.cb6 &&
                   cb7 == student.cb7 &&
                   cb8 == student.cb8;
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