using HubDeJogos.BatalhaNaval.Services;
using HubDeJogos.BatalhaNaval.Views;
using HubDeJogos.Controllers;

Console.Title = "Hub de Jogos";
Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.ForegroundColor = ConsoleColor.White;
Console.Clear();


new BatalhaNaval(new HubDeJogos.Models.Jogador("fulano", ""), new HubDeJogos.Models.Jogador("beltrano", ""));

Hub hub = new Hub();
hub.Menu();



