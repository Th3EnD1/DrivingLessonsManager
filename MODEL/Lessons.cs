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

        public Lessons SelectLessonsForTeacher(Student student, int teacherId)
        {
            List<Lesson> list = DbTable<Lesson>.SelectQuery("SELECT * FROM Lessons WHERE TeacherId=" + student.TeacherId);

            Lessons lessons = new Lessons();

            if (list != null)
                lessons.AddRange(list);

            return lessons;
        }

        public Lessons SelectLessonsForStudent(Student student)
        {
            List<Lesson> list = DbTable<Lesson>.SelectQuery("SELECT * FROM Lessons WHERE StudentNo=" + student.Id);

            Lessons lessons = new Lessons();

            if (list != null)
                lessons.AddRange(list);

            return lessons;
        }

        public Lessons SelectPaidLessons(Student student)
        {
            List<Lesson> list = DbTable<Lesson>.SelectQuery("SELECT * FROM Lessons WHERE StudentNo=" + student.Id + " AND Paid=" + true);

            Lessons lessons = new Lessons();

            if (list != null)
                lessons.AddRange(list);

            return lessons;
        }

        public Lessons SelectNotPaidLessons(Student student)
        {
            List<Lesson> list = DbTable<Lesson>.SelectQuery("SELECT * FROM Lessons WHERE StudentNo=" + student.Id + " AND Paid=" + false);

            Lessons lessons = new Lessons();

            if (list != null)
                lessons.AddRange(list);

            return lessons;
        }

        public Lessons SelectLessonsLeft(Student student)
        {
            DateTime d = DateTime.Now;

            List<Lesson> list = DbTable<Lesson>.SelectQuery("SELECT * FROM Lessons WHERE StudentNo=" + student.Id + " AND Date>" + d + " AND Time>" + d);

            Lessons lessons = new Lessons();

            if (list != null)
                lessons.AddRange(list);

            return lessons;
        }

        public int InsertDb(Lesson lesson)
        {
            return DbTable<Lesson>.Insert(lesson);
        }

        public int DeleteDb(Lesson lesson)
        {
            return DbTable<Lesson>.Delete(lesson);
        }
    }
}