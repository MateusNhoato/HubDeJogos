namespace HubDeJogos.BatalhaNaval.Models
{
    public class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }


        public Posicao()
        {
            Random random = new Random();
            Linha = random.Next(0, 10);
            Coluna = random.Next(0, 10);
        }
        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public void MudarPosicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public string ParaPosicaoDeTabuleiro()
        {
            char coluna = 'a';
            switch (Coluna)
            {
                case 1:
                    coluna = 'b';
                    break;
                case 2:
                    coluna = 'c';
                    break;
                case 3:
                    coluna = 'd';
                    break;
                case 4:
                    coluna = 'e';
                    break;
                case 5:
                    coluna = 'f';
                    break;
                case 6:
                    coluna = 'g';
                    break;
                case 7:
                    coluna = 'h';
                    break;
                case 8:
                    coluna = 'i';
                    break;
                case 9:
                    coluna = 'j';
                    break;
            }
            return $"{coluna}{Linha + 1}";
        }

        public static Posicao DePosicaoDeTabuleiroParaPosicao(string jogada)
        {
            int linha = (int)Char.GetNumericValue(jogada[1]) - 1;
            char coluna = jogada[0];

            if (jogada.Length > 2)
                linha = 9;

            return new Posicao(linha, coluna - 'a');
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Posicao)
                return false;

            Posicao other = obj as Posicao;
            return (other.Coluna == Coluna && other.Linha == Linha);
        }
    }
}
