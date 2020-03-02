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
    public class LessonTypes : BaseList<LessonType>
    {
        public LessonTypes() { }
        public LessonTypes SelectAll()
        {
            List<LessonType> list = DbTable<LessonType>.SelectQuery("SELECT * FROM LessonTypes ORDER BY Name");

            LessonTypes lessonTypes = new LessonTypes();

            if (list != null)
                lessonTypes.AddRange(list);

            return lessonTypes;
        }

        public override bool Save()
        {
            GenereteUpdateLists();

            if (InsertList.Count > 0)
                foreach (LessonType l in InsertList)
                    DbTable<LessonType>.Insert(l);

            if (UpdateList.Count > 0)
                foreach (LessonType l in UpdateList)
                    DbTable<LessonType>.Update(l);

            if (DeleteList.Count > 0)
                foreach (LessonType l in DeleteList)
                    DbTable<LessonType>.Delete(l);

            return base.Save();
        }

        public override bool Exists(LessonType t, bool forChange = false)
        {
            bool exists;

            if (!forChange)
                exists = base.Exists(item => item.Name.Equals(t.Name));
            else
                exists = base.Exists(item => item.Name.Equals(t.Name) && item.Id != t.Id);

            return exists;
        }

        public override void Sort()
        {
            base.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));
        }
    }
}