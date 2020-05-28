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

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "WhatsNewActivity")]
    public class WhatsNewActivity : AppCompatActivity
    {
        private CheckBox checkBoxEye,
                         checkBoxDoc,
                         checkBoxGreen,
                         checkBoxTheory,
                         checkBoxInnerTest,
                         checkBoxOutterTest,
                         checkBoxMorning,
                         checkBoxEvening;
        private Button btnApply;
        private IList<string> cbList;

        public void SetViews()
        {
            checkBoxEye = FindViewById<CheckBox>(Resource.Id.checkBoxEye);
            checkBoxDoc = FindViewById<CheckBox>(Resource.Id.checkBoxDoc);
            checkBoxGreen = FindViewById<CheckBox>(Resource.Id.checkBoxGreen);
            checkBoxTheory = FindViewById<CheckBox>(Resource.Id.checkBoxTheory);
            checkBoxInnerTest = FindViewById<CheckBox>(Resource.Id.checkBoxInnerTest);
            checkBoxOutterTest = FindViewById<CheckBox>(Resource.Id.checkBoxOutterTest);
            checkBoxMorning = FindViewById<CheckBox>(Resource.Id.checkBoxMorning);
            checkBoxEvening = FindViewById<CheckBox>(Resource.Id.checkBoxEvening);

            btnApply = FindViewById<Button>(Resource.Id.btnApply);

            btnApply.Click += BtnApply_Click;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 4)
            {
                if (resultCode == Result.Ok)
                {
                    if (data.Extras != null)
                    {
                        if (data.Extras.ContainsKey("CHECK"))
                        {
                            cbList = data.GetStringArrayListExtra("CHECK");
                        }
                    }
                }
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            cbList[0] = checkBoxEye.Checked.ToString();
            cbList[1] = checkBoxDoc.Checked.ToString();
            cbList[2] = checkBoxGreen.Checked.ToString();
            cbList[3] = checkBoxTheory.Checked.ToString();
            cbList[4] = checkBoxInnerTest.Checked.ToString();
            cbList[5] = checkBoxOutterTest.Checked.ToString();
            cbList[6] = checkBoxMorning.Checked.ToString();
            cbList[7] = checkBoxEvening.Checked.ToString();
            Intent intent = new Intent(this, typeof(StudentActivity));
            intent.PutStringArrayListExtra("CHECK", cbList);
            SetResult(Result.Ok, intent);
            Finish();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.WhatsNewCheckList);
            SetViews();
            if (cbList == null)
            {
                cbList = new List<string>();
                for (int i = 0; i < 8; i++)
                {
                    cbList.Add("false");
                }
                checkBoxEye.Checked = false;
                checkBoxDoc.Checked = false;
                checkBoxGreen.Checked = false;
                checkBoxTheory.Checked = false;
                checkBoxInnerTest.Checked = false;
                checkBoxOutterTest.Checked = false;
                checkBoxMorning.Checked = false;
                checkBoxEvening.Checked = false;
            }
            else
            {
                if (cbList[0] == "true")
                    checkBoxEye.Checked = true;
                else
                    checkBoxEye.Checked = false;

                if (cbList[1] == "true")
                    checkBoxDoc.Checked = true;
                else
                    checkBoxDoc.Checked = false;

                if (cbList[2] == "true")
                    checkBoxGreen.Checked = true;
                else
                    checkBoxGreen.Checked = false;

                if (cbList[3] == "true")
                    checkBoxTheory.Checked = true;
                else
                    checkBoxTheory.Checked = false;

                if (cbList[4] == "true")
                    checkBoxInnerTest.Checked = true;
                else
                    checkBoxInnerTest.Checked = false;

                if (cbList[5] == "true")
                    checkBoxOutterTest.Checked = true;
                else
                    checkBoxOutterTest.Checked = false;

                if (cbList[6] == "true")
                    checkBoxMorning.Checked = true;
                else
                    checkBoxMorning.Checked = false;

                if (cbList[7] == "true")
                    checkBoxEvening.Checked = true;
                else
                    checkBoxEvening.Checked = false;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.mnuMainActivity:
                    {
                        StartActivity(new Intent(this, typeof(MainActivity)));
                        break;
                    }

                case Resource.Id.mnuExit:
                    {
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}