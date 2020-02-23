using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DAL;
using Plugin.CloudFirestore;

namespace MODEL.FireStore
{
    public class FireStoreLesson
    {
        private const string LESSONS_COLLECTION = "lessons";

        public static async Task<Lessons> GetAllLessonsAsync()
        {
            Lessons lessons = new Lessons();
            List<Lesson> lessonList = new List<Lesson>();

            try
            {
                IQuerySnapshot query = await FireStoreDB.Connection
                .GetCollection(LESSONS_COLLECTION)
                .GetDocumentsAsync();

                lessonList = query.ToObjects<Lesson>().ToList();

                lessons.AddRange(lessonList);
            }
            catch (Exception e)
            {

            }

            return lessons;

        }


        public static async Task<bool> Add(Lesson lesson)
        {
            try
            {
                int id = await GetNextId();

                if (id > 0)
                {
                    lesson.Id = id;

                    await FireStoreDB.Connection
                         .GetCollection(LESSONS_COLLECTION)
                         .GetDocument(id.ToString())
                         .SetDataAsync(lesson);

                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        private static async Task<int> GetNextId()
        {
            int id;

            try
            {
                IQuerySnapshot query = await FireStoreDB.Connection
     .GetCollection(LESSONS_COLLECTION)
     .GetDocumentsAsync();

                id = query.Count + 1;

                // Check if there is a document with "id"
                var document = await FireStoreDB.Connection
.GetCollection("lessons")
.GetDocument(id.ToString())
                                 .GetDocumentAsync();

                // Find the next "id" available
                while (document.ToObject<Lesson>() != null)
                {
                    id++;

                    document = await FireStoreDB.Connection
                                .GetCollection("lessons")
                                .GetDocument(id.ToString())
                                 .GetDocumentAsync();
                }

                return id;
            }
            catch
            {
                return -1;
            }
        }


        public static async Task<bool> Update(Lesson user)
        {
            try
            {
                await FireStoreDB.Connection
                    .GetCollection(LESSONS_COLLECTION)
                    .GetDocument(user.Id.ToString())
                    .UpdateDataAsync(user);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        public static async Task<bool> Delete(Lesson lesson)
        {
            try
            {
                await FireStoreDB.Connection
                    .GetCollection(LESSONS_COLLECTION)
                    .GetDocument(lesson.Id.ToString())
                    .DeleteDocumentAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}