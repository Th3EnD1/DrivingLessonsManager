using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace MODEL
{
    [Table ("Persons")]
    [Serializable]
    public class Person : BaseEntity
    {
        private string   name;
        private string   family;
        private DateTime born;
        private int      cityNo;
        private int      areaCodeNo;
        private string   phone;
        private string   image;

        public Person() { }

        public Person(string name, string family, DateTime born, int cityNo, int areaCodeNo, string phone, string image)
        {
            this.Name       = name;
            this.Family     = family;
            this.Born       = born;
            this.CityNo     = cityNo;
            this.areaCodeNo = areaCodeNo;
            this.phone      = phone;
            this.Image      = image;
        }

        public string   Name        { get => name;   set => name     = value; }
        public string   Family      { get => family; set => family   = value; }
        public DateTime Born        { get => born;   set => born     = value; }
        public int      CityNo      { get => cityNo; set => cityNo   = value; }
        public int      AreaCodeNo  { get => areaCodeNo; set => areaCodeNo = value; }
        public string   Phone       { get => phone; set => phone = value; }
        public string   Image       { get => image;  set => image    = value; }

        [Ignore]
        public string Age
        {
            get { return ""; }
        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   base.Equals(obj)                 &&
                   name       == person.name        &&
                   family     == person.family      &&
                   born       == person.born        &&
                   cityNo     == person.cityNo      &&
                   areaCodeNo == person.areaCodeNo  &&
                   phone      == person.phone       &&
                   image      == person.image;
        }

        public static bool operator ==(Person left, Person right)
        {
            return EqualityComparer<Person>.Default.Equals(left, right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !(left == right);
        }
    }
}
