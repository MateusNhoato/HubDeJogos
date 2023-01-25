using HubDeJogos.BatalhaNaval.Models.Enum;
namespace HubDeJogos.BatalhaNaval.Models
{

    public class Navio
    {
        // horizontal ou vertical
        public Direcao Direcao { get; private set; }
        public int Tamanho { get; private set; }
        public List<Parte> Partes { get; private set; } = new List<Parte>();


        public Navio(int tamanho)
        {
            Random random = new Random();
            if (random.Next(1, 3) == 1)
                Direcao = Direcao.Horizontal;
            else
                Direcao = Direcao.Vertical;

            Tamanho = tamanho;

            for (int i = 0; i < Tamanho; i++)
                Partes.Add(new Parte());
        }

        public bool EstaDestruido()
        {
            foreach (Parte parte in Partes)
            {
                if (!parte.Destruida)
                    return false;
            }
            return true;
        }
    }
}
