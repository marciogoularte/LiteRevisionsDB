using System.IO;

namespace LiteRevisionsDB.net45
{
    public class LiteRevisionsDB<T>
    {
        internal MemoryStream _mem;


        public LiteRevisionsDB(string dbFilePath)
        {
            DbPath = dbFilePath;
        }


        public LiteRevisionsDB(MemoryStream memoryStream)
        {
            _mem = memoryStream;
        }


        public string  DbPath  { get; }
    }
}
