using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace Leikjavefur.Controllers
{
    public class GameHub : Hub
    {
        /*
            Þessi klasi á að halda utan um öll tilvik af TacTacToe og S&L og lika að geta haldið utan um nýja leiki ef þeir
            væru bættir við...
            Þessi klasi mun sjá um að taka á móti skilaboðum frá clients, og svo senda þeim áfram til aðra clienta.
            Þessi væri einungis notaður fyrir leiki, svo er hægt að gera annan klasa fyrir chat.. t.d. "ChatHub.cs" :)
          
            Hafa í huga þegar þið eruð að forrita þennan klasa.. að hafa hann þannig að hlægt sé að bæta klasa auðveldlega við.
            Þá meina ég að það þurfi ekki að harðkóða allt í þennan klasa ef við viljum bæta við annan leik.
          
            Þarf að pæla aðeins í hvernig þessi klasi á að vera útfærður.
                -Natan
         */


    }
}