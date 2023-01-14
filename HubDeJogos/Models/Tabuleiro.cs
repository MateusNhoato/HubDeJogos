namespace HubDeJogos.Models
{
    public abstract class Tabuleiro
    {
        public abstract string[,] TabuleiroMatriz { get; protected set; }

        protected abstract string[,] GerarTabuleiro();
    }
}
