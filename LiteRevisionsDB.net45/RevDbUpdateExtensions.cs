using System;

namespace LiteRevisionsDB.net45
{
    public static class RevDbUpdateExtensions
    {
        public static Versioned<T> Update<T>(this LiteRevisionsDB<T> revDb,
            long groupId, T newDTO, string updatedBy, string changeLog)
        {
            var newVer = new Versioned<T>
            {
                GroupId    = groupId,
                Content    = newDTO,
                ChangeLog  = changeLog,
                ChangedBy  = updatedBy,
                ChangeDate = DateTime.Now
            };

            using (var db = revDb.OpenWrite())
                return db.InsertVersioned(newVer);
        }
    }
}
