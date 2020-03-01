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
    public class Categories : BaseList<Category>
    {
        public override bool Exists(Category t, bool forChange = false)
        {
            throw new NotImplementedException();
        }

        public override void Sort()
        {
            throw new NotImplementedException();
        }
    }
}