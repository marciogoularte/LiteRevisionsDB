using FluentAssertions;
using LiteRevisionsDB.net45;
using System;
using System.IO;
using Xunit;

namespace LiteRevisionsDB.Tests.LibC.net45Tests
{
    public class net45Facts
    {
        [Fact(DisplayName = "Inserts a record")]
        public void Insertsarecord()
        {
            var mem = new MemoryStream();
            var sut = new LiteRevisionsDB<SampleClass1>(mem);

            sut.CountAll().Should().Be(0);

            var obj = new SampleClass1 { Text1 = "bla" };
            var ver = sut.Insert(obj, "author", "sample log");

            sut.CountAll().Should().Be(1);
            ver.Id.Should().Be(1);
            ver.GroupId.Should().Be(1);
            ver.ChangeLog.Should().Be("sample log");
            ver.ChangedBy.Should().Be("author");
            ver.ChangeDate.Date.Should().Be(DateTime.Now.Date);

        }


        [Fact(DisplayName = "Auto-increments IDs")]
        public void AutoincrementsIDs()
        {
            var mem = new MemoryStream();
            var sut = new LiteRevisionsDB<SampleClass1>(mem);

            sut.Insert(new SampleClass1(), "author");
            var ver = sut.Insert(new SampleClass1(), "author");

            sut.CountAll().Should().Be(2);
            ver.Id        .Should().Be(2);
            ver.GroupId   .Should().Be(2);
        }


        [Fact(DisplayName = "Updates record")]
        public void CanEdit()
        {
            var mem = new MemoryStream();
            var sut = new LiteRevisionsDB<SampleClass1>(mem);

            sut.Insert(new SampleClass1(), "author");
            sut.Insert(new SampleClass1(), "author");

            var oldVer = sut.Insert(new SampleClass1(), "author");
            sut.CountAll().Should().Be(3);
            oldVer.Id     .Should().Be(3);
            oldVer.GroupId.Should().Be(3);

            oldVer.Content.Text1 = "something else";

            var newVer = sut.Update(oldVer.GroupId, oldVer.Content, "", "");
            sut.CountAll().Should().Be(3);
            newVer.Id     .Should().Be(4);
            newVer.GroupId.Should().Be(3);

            var dbRec = sut.GetById(3);
            dbRec.Text1.Should().Be("something else");
        }
    }
}
