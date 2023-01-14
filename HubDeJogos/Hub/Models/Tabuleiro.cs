
namespace HubDeJogos.Models
{
    public abstract class Tabuleiro
    {
        public abstract Object[,] TabuleiroMatriz { get; protected set; }

        protected abstract string[,] GerarTabuleiro();
    }
}
