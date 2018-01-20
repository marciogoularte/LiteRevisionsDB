using LiteDB;
using System;

namespace LiteRevisionsDB.net45
{
    public static class RevDbInsertExtensions
    {
        public static Versioned<T> Insert<T>(this LiteRevisionsDB<T> revDb,
            T doc, string author, string changeLog = null)
        {
            using (var db = revDb.OpenWrite())
                return db.InsertVersioned(new Versioned<T>
                {
                    GroupId    = db.CountLatests<T>() + 1,
                    Content    = doc,
                    ChangeLog  = changeLog,
                    ChangedBy  = author,
                    ChangeDate = DateTime.Now
                });
        }


        internal static Versioned<T> InsertVersioned<T>(
            this LiteDatabase db, Versioned<T> verDoc)
        {
            var id    = db.Version1<T>().Insert(verDoc);
            verDoc.Id = id;
            return verDoc;
        }
    }
}
