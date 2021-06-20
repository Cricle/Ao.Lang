using Microsoft.Extensions.Configuration.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;

namespace Ao.Microsoft.Extensions.Configuration.Resources.Test
{
    [TestClass]
    public class ResourceHelperTest
    {
        [TestMethod]
        public void GivenNull_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ResourceHelper.GetData(null));
        }
        class ReadonlyStream : Stream
        {
            public override bool CanRead => false;

            public override bool CanSeek => false;

            public override bool CanWrite => false;

            public override long Length => 0;

            public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override void Flush()
            {
                throw new NotImplementedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }
        }
        [TestMethod]
        public void GivenCannotRead_MustThrowException()
        {
            Assert.ThrowsException<InvalidOperationException>(() => ResourceHelper.GetData(new ReadonlyStream()));
        }
        [TestMethod]
        public void GivenResourceStream_MustPased()
        {
            var mem = new MemoryStream();
            var res = new ResourceWriter(mem);
            res.AddResource("a", "1");
            res.AddResource("b", "2");
            res.AddResource("q", new byte[] { 1, 2, 3});
            res.Generate();
            mem.Seek(0, SeekOrigin.Begin);

            var map = ResourceHelper.GetData(mem);
            Assert.AreEqual(2, map.Count);
            Assert.AreEqual("1", map["a"]);
            Assert.AreEqual("2", map["b"]);
        }
    }
}
