using System;
using System.Collections.Generic;
using System.Text;

namespace MODEL
{
    public class Persons : BaseList<Person>
    {
        public Persons() { }

        public override bool Exists(Person p, bool forChange = false)
        {
            bool exists;

            if (!forChange)
                exists = base.Exists(item => item.Name.Equals(p.Name) && item.Family.Equals(p.Family));
            else
                exists = base.Exists(item => item.Name.Equals(p.Name) && item.Family.Equals(p.Family) && item.Id != p.Id);

            return exists;
        }

        public override void Sort()
        {
            base.Sort((item1, item2) =>
                {
                    int isSame = item1.Family.CompareTo(item2.Family);
                    return (isSame != 0) ? isSame : item1.Name.CompareTo(item2.Name);
                }
             );
        }
    }
}
