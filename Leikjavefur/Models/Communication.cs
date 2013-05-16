using System;
using Leikjavefur.Models.Repository;
using Leikjavefur.Models.Interfaces;
using Microsoft.AspNet.SignalR;
using System.Linq;

namespace Leikjavefur.Models
{
    public class Communication : Hub
    {
        private readonly IDataRepository _dataRepository = new DataRepository();

#region Basic Functions
        //adds users to groups, either char or game group.
        public void Join(string groupId)
        {
            Groups.Add(Context.ConnectionId, groupId);
        }

        //removes users from groups, 
        public void Leave(string groupId)
        {
            Groups.Remove(Context.ConnectionId, groupId);
        }

        public void GameOver(string gameInstanceId)
        {
            _dataRepository.GameInstanceRepository.DeleteGameInstance(gameInstanceId);
        }

        public void ActivateGame(string gameInstanceId)
        {
            _dataRepository.GameInstanceRepository.ActivateGameInstance(gameInstanceId);
            _dataRepository.GameInstanceRepository.Save();
        }

        public void SendPlayerCount(string groupId, string playerCount, string userName)
        {
            Clients.OthersInGroup(groupId).receivePlayerCount(playerCount, userName);
        }

        public void StartGame(string groupId)
        {
            Clients.OthersInGroup(groupId).gameStarted();
        }

        public void SaveWinOrLoss(string userId, string gameId, string winOrLoose) 
        {
            Statistic myStat = _dataRepository.StatisticRepository.FindByUserIdAndGameID(Convert.ToInt32(userId), Convert.ToInt32(gameId));
            if (myStat == null)
            {
                myStat = new Statistic();
               // myStat.Id = 0;
                myStat.UserID = Convert.ToInt32(userId);
                myStat.GameID = Convert.ToInt32(gameId);
                myStat.GamesPlayed = 1;
                myStat.Points = 0;

                if (winOrLoose == "Won")
                {
                    myStat.Wins = 1;
                    myStat.Losses = 0;
                     myStat.Draws = 0;
                }
                else if (winOrLoose == "Lost")
                {
                    myStat.Wins = 0;
                    myStat.Losses = 1;
                    myStat.Draws = 0;
                }
                else if (winOrLoose == "Tie")
                {
                    myStat.Wins = 0;
                    myStat.Losses = 0;
                    myStat.Draws = 1;
                }
            }
            else if (myStat != null)
            {
                myStat.GamesPlayed += 1;
                if (winOrLoose == "Won")
                {
                    myStat.Wins += 1;
                }
                else if (winOrLoose == "Lost")
                {
                    myStat.Losses += 1;
                }
                else if (winOrLoose == "Tie")
                {
                    myStat.Draws += 1;
                }
            }
            _dataRepository.StatisticRepository.InsertOrUpdate(myStat);
            _dataRepository.StatisticRepository.Save();
        }

        public void DeleteGameById(string gameId)
        {
            _dataRepository.GameInstanceRepository.DeleteGameInstance(gameId);
            _dataRepository.GameInstanceRepository.Save();
        }

#endregion

#region Chat Functions 
        //Sends message to everyone in the same group as the UserProfile
        public void SendMessage(string groupiD, string message)
        {
            Clients.OthersInGroup(groupiD).ReceiveMessage(message);
        }

#endregion

#region TicTacToe Functions 
        //sends the clicked cell id to oponent. (and others in group)
        public void ClickCell(string groupId, string cellId)
        {
            //Clients.OthersInGroup(groupId).cellClicked(cellId, IsGameOver);
            Clients.OthersInGroup(groupId).cellClicked(cellId);
        }

       // public void IsGameOver(string groupId,

#endregion

#region Snakes'n'Ladders Functions
        //Rolls the dice and sends the outcome to everyone (including UserProfile who rolled)
        //Not fnished
        public void RollDice(string groupId, string userNumber, string userPosition)
        {
            string isGameOver = "false";
            string snakeOrLadder = "false";
            string snakeOrLadderValue = "0";

            //nextPlayer: used to find out which player rolls next.
            int userRoll;
            Random rand = new Random();
            userRoll = rand.Next(1, 7);

            int nextCell = userRoll + Convert.ToInt32(userPosition);

            if (nextCell > 30)
            {
                int overFlow = nextCell - 30;
                nextCell = 30 - overFlow;
            }

            //Ladders and Snakes
            if (nextCell == 3){
                snakeOrLadderValue = "3";
                nextCell = 22;
                snakeOrLadder = "true";
            }
            else if (nextCell == 5){
                snakeOrLadderValue = "5";
                nextCell = 8;
                snakeOrLadder = "true";
            }
            else if (nextCell == 11){
                snakeOrLadderValue = "11";
                nextCell = 26;
                snakeOrLadder = "true";
            }
            else if (nextCell == 17){
                snakeOrLadderValue = "17";
                nextCell = 4;
                snakeOrLadder = "true";
            }
            else if (nextCell == 19){
                snakeOrLadderValue = "19";
                nextCell = 7;
                snakeOrLadder = "true";
            }
            else if (nextCell == 20){
                snakeOrLadderValue = "20";
                nextCell = 29;
                snakeOrLadder = "true";
            }
            else if (nextCell == 21){
                snakeOrLadderValue = "21";
                nextCell = 9;
                snakeOrLadder = "true";
            }
            else if (nextCell == 27){
                snakeOrLadderValue = "27";
                nextCell = 1;
                snakeOrLadder = "true";
            }

            if (nextCell == 30)
            {
                isGameOver = "true";
            }

            Clients.Caller.receiveRollValueAndNextPlayer(userPosition, Convert.ToString(nextCell), userNumber, isGameOver, Convert.ToString(userRoll), snakeOrLadder, snakeOrLadderValue);
            Clients.OthersInGroup(groupId).receiveRollValueAndNextPlayer(userPosition, Convert.ToString(nextCell), userNumber, isGameOver, Convert.ToString(userRoll), snakeOrLadder, snakeOrLadderValue); 
        }

#endregion
        
    }
}