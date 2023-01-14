
namespace HubDeJogos.Models
{
    public abstract class Tabuleiro
    {
        public abstract object[,] TabuleiroMatriz { get; set; }
        public abstract int Tamanho { get; set; }
        protected abstract void GerarTabuleiro();
    }
}
