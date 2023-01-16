namespace Utilidades
{
    internal static class Utilidades
    {
        public readonly static string Linha = @"                                                
   ____   ____   ____   ____   ____   ____   ____  
  |____| |____| |____| |____| |____| |____| |____| ";

        public readonly static string[] explicacoesXadrez = new string[4]
        {
          "\n  Olá, seja bem-vindo ao Xadrez do Hub de Jogos!\n" +
            "  Para jogar é muito simples! Vamos aos detalhes:\n\n" +
            "  Assim como no xadrez tradicional, as peças brancas\n" +
            "  começam e os turnos se alternam entre brancas e pretas.\n" +
            "  Mas, diferente do tradicional, aqui as peças são\n" +
            "  representadas por letras:\n" +
            "  | 'P' = Peão  | 'T' = Torre | 'C' = Cavalo |\n" +
            "  | 'B' = Bispo |'D' = rainha(Dama) | 'R'= Rei |\n\n" +
            "  Além disso, a distinção das cores das letras indica\n" +
            "  de quem é a peça, as letras brancas são peças brancas\n" +
            "  e as letras pretas são peças pretas.\n\n" +
            "  A seguir você terá um vislumbre do tabuleiro.",

          "\n  Como pôde ver, na parte de cima do tabuleiro e no seu\n" +
            "  lado estão as marcações de linhas e colunas padrão do\n" +
            "  xadrez (as colunas vão de A até H, e as linhas vão de\n" +
            "  1 a 8). Para fazer uma jogada basta então escrever a \n" +
            "  posição da coluna seguido da posição da linha da peça\n" +
            "  que quer mexer. Exemplos: 'A1', 'H5', 'B4'.\n\n" +
            "  Após selecionar uma peça, o tabuleiro se altera visualmente:\n" +
            "  aparecem casas destacadas. Estas casas são as jogadas possíveis\n" +
            "  para a peça selecionada. Uma escolha inválida de casa\n" +
            "  faz com que a peça seja desselecionada, logo precisará\n" +
            "  selecionar ela novamente ou poderá seelecionar outra.\n\n" +
            "  A seguir temos um exemplo de como uma jogada ocorre!",

            "\n  Exitem duas jogadas que não são para mover peças.\n\n" +
            "  Como no xadrez existem casos em que a vítória não é mais possível,\n" +
            "  no xadrez do Hub é possível se render. Basta digitar 'render'\n" +
            "  na sua jogada. E também é possível propor um empate ao digitar\n" +
            "  'empate' na jogada. O oponente pode concordar ao também digitar\n" +
            "  'empate' e então o jogo se encerra. Caso o oponente não concorde\n" +
            "  com o empate, o jogo continua de onde parou: na vez do jogador\n" +
            "  que propôs o empate. Vamos ver um exemplo de 'render' a seguir.",

            "\n  E essas são todas as informações que precisa saber!\n" +
            "  Agora, mesmo que não conheça os movimentos das peças,\n" +
            "  é possível jogar xadrez facilmente!\n" +
            "  Espero que se divirta :)"
        };

        public readonly static string arteFinalTutorialXadrez = @"
                                                        .::.
                                             _()_       _::_
                                   _O      _/____\_   _/____\_
            _  _  _     ^^__      / //\    \      /   \      /
           | || || |   /  - \_   {     }    \____/     \____/
           |_______| <|    __<    \___/     (____)     (____)
     _     \__ ___ / <|    \      (___)      |  |       |  |
    (_)     |___|_|  <|     \      |_|       |__|       |__|
   (___)    |_|___|  <|______\    /   \     /    \     /    \
   _|_|_    |___|_|   _|____|_   (_____)   (______)   (______)
  (_____)  (_______) (________) (_______) (________) (________)
  /_____\  /_______\ /________\ /_______\ /________\ /________\
    Peão     Torre     Cavalo     Bispo      Dama        Rei   ";

        // função utilitária para comunicação com usuário        
        public static void AperteEnterParaContinuar()
        {
            Console.WriteLine(Linha);
            Console.Write("  Aperte enter para continuar ");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
