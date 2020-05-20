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
using HELPER;

namespace MODEL
{
    public class Diaries : BaseList<Diary>
    {
        public Diaries SelectAll()
        {
            List<Diary> list = DbTable<Diary>.SelectQuery("SELECT * FROM Diary ORDER BY Date");

            Diaries diaries = new Diaries();

            if (list != null)
                diaries.AddRange(list);

            return diaries;
        }

        public Diaries SelectAll(DateTime date, DateTime time)
        {
            string d = Global.GetSqLiteDate( DateTime.Now);

            List<Diary> list = DbTable<Diary>.SelectQuery("SELECT * FROM Diary WHERE date(date)<=date('" + d + ") ORDER BY Date");

            Diaries diaries = new Diaries();

            if (list != null)
                diaries.AddRange(list);

            return diaries;
        }

        public Diaries SelectAll(Student student)
        {
            List<Diary> list = DbTable<Diary>.SelectQuery("SELECT * FROM Diary WHERE StudentName='" + student.Name + "' ORDER BY Date");

            Diaries diaries = new Diaries();

            if (list != null)
                diaries.AddRange(list);

            return diaries;
        }

        public int Insert(Diary diary)
        {
            return DbTable<Diary>.Insert(diary);
        }
        public int Update(Diary diary)
        {
            return DbTable<Diary>.Update(diary);
        }
        public int Delete(Diary diary)
        {
            return DbTable<Diary>.Delete(diary);
        }

        public override bool Exists(Diary t, bool forChange = false)
        {
            if (!forChange)
                return base.Exists(item => item.Date.Equals(t.Date));
            else
                return base.Exists(item => item.Date.Equals(t.Date) && item.Id.Equals(t.Id));
        }

        public override void Sort()
        {
            base.Sort((item1, item2) => item1.Date.CompareTo(item2.Date));
        }
    }
}