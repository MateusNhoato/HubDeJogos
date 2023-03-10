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
                Thread.Sleep(300);
                Som.Musica(Musica.menujogos);
                if (!tutorial)
                {
                    _tela.ImprimirMenuDeJogos();
                    opcao = Console.ReadLine();
                }
                else
                {
                    RecepcionarJogadores();                
                    opcao = "3";
                    tutorial = false;
                }

                switch (opcao)
                {
                    case "0":
                        Comunicacao.ComunicacaoComUsuario(Efeito.voltar, null);
                        break;
                    case "1":
                        Jogar();
                        break;
                    case "2":
                        HistoricoDosJogadores();
                        break;
                    case "3":
                        Comunicacao.ComunicacaoComUsuario(Efeito.novatela, null);
                        Tutoriais();
                        break;
                    default:
                        Comunicacao.ComunicacaoComUsuario(Efeito.falha, null);
                        break;
                }
            } while (opcao != "0");
        }
        private void Jogar()
        {
            string opcao;
            do
            {
                Thread.Sleep(300);
                _tela.ImprimirMenuDeEscolhaDeJogos();
                opcao = Console.ReadLine();
                    
                switch (opcao)
                {
                    case "0":
                        Comunicacao.ComunicacaoComUsuario(Efeito.voltar, null);
                        break;
                    case "1":
                        NovoJogoDaVelha(false);
                        opcao = "0";
                        break;
                    case "2":
                        NovoJogoDeXadrez(false);
                        opcao = "0";
                        break;
                    case "3":
                        NovoJogoDeBatalhaNaval(false);
                        opcao = "0";
                        break;
                    default:
                        Comunicacao.ComunicacaoComUsuario(Efeito.falha, null);
                        break;
                }
            } while (opcao != "0");
        }

        private void Tutoriais()
        {
            string opcao;
            do
            {
                Thread.Sleep(300);
                Som.Musica(Musica.tutorial);
                _tela.ImprimirTutorial();
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "0":
                        Som.ReproduzirEfeito(Efeito.voltar);
                        break;
                    case "1":
                        Som.Musica(Musica.jogodavelha);
                        NovoJogoDaVelha(true);
                        break;
                    case "2":
                        Som.Musica(Musica.xadrez);
                        NovoJogoDeXadrez(true);
                        break;
                    case "3":
                        Som.Musica(Musica.batalhanaval);
                        NovoJogoDeBatalhaNaval(true);
                        break;
                    default:
                        Comunicacao.ComunicacaoComUsuario(Efeito.falha, null);
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
                Comunicacao.ComunicacaoComUsuario(Efeito.gamestart, null);
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
                Comunicacao.ComunicacaoComUsuario(Efeito.gamestart, null);
                new Xadrez.Services.Xadrez(_jogador1, _jogador2);
                _hub.PassarListaDeJogadoresParaRepositorio();
            }
        }

        private void NovoJogoDeBatalhaNaval(bool tutorial)
        {
            if (tutorial)
                new BatalhaNaval.Services.BatalhaNaval();
            else
            {
                Comunicacao.ComunicacaoComUsuario(Efeito.gamestart, null);
                new BatalhaNaval.Services.BatalhaNaval(_jogador1, _jogador2);
                _hub.PassarListaDeJogadoresParaRepositorio();
            }
        }
        private void HistoricoDosJogadores()
        {
            string opcao;
            do
            {
                Thread.Sleep(300);
                _tela.ImprimirHistoricoMenu(_jogador1.NomeDeUsuario, _jogador2.NomeDeUsuario);
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "0":
                        Som.ReproduzirEfeito(Efeito.voltar);
                        break;
                    case "1":
                        _tela.ImprimirHistoricoMenu(_jogador1.NomeDeUsuario);
                        Utilidades.Comunicacao.Carregando();
                        Som.Musica(Musica.historico);
                        _tela.ImprimirHistoricoDoJogador(_jogador1.HistoricoDePartidas);
                        opcao = "0";
                        break;
                    case "2":
                        _tela.ImprimirHistoricoMenu(_jogador2.NomeDeUsuario);
                        Utilidades.Comunicacao.Carregando();
                        Som.Musica(Musica.historico);
                        _tela.ImprimirHistoricoDoJogador(_jogador2.HistoricoDePartidas);
                        opcao = "0";
                        break;
                    default:
                        Comunicacao.ComunicacaoComUsuario(Efeito.falha, null);
                        break;
                }
            } while (opcao != "0");
        }

        private void RecepcionarJogadores()
        {
            Console.Clear();
            Console.WriteLine($"\n  Olá, {_jogador1.NomeDeUsuario} e {_jogador2.NomeDeUsuario}!\n\n" +
                                "  Parece que pelo menos um de vocês está acessando o Hub de Jogos pela primeira vez.\n" +
                                "  Vamos redirecioná-los para nossa seção de Tutoriais.\n" +
                                "  Caso não quiserem ver os tutoriais, basta voltar para o menu principal de jogos.");
            Comunicacao.AperteEnterParaContinuar();
        }
    }
}
