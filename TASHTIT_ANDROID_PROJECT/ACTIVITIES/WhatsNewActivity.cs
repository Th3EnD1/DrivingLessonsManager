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
        private IList<string> cbString;
        private List<CheckBox> cbList;

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

            IList<String> cb = Intent.GetStringArrayListExtra("CHECK");

            for (int i = 0; i < cb.Count; i++)
            {
                if (cb[i] == "true")
                    cbList[i].Checked = true;
                else
                    cbList[i].Checked = false;
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbList.Count; i++)
            {
                if (cbList[i].Checked == true)
                    cbString[i] = "true";
                else
                    cbString[i] = "false";
            }
            Intent intent = new Intent(this, typeof(StudentActivity));
            intent.PutStringArrayListExtra("CHECK", cbString);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.WhatsNewCheckList);
            SetViews();
            cbList = new List<CheckBox>();
            cbList.Add(checkBoxEye);
            cbList.Add(checkBoxDoc);
            cbList.Add(checkBoxGreen);
            cbList.Add(checkBoxTheory);
            cbList.Add(checkBoxInnerTest);
            cbList.Add(checkBoxOutterTest);
            cbList.Add(checkBoxMorning);
            cbList.Add(checkBoxEvening);
            cbString = new List<string>();
        }
    }
}