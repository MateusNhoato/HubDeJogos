using HubDeJogos.Hub.Models.Interfaces;
using HubDeJogos.Models;

namespace HubDeJogos.BatalhaNaval.Models
{
    public class TabuleiroBatalhaNaval : Tabuleiro, IAlteraTabuleiro
    {
        public Parte?[,] MatrizNavios { get; private set; }
        public List<Navio> Navios { get; private set; }

        public int NumeroDeNavios => Navios.Count;

        public TabuleiroBatalhaNaval()
        {
            Tamanho = 10;
            MatrizNavios = new Parte?[10, 10];
            PreencherMatriz();
        }

        private void PreencherMatriz()
        {
            Navios = new List<Navio>();
            AdicionarNavio(2);
            AdicionarNavio(2);
            AdicionarNavio(2);
            AdicionarNavio(3);
            AdicionarNavio(3);
            AdicionarNavio(4);
            AdicionarNavio(4);

        }

        private void AdicionarNavio(int tamanho)
        {
            Navio navio = new Navio(tamanho);
            Posicao pos = new Posicao();
            Posicao posAux = new Posicao(pos.Linha, pos.Coluna);

            for (int i = 0; i < navio.Tamanho; i++)
            {
                if (MatrizNavios[posAux.Linha, posAux.Coluna] != null)
                {
                    AdicionarNavio(tamanho);
                    return;
                }

                if (navio.Direcao == Enum.Direcao.Horizontal)
                    posAux.Linha++;
                else
                    posAux.Coluna++;

                if (posAux.Coluna >= 10 || posAux.Linha >= 10)
                {
                    AdicionarNavio(tamanho);
                    return;
                }

            }

            for (int k = 0; k < navio.Tamanho; k++)
            {
                navio.Partes[k].Posicao.MudarPosicao(pos.Linha, pos.Coluna);
                MatrizNavios[pos.Linha, pos.Coluna] = navio.Partes[k];
                if (navio.Direcao == Enum.Direcao.Horizontal)
                    pos.Linha++;
                else
                    pos.Coluna++;
            }
            Navios.Add(navio);
        }


        public bool CheckarNavioDestruido()
        {
            for (int i = 0; i < NumeroDeNavios; i++)
            {
                Navio navio = Navios[i];

                if (navio.EstaDestruido())
                {
                    Navios.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }




        public void AlterarTabuleiroMatrizParaRegistro()
        {
            TabuleiroMatriz = new string[Tamanho, Tamanho];

            for (int i = 0; i < Tamanho; i++)
            {
                for (int j = 0; j < Tamanho; j++)
                {
                    string aux = "   ";

                    if (MatrizNavios[i, j] != null)
                    {
                        if (MatrizNavios[i, j].Destruida)
                            aux = " x ";
                        else
                            aux = " i ";
                    }

                    TabuleiroMatriz[i, j] = aux;
                }
            }
        }

    }
}
