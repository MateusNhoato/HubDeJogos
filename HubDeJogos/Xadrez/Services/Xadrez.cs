﻿using HubDeJogos.Xadrez.Models;
using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Xadrez.Models.Tabuleiro;
using HubDeJogos.Xadrez.Models.Pecas;
using HubDeJogos.Xadrez.Views;
using HubDeJogos.Models;
using HubDeJogos.Repositories;
using HubDeJogos.Models.Enums;
using HubDeJogos.Hub.Repositories;

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
    private bool _empate = false;
    private readonly Tela _tela = new();
    private readonly Jogador _jogador1;
    private readonly Jogador _jogador2;


    public Xadrez(Jogador jogador1, Jogador jogador2, bool tutorial)
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
        ColocarPecas();

        if (tutorial)
            Tutorial();
        else
            Jogar();
    }


    public void Tutorial()
    {
        string[] explicacoes = new string[5] 
        { 
            "", 
            "", 
            "", 
            "", 
            ""
        };
        
        for (int i=0; i <5; i++)
        {
            Console.Clear();
            _tela.ImprimirPartida(this);
            Console.WriteLine(explicacoes[i]);
        }
        
       
    }


    public void Jogar()
    {
        while (!Terminada)
        {
            try
            {
                Console.Clear();
                _tela.ImprimirPartida(this);



                Console.Write("\nOrigem: ");
                Posicao origem = _tela.LerPosicaoXadrez().toPosicao();
                ValidarPosicaoDeOrigem(origem);

                bool[,] posicoesPossiveis = Tabuleiro.Peca(origem).MovimentosPossiveis();
                Console.Clear();
                _tela.ImprimirTabuleiro(Tabuleiro, posicoesPossiveis);


                Console.Write("\nDestino: ");
                Posicao destino = _tela.LerPosicaoXadrez().toPosicao();
                ValidarPosicaoDeDestino(origem, destino);

                RealizaJogada(origem, destino);

            }
            catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        Console.Clear();
        _tela.ImprimirPartida(this);

        // informações da partida para registro
        string vencedor = _jogador1.NomeDeUsuario;
        string perdedor = _jogador2.NomeDeUsuario;
        Resultado resultado = Resultado.Decisivo;

        if(CorAtual is Cor.Preta)
        {
            vencedor = _jogador2.NomeDeUsuario;
            perdedor = _jogador1.NomeDeUsuario;
        }

        if (_empate)
            resultado = Resultado.Empate;

        // alterando tabuleiro para registrá-lo
        Tabuleiro.AlterarTabuleiroMatrizParaRegistro();
        Partida partida = new Partida(Jogo.Xadrez, vencedor, perdedor, resultado , Tabuleiro);

        //adicionando a partida aos historicos
        Partidas.HistoricoDePartidas.Add(partida);
        Partidas.SalvarPartidas();
        _jogador1.HistoricoDePartidas.Add(partida);
        _jogador2.HistoricoDePartidas.Add(partida);
    }


    public Peca? ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca peca = Tabuleiro.RetirarPeca(origem);
        peca.IncrementarQteMovimentos();
        Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
        Tabuleiro.ColocarPeca(peca, destino);

        if(pecaCapturada != null) 
        {
            _capturadas.Add(pecaCapturada);
        }

        // #jogadaespecial roque pequeno
        if(peca is Rei && destino.Coluna == origem.Coluna +2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna +3);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca torre = Tabuleiro.RetirarPeca(origemTorre);
            torre.IncrementarQteMovimentos();
            Tabuleiro.ColocarPeca(torre, destinoTorre);
        }

        // #jogadaespecial roque grande
        if (peca is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca torre = Tabuleiro.RetirarPeca(origemTorre);
            torre.IncrementarQteMovimentos();
            Tabuleiro.ColocarPeca(torre, destinoTorre);
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
                pecaCapturada = Tabuleiro.RetirarPeca(posPeao);
                _capturadas.Add(pecaCapturada);
            }
        }



        return pecaCapturada;
    }

    
    public void ValidarPosicaoDeOrigem(Posicao pos)
    {
        if (Tabuleiro.Peca(pos) == null)
            throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
        if (CorAtual != Tabuleiro.Peca(pos).Cor)
            throw new TabuleiroException("A peça escolhida não é sua!");
        if (!Tabuleiro.Peca(pos).ExisteMovimentosPossiveis())
            throw new TabuleiroException("Não há movimentos possíveis para a peça!");
    }
    
    public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca peca = Tabuleiro.RetirarPeca(destino);
        peca.DecrementarQteMovimentos();
        if(pecaCapturada != null)
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
                if(peca.Cor == Cor.Branca)
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
    
    
    
    public void RealizaJogada(Posicao origem, Posicao destino)
    {
       Peca pecaCapturada = ExecutaMovimento(origem, destino);

        if(EstaEmXeque(CorAtual))
        {
            DesfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Você não pode se colocar em xeque!");
        }

        Peca peca = Tabuleiro.Peca(destino);

        // #jogadaespecial promoção
        if(peca is Peao)
        {
          if((peca.Cor == Cor.Branca && destino.Linha == 0) || (peca.Cor == Cor.Preta && destino.Linha == 7))
            {
                peca = Tabuleiro.RetirarPeca(destino);
                _pecas.Remove(peca);
                Peca dama = new Dama(Tabuleiro, peca.Cor);
                Tabuleiro.ColocarPeca(dama, destino);
                _pecas.Add(dama);
            }
        }


        if (EstaEmXeque(_adversaria(CorAtual)))
        {
            Xeque = true;
        }
        else
        {
            Xeque = false;
        }

        if (TesteXequemate(_adversaria(CorAtual)))
        {
            Terminada = true;
        }
        else
        {
            Turno++;
            MudaJogador();
        }

        

        // #jogadaespecial en passant
        if (peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            VulneravelEnPassant = peca;
        else
            VulneravelEnPassant = null;
    }

    private void MudaJogador()
    {
        if (CorAtual == Cor.Branca)
            CorAtual = Cor.Preta;
        else
            CorAtual = Cor.Branca;
    }

    public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
        if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
            throw new TabuleiroException("Posição de destino inválida.");
    }


    private Cor _adversaria(Cor cor)
    {
        if (cor == Cor.Branca)
            return Cor.Preta;
        else
            return Cor.Branca;
    }

    private Peca _rei(Cor cor)
    {
        foreach(Peca p in PecasEmJogo(cor))
        {
            if (p is Rei)
                return p;
        }
        return null;
    }

    public bool EstaEmXeque(Cor cor)
    {
        Peca R = _rei(cor);
        foreach(Peca p in PecasEmJogo(_adversaria(cor)))
        {
            bool[,] matriz = p.MovimentosPossiveis();
            if (matriz[R.Posicao.Linha, R.Posicao.Coluna])
                return true;
        }
        return false;
    }

    public bool TesteXequemate(Cor cor)
    {
        if (!EstaEmXeque(cor)) 
            return false;
    
        foreach(Peca p in PecasEmJogo(cor))
        {
            bool[,] matriz = p.MovimentosPossiveis();
            for(int i=0; i<Tabuleiro.Linhas; i++)
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

    public void ColocarNovaPeca(char coluna, int linha, Peca peca)
    {
        Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
        _pecas.Add(peca);
    }

    public HashSet<Peca> PecasCapturadas(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach(Peca p in _capturadas)
        {
            if(p.Cor == cor)
                aux.Add(p);
        }
        return aux;
    }

    public HashSet<Peca> PecasEmJogo(Cor cor)
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


    private void ColocarPecas()
    {
        ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));

        ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));


    }


}
