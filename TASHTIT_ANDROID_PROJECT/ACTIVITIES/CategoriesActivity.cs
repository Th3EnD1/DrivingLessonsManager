using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using HELPER;
using MODEL;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "CategoriesActivity")]
    public class CategoriesActivity : AppCompatActivity
    {
        private ListView lvCategories;
        private EditText etCategory;
        private ImageButton btnOk;
        private ImageButton btnCancel;
        private TextView txtHeader;

        private Categories categories;
        private ArrayAdapter<string> adapter;

        int position = -1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListOfCategoriesOrLessonType);

            SetViews();

            txtHeader.Text = "Categories list";
            etCategory.Hint = "New Category";
            //etCity.InputType = Android.Text.InputTypes.ClassNumber;

            categories = new Categories();
            categories = categories.SelectAll();

            RefreshListView();

            Global.HideKeyboard(this);
        }

        private void SetViews()
        {
            lvCategories  = FindViewById<ListView>(Resource.Id.lvCategories);
            etCategory    = FindViewById<EditText>(Resource.Id.etCategory);
            btnOk     = FindViewById<ImageButton>(Resource.Id.btnOk);
            btnCancel = FindViewById<ImageButton>(Resource.Id.btnCancel);
            txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);

            lvCategories.ItemClick += LvCities_ItemClick;
            lvCategories.ItemLongClick += LvCities_ItemLongClick;

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void RefreshListView()
        {
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, categories.Select(category => category.Name).OrderBy(name => name).ToList());
            lvCategories.Adapter = adapter;
        }

        private void LvCities_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            position = e.Position;
            etCategory.Text = categories[position].Name;
        }

        private void LvCities_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

            alertDiag.SetTitle("Confirm delete");
            alertDiag.SetMessage("Once '" + categories[e.Position].Name + "' deleted the move cannot be undone");

            alertDiag.SetCancelable(true);

            alertDiag.SetPositiveButton("Delete", (EventHandler<DialogClickEventArgs>)((senderAlert, args)
            => {
                Category category = categories[e.Position];

                if (category.Id != 0)
                    category.EntityStatus = EntityStatus.DELETED;
                else
                    categories.Remove(category);

                categories.Save();

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

            if (etCategory.Text != "")
            {
                if (!etCategory.Text.StartsWith('0') || etCategory.Text.Length > 3)
                {
                    Global.ToastCenteredText(this, "Area code should follow \n 1. Start with '0' \n 2. Maximum 3 digits", ToastLength.Long);
                }
                else
                {
                    Category category = new Category(etCategory.Text);

                    if (isNew)
                    {
                        if (!categories.Exists(category))
                        {
                            category.EntityStatus = EntityStatus.ADDED;
                            categories.Add(category);
                            dataSetChanged = true;
                        }
                    }
                    else
                    {
                        category.Id = categories[position].Id;
                        category.EntityStatus = categories[position].EntityStatus;

                        if (category.Id != 0)
                            category.EntityStatus = EntityStatus.MODIFIED;

                        if (!categories.Exists(category, true))
                        {
                            categories[position] = category;
                            dataSetChanged = true;
                        }
                    }

                    if (dataSetChanged)
                    {
                        etCategory.Text = "";
                        position = -1;

                        categories.Sort();

                        RefreshListView();
                    }
                    else
                    {
                        Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

                        alertDiag.SetTitle("Exists");
                        alertDiag.SetMessage(category.Name + " already exists");

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
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            etCategory.Text = "";
            position = -1;
        }

        protected override void OnStop()
        {
            base.OnStop();
            categories.Save();
        }
    }
}