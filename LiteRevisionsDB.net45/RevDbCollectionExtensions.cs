using LiteDB;

namespace LiteRevisionsDB.net45
{
    public static class RevDbCollectionExtensions
    {
        private const string VERSION1 = "version1";

        public static LiteCollection<Versioned<T>> Version1<T>(this LiteDatabase db)
            => db.GetCollection<Versioned<T>>(VERSION1);
    }
}
