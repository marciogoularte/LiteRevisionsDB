using LiteDB;
using System.IO;

namespace LiteRevisionsDB.net45
{
    public static class RevDbConnectionExtensions
    {
        public static LiteDatabase OpenRead<T>(this LiteRevisionsDB<T> revDb)
        {
            if (revDb._mem == null && !File.Exists(revDb.DbPath))
            {
                var db = revDb.Connect(LiteDB.FileMode.Shared);
                db.Dispose();
            }
            return revDb.Connect(LiteDB.FileMode.ReadOnly);
        }


        public static LiteDatabase OpenWrite<T>(this LiteRevisionsDB<T> revDb)
            => revDb.Connect(LiteDB.FileMode.Shared);



        private static LiteDatabase Connect<T>(this LiteRevisionsDB<T> revDb, LiteDB.FileMode fileMode)
        {
            var db = revDb._mem != null
                   ? new LiteDatabase(revDb._mem)
                   : new LiteDatabase(new ConnectionString
                   {
                       Filename  = revDb.DbPath,
                       Journal   = false,
                       Mode      = fileMode,
                       LimitSize = long.MaxValue
                   });

            if (fileMode != LiteDB.FileMode.ReadOnly)
                db.Version1<T>().EnsureIndex(nameof(Versioned<T>.GroupId));

            return db;
        }
    }
}
