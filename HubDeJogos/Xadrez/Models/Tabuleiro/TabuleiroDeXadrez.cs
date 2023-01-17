using HubDeJogos.Hub.Models.Interfaces;
using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Xadrez.Models.Pecas;
using Newtonsoft.Json;

namespace HubDeJogos.Xadrez.Models.Tabuleiro
{
    public class TabuleiroDeXadrez : HubDeJogos.Models.Tabuleiro, IAlteraTabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private readonly Peca[,] _pecas;
        [JsonIgnore]
        public HashSet<Peca> PecasIniciais { get; private set; }
        private readonly Services.Xadrez _partida;

        public TabuleiroDeXadrez(int tamanho, Services.Xadrez partida)
        {
            Tamanho = tamanho;
            Linhas = tamanho;
            Colunas = tamanho;
            _pecas = new Peca[Linhas, Colunas];
            TabuleiroMatriz = new string[Tamanho, Tamanho];
            _partida = partida;
            PecasIniciais = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca Peca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        public Peca Peca(Posicao pos)
        {
            return _pecas[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }
        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
                throw new TabuleiroException("Já existe uma peça nessa posição!");

            _pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if (Peca(pos) == null)
                return null;
            Peca aux = Peca(pos);
            aux.Posicao = null;
            _pecas[pos.Linha, pos.Coluna] = null;
            return aux;

        }

        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas) return false;
            if (pos.Coluna < 0 || pos.Coluna >= Colunas) return false;
            return true;
        }
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
                throw new TabuleiroException("Posição inválida!");
        }

        private void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            PecasIniciais.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 2, new Peao(this, Cor.Brancas, _partida));
            ColocarNovaPeca('b', 2, new Peao(this, Cor.Brancas, _partida));
            ColocarNovaPeca('c', 2, new Peao(this, Cor.Brancas, _partida));
            ColocarNovaPeca('d', 2, new Peao(this, Cor.Brancas, _partida));
            ColocarNovaPeca('e', 2, new Peao(this, Cor.Brancas, _partida));
            ColocarNovaPeca('f', 2, new Peao(this, Cor.Brancas, _partida));
            ColocarNovaPeca('g', 2, new Peao(this, Cor.Brancas, _partida));
            ColocarNovaPeca('h', 2, new Peao(this, Cor.Brancas, _partida));
            ColocarNovaPeca('a', 1, new Torre(this, Cor.Brancas));
            ColocarNovaPeca('b', 1, new Cavalo(this, Cor.Brancas));
            ColocarNovaPeca('c', 1, new Bispo(this, Cor.Brancas));
            ColocarNovaPeca('d', 1, new Dama(this, Cor.Brancas));
            ColocarNovaPeca('e', 1, new Rei(this, Cor.Brancas, _partida));
            ColocarNovaPeca('f', 1, new Bispo(this, Cor.Brancas));
            ColocarNovaPeca('g', 1, new Cavalo(this, Cor.Brancas));
            ColocarNovaPeca('h', 1, new Torre(this, Cor.Brancas));

            ColocarNovaPeca('a', 7, new Peao(this, Cor.Pretas, _partida));
            ColocarNovaPeca('b', 7, new Peao(this, Cor.Pretas, _partida));
            ColocarNovaPeca('c', 7, new Peao(this, Cor.Pretas, _partida));
            ColocarNovaPeca('d', 7, new Peao(this, Cor.Pretas, _partida));
            ColocarNovaPeca('e', 7, new Peao(this, Cor.Pretas, _partida));
            ColocarNovaPeca('f', 7, new Peao(this, Cor.Pretas, _partida));
            ColocarNovaPeca('g', 7, new Peao(this, Cor.Pretas, _partida));
            ColocarNovaPeca('h', 7, new Peao(this, Cor.Pretas, _partida));
            ColocarNovaPeca('a', 8, new Torre(this, Cor.Pretas));
            ColocarNovaPeca('b', 8, new Cavalo(this, Cor.Pretas));
            ColocarNovaPeca('c', 8, new Bispo(this, Cor.Pretas));
            ColocarNovaPeca('d', 8, new Dama(this, Cor.Pretas));
            ColocarNovaPeca('e', 8, new Rei(this, Cor.Pretas, _partida));
            ColocarNovaPeca('f', 8, new Bispo(this, Cor.Pretas));
            ColocarNovaPeca('g', 8, new Cavalo(this, Cor.Pretas));
            ColocarNovaPeca('h', 8, new Torre(this, Cor.Pretas));
        }




        public void AlterarTabuleiroMatrizParaRegistro()
        {
            for(int i=0; i<_pecas.GetLength(1); i++) 
            {
                for(int j=0; j<_pecas.GetLength(0); j++)
                {
                    if (_pecas[i,j] != null)
                    {
                        if (_pecas[i,j].Cor == Enums.Cor.Brancas)
                                TabuleiroMatriz[i, j] = $"x{_pecas[i, j]} ";
                        else
                            TabuleiroMatriz[i, j] = $" {_pecas[i, j]} ";
                    }                       
                    else
                        TabuleiroMatriz[i, j] = "   ";
                }
                Console.WriteLine();
            }
           
        }
    }

}
