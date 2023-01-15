using HubDeJogos.Xadrez.Models;
using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Xadrez.Models.Tabuleiro;
using HubDeJogos.Xadrez.Models.Pecas;
using HubDeJogos.Xadrez.Views;
using HubDeJogos.Models;

namespace HubDeJogos.Xadrez.Services;

public class Xadrez
{
    public TabuleiroDeXadrez Tabuleiro { get; private set; }
    public int Turno { get; private set; }
    public Cor CorAtual { get; private set; }
    public bool Terminada { get; private set; }
    public Peca VulneravelEnPassant { get; private set; }
    private readonly HashSet<Peca> _pecas;
    private readonly HashSet<Peca> _capturadas;
    public bool Xeque {get; private set;}
    private readonly Tela _tela = new();
    private readonly Jogador _jogador1;
    private readonly Jogador _jogador2;


    public Xadrez(Jogador jogador1, Jogador jogador2)
    {
        Tabuleiro = new TabuleiroDeXadrez(8);
        Turno = 1;
        CorAtual = Cor.Branca;
        Terminada = false;
        VulneravelEnPassant = null;
        _pecas = new HashSet<Peca>();
        _capturadas= new HashSet<Peca>();
        Xeque = false;
        _jogador1 = jogador1;
        _jogador2 = jogador2;
        colocarPecas();
        Jogar();
    }

    public void Jogar()
    {
        while (!Terminada)
        {
            try
            {
                Console.Clear();
                _tela.imprimirPartida(this);



                Console.Write("\nOrigem: ");
                Posicao origem = _tela.lerPosicaoXadrez().toPosicao();
                validarPosicaoDeOrigem(origem);

                bool[,] posicoesPossiveis = Tabuleiro.peca(origem).movimentosPossiveis();
                Console.Clear();
                _tela.imprimirTabuleiro(Tabuleiro, posicoesPossiveis);


                Console.Write("\nDestino: ");
                Posicao destino = _tela.lerPosicaoXadrez().toPosicao();
                validarPosicaoDeDestino(origem, destino);

                realizaJogada(origem, destino);

            }
            catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        Console.Clear();
        _tela.imprimirPartida(this);


    }


    public Peca? executaMovimento(Posicao origem, Posicao destino)
    {
        Peca peca = Tabuleiro.retirarPeca(origem);
        peca.incrementarQteMovimentos();
        Peca pecaCapturada = Tabuleiro.retirarPeca(destino);
        Tabuleiro.colocarPeca(peca, destino);

        if(pecaCapturada != null) 
        {
            _capturadas.Add(pecaCapturada);
        }

        // #jogadaespecial roque pequeno
        if(peca is Rei && destino.Coluna == origem.Coluna +2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna +3);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca torre = Tabuleiro.retirarPeca(origemTorre);
            torre.incrementarQteMovimentos();
            Tabuleiro.colocarPeca(torre, destinoTorre);
        }

        // #jogadaespecial roque grande
        if (peca is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca torre = Tabuleiro.retirarPeca(origemTorre);
            torre.incrementarQteMovimentos();
            Tabuleiro.colocarPeca(torre, destinoTorre);
        }


        // #jogadaespecial en passant
        if(peca is Peao)
        {
            if(origem.Coluna != destino.Coluna && pecaCapturada == null)
            {
                Posicao posPeao;
                if(peca.Cor == Cor.Branca)
                {
                    posPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                }
                else
                {
                    posPeao = new Posicao(destino.Linha -1, destino.Coluna);
                }
                pecaCapturada = Tabuleiro.retirarPeca(posPeao);
                _capturadas.Add(pecaCapturada);
            }
        }



        return pecaCapturada;
    }

    
    public void validarPosicaoDeOrigem(Posicao pos)
    {
        if (Tabuleiro.peca(pos) == null)
            throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
        if (CorAtual != Tabuleiro.peca(pos).Cor)
            throw new TabuleiroException("A peça escolhida não é sua!");
        if (!Tabuleiro.peca(pos).existeMovimentosPossiveis())
            throw new TabuleiroException("Não há movimentos possíveis para a peça!");
    }
    
    public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca peca = Tabuleiro.retirarPeca(destino);
        peca.decrementarQteMovimentos();
        if(pecaCapturada != null)
        {
            Tabuleiro.colocarPeca(pecaCapturada, destino);
            _capturadas.Remove(pecaCapturada);
        }
        Tabuleiro.colocarPeca(peca, origem);

        // #jogadaespecial roque pequeno
        if (peca is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca torre = Tabuleiro.retirarPeca(destinoTorre);
            torre.decrementarQteMovimentos();
            Tabuleiro.colocarPeca(torre, origemTorre);
        }

        // #jogadaespecial roque grande
        if (peca is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca torre = Tabuleiro.retirarPeca(destinoTorre);
            torre.decrementarQteMovimentos();
            Tabuleiro.colocarPeca(torre, origemTorre);
        }

        // #jogadaespecial en passant
        if (peca is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
            {
                Peca peao = Tabuleiro.retirarPeca(destino);
                Posicao posP;
                if(peca.Cor == Cor.Branca)
                {
                    posP = new Posicao(3, destino.Coluna);
                }
                else
                {
                    posP = new Posicao(4, destino.Coluna);
                }
                Tabuleiro.colocarPeca(peao, posP);
            }
        }
    }
    
    
    
    public void realizaJogada(Posicao origem, Posicao destino)
    {
       Peca pecaCapturada = executaMovimento(origem, destino);

        if(estaEmXeque(CorAtual))
        {
            desfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Você não pode se colocar em xeque!");
        }

        Peca peca = Tabuleiro.peca(destino);

        // #jogadaespecial promoção
        if(peca is Peao)
        {
          if((peca.Cor == Cor.Branca && destino.Linha == 0) || (peca.Cor == Cor.Preta && destino.Linha == 7))
            {
                peca = Tabuleiro.retirarPeca(destino);
                _pecas.Remove(peca);
                Peca dama = new Dama(Tabuleiro, peca.Cor);
                Tabuleiro.colocarPeca(dama, destino);
                _pecas.Add(dama);
            }
        }


        if (estaEmXeque(adversaria(CorAtual)))
        {
            Xeque = true;
        }
        else
        {
            Xeque = false;
        }

        if (testeXequemate(adversaria(CorAtual)))
        {
            Terminada = true;
        }
        else
        {
            Turno++;
            mudaJogador();
        }

        

        // #jogadaespecial en passant
        if (peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            VulneravelEnPassant = peca;
        else
            VulneravelEnPassant = null;
    }

    private void mudaJogador()
    {
        if (CorAtual == Cor.Branca)
            CorAtual = Cor.Preta;
        else
            CorAtual = Cor.Branca;
    }

    public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
        if (!Tabuleiro.peca(origem).movimentoPossivel(destino))
            throw new TabuleiroException("Posição de destino inválida.");
    }


    private Cor adversaria(Cor cor)
    {
        if (cor == Cor.Branca)
            return Cor.Preta;
        else
            return Cor.Branca;
    }

    private Peca rei(Cor cor)
    {
        foreach(Peca p in pecasEmJogo(cor))
        {
            if (p is Rei)
                return p;
        }
        return null;
    }

    public bool estaEmXeque(Cor cor)
    {
        Peca R = rei(cor);
        foreach(Peca p in pecasEmJogo(adversaria(cor)))
        {
            bool[,] matriz = p.movimentosPossiveis();
            if (matriz[R.Posicao.Linha, R.Posicao.Coluna])
                return true;
        }
        return false;
    }

    public bool testeXequemate(Cor cor)
    {
        if (!estaEmXeque(cor)) 
            return false;
    
        foreach(Peca p in pecasEmJogo(cor))
        {
            bool[,] matriz = p.movimentosPossiveis();
            for(int i=0; i<Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (matriz[i, j])
                    {
                        Posicao origem = p.Posicao;
                        Posicao destino = new Posicao(i, j);
                        Peca pecaCapturada = executaMovimento(origem, destino);
                        bool testeXeque = estaEmXeque(cor);
                        desfazMovimento(origem, destino, pecaCapturada);

                        if (!testeXeque)
                            return false;
                    }
                }
            }             
        }
        return true;
    
    }

    public void colocarNovaPeca(char coluna, int linha, Peca peca)
    {
        Tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
        _pecas.Add(peca);
    }

    public HashSet<Peca> pecasCapturadas(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach(Peca p in _capturadas)
        {
            if(p.Cor == cor)
                aux.Add(p);
        }
        return aux;
    }

    public HashSet<Peca> pecasEmJogo(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca p in _pecas)
        {
            if (p.Cor == cor)
                aux.Add(p);
        }
        aux.ExceptWith(pecasCapturadas(cor));
        return aux;
    }


    private void colocarPecas()
    {
        

        colocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
        colocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
        colocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
        colocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
        colocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
        colocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
        colocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
        colocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));
        colocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
        colocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
        colocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
        colocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
        colocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
        colocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
        colocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
        colocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));

        colocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
        colocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
        colocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
        colocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
        colocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
        colocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
        colocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
        colocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
        colocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
        colocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
        colocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
        colocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
        colocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
        colocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
        colocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
        colocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));


    }


}
