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
        //IList<String> cb;

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

            cbList.Add(checkBoxEye);
            cbList.Add(checkBoxDoc);
            cbList.Add(checkBoxGreen);
            cbList.Add(checkBoxTheory);
            cbList.Add(checkBoxInnerTest);
            cbList.Add(checkBoxOutterTest);
            cbList.Add(checkBoxMorning);
            cbList.Add(checkBoxEvening);

            if (MainActivity.student.Cb != null)
            {
                for (int i = 0; i < MainActivity.student.Cb.Count; i++)
                {
                    if (MainActivity.student.Cb[i] == "true")
                        cbList[i].Checked = true;
                    else
                        cbList[i].Checked = false;
                }
            }
            else
            {
                checkBoxEye.Checked = false;
                checkBoxDoc.Checked = false;
                checkBoxGreen.Checked = false;
                checkBoxTheory.Checked = false;
                checkBoxInnerTest.Checked = false;
                checkBoxOutterTest.Checked = false;
                checkBoxMorning.Checked = false;
                checkBoxEvening.Checked = false;
            }

            btnApply = FindViewById<Button>(Resource.Id.btnApply);

            btnApply.Click += BtnApply_Click;

            //cb = Intent.GetStringArrayListExtra("CHECK");
            //if (cb.Count != 0 || cb != null)
            //{
            //    for (int i = 0; i < cb.Count; i++)
            //    {
            //        if (cb[i] == "true")
            //            cbList[i].Checked = true;
            //        else
            //            cbList[i].Checked = false;
            //    }
            //}
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
                        // Studentבדיקה אם הגיע 
                        //if (data.Extras.ContainsKey("TEACHER"))
                        //{
                        // Studentחילוץ ה-
                        // "דה-סריאליזציה"
                        //TeacherForPick = Serializer.ByteArrayToObject(data.GetByteArrayExtra("TEACHER")) as Teacher;
                        //}

                        if (data.Extras.ContainsKey("CHECK"))
                        {
                            for (int i = 0; i < cbString.Count; i++)
                            {
                                if (cbString[i] == "true")
                                    cbList[i].Checked = true;
                                else
                                    cbList[i].Checked = false;
                            }
                        }
                    }
                }
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
            SetResult(Result.Ok, intent);
            Finish();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.WhatsNewCheckList);
            cbList = new List<CheckBox>();
            SetViews();
            cbString = new List<string>();
            //cb = new List<string>();
            for (int i = 0; i < cbList.Count; i++)
            {
                cbString.Add("false");
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