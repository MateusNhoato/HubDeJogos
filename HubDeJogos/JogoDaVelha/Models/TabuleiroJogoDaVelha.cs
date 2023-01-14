using HubDeJogos.Models;

namespace HubDeJogos.JogoDaVelha.Models
{
    public class TabuleiroJogoDaVelha : Tabuleiro
    {
        public override object[,] TabuleiroMatriz { get; set; }
        public List<string>? JogadasPossiveis { get; private set; }
        private static int _tamanhoDoTabuleiro;


        // construtor para tabuleiro novo
        public TabuleiroJogoDaVelha(int tamanho)
        {
            // configurando o tabuleiro e gerando lista de jogadas possíveis
            Tamanho = tamanho;
            GerarTabuleiro();
            JogadasPossiveis = ListarJogadasPossiveis();

        }
        // construtor para tabuleiro de registro 
        public TabuleiroJogoDaVelha(string[,] matrizTabuleiro, int tamanhoDoTabuleiro)
        {
            TabuleiroMatriz = matrizTabuleiro;
            Tamanho = (tamanhoDoTabuleiro + 1) / 2;
        }

        public override int Tamanho
        {
            get { return _tamanhoDoTabuleiro; }
            set { _tamanhoDoTabuleiro = value * 2 - 1; }
        }

        // função para gerar o tabuleiro de 3 até 10 
        protected override void GerarTabuleiro()
        {
            int cont = 1;
            string[,] matrizTabuleiro = new string[Tamanho, Tamanho];

            for (int i = 0; i < Tamanho; i++)
            {
                for (int j = 0; j < Tamanho; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            if (cont > 9)
                                matrizTabuleiro[i, j] = $" {cont}";
                            else if (cont > 99)
                                matrizTabuleiro[i, j] = $"{cont}";
                            else
                                matrizTabuleiro[i, j] = $" {cont} ";
                            cont++;
                        }
                        else
                            matrizTabuleiro[i, j] = "|";
                    }
                    else
                    {
                        if (j % 2 != 0)
                            matrizTabuleiro[i, j] = "+";
                        else
                            matrizTabuleiro[i, j] = "---";
                    }

                }
            }
           TabuleiroMatriz = matrizTabuleiro;
        }

        // função para criar lista de jogadas possíveis
        private List<string> ListarJogadasPossiveis()
        {
            List<string> jogadasPossiveis = new List<string>();
            for (int i = 1; i <= Math.Pow((Tamanho + 1) / 2, 2); i++)
            {
                jogadasPossiveis.Add($"{i}");
            }
            return jogadasPossiveis;
        }




    }
}
