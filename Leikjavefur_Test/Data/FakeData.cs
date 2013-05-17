using Leikjavefur.Models;
using Leikjavefur_Test.Contexts;

namespace Leikjavefur_Test.Data
{
    public class FakeData
    {
        public MockContext PopulateFakeData()
        {
            var inst = new MockContext
            {
                GameInstances =
                                   {
                                       new GameInstance{GameID = 1, GameInstanceID = "1", IsActive = true, UserID = 1},
                                       new GameInstance{GameID = 1, GameInstanceID = "1", IsActive = true, UserID = 1},
                                       new GameInstance{GameID = 2, GameInstanceID = "2", IsActive = false, UserID = 3},
                                       new GameInstance{GameID = 3, GameInstanceID = "2", IsActive = true, UserID = 5},
                                       new GameInstance{GameID = 3, GameInstanceID = "3", IsActive = false, UserID = 1},
                                   }
            };


            return inst;
        }
    }
}
