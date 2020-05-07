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
    public class Teachers : BaseList<Teacher>
    {
        public Teachers()
        {

        }
        public override bool Exists(Teacher t, bool forChange = false)
        {
            if (!forChange)
            {
                return base.Exists(item => item.Email == t.Email);
            }
            else
            {
                return base.Exists(item => item.Email == t.Email && item.Id == t.Id);
            }
        }

        public override void Sort()
        {
            base.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));
        }

        public Teachers SelectAll()
        {
            List<Teacher> list = DbTable<Teacher>.SelectQuery("SELECT * FROM Teachers ORDER BY Name");

            Teachers teachers = new Teachers();

            if (list != null)
                teachers.AddRange(list);

            return teachers;
        }

        public int Insert(Teacher teacher)
        {
            return DbTable<Teacher>.Insert(teacher);
        }
        public int Update(Teacher teacher)
        {
            return DbTable<Teacher>.Update(teacher);
        }
        public int Delete(Teacher teacher)
        {
            return DbTable<Teacher>.Delete(teacher);
        }
    }
}