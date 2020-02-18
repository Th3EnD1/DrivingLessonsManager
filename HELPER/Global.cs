using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HELPER
{
    public class Global
    {
        public static void ToastCenteredText(Context context, string message, ToastLength toastLength)
        {
            Toast toast = Toast.MakeText(context, message, toastLength);
            LinearLayout layout = (LinearLayout)toast.View;

            if (layout.ChildCount > 0)
            {
                TextView tv = (TextView)layout.GetChildAt(0);
                tv.Gravity = GravityFlags.Center;
            }
            toast.Show();
        }

        public static void ToastCenteredText(Context context, int message, ToastLength toastLength)
        {
            ToastCenteredText(context, context.Resources.GetString(message), toastLength);
        }

        public static void HideKeyboardOnCreate(Android.App.Activity activity)
        {
            activity.Window.SetSoftInputMode(SoftInput.StateHidden); // getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN);
        }

        public static void HideKeyboard(Android.App.Activity activity, bool onCreate = false)
        {
            if (!onCreate)
            {
                // Check if no view has focus:
                View view = activity.CurrentFocus; // GetCurrentFocus();
                if (view != null)
                {
                    Android.Views.InputMethods.InputMethodManager imm = (Android.Views.InputMethods.InputMethodManager)activity.GetSystemService(Context.InputMethodService /*INPUT_METHOD_SERVICE*/);
                    imm.HideSoftInputFromWindow(view.WindowToken /*GetWindowToken()*/, 0);
                }
            }
            else
            {
                activity.Window.SetSoftInputMode(SoftInput.StateHidden); // getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN);
            }
        }
    }
}
