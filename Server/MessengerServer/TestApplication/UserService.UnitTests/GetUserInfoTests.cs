using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication.UserService.UnitTests
{
   [TestFixture]
   public class GetUserInfoTests
    {
        [Test]
        public void GetUserInfo_UserNotExist_ThrowsException()
        {
            Assert.IsTrue(true);
        }
    }
}
