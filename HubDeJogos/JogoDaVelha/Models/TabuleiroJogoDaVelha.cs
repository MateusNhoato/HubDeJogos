using HubDeJogos.Models;

namespace HubDeJogos.JogoDaVelha.Models
{
    public class TabuleiroJogoDaVelha : Tabuleiro
    {
        public string[,] MatrizTabuleiro { get; private set; }
        public List<string>? JogadasPossiveis { get; private set; }
        private static int _tamanhoDoTabuleiro;


        // construtor para tabuleiro novo
        public TabuleiroJogoDaVelha(int tamanho)
        {
            // configurando o tabuleiro e gerando lista de jogadas possíveis
            TamanhoDoTabuleiro = tamanho;
            MatrizTabuleiro = GerarTabuleiro();
            JogadasPossiveis = ListarJogadasPossiveis();

        }
        // construtor para tabuleiro de registro 
        public TabuleiroJogoDaVelha(string[,] matrizTabuleiro, int tamanhoDoTabuleiro)
        {
            MatrizTabuleiro = matrizTabuleiro;
            TamanhoDoTabuleiro = (tamanhoDoTabuleiro + 1) / 2;
        }

        public int TamanhoDoTabuleiro
        {
            get { return _tamanhoDoTabuleiro; }
            set { _tamanhoDoTabuleiro = value * 2 - 1; }
        }

        public override object[,] TabuleiroMatriz { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }


        // função para gerar o tabuleiro de 3 até 10 
        protected override string[,] GerarTabuleiro()
        {
            int cont = 1;
            string[,] matrizTabuleiro = new string[TamanhoDoTabuleiro, TamanhoDoTabuleiro];

            for (int i = 0; i < TamanhoDoTabuleiro; i++)
            {
                for (int j = 0; j < TamanhoDoTabuleiro; j++)
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
            return matrizTabuleiro;
        }

        // função para criar lista de jogadas possíveis
        private List<string> ListarJogadasPossiveis()
        {
            List<string> jogadasPossiveis = new List<string>();
            for (int i = 1; i <= Math.Pow((TamanhoDoTabuleiro + 1) / 2, 2); i++)
            {
                jogadasPossiveis.Add($"{i}");
            }
            return jogadasPossiveis;
        }




    }
}
