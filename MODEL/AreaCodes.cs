using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;

namespace MODEL
{
    public class AreaCodes : BaseList<AreaCode>
    {
        public AreaCodes() { }

        public AreaCodes SelectAll()
        {
            List<AreaCode> list = DbTable<AreaCode>.SelectQuery("SELECT * FROM AreaCodes ORDER BY Name");

            AreaCodes areaCodes = new AreaCodes();

            if (list != null)
                areaCodes.AddRange(list);

            return areaCodes;
        }

        public override bool Save()
        {
            GenereteUpdateLists();

            if (InsertList.Count > 0)
                foreach (AreaCode c in InsertList)
                    DbTable<AreaCode>.Insert(c);

            if (UpdateList.Count > 0)
                foreach (AreaCode c in UpdateList)
                    DbTable<AreaCode>.Update(c);

            if (DeleteList.Count > 0)
                foreach (AreaCode c in DeleteList)
                    DbTable<AreaCode>.Delete(c);

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

        public override bool Exists(AreaCode t, bool forChange = false)
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
