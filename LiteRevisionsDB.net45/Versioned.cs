using System;

namespace LiteRevisionsDB.net45
{
    public class Versioned<T>
    {
        public long      Id          { get; set; }
        public long      GroupId     { get; set; }
        public T         Content     { get; set; }
        public string    ChangeLog   { get; set; }
        public string    ChangedBy   { get; set; }
        public DateTime  ChangeDate  { get; set; }
    }
}
