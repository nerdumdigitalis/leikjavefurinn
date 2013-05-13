namespace Leikjavefur.Models.Interfaces
{
    public interface IDataRepository
    {
        IGameInstanceRepository GameInstanceRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        IGameRepository GameRepository { get; set; }
        IStatisticRepository StatisticRepository { get; set; }

    }
}
