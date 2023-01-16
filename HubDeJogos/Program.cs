using HubDeJogos.Controllers;
using HubDeJogos.Hub.Repositories;
using HubDeJogos.JogoDaVelha.Services;
using HubDeJogos.Models;
using HubDeJogos.Xadrez.Services;

Console.BackgroundColor = ConsoleColor.DarkCyan;
Console.ForegroundColor= ConsoleColor.Gray;
Console.Clear();

// teste do tutorial
//  new Xadrez();
 new JogoDaVelha();
// new JogoDaVelha(new Jogador("Fulano", ""), new Jogador("Ciclano", ""));

Hub hub = new Hub();
hub.Menu();