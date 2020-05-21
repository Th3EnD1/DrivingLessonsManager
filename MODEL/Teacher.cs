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
    [Table("Teachers")]
    public class Teacher : BaseEntity
    {
        private string name;
        private string email;
        private string psw;
        private string phone;
        private int cost;

        private DateTime startHour;
        private DateTime endHour;

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Psw { get => psw; set => psw = value; }
        public DateTime StartHour { get => startHour; set => startHour = value; }
        public DateTime EndHour { get => endHour; set => endHour = value; }
        public string Phone { get => phone; set => phone = value; }
        public int Cost { get => cost; set => cost = value; }

        public Teacher()
        {
        }

        public Teacher(string name, string email, string psw, DateTime startHour, DateTime endHour, string phone, int cost)
        {
            this.name = name;
            this.email = email;
            this.psw = psw;
            this.startHour = startHour;
            this.endHour = endHour;
            this.phone = phone;
            this.cost = cost;
        }

        public override bool Equals(object obj)
        {
            return obj is Teacher teacher &&
                   base.Equals(obj) &&
                   name == teacher.name &&
                   email == teacher.email &&
                   psw == teacher.psw &&
                   startHour == teacher.startHour &&
                   endHour == teacher.endHour &&
                   phone == teacher.phone &&
                   cost == teacher.cost;
        }

        public static bool operator ==(Teacher left, Teacher right)
        {
            return EqualityComparer<Teacher>.Default.Equals(left, right);
        }

        public static bool operator !=(Teacher left, Teacher right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return name + " - " + phone;
        }
    }
}