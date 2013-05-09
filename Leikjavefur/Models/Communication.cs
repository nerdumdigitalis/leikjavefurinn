using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
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

#endregion

#region Chat Functions 
        //Sends message to everyone in the same group as the user
        public void SendMessage(string groupiD, string message)
        {
            Clients.OthersInGroup(groupiD).ReceiveMessage(message);
        }

#endregion

#region TicTacToe Functions 
        //sends the clicked cell id to oponent. (and others in group)
        public void ClickCell(string groupId, string cellId)
        {
            Clients.OthersInGroup(groupId).cellClicked(cellId);
        }

       // public void IsGameOver(string groupId,

#endregion

#region Snakes'n'Ladders Functions
        //Rolls the dice and sends the outcome to everyone (including user who rolled)
        //Not fnished
        public void RollDice(string groupId, string userNumber)
        {
            //nextPlayer: used to find out which player rolls next.
            int nextPlayer = Convert.ToInt32(userNumber);
            int userRoll;
            Random rand = new Random();
            userRoll = rand.Next(1, 6);
            
            if(nextPlayer == 4)
            {
                nextPlayer = 0;
            }
            
            //Sends roll value and who rolls next.
            //if last roller was #4 then new roller will be 0+1 = 1 which is player 1
            //if last roller was <4 then new roller will be players 2, 3 or 4.
            Clients.Group(groupId).rollValueAndNextPlayer(userRoll, (nextPlayer + 1));
        }


#endregion
        
    }
}