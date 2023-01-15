
using Newtonsoft.Json;

namespace HubDeJogos.Models
{
    
    public class Tabuleiro
    {
        public virtual object[,]? TabuleiroMatriz { get; protected set; }
        public virtual int Tamanho { get; protected set; }
        
        public Tabuleiro() { }
        public Tabuleiro(int tamanho) 
        {
            Tamanho = tamanho;
        }

        [JsonConstructor]
        public Tabuleiro(object[,] tabuleiroMatriz, int tamanho) : this (tamanho)
        {
            TabuleiroMatriz = tabuleiroMatriz;
        }
        
        public string TabuleiroParaImpressao()
        {
            string tabuleiroParaImpressao = string.Empty;
            for(int i=0; i<Tamanho; i++) 
            {
                for(int j=0; j<Tamanho; j++)
                {
                    tabuleiroParaImpressao += $"{TabuleiroMatriz[i, j]};";
                }
            }
            return tabuleiroParaImpressao;
        }
    }
}
