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

namespace MODEL
{
    public class LessonType : BaseEntity
    {
        private string name;

        public LessonType()
        {
        }

        public LessonType(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
    }
}