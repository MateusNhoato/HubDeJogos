using HubDeJogos.Xadrez.Models.Enums;
using HubDeJogos.Xadrez.Models.Tabuleiro;


namespace HubDeJogos.Xadrez.Models.Pecas
{
    public abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public TabuleiroDeXadrez Tab { get; protected set; } 


        public Peca(Cor cor,  TabuleiroDeXadrez tab)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QteMovimentos= 0;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matriz = MovimentosPossiveis();
            for(int i=0; i< Tab.Linhas; i++)
            {
                for(int j=0; j < Tab.Colunas; j++)
                {
                    if (matriz[i, j])
                        return true;
                }
            }
            return false;
        }

        public void IncrementarQteMovimentos() => QteMovimentos++;
       

        public void DecrementarQteMovimentos() => QteMovimentos--;
        
        public bool MovimentoPossivel(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }
        public abstract bool[,] MovimentosPossiveis();
        
    }
}
