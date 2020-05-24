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

using MODEL;
using HELPER;
using Android.Support.V7.App;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "ListViewActivity")]
    public class ListViewActivity : AppCompatActivity
    {
        private ListView    lvCities;
        private EditText    etCity;
        private ImageButton btnOk;
        private ImageButton btnCancel;
        private TextView    txtHeader;

        private Cities               cities;
        private ArrayAdapter<string> adapter;

        int position = -1;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //SetContentView(Resource.Layout.cities_layout);

            SetViews();

            txtHeader.Text = "Cities list";
            etCity.Hint    = "New City";

            cities = new Cities();
            cities = cities.SelectAll();

            RefreshListView();

            Global.HideKeyboard(this, true);
        }

        private void SetViews()
        {
            //lvCities  = FindViewById<ListView>(Resource.Id.lvCities);
            //etCity    = FindViewById<EditText>(Resource.Id.etCity);
            //btnOk     = FindViewById<ImageButton>(Resource.Id.btnOk);
            //btnCancel = FindViewById<ImageButton>(Resource.Id.btnCancel);
            //txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);

            lvCities.ItemClick     += LvCities_ItemClick;
            lvCities.ItemLongClick += LvCities_ItemLongClick;

            btnOk.Click     += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void RefreshListView()
        {
//            citiesList = new List<string>();
            
//            foreach (City c in cities)
//                citiesList.Add(c.Name);

//            //List<string> orderedNames = people.Select(person => person.FirstName).OrderBy(name => name).ToList();

////            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, citiesList);
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, cities.Select(city => city.Name).OrderBy(name => name).ToList());
            lvCities.Adapter = adapter;
        }

        private void LvCities_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            position = e.Position;
            etCity.Text = cities[position].Name;
        }

        private void LvCities_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

            alertDiag.SetTitle("Confirm delete");
            alertDiag.SetMessage("Once '" + cities[e.Position].Name + "' deleted the move cannot be undone");

            alertDiag.SetCancelable(true);

            alertDiag.SetPositiveButton("Delete", (EventHandler<DialogClickEventArgs>)((senderAlert, args)
            => {
                City city = cities[e.Position];

                if (city.Id != 0)
                    city.EntityStatus = EntityStatus.DELETED;
                else
                    cities.Remove(city);

                cities.Save();

                RefreshListView();

                alertDiag.Dispose();
            }));

            alertDiag.SetNegativeButton("Cancel", (senderAlert, args)
            => {
                alertDiag.Dispose();
            });

            Dialog diag = alertDiag.Create();
            diag.Show();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            bool isNew = position == -1;
            bool dataSetChanged = false;

            Global.HideKeyboard(this);

            if (etCity.Text != "")
            {
                City city = new City(etCity.Text);

                if (isNew)
                {
                    if (!cities.Exists(city))
                    {
                        city.EntityStatus = EntityStatus.ADDED;
                        cities.Add(city);
                        dataSetChanged = true;
                    }
                }
                else
                {
                    city.Id = cities[position].Id;
                    city.EntityStatus = cities[position].EntityStatus;

                    if (city.Id != 0)
                        city.EntityStatus = EntityStatus.MODIFIED;

                    if (!cities.Exists(city, true))
                    {
                        cities[position] = city;
                        dataSetChanged = true;
                    }
                }

                if (dataSetChanged)
                {
                    etCity.Text = "";
                    position = -1;

                    cities.Sort();

                    RefreshListView();
                }
                else
                {
                    Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

                    alertDiag.SetTitle("Exists");
                    alertDiag.SetMessage(city.Name + " already exists");

                    alertDiag.SetCancelable(true);

                    alertDiag.SetPositiveButton("OK", (senderAlert, args)
                    =>
                    {
                        alertDiag.Dispose();
                    });

                    Dialog diag = alertDiag.Create();
                    diag.Show();
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            etCity.Text = "";
            position    = -1;
        }

        protected override void OnStop()
        {
            base.OnStop();
            cities.Save();
        }
    }
}