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

using Firebase;
using Plugin.CloudFirestore;


namespace DAL
{
    public class FireStoreDB
    {
        private static FireStoreDB instance = null;
        private FirebaseApp app;
        private static IFirestore connection = null;
        public static readonly object padlock = new object();
        private FireStoreDB()
        {
            FirebaseOptions options = new FirebaseOptions.Builder()
             .SetProjectId("drivinglessonmanager")
             .SetApplicationId("drivinglessonmanager")
             .SetApiKey("AIzaSyBSVjF5aYmWxelRle00hV7AIi9FBSci3eg")
             .SetDatabaseUrl("https://drivinglessonmanager.firebaseio.com")
             .SetStorageBucket("drivinglessonmanager.appspot.com")
             .Build();

            app = FirebaseApp.InitializeApp(Application.Context, options);

            connection = CrossCloudFirestore.Current.Instance;
        }

        public static FireStoreDB Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new FireStoreDB();
                        }
                    }
                }
                return instance;
            }
        }

        public static IFirestore Connection
        {
            get
            {
                if (connection == null)
                {
                    lock (padlock)
                    {
                        if (connection == null)
                        {
                            instance = new FireStoreDB();
                        }
                    }
                }
                return connection;
            }
        }
    }
}

