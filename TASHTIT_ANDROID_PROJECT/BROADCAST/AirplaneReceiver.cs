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

namespace TASHTIT_ANDROID_PROJECT.BROADCAST
{
    [BroadcastReceiver]
    public class AirplaneReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            bool status = intent.GetBooleanExtra("state", true);
            if (status)
            {
                Toast.MakeText(context, "Airplane Mode is on.", ToastLength.Short).Show();
            }

            else
            {
                Toast.MakeText(context, "Airplane Mode is off.", ToastLength.Short).Show();
            }
        }
    }
}