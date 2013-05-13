using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Leikjavefur.Models.Repository;
using Microsoft.AspNet.SignalR;

namespace Leikjavefur.Models
{
    public class Communication : Hub
    {
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
            GameInstanceRepository gameInstRep = new GameInstanceRepository();
            gameInstRep.DeleteGameInstance(gameInstanceId);
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
            //nextPlayer: used to find out which player rolls next.
            int userRoll;
            Random rand = new Random();
            userRoll = rand.Next(1, 6);

            int nextCell = userRoll + Convert.ToInt32(userPosition);

            //Ladders and Snakes
            if (nextCell == 3)
                nextCell = 22;
            else if (nextCell == 5)
                nextCell = 8;
            else if (nextCell == 11)
                nextCell = 26;
            else if (nextCell == 17)
                nextCell = 4;
            else if (nextCell == 19)
                nextCell = 7;
            else if (nextCell == 20)
                nextCell = 29;
            else if (nextCell == 21)
                nextCell = 9;
            else if (nextCell == 27)
                nextCell = 1;

            if (nextCell == 30)
            {
                isGameOver = "true";
            }
            //Sends roll value and who rolls next.
            //if last roller was #4 then new roller will be 0+1 = 1 which is player 1
            //if last roller was <4 then new roller will be players 2, 3 or 4.
            Clients.Group(groupId).receiveRollValueAndNextPlayer(Convert.ToString(nextCell), userNumber, isGameOver);
        }


#endregion
        
    }
}