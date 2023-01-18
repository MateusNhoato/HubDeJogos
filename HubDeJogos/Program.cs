using HubDeJogos.Controllers;
using HubDeJogos.Xadrez.Repositories;

Console.BackgroundColor = ConsoleColor.DarkCyan;
Console.ForegroundColor = ConsoleColor.Gray;
Console.Clear();



Pgns.CriarDiretorioSeAusente();
Hub hub = new Hub();
hub.Menu();



