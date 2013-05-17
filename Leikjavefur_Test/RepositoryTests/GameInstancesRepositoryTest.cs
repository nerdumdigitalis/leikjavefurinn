using System.Collections.Generic;
using System.Linq;
using Leikjavefur.Models;
using Leikjavefur.Models.Context;
using Leikjavefur.Models.Repository;
using Leikjavefur_Test.Contexts;
using Leikjavefur_Test.Data;
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
        public void TestGetDistinctGameInstancesNotNull()
        {
            Assert.IsNotNull(_repository.GetGameInstances());
        }

        [TestMethod]
        public void TestGetUsersByGameInstanceCount()
        {
            Assert.AreEqual(_repository.GetUsersByGameInstance("1").Count(), 1);
        }

        [TestMethod]
        public void TestActivateGameInstance()
        {
            
        }
    }
}
