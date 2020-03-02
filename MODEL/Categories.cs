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
    public class Categories : BaseList<Category>
    {
        public Categories() { }

        public Categories SelectAll()
        {
            List<Category> list = DbTable<Category>.SelectQuery("SELECT * FROM Categories ORDER BY Name");

            Categories categories = new Categories();

            if (list != null)
                categories.AddRange(list);

            return categories;
        }

        public override bool Save()
        {
            GenereteUpdateLists();

            if (InsertList.Count > 0)
                foreach (Category c in InsertList)
                    DbTable<Category>.Insert(c);

            if (UpdateList.Count > 0)
                foreach (Category c in UpdateList)
                    DbTable<Category>.Update(c);

            if (DeleteList.Count > 0)
                foreach (Category c in DeleteList)
                    DbTable<Category>.Delete(c);

            return base.Save();
        }

        public override bool Exists(Category t, bool forChange = false)
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