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
            _tabuleiroAtual = _tabuleiroJogador1;
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


        private void Jogar()
        {
            Jogador jogador = _jogador1;
            
            while (_tabuleiroAtual.NumeroDeNavios > 0)
            {
                string padraoParaJogada = @"^[a-j]([1-9]|10)$";
                Regex rg = new Regex(padraoParaJogada);
                string jogada;
                do
                {


                    _tela.ImprimirTabuleiro(_tabuleiroAtual, TirosPossiveis(jogador));
                    Console.WriteLine($"\n  Vez de {jogador.NomeDeUsuario}");
                    Console.Write("  Digite sua jogada: ");
                    jogada = Console.ReadLine().ToLower();
                } while (!rg.IsMatch(jogada));

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
                bool tiro = Atirar(pos);            
                _tela.ImprimirTabuleiro(_tabuleiroAtual, pos);
                Thread.Sleep(1000);

                if (!tiro)
                {
                    jogador = MudarJogador(jogador);
                    MudarTabuleiro();
                }
                else
                {
                    if (_tabuleiroAtual.CheckarNavioDestruido())
                    {
                        ConsoleColor aux = Console.ForegroundColor;

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\n  Navio afundado!!!");
                        Som.NavioAfundado();
                        Thread.Sleep(1000);
                        Console.ForegroundColor = aux;
                    }
                }                   
            }
            _tela.ImprimirTabuleiro(_tabuleiroAtual, TirosPossiveis(jogador));
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
            Utilidades.Utilidades.AperteEnterParaContinuar();
        }


        private bool Atirar(Posicao pos)
        {
            if (_tabuleiroAtual.MatrizNavios[pos.Linha, pos.Coluna] != null)
            {
                _tabuleiroAtual.MatrizNavios[pos.Linha, pos.Coluna].Destruir();
                Som.BatalhaNaval(true);
                return true;
            }
            Som.BatalhaNaval(false);
            return false;
        }

        private Jogador MudarJogador(Jogador jogador)
        {
            if (jogador.Equals(_jogador1))
               return _jogador2;
            else
                return _jogador1;
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
                foreach(Posicao posicao in _posicoesAtiradasJg1)
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
            return (jogador == _jogador1) ?
            TabuleiroBatalhaNaval.TirosPossiveis(_posicoesAtiradasJg1) :
            TabuleiroBatalhaNaval.TirosPossiveis(_posicoesAtiradasJg2);
        }
        private void AdicionarPosicaoAtiradaLista(Posicao pos, Jogador jogador)
        {
            if (jogador.Equals(_jogador1))
                _posicoesAtiradasJg1.Add(pos);
            else
                _posicoesAtiradasJg2.Add(pos);
        }
    }
}
