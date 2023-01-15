using HubDeJogos.Controllers;
using HubDeJogos.Hub.Repositories;
using HubDeJogos.Models;


Console.BackgroundColor = ConsoleColor.Cyan;
Console.ForegroundColor= ConsoleColor.Black;
Console.Clear();

Hub hub = new Hub();
hub.Menu();