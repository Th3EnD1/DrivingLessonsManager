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
    public class Lessons : BaseList<Lesson>
    {
         public override bool Exists(Lesson l, bool forChange = false)
        {
            bool exists;

            if (!forChange)
                exists = base.Exists(item => item.Date.Equals(l.Date) );
            else
                exists = base.Exists(item => item.Date.Equals(l.Date)  && item.Id != l.Id);

            return exists;
        }

        public override void Sort()
        {
            base.Sort((item1, item2) =>
            {
                return item1.Date.CompareTo(item2.Date);
            }
             );
        }
 
    public Lessons SelectAll()
    {
            List<Lesson> list;

            try
            {
                list = DbTable<Lesson>.SelectQuery("SELECT * FROM Lessons");
            }
            catch (Exception e)
            {

                throw;
            }

        Lessons lessons = new Lessons();

        if (list != null)
            lessons.AddRange(list);

        return lessons;
    }

    public Lessons SelectLessonsForStudent(Student student)
    {
           //List<Lesson> list;

           // try
           // {
           //     list = DbTable<Lesson>.SelectQuery("SELECT * FROM Lessons WHERE StudentNo='" + student.Id + "' ORDER BY Name");
           // }
           // catch (Exception e)
           // {

           //     throw;
           // }
        List<Lesson> list = DbTable<Lesson>.SelectQuery("SELECT * FROM Lessons WHERE StudentNo='" + student.Id + "'");

        Lessons lessons = new Lessons();

        if (list != null)
            lessons.AddRange(list);

        return lessons;
    }
}
}