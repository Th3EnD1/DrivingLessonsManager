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

namespace TASHTIT_ANDROID_PROJECT.ADAPTERS
{
    public class LessonsAdapter : ArrayAdapter<Lesson>
    {
        private Context context;
        private Lessons lessons;
        private int resource;
        private LayoutInflater inflater;

        // ViewHolder הכרזה על אובייקט
        private ViewHolder viewHolder;

        private Lesson lesson;


        public LessonsAdapter(Context context, int resource, Lessons lessons)
            : base(context, resource, lessons)
        {
            this.context = context;
            this.resource = resource;
            this.lessons = lessons;
            inflater = ((Activity)context).LayoutInflater;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = inflater.Inflate(resource, parent, false);

                viewHolder = new ViewHolder();

                viewHolder.txtDate = convertView.FindViewById<TextView>(Resource.Id.txtDate);
                viewHolder.txtTime = convertView.FindViewById<TextView>(Resource.Id.txtTime);
                viewHolder.checkBoxPaid = convertView.FindViewById<CheckBox>(Resource.Id.checkBoxPaid);
                viewHolder.txtType = convertView.FindViewById<TextView>(Resource.Id.txtType);
                

                // viewHolderשמירת האובייקט 
                // TAG במאפיין
                // convertView של האובייקט
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }

            lesson = GetItem(position);

            if (lesson != null)
            {
                viewHolder.txtDate.Text = lesson.Date.ToShortDateString();
                viewHolder.txtTime.Text = lesson.Time.ToShortTimeString();
                viewHolder.txtType.Text = lesson.LessonTypeNo.ToString();
                viewHolder.checkBoxPaid.Checked = lesson.Paid;
            }

            viewHolder.checkBoxPaid.Enabled = false;

            return convertView;
        }


        public class ViewHolder : Java.Lang.Object
        {
            public TextView txtDate;
            public TextView txtTime;
            public TextView txtType;
            public CheckBox checkBoxPaid;
        }

    }
}