using HubDeJogos.BatalhaNaval.Models;
using HubDeJogos.BatalhaNaval.Views;
using HubDeJogos.Hub.Repositories;
using HubDeJogos.Models;
using HubDeJogos.Repositories;
using System.Text.RegularExpressions;
using Utilidades;

namespace HubDeJogos.BatalhaNaval.Services
{
    internal class BatalhaNaval
    {
        private readonly Jogador _jogador1;
        private readonly Jogador _jogador2;
        private readonly TabuleiroBatalhaNaval _tabuleiroJogador1;
        private readonly TabuleiroBatalhaNaval _tabuleiroJogador2;
        private TabuleiroBatalhaNaval _tabuleiroAtual;
        private List<Posicao> _posicoesAtiradasJg1;
        private List<Posicao> _posicoesAtiradasJg2;
        private readonly Tela _tela = new Tela();
        public int Jogador1Score { get; private set; }
        public int Jogador2Score { get; private set; }



        public BatalhaNaval()
        {
            _jogador1 = new Jogador("Beltrano", "");
            _jogador2 = new Jogador("Fulano", "");

            _tabuleiroJogador1 = new TabuleiroBatalhaNaval();
            _tabuleiroJogador2 = new TabuleiroBatalhaNaval();

            _posicoesAtiradasJg1 = new List<Posicao>();
            _posicoesAtiradasJg2 = new List<Posicao>();

            _tabuleiroAtual = _tabuleiroJogador1;

            Tutorial();
        }


        public BatalhaNaval(Jogador jogador1, Jogador jogador2)
        {
            _jogador1 = jogador1;
            _jogador2 = jogador2;

            _tabuleiroJogador1 = new TabuleiroBatalhaNaval();
            _tabuleiroJogador2 = new TabuleiroBatalhaNaval();
            _tabuleiroAtual = _tabuleiroJogador1;

            _posicoesAtiradasJg1 = new List<Posicao>();
            _posicoesAtiradasJg2 = new List<Posicao>();

            Jogar();
        }

        private void Tutorial()
        {
            Console.CursorVisible = false;
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine(Hub.Views.Tutoriais.ExplicacoesBatalhaNaval[i]);
                Visual.AperteEnterParaContinuar();
                if (i == 0)
                {
                    _tela.ImprimirTabuleiro(_tabuleiroAtual, TirosPossiveis(_jogador1));
                    Console.WriteLine("\n  Bonito, não? Abaixo do tabuleiro fica o número de navios restantes!\n" +
                                      "  Obs: as marcas '~' no tabuleiro são as ondas do mar,\n" +
                                      "  que é muito temperamental, as ondas mudam constantemente.");
                }
                else if (i == 1)
                {
                    _tela.ImprimirVezDoJogador(1);
                    _tela.ImprimirTabuleiro(_tabuleiroAtual, TirosPossiveis(_jogador1));

                    Posicao pos = EncontrarPosicaoParaAtirarTutorial(_tabuleiroAtual, true);

                    Console.WriteLine($"  Digite sua jogada: {pos.ParaPosicaoDeTabuleiro()}");
                    Visual.AperteEnterParaContinuar();

                    _tela.ImprimirTabuleiro(_tabuleiroAtual, pos);
                    Atirar(pos, true);
                    AdicionarPosicaoAtiradaLista(Posicao.DePosicaoDeTabuleiroParaPosicao(pos.ParaPosicaoDeTabuleiro()), _jogador1);
                    Thread.Sleep(1000);
                    _tela.ImprimirTabuleiro(_tabuleiroAtual, TirosPossiveis(_jogador1));
                    Thread.Sleep(500);
                    Console.WriteLine("\n  Porque o tiro foi certeiro, nosso jogador pode atirar novamente!\n" +
                                      "  Vamos ver um exemplo de um erro agora.");
                    Visual.AperteEnterParaContinuar();

                    _tela.ImprimirTabuleiro(_tabuleiroAtual, TirosPossiveis(_jogador1));
                    pos = EncontrarPosicaoParaAtirarTutorial(_tabuleiroAtual, false);
                    Console.WriteLine($"  Digite sua jogada: {pos.ParaPosicaoDeTabuleiro()}");
                    Console.WriteLine("\n  Note que a posição atirada previamente está marcada com um X.\n" +
                                        "  Esta marca aparecerá independente se for um acerto ou não.\n" +
                                        "  Não é possível atirar em uma mesma posição mais de uma vez, pois a área já está revelada.");
                    Visual.AperteEnterParaContinuar();

                    _tela.ImprimirTabuleiro(_tabuleiroAtual, pos);
                    Atirar(pos, true);
                    AdicionarPosicaoAtiradaLista(Posicao.DePosicaoDeTabuleiroParaPosicao(pos.ParaPosicaoDeTabuleiro()), _jogador1);
                    Thread.Sleep(1000);
                    _tela.ImprimirTabuleiro(_tabuleiroAtual, TirosPossiveis(_jogador1));
                    Thread.Sleep(500);
                    Console.WriteLine("\n  Como o tiro não acertou nenhum navio, agora a vez passa para o próximo jogador.");
                }
                if (i == 2)
                {
                    Console.WriteLine(Hub.Views.Tutoriais.ArteBatalhaNaval);
                }


                Visual.AperteEnterParaContinuar();
            }
            Console.CursorVisible = true;
        }

        private void Jogar()
        {
            Jogador jogador = _jogador1;
            _tela.ImprimirVezDoJogador(1);
            while (_tabuleiroAtual.NumeroDeNavios > 0)
            {
                string padraoParaJogada = @"^[a-j]([1-9]|10)$";
                Regex rg = new Regex(padraoParaJogada);
                string jogada;
                do
                {
                    _tela.ImprimirTabuleiro(_tabuleiroAtual, TirosPossiveis(jogador));
                    Console.WriteLine($"\n  Vez de {jogador.NomeDeUsuario}");
                    Console.CursorVisible = true;
                    Console.Write("  Digite sua jogada: ");
                    jogada = Console.ReadLine().ToLower();
                } while (!rg.IsMatch(jogada));
                Console.CursorVisible = false;
                Posicao pos = Posicao.DePosicaoDeTabuleiroParaPosicao(jogada);

                // se a posição já foi atirada previamente, ela não fica mais disponível
                if (!CheckarPosicaoTiro(pos, jogador))
                {
                    Console.WriteLine("  A posição já foi atirada!");
                    Som.ReproduzirEfeito(Efeito.falha2);
                    Thread.Sleep(1000);
                    continue;
                }

                AdicionarPosicaoAtiradaLista(Posicao.DePosicaoDeTabuleiroParaPosicao(jogada), jogador);
                bool tiro = Atirar(pos, false);
                _tela.ImprimirTabuleiro(_tabuleiroAtual, pos);
                Thread.Sleep(1000);
               
                if (!tiro)
                {
                    _tela.ImprimirTabuleiro(_tabuleiroAtual, TirosPossiveis(jogador));
                    Thread.Sleep(1000);
                    jogador = MudarJogador(jogador);
                    MudarTabuleiro();
                    _tela.ImprimirVezDoJogador((jogador.Equals(_jogador1)) ? 1 : 2);
                }
                else
                {
                    if (_tabuleiroAtual.CheckarNavioDestruido())
                    {
                        ConsoleColor aux = Console.ForegroundColor;

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.CursorVisible = false;
                        Console.WriteLine("\n  Navio afundado!!!");
                        Som.NavioAfundado();
                        Thread.Sleep(1000);
                        Console.ForegroundColor = aux;
                        Console.CursorVisible = true;
                    }
                }
            }
            _tela.ImprimirTabuleiro(_tabuleiroAtual, TirosPossiveis(jogador));
            Console.WriteLine($"\n  Vencedor: {jogador.NomeDeUsuario}");
            Som.ReproduzirEfeito(Efeito.vitoria);

            // alterando os tabuleiros
            _tabuleiroJogador1.AlterarTabuleiroMatrizParaRegistro();
            _tabuleiroJogador2.AlterarTabuleiroMatrizParaRegistro();


            // criando a partida
            Partida partida = new Partida
                (HubDeJogos.Models.Enums.Jogo.BatalhaNaval,
                _jogador1.NomeDeUsuario,
                _jogador2.NomeDeUsuario,
                jogador.NomeDeUsuario,
                HubDeJogos.Models.Enums.Resultado.Decisivo,
                _tabuleiroJogador1,
                _tabuleiroJogador2);
            //adicionando a partida no histórico de partidas
            Partidas.HistoricoDePartidas.Add(partida);
            Partidas.SalvarPartidas();

            //adicionando a partida nos históricos dos jogadores
            _jogador1.HistoricoDePartidas.Add(partida);
            _jogador2.HistoricoDePartidas.Add(partida);
            Visual.AperteEnterParaContinuar();
        }


        private bool Atirar(Posicao pos, bool tutorial)
        {
            if (_tabuleiroAtual.MatrizNavios[pos.Linha, pos.Coluna] != null)
            {
                _tabuleiroAtual.MatrizNavios[pos.Linha, pos.Coluna].Destruir();
                if (!tutorial)
                    Som.BatalhaNaval(true);
                return true;
            }
            if (!tutorial)
                Som.BatalhaNaval(false);
            return false;
        }

        private Jogador MudarJogador(Jogador jogador)
        {
            return (jogador.Equals(_jogador1)) ? _jogador2 : _jogador1;
        }


        private void MudarTabuleiro()
        {
            if (_tabuleiroAtual.Equals(_tabuleiroJogador1))
                _tabuleiroAtual = _tabuleiroJogador2;
            else
                _tabuleiroAtual = _tabuleiroJogador1;
        }

        private bool CheckarPosicaoTiro(Posicao pos, Jogador jogador)
        {
            if (jogador.Equals(_jogador1))
            {
                foreach (Posicao posicao in _posicoesAtiradasJg1)
                {
                    if (posicao.Equals(pos))
                        return false;
                }
                return true;
            }
            else
            {
                foreach (Posicao posicao in _posicoesAtiradasJg2)
                {
                    if (posicao.Equals(pos))
                        return false;
                }
                return true;
            }
        }

        private bool[,] TirosPossiveis(Jogador jogador)
        {
            List<Posicao> posicoes = (jogador.Equals(_jogador1)) ? _posicoesAtiradasJg1 : _posicoesAtiradasJg2;
            bool[,] tirosPossiveis = new bool[10, 10];

            foreach (Posicao pos in posicoes)
            {
                tirosPossiveis[pos.Linha, pos.Coluna] = true;
            }
            return tirosPossiveis;
        }

        private void AdicionarPosicaoAtiradaLista(Posicao pos, Jogador jogador)
        {
            if (jogador.Equals(_jogador1))
                _posicoesAtiradasJg1.Add(pos);
            else
                _posicoesAtiradasJg2.Add(pos);
        }

        private Posicao EncontrarPosicaoParaAtirarTutorial(TabuleiroBatalhaNaval tabuleiro, bool acertar)
        {
            for (int i = 0; i < tabuleiro.Tamanho; i++)
            {
                for (int j = 0; j < tabuleiro.Tamanho; j++)
                {
                    if (acertar)
                    {
                        if (tabuleiro.MatrizNavios[i, j] != null)
                            return new Posicao(i, j);
                    }
                    else
                    {
                        if (tabuleiro.MatrizNavios[i, j] == null)
                            return new Posicao(i, j);
                    }
                }
            }
            return new Posicao(0, 0);
        }
    }
}
