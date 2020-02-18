using System;
using System.Collections.Generic;
using System.Text;

using DAL;

namespace MODEL
{
    public class Cities : BaseList<City>
    {
        public Cities() { }

        public Cities SelectAll()
        {
            List<City> list = DbTable<City>.SelectQuery("SELECT * FROM Cities ORDER BY Name");

            Cities cities = new Cities();

            if (list != null)
                cities.AddRange(list);

            return cities;
        }

        public override bool Save()
        {
            GenereteUpdateLists();

            if (InsertList.Count > 0)
                foreach (City c in InsertList)
                    DbTable<City>.Insert(c);

            if (UpdateList.Count > 0)
                foreach (City c in UpdateList)
                    DbTable<City>.Update(c);

            if (DeleteList.Count > 0)
                foreach (City c in DeleteList)
                    DbTable<City>.Delete(c);

            //foreach (City c in this)
            //    if (c.EntityStatus == EntityStatus.ADDED)
            //    {
            //        DbTable<City>.Insert(c);
            //    }
            //    else
            //        if (c.EntityStatus == EntityStatus.MODIFIED)
            //    {
            //        DbTable<City>.Update(c);
            //    }
            //    else
            //    {
            //        if (c.EntityStatus == EntityStatus.DELETED)
            //        {
            //            DbTable<City>.Delete(c);
            //        }
            //    }

            return base.Save();
        }

        public override bool Exists(City c, bool forChange = false)
        {
            bool exists;

            if (!forChange)
                exists = base.Exists(item => item.Name.Equals(c.Name));
            else
                exists = base.Exists(item => item.Name.Equals(c.Name) && item.Id != c.Id);

            return exists;
        }

        public override void Sort()
        {
            base.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));
        }

    }
}
