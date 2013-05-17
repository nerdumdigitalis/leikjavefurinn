using System.Collections.Generic;
using System.Linq;
using Leikjavefur.Models;
using Leikjavefur.Models.Context;
using Leikjavefur.Models.Repository;
using Leikjavefur_Test.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Leikjavefur_Test.RepositoryTests
{
    [TestClass]
    public class GameInstancesRepositoryTest
    {
        private readonly GameInstanceRepository _repository;
        private readonly IDataContext _context = new MockContext();

        public GameInstancesRepositoryTest()
        {
            _repository = new GameInstanceRepository(_context);
        }

        [TestMethod]
        public void TestGetAllGameInstances()
        {
            Assert.IsNotNull(_repository.All);
            Assert.AreEqual(7, _repository.All.Count());
            Assert.IsInstanceOfType(_repository.All, typeof(IQueryable<GameInstance>));
        }

        [TestMethod]
        public void TestGetDistinctGameInstances()
        {
            Assert.IsNotNull(_repository.All);
            Assert.AreEqual(_repository.GetGameInstances().Count, 4);
            Assert.IsInstanceOfType(_repository.GetGameInstances(), typeof(List<GameInstance>));
        }

        [TestMethod]
        public void TestGetUsersByGameInstance()
        {
            Assert.AreEqual(_repository.GetUsersByGameInstance("1").Count(), 1);
            Assert.IsInstanceOfType(_repository.GetUsersByGameInstance("1"), typeof(IQueryable<UserProfile>));
            Assert.AreEqual(_repository.GetUsersByGameInstance("100").Count(),0);
        }

        [TestMethod]
        public void TestActivateGameInstance()
        {
            Assert.IsFalse(_context.GameInstances.ElementAt(0).IsActive);
            _repository.ActivateGameInstance(_context.GameInstances.ElementAt(0).GameInstanceID);
            Assert.IsTrue(_context.GameInstances.ElementAt(0).IsActive);
            _repository.ActivateGameInstance(_context.GameInstances.ElementAt(0).GameInstanceID);
            Assert.IsTrue(_context.GameInstances.ElementAt(0).IsActive);
        }

        [TestMethod]
        public void TestGetGameIDByGameInstanceID()
        {
            Assert.IsInstanceOfType(_repository.GetGameIDByGameInstanceID("1"), typeof(int));
            Assert.AreEqual(_repository.GetGameIDByGameInstanceID(_context.GameInstances.ElementAt(0).GameInstanceID), _context.GameInstances.ElementAt(0).GameID);
            Assert.AreNotEqual(_repository.GetGameIDByGameInstanceID(_context.GameInstances.ElementAt(0).GameInstanceID), -1);
            Assert.AreEqual(_repository.GetGameIDByGameInstanceID("b"), -1);
        }
        
    }
}
