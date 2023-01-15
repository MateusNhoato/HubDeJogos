using HubDeJogos.Hub.Models.Interfaces;
using HubDeJogos.Xadrez.Models.Pecas;

namespace HubDeJogos.Xadrez.Models.Tabuleiro
{
    public class TabuleiroDeXadrez : HubDeJogos.Models.Tabuleiro, IAlteraTabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private readonly Peca[,] _pecas;

        public TabuleiroDeXadrez(int tamanho)
        {
            Tamanho = tamanho;
            Linhas = tamanho;
            Colunas = tamanho;
            _pecas = new Peca[Linhas, Colunas];
            TabuleiroMatriz = new string[Tamanho, Tamanho];
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

        public void AlterarTabuleiroMatrizParaRegistro()
        {
            //for (int i=0; i<Tamanho; i++)
            //{
            //    for(int j=0; j<Tamanho; i++)
            //    {
            //        if (_pecas[i, j] != null)
            //            TabuleiroMatriz[i, j] = _pecas[i, j].ToString() + " ";
            //        else
            //            TabuleiroMatriz[i, j] = "-";
            //    }
            //}
            for(int i=0; i<_pecas.GetLength(1); i++) 
            {
                for(int j=0; j<_pecas.GetLength(0); j++)
                {
                    if (_pecas[i,j] != null)
                       TabuleiroMatriz[i,j] = _pecas[i,j].ToString() + " ";
                    else
                        TabuleiroMatriz[i, j] = "- ";
                }
                Console.WriteLine();
            }
           
        }
    }

}
