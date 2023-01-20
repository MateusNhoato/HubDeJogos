using HubDeJogos.Models;
using HubDeJogos.Views;
using Utilidades;

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

        public void Menu(bool tutorial)
        {

            string? opcao;
            do
            {
                Som.Musica(Musica.menujogos);
                if (!tutorial)
                {
                    _tela.ImprimirMenuDeJogos();
                    opcao = Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"\n  Olá, {_jogador1.NomeDeUsuario} e {_jogador2.NomeDeUsuario}!\n\n" +
                                        "  Parece que pelo menos um de vocês está acessando o Hub de Jogos pela primeira vez.\n" +
                                        "  Vamos redirecioná-los para nossa seção de Tutoriais.\n" +
                                        "  Caso não quiserem ver os tutoriais, basta voltar para o menu principal de jogos.");
                    Utilidades.Utilidades.AperteEnterParaContinuar();
                    opcao = "3";
                    tutorial = false;
                }

                switch (opcao)
                {
                    case "0":
                        Som.ReproduzirEfeito(Efeito.voltar);
                        break;
                    case "1":
                        Jogar();
                        break;
                    case "2":
                        HistoricoDosJogadores();
                        break;
                    case "3":
                        Som.ReproduzirEfeito(Efeito.novatela);
                        Thread.Sleep(300);
                        Som.Musica(Musica.tutorial);
                        Tutoriais();
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

                if (opcao != "0")
                    Som.ReproduzirEfeito(Efeito.gamestart);

                switch (opcao)
                {
                    case "0":
                        Som.ReproduzirEfeito(Efeito.voltar);
                        break;
                    case "1":
                        NovoJogoDaVelha(false);
                        opcao = "0";
                        break;
                    case "2":
                        NovoJogoDeXadrez(false);
                        opcao = "0";
                        break;
                    default:
                        Console.WriteLine("  Opção não encontrada.");
                        break;
                }
            } while (opcao != "0");
        }

        private void Tutoriais()
        {
            string opcao;
            do
            {
                _tela.ImprimirTutorial();
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "0":
                        Som.ReproduzirEfeito(Efeito.voltar);
                        break;
                    case "1":
                        NovoJogoDaVelha(true);
                        break;
                    case "2":
                        NovoJogoDeXadrez(true);
                        break;
                    default:
                        Console.WriteLine("  Opção não encontrada.");
                        break;
                }
            } while (opcao != "0");


        }
        private void NovoJogoDaVelha(bool tutorial)
        {
            if (tutorial)
                new JogoDaVelha.Services.JogoDaVelha();
            else
            {
                new JogoDaVelha.Services.JogoDaVelha(_jogador1, _jogador2);
                _hub.PassarListaDeJogadoresParaRepositorio();
            }
        }
        private void NovoJogoDeXadrez(bool tutorial)
        {
            if (tutorial)
                new Xadrez.Services.Xadrez();
            else
            {
                new Xadrez.Services.Xadrez(_jogador1, _jogador2);
                _hub.PassarListaDeJogadoresParaRepositorio();
            }
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
                        Som.ReproduzirEfeito(Efeito.voltar);
                        break;
                    case "1":
                        _tela.ImprimirHistoricoMenu(_jogador1.NomeDeUsuario);
                        Utilidades.Utilidades.Carregando();
                        Som.Musica(Musica.historico);
                        _tela.ImprimirHistoricoDoJogador(_jogador1.HistoricoDePartidas);
                        opcao = "0";
                        break;
                    case "2":
                        _tela.ImprimirHistoricoMenu(_jogador2.NomeDeUsuario);
                        Utilidades.Utilidades.Carregando();
                        Som.Musica(Musica.historico);
                        _tela.ImprimirHistoricoDoJogador(_jogador2.HistoricoDePartidas);
                        opcao = "0";
                        break;
                    default:
                        Console.WriteLine("  Opção não encontrada.");
                        break;
                }
            } while (opcao != "0");
        }



    }
}
