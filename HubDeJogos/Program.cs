﻿using HubDeJogos.Controllers;
using HubDeJogos.Hub.Repositories;
using HubDeJogos.JogoDaVelha.Services;
using HubDeJogos.Models;
using HubDeJogos.Xadrez.Models;
using HubDeJogos.Xadrez.Repositories;
using HubDeJogos.Xadrez.Services;

Console.BackgroundColor = ConsoleColor.DarkCyan;
Console.ForegroundColor= ConsoleColor.Gray;
Console.Clear();



Pgns.CriarDiretorioSeAusente();
Hub hub = new Hub();
hub.Menu();

// testes dos tutoriais
// new Xadrez();
// new JogoDaVelha();

// teste pgn
//Pgn pgn = new Pgn("bilu", "tiziu");
//pgn.Jogadas.Add("1. e4 b5");
//
//Pgns.CriarArquivoPgn(pgn);

// testes de partidas
// new Xadrez(new Jogador("bilu",""), new Jogador("tiziu", ""));
// new JogoDaVelha(new Jogador("bilu",""), new Jogador("tiziu", ""));


