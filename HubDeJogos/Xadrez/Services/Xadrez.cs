using HubDeJogos.Hub.Repositories;
using HubDeJogos.Hub.Views;
using HubDeJogos.Models;
using HubDeJogos.Models.Enums;
using HubDeJogos.Repositories;
using HubDeJogos.Xadrez.Models;
using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Xadrez.Models.Pecas;
using HubDeJogos.Xadrez.Models.Tabuleiro;
using HubDeJogos.Xadrez.Repositories;
using HubDeJogos.Xadrez.Views;
using System.Text.RegularExpressions;
using Utilidades;

namespace HubDeJogos.Xadrez.Services;

public class Xadrez
{
    public TabuleiroDeXadrez Tabuleiro { get; private set; }
    public int Turno { get; private set; }
    public Cor CorAtual { get; private set; }
    public bool Terminada { get; private set; }
    public Peca? VulneravelEnPassant { get; private set; }
    private readonly HashSet<Peca> _pecas;
    private readonly HashSet<Peca> _capturadas;
    public bool Xeque { get; private set; }
    public bool Empate { get; private set; } = false;
    public bool Render { get; private set; } = false;
    private bool _roquePequeno = false;
    private bool _roqueGrande = false;
    private bool _tutorial = false;
    private readonly Tela _tela = new();
    public readonly Jogador Jogador1;
    public readonly Jogador Jogador2;
    private readonly Pgn? _pgn;
    private bool _brancasPediuEmpate = false;
    private bool _pretasPediuEmpate = false;


    public Xadrez()
    {
        Tabuleiro = new TabuleiroDeXadrez(8, this);
        Turno = 1;
        CorAtual = Cor.Brancas;
        Terminada = false;
        VulneravelEnPassant = null;
        _pecas = Tabuleiro.PecasIniciais;
        _capturadas = new HashSet<Peca>();
        _tutorial = true;
        Xeque = false;
        Jogador1 = new Jogador("Fulano", "");
        Jogador2 = new Jogador("Beltrano", "");

        Tutorial();

    }
    public Xadrez(Jogador jogador1, Jogador jogador2)
    {
        Tabuleiro = new TabuleiroDeXadrez(8, this);
        Turno = 1;
        CorAtual = Cor.Brancas;
        Terminada = false;
        VulneravelEnPassant = null;
        _pecas = Tabuleiro.PecasIniciais;
        _capturadas = new HashSet<Peca>();
        Xeque = false;
        Jogador1 = jogador1;
        Jogador2 = jogador2;
        _pgn = new Pgn(jogador1.NomeDeUsuario, jogador2.NomeDeUsuario);
        Jogar();
    }


    private void Tutorial()
    {
        for (int i = 0; i < 4; i++)
        {
            Console.Clear();
            Console.WriteLine(Tutoriais.ExplicacoesXadrez[i]);
            Utilidades.Utilidades.AperteEnterParaContinuar();

            if (i == 0)
            {
                _tela.ImprimirTabuleiro(new TabuleiroDeXadrez(8, this), new bool[8, 8]);
            }
            else if (i == 1)
            {
                _tela.ImprimirPartida(this);
                Console.WriteLine("  Jogada: a2");
                Utilidades.Utilidades.AperteEnterParaContinuar();

                Posicao origem = _tela.LerPosicaoXadrez("a2").ToPosicao();
                ValidarPosicaoDeOrigem(origem);
                bool[,] posicoesPossiveis = Tabuleiro.Peca(origem).MovimentosPossiveis();
                Console.Clear();
                _tela.ImprimirTabuleiro(Tabuleiro, posicoesPossiveis);
                Console.WriteLine("\n  A peça selecionada foi o Peão Branco na 'a2', logo o destaque aparece\n" +
                                  "  nas posições 'a3' e 'a4', que são os movimentos disponíveis para ele.\n" +
                                  "  Vamos mover o peão para 'a4'.");
                Utilidades.Utilidades.AperteEnterParaContinuar();

                Posicao destino = _tela.LerPosicaoXadrez("a4").ToPosicao();
                ValidarPosicaoDeDestino(origem, destino);


                RealizaJogada(origem, destino);
                _tela.ImprimirPartida(this);
            }
            else if (i == 2)
            {
                _tela.ImprimirPartida(this);
                Console.WriteLine("  Jogada: render");
                Utilidades.Utilidades.AperteEnterParaContinuar();
                Terminada = true;
                Render = true;
                MudaJogador();
                _tela.ImprimirPartida(this);
                Console.WriteLine("\n\n  Beltrano(Pretas) se rendeu, logo quem ganhou foi Fulano(Brancas)!");
            }
            else
            {
                Console.WriteLine("\n  Peças:");
                Console.WriteLine(Tutoriais.ArteFinalTutorialXadrez);
            }
            Utilidades.Utilidades.AperteEnterParaContinuar();
        }
    }


    private void Jogar()
    {
        string padraoParaJogada = @"^[a-h][1-8]$";
        Regex rg = new Regex(padraoParaJogada);
        while (!Terminada)
        {
            try
            {
                string jogada;
                do
                {
                    Console.Clear();
                    _tela.ImprimirPartida(this);
                    Console.Write("\n  Jogada: ");
                    jogada = Console.ReadLine().ToLower();
                    if (jogada == "render")
                    {
                        Terminada = true;
                        Render = true;
                        MudaJogador();
                        break;
                    }
                    // no caso de um dos jogadores sugerir empate
                    else if (jogada == "empate")
                    {
                        if (PodePedirEmpate())
                        {
                            string nomeDoJogador =
                            (CorAtual == Cor.Brancas) ? Jogador1.NomeDeUsuario : Jogador2.NomeDeUsuario;
                            // mudando de jogador para ver se o outro jogador concorda com o empate
                            MudaJogador();
                            Console.Clear();
                            _tela.ImprimirPartida(this);
                            Console.WriteLine($"  {nomeDoJogador}({CorAtual}) sugeriu um empate. Caso queria aceitar basta digitar 'empate'");
                            Console.Write("\n  Jogada: ");
                            jogada = Console.ReadLine().ToLower();

                            if (jogada == "empate")
                            {
                                Terminada = true;
                                Empate = true;
                                break;
                            }
                            else
                                MudaJogador();
                        }
                        else
                        {
                            Console.WriteLine("  Cada jogador só pode pedir empate uma vez!");
                            Utilidades.Utilidades.AperteEnterParaContinuar();
                        }
                    }
                } while (!rg.IsMatch(jogada));

                if (Terminada)
                    break;

                Posicao origem = _tela.LerPosicaoXadrez(jogada).ToPosicao();
                ValidarPosicaoDeOrigem(origem);
                bool[,] posicoesPossiveis = Tabuleiro.Peca(origem).MovimentosPossiveis();

                do
                {
                    Console.Clear();
                    _tela.ImprimirTabuleiro(Tabuleiro, posicoesPossiveis);
                    Console.Write("\n  Destino: ");
                    jogada = Console.ReadLine().ToLower();
                } while (!rg.IsMatch(jogada));


                Posicao destino = _tela.LerPosicaoXadrez(jogada).ToPosicao();
                ValidarPosicaoDeDestino(origem, destino);

                RealizaJogada(origem, destino);

            }
            catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        // informações da partida para registro
        string vencedor = Jogador1.NomeDeUsuario;
        string jogador1 = Jogador1.NomeDeUsuario;
        string jogador2 = Jogador2.NomeDeUsuario;
        Resultado resultado = Resultado.Decisivo;

        string resultadoPgn = "1-0";

        if (CorAtual is Cor.Pretas)
        {
            vencedor = Jogador2.NomeDeUsuario;
            resultadoPgn = "0-1";
        }

        if (Empate)
        {
            resultado = Resultado.Empate;
            vencedor = null;
            resultadoPgn = "1/2-1/2";
        }

        // alterando tabuleiro para registrá-lo
        Tabuleiro.AlterarTabuleiroMatrizParaRegistro();
        Partida partida = new Partida(Jogo.Xadrez, jogador1, jogador2, vencedor, resultado, Tabuleiro, null);

        //adicionando a partida aos historicos
        Partidas.HistoricoDePartidas.Add(partida);
        Partidas.SalvarPartidas();
        Jogador1.HistoricoDePartidas.Add(partida);
        Jogador2.HistoricoDePartidas.Add(partida);

        //adicionando resultado ao pgn e criando o arquivo da pgn da partida
        _pgn.Resultado = resultadoPgn;
        Pgns.CriarArquivoPgn(_pgn);



        Console.Clear();
        _tela.ImprimirPartida(this);
        Utilidades.Utilidades.AperteEnterParaContinuar();
        Utilidades.Som.Musica(Musica.pgn);
        _tela.ImprimirPgn(_pgn);
        Utilidades.Utilidades.AperteEnterParaContinuar();
    }


    private Peca? ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca peca = Tabuleiro.RetirarPeca(origem);
        peca.IncrementarQteMovimentos();
        Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
        Tabuleiro.ColocarPeca(peca, destino);

        if (pecaCapturada != null)
        {
            _capturadas.Add(pecaCapturada);
        }

        // #jogadaespecial roque pequeno
        if (peca is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca torre = Tabuleiro.RetirarPeca(origemTorre);
            torre.IncrementarQteMovimentos();
            Tabuleiro.ColocarPeca(torre, destinoTorre);

            _roquePequeno = true;
        }

        // #jogadaespecial roque grande
        if (peca is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca torre = Tabuleiro.RetirarPeca(origemTorre);
            torre.IncrementarQteMovimentos();
            Tabuleiro.ColocarPeca(torre, destinoTorre);

            _roqueGrande = true;
        }


        // #jogadaespecial en passant
        if (peca is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == null)
            {
                Posicao posPeao;
                if (peca.Cor == Cor.Brancas)
                {
                    posPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                }
                else
                {
                    posPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                }
                pecaCapturada = Tabuleiro.RetirarPeca(posPeao);
                _capturadas.Add(pecaCapturada);
            }
        }



        return pecaCapturada;
    }


    private void ValidarPosicaoDeOrigem(Posicao pos)
    {
        if (Tabuleiro.Peca(pos) == null)
            throw new TabuleiroException("  Não existe peça na posição de origem escolhida!");
        if (CorAtual != Tabuleiro.Peca(pos).Cor)
            throw new TabuleiroException("  A peça escolhida não é sua!");
        if (!Tabuleiro.Peca(pos).ExisteMovimentosPossiveis())
            throw new TabuleiroException("  Não há movimentos possíveis para a peça!");
    }

    private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca peca = Tabuleiro.RetirarPeca(destino);
        peca.DecrementarQteMovimentos();
        if (pecaCapturada != null)
        {
            Tabuleiro.ColocarPeca(pecaCapturada, destino);
            _capturadas.Remove(pecaCapturada);
        }
        Tabuleiro.ColocarPeca(peca, origem);

        // #jogadaespecial roque pequeno
        if (peca is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
            torre.DecrementarQteMovimentos();
            Tabuleiro.ColocarPeca(torre, origemTorre);
        }

        // #jogadaespecial roque grande
        if (peca is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
            torre.DecrementarQteMovimentos();
            Tabuleiro.ColocarPeca(torre, origemTorre);
        }

        // #jogadaespecial en passant
        if (peca is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
            {
                Peca peao = Tabuleiro.RetirarPeca(destino);
                Posicao posP;
                if (peca.Cor == Cor.Brancas)
                {
                    posP = new Posicao(3, destino.Coluna);
                }
                else
                {
                    posP = new Posicao(4, destino.Coluna);
                }
                Tabuleiro.ColocarPeca(peao, posP);
            }
        }
    }



    private void RealizaJogada(Posicao origem, Posicao destino)
    {

        // string para registrar jogadas no pgn
        string jogada = string.Empty;

        if (CorAtual == Cor.Brancas)
            jogada = $"{Turno}.";

        // pegando qual peça se moveu para pgn, se foi peão fica vazio
        Peca pecaOrigem = Tabuleiro.Peca(origem);
        if (pecaOrigem is not Peao)
            jogada += pecaOrigem.ToString();



        //checkando ambiguidades para o registro pgn    

        foreach (Peca pecaChecagem in PecasEmJogo(pecaOrigem.Cor))
        {
            if (!pecaChecagem.MesmoTipoDePeca(pecaOrigem))
                continue;

            if (pecaChecagem.MovimentosPossiveis()[destino.Linha, destino.Coluna] == true)
            {
                // comparando as posições para ver as diferenças
                if (pecaChecagem.Posicao.Equals(pecaOrigem.Posicao))
                    continue;
                else
                {

                    PosicaoXadrez pos = pecaOrigem.Posicao.ToPosicaoXadrez();
                    string posicaoDiferente = pos.Coluna.ToString();

                    if (pecaChecagem.Posicao.Coluna == pecaOrigem.Posicao.Coluna)
                        posicaoDiferente = (8 - pecaOrigem.Posicao.Linha).ToString();

                    jogada += posicaoDiferente;
                }
            }
        }
        Peca pecaCapturada = ExecutaMovimento(origem, destino);
        // se houve alguma captura adicionamos 'x' no pgn
        if (pecaCapturada != null)
        {
            if (pecaOrigem is not Peao)
                jogada += "x";
            else
            {
                PosicaoXadrez pos = origem.ToPosicaoXadrez();
                string colunaPeao = pos.Coluna.ToString();
                jogada += $"{colunaPeao}x";
            }
        }


        if (EstaEmXeque(CorAtual))
        {
            DesfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("  Você não pode se colocar em xeque!");
        }

        Peca peca = Tabuleiro.Peca(destino);
        // adicionando o destino no pgn
        jogada += $"{destino.ToPosicaoXadrez()}";

        // #jogadaespecial promoção
        if (peca is Peao)
        {
            if ((peca.Cor == Cor.Brancas && destino.Linha == 0) || (peca.Cor == Cor.Pretas && destino.Linha == 7))
            {
                string escolha = string.Empty;
                string[] escolhasDePeca = { "Q", "R", "B", "N" };
                Peca novaPeca = null;

                do
                {
                    Console.Clear();
                    _tela.ImprimirTabuleiro(Tabuleiro);
                    Console.WriteLine($"  Destino: {destino.ToPosicaoXadrez()}\n");
                    Console.WriteLine($"  O peão foi promovido na casa {destino.ToPosicaoXadrez()}!");
                    Console.WriteLine("  Escolha uma das seguintes peças para o Peão se transformar: ");
                    Console.WriteLine("  Q = Rainha | R = Torre | B = Bispo | N = Cavalo\n");
                    Console.Write("  Peça escolhida: ");
                    escolha = Console.ReadLine().ToUpper();
                } while (!escolhasDePeca.Contains(escolha));


                switch (escolha)
                {
                    case "Q":
                        novaPeca = new Rainha(Tabuleiro, peca.Cor);
                        break;
                    case "R":
                        novaPeca = new Torre(Tabuleiro, peca.Cor);
                        break;
                    case "B":
                        novaPeca = new Bispo(Tabuleiro, peca.Cor);
                        break;
                    case "N":
                        novaPeca = new Cavalo(Tabuleiro, peca.Cor);
                        break;
                }

                // após a escolha, a troca de peças ocorre
                peca = Tabuleiro.RetirarPeca(destino);
                _pecas.Remove(peca);
                Tabuleiro.ColocarPeca(novaPeca, destino);
                _pecas.Add(novaPeca);

                //adicionando a promoção no pgn
                jogada += $"={novaPeca}";

            }
        }

        // pgn: caso dos Roques
        if (_roquePequeno)
        {
            if (CorAtual == Cor.Brancas)
                jogada = $"{Turno}.O-O";
            else
                jogada = "O-O";

            _roquePequeno = false;
        }
        else if (_roqueGrande)
        {
            if (CorAtual == Cor.Brancas)
                jogada = $"{Turno}.O-O-O";
            else
                jogada = "O-O-O";

            _roqueGrande = false;
        }

        // checkando xeque em xequemate
        if (EstaEmXeque(_adversaria(CorAtual)))
        {
            Xeque = true;
            jogada += "+";

        }
        else
        {
            Xeque = false;
        }

        if (TesteXequemate(_adversaria(CorAtual)))
        {
            Terminada = true;
            jogada = jogada.Replace('+', '#');
        }
        else
        {
            if (CorAtual == Cor.Pretas)
                Turno++;
            MudaJogador();
        }


        // #jogadaespecial en passant
        if (peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            VulneravelEnPassant = peca;
        else
            VulneravelEnPassant = null;

        if (!_tutorial)
        {
            if (!EstaEmXeque(peca.Cor))
                _pgn.Jogadas.Add(jogada);
            //reproduzindo o som
            Som.MovimentoPecaXadrez(jogada);
        }
    }

    private void MudaJogador()
    {
        if (CorAtual == Cor.Brancas)
            CorAtual = Cor.Pretas;
        else
            CorAtual = Cor.Brancas;
    }

    private void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
        if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
            throw new TabuleiroException("  Posição de destino inválida.");
    }


    private Cor _adversaria(Cor cor)
    {
        if (cor == Cor.Brancas)
            return Cor.Pretas;
        else
            return Cor.Brancas;
    }

    private Peca _rei(Cor cor)
    {
        foreach (Peca p in PecasEmJogo(cor))
        {
            if (p is Rei)
                return p;
        }
        return null;
    }

    private bool EstaEmXeque(Cor cor)
    {
        Peca R = _rei(cor);
        foreach (Peca p in PecasEmJogo(_adversaria(cor)))
        {
            bool[,] matriz = p.MovimentosPossiveis();
            if (matriz[R.Posicao.Linha, R.Posicao.Coluna])
                return true;
        }
        return false;
    }

    private bool TesteXequemate(Cor cor)
    {
        if (!EstaEmXeque(cor))
            return false;

        foreach (Peca p in PecasEmJogo(cor))
        {
            bool[,] matriz = p.MovimentosPossiveis();
            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (matriz[i, j])
                    {
                        Posicao origem = p.Posicao;
                        Posicao destino = new Posicao(i, j);
                        Peca pecaCapturada = ExecutaMovimento(origem, destino);
                        bool testeXeque = EstaEmXeque(cor);
                        DesfazMovimento(origem, destino, pecaCapturada);

                        if (!testeXeque)
                            return false;
                    }
                }
            }
        }
        return true;

    }



    public HashSet<Peca> PecasCapturadas(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca p in _capturadas)
        {
            if (p.Cor == cor)
                aux.Add(p);
        }
        return aux;
    }

    private HashSet<Peca> PecasEmJogo(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca p in _pecas)
        {
            if (p.Cor == cor)
                aux.Add(p);
        }
        aux.ExceptWith(PecasCapturadas(cor));
        return aux;
    }

    private bool PodePedirEmpate()
    {
        if (CorAtual == Cor.Brancas)
        {
            if (!_brancasPediuEmpate)
            {
                _brancasPediuEmpate = true;
                return true;
            }
            return false;
        }

        if (!_pretasPediuEmpate)
        {
            _pretasPediuEmpate = true;
            return true;
        }
        return false;
    }


}
