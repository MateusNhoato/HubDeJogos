using HubDeJogos.Controllers;
using HubDeJogos.Xadrez.Repositories;
using HubDeJogos.Xadrez.Services;

Console.BackgroundColor = ConsoleColor.DarkCyan;
Console.ForegroundColor = ConsoleColor.Gray;
Console.Clear();

Pgns.CriarDiretorioSeAusente();


Hub hub = new Hub();
hub.Menu();



