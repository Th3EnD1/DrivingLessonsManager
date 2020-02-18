using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace MODEL
{
    [Table ("AreaCodes")]
    public class AreaCode : BaseEntity
    {
        private string name;

        public AreaCode() { }

        public AreaCode(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }

        public override bool Equals(object obj)
        {
            return obj is AreaCode code &&
                   base.Equals(obj) &&
                   name == code.name;
        }

        public static bool operator ==(AreaCode left, AreaCode right)
        {
            return EqualityComparer<AreaCode>.Default.Equals(left, right);
        }

        public static bool operator !=(AreaCode left, AreaCode right)
        {
            return !(left == right);
        }
    }
}
