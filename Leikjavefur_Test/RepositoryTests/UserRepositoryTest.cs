using System.Collections.Generic;
using Leikjavefur.Models;
using Leikjavefur.Models.Context;
using Leikjavefur.Models.Repository;
using Leikjavefur_Test.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Leikjavefur_Test.RepositoryTests
{
    [TestClass]
    public class UserRepositoryTest
    {
        private readonly UserRepository _repository;
        private readonly IDataContext _context = new MockContext();

        public UserRepositoryTest()
        {
            _repository = new UserRepository(_context);
        }


        [TestMethod]
        public void TestGetFriend()
        {
            Assert.IsInstanceOfType(_repository.GetFriend(1, 2), typeof (Friends));
            Assert.IsNull(_repository.GetFriend(1, 7));
            Assert.IsNotNull(_repository.GetFriend(1, 2));
        }

        [TestMethod]
        public void TestIsFriend()
        {
            Assert.IsTrue(_repository.IsFriend(1,2));
            Assert.IsFalse(_repository.IsFriend(1,7));
        }

        [TestMethod]
        public void TestRemoveFriend()
        {
            Assert.IsTrue(_repository.IsFriend(1,2));
            _repository.RemoveFriend(1,2);
            Assert.IsFalse(_repository.IsFriend(1,2));
        }

        [TestMethod]
        public void TestAddFriend()
        {
            Assert.IsFalse(_repository.IsFriend(1,5));
            _repository.AddFriend(1,5);
            Assert.IsTrue(_repository.IsFriend(1,5));
        }
    }
}
