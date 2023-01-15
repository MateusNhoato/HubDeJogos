using HubDeJogos.Controllers;
using HubDeJogos.Hub.Repositories;
using HubDeJogos.JogoDaVelha.Services;
using HubDeJogos.Models;
using HubDeJogos.Xadrez.Services;

Console.BackgroundColor = ConsoleColor.DarkCyan;
Console.ForegroundColor= ConsoleColor.Gray;
Console.Clear();


// new Xadrez(new Jogador("Fulano", ""), new Jogador("Ciclano", ""), false);
// new JogoDaVelha(new Jogador("Fulano", ""), new Jogador("Ciclano", ""));

Hub hub = new Hub();
hub.Menu();