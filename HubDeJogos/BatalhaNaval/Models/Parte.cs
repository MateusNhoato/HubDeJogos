namespace HubDeJogos.BatalhaNaval.Models
{
    public class Parte
    {
        public Posicao Posicao { get; set; }
        public bool Destruida { get; private set; }

        public Parte()
        {
            Posicao = new Posicao(0, 0);
            Destruida = false;
        }

        public void Destruir()
        {
            Destruida = true;
        }
    }
}
