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

namespace TASHTIT_ANDROID_PROJECT.ACTIVITIES
{
    [Activity(Label = "WhatsNewActivity")]
    public class WhatsNewActivity : Activity
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

            Intent intent = new Intent();
            checkBoxEye.Checked = intent.GetBooleanExtra("Eye", false);
            checkBoxDoc.Checked = intent.GetBooleanExtra("Doc", false);
            checkBoxGreen.Checked = intent.GetBooleanExtra("Green", false);
            checkBoxTheory.Checked = intent.GetBooleanExtra("Theory", false);
            checkBoxInnerTest.Checked = intent.GetBooleanExtra("InnerTest", false);
            checkBoxOutterTest.Checked = intent.GetBooleanExtra("OutterTest", false);
            checkBoxMorning.Checked = intent.GetBooleanExtra("Morning", false);
            checkBoxEvening.Checked = intent.GetBooleanExtra("Evening", false);

        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.PutExtra("Eye", checkBoxEye.Checked);
            intent.PutExtra("Doc", checkBoxDoc.Checked);
            intent.PutExtra("Green", checkBoxGreen.Checked);
            intent.PutExtra("Theory", checkBoxTheory.Checked);
            intent.PutExtra("InnerTest", checkBoxInnerTest.Checked);
            intent.PutExtra("OutterTest", checkBoxOutterTest.Checked);
            intent.PutExtra("Morning", checkBoxMorning.Checked);
            intent.PutExtra("Evening", checkBoxEvening.Checked);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.WhatsNewCheckList);
            SetViews();
        }
    }
}