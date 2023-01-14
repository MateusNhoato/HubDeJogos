
using Newtonsoft.Json;

namespace HubDeJogos.Models
{
    
    public class Tabuleiro
    {
        public virtual object[,] TabuleiroMatriz { get; protected set; }
        public virtual int Tamanho { get; protected set; }
        

        public Tabuleiro() { }

        [JsonConstructor]
        public Tabuleiro(object[,] tabuleiroMatriz, int tamanho)
        {
            TabuleiroMatriz = tabuleiroMatriz;
            Tamanho = tamanho;
        }

        public virtual string TabuleiroParaImpressao()
        {
            string tabuleiroParaImpressao = string.Empty;
            for(int i=0; i<Tamanho; i++) 
            {
                for(int j=0; j<Tamanho; j++)
                {
                    tabuleiroParaImpressao += $"{TabuleiroMatriz[i, j].ToString()};";
                }
            }
            return tabuleiroParaImpressao;
        }
    }
}
