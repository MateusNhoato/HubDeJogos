using HubDeJogos.Controllers;
using HubDeJogos.Hub.Repositories;
using HubDeJogos.JogoDaVelha.Services;
using HubDeJogos.Models;
using HubDeJogos.Xadrez.Services;

Console.BackgroundColor = ConsoleColor.DarkCyan;
Console.ForegroundColor= ConsoleColor.Gray;
Console.Clear();

// testes dos tutoriais
// new Xadrez();
 new JogoDaVelha();

// testes de partidas
// new Xadrez(new Jogador("bilu",""), new Jogador("tiziu", ""));
// new JogoDaVelha(new Jogador("bilu",""), new Jogador("tiziu", ""));

Hub hub = new Hub();
hub.Menu();