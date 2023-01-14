using HubDeJogos.Models;
using HubDeJogos.Models.Enums;
using HubDeJogos.Repositories;
using HubDeJogos.Views;
using HubDeJogos.JogoDaVelha.Services;


namespace HubDeJogos.Controllers
{
    public class MenuDeJogos
    {
        private readonly Tela _tela = new Tela();
        private Jogador _jogador1;
        private Jogador _jogador2;

        public MenuDeJogos(Jogador jogador1, Jogador jogador2)
        {
            _jogador1 = jogador1;
            _jogador2 = jogador2;
        }

        public void Menu()
        {
            string opcao;
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
                        Console.WriteLine("Opção não encontrada.");
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

                        break;
                    default:
                        Console.WriteLine("Opção não encontrada.");
                        break;
                }
            } while (opcao != "0");
        }

    private void NovoJogoDaVelha()
        {
            Partida partida = new Partida(Jogo.JogoDaVelha);
            partida.Jogador = _jogador1;
            partida.Oponente = _jogador2;
            partida.Resultado = Resultado.Vitoria;
            JogoDaVelha.Services.JogoDaVelha jogo = new JogoDaVelha.Services.JogoDaVelha(_jogador1, _jogador2, partida);

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
                        Console.WriteLine("Opção não encontrada.");
                        break;
                }
            } while (opcao != "0");
        }
    
    

    }
}
