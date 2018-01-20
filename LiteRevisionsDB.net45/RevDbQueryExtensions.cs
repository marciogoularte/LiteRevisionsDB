using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace LiteRevisionsDB.net45
{
    public static class RevDbQueryExtensions
    {
        public static IEnumerable<Versioned<T>> GetLatests<T>(this LiteRevisionsDB<T> revDb)
        {
            using (var db = revDb.OpenRead())
                return db.Version1<T>()
                         .FindAll ()
                         .GroupBy (_ => _.GroupId)
                         .Select  (_ => _.Last());
        }


        public static long NextDocID<T>(this LiteRevisionsDB<T> revDb)
            => revDb.CountAll() + 1;


        public static long CountAll<T>(this LiteRevisionsDB<T> revDb)
        {
            using (var db = revDb.OpenRead())
                return db.CountLatests<T>();
        }


        internal static long CountLatests<T>(this LiteDatabase db)
        {
            var coll = db.Version1<T>();
            if (coll.Count() == 0) return 0;
            return coll.Max(nameof(Versioned<T>.GroupId));
        }


        public static T GetById<T>(this LiteRevisionsDB<T> revDb, long groupId)
            where T : class
        {
            using (var db = revDb.OpenRead())
            {
                var vers = db.Version1<T>()
                             .Find    (_ => _.GroupId == groupId)
                             .OrderBy (_ => _.Id);

                return vers.LastOrDefault()?.Content;
            }
        }
    }
}
