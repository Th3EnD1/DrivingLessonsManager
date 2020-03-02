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
using HELPER;
using MODEL;

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "TypesActivity")]
    public class TypesActivity : Activity
    {
        private ListView lvTypes;
        private EditText etType;
        private ImageButton btnOk;
        private ImageButton btnCancel;
        private TextView txtHeader;

        private LessonTypes lessonTypes;
        private ArrayAdapter<string> adapter;

        int position = -1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListOfCategoriesOrLessonType);

            SetViews();

            txtHeader.Text = "Types list";
            etType.Hint = "New Type";
            //etCity.InputType = Android.Text.InputTypes.ClassNumber;

            lessonTypes = new LessonTypes();
            lessonTypes = lessonTypes.SelectAll();

            RefreshListView();

            Global.HideKeyboard(this);
        }

        private void SetViews()
        {
            lvTypes = FindViewById<ListView>(Resource.Id.lvCategories);
            etType = FindViewById<EditText>(Resource.Id.etCategory);
            btnOk = FindViewById<ImageButton>(Resource.Id.btnOk);
            btnCancel = FindViewById<ImageButton>(Resource.Id.btnCancel);
            txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);

            lvTypes.ItemClick += lvTypes_ItemClick;
            lvTypes.ItemLongClick += lvTypes_ItemLongClick;

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void RefreshListView()
        {
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lessonTypes.Select(lessonType => lessonType.Name).OrderBy(name => name).ToList());
            lvTypes.Adapter = adapter;
        }

        private void lvTypes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            position = e.Position;
            etType.Text = lessonTypes[position].Name;
        }

        private void lvTypes_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

            alertDiag.SetTitle("Confirm delete");
            alertDiag.SetMessage("Once '" + lessonTypes[e.Position].Name + "' deleted the move cannot be undone");

            alertDiag.SetCancelable(true);

            alertDiag.SetPositiveButton("Delete", (EventHandler<DialogClickEventArgs>)((senderAlert, args)
            => {
                LessonType lessonType = lessonTypes[e.Position];

                if (lessonType.Id != 0)
                    lessonType.EntityStatus = EntityStatus.DELETED;
                else
                    lessonTypes.Remove(lessonType);

                lessonTypes.Save();

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

            if (etType.Text != "")
            {
                if (!etType.Text.StartsWith('0') || etType.Text.Length > 3)
                {
                    Global.ToastCenteredText(this, "Area code should follow \n 1. Start with '0' \n 2. Maximum 3 digits", ToastLength.Long);
                }
                else
                {
                    LessonType lessonType = new LessonType(etType.Text);

                    if (isNew)
                    {
                        if (!lessonTypes.Exists(lessonType))
                        {
                            lessonType.EntityStatus = EntityStatus.ADDED;
                            lessonTypes.Add(lessonType);
                            dataSetChanged = true;
                        }
                    }
                    else
                    {
                        lessonType.Id = lessonTypes[position].Id;
                        lessonType.EntityStatus = lessonTypes[position].EntityStatus;

                        if (lessonType.Id != 0)
                            lessonType.EntityStatus = EntityStatus.MODIFIED;

                        if (!lessonTypes.Exists(lessonType, true))
                        {
                            lessonTypes[position] = lessonType;
                            dataSetChanged = true;
                        }
                    }

                    if (dataSetChanged)
                    {
                        etType.Text = "";
                        position = -1;

                        lessonTypes.Sort();

                        RefreshListView();
                    }
                    else
                    {
                        Android.Support.V7.App.AlertDialog.Builder alertDiag = new Android.Support.V7.App.AlertDialog.Builder(this);

                        alertDiag.SetTitle("Exists");
                        alertDiag.SetMessage(lessonType.Name + " already exists");

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
            etType.Text = "";
            position = -1;
        }

        protected override void OnStop()
        {
            base.OnStop();
            lessonTypes.Save();
        }
    }
}