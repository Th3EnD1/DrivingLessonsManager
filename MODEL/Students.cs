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
using DAL;

namespace MODEL
{
    public class Students : BaseList<Student>
    {
        public Students()
        {

        }
        public override bool Exists(Student t, bool forChange = false)
        {
            if (!forChange)
            {
                return base.Exists(item => item.Tz == t.Tz);
            }
            else
            {
                return base.Exists(item => item.Tz == t.Tz && item.Id == t.Id);
            }
        }

        public override void Sort()
        {
            base.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));
        }

        public Students SelectAll()
        {
            List<Student> list = DbTable<Student>.SelectQuery("SELECT * FROM Students ORDER BY Name");

            Students students = new Students();

            if (list != null)
                students.AddRange(list);

            return students;
        }

        public int Insert(Student student)
        {
            return DbTable<Student>.Insert(student);
        }
        public int Update(Student student)
        {
            return DbTable<Student>.Update(student);
        }
        public int Delete(Student student)
        {
            return DbTable<Student>.Delete(student);
        }
    }
}