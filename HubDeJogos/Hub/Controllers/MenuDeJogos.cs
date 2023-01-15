using HubDeJogos.Models;
using HubDeJogos.Models.Enums;
using HubDeJogos.Repositories;
using HubDeJogos.Views;
using HubDeJogos.JogoDaVelha.Services;
using HubDeJogos.Hub.Repositories;

namespace HubDeJogos.Controllers
{
    public class MenuDeJogos
    {
        private readonly Tela _tela = new Tela();
        private readonly Hub _hub;
        private readonly Jogador _jogador1;
        private readonly Jogador _jogador2;

        public MenuDeJogos(Jogador jogador1, Jogador jogador2, Hub hub)
        {
            _jogador1 = jogador1;
            _jogador2 = jogador2;
            _hub = hub;
        }

        public void Menu()
        {
            string? opcao;
            do
            {
                _tela.ImprimirMenuDeJogos();
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "0":
                        break;
                    case "1":
                        Jogar();
                        break;
                    case "2":
                        HistoricoDosJogadores();
                        break;
                    default:
                        Console.WriteLine("  Opção não encontrada.");
                        break;
                }
            } while (opcao != "0");
        }
    private void Jogar()
        {
            string opcao;
            do
            {
                _tela.ImprimirMenuDeEscolhaDeJogos();
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "0":
                        break;
                    case "1":
                        NovoJogoDaVelha();
                        break;
                    case "2":
                        NovoJogoDeXadrez(false);
                        break;
                    case "3":
                        NovoJogoDeXadrez(true);
                        break;
                    default:
                        Console.WriteLine("  Opção não encontrada.");
                        break;
                }
            } while (opcao != "0");
        }

    private void NovoJogoDaVelha()
        {
             new JogoDaVelha.Services.JogoDaVelha(_jogador1, _jogador2);
            _hub.PassarListaDeJogadoresParaRepositorio();
        }
    private void NovoJogoDeXadrez(bool tutorial)
        {
            new Xadrez.Services.Xadrez(_jogador1, _jogador2, tutorial);
            _hub.PassarListaDeJogadoresParaRepositorio();
        }
        private void HistoricoDosJogadores()
        {
            string opcao;
            do
            {
                _tela.ImprimirHistoricoMenu(_jogador1.NomeDeUsuario, _jogador2.NomeDeUsuario);
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "0":
                        break;
                    case "1":
                        _tela.ImprimirHistoricoMenu(_jogador1.NomeDeUsuario);
                        _tela.ImprimirHistoricoDoJogador(_jogador1.HistoricoDePartidas);
                        break;
                    case "2":
                        _tela.ImprimirHistoricoMenu(_jogador2.NomeDeUsuario);
                        _tela.ImprimirHistoricoDoJogador(_jogador2.HistoricoDePartidas);
                        break;
                    default:
                        Console.WriteLine("  Opção não encontrada.");
                        break;
                }
            } while (opcao != "0");
        }
    
    

    }
}
