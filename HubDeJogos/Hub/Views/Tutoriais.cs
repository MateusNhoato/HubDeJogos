namespace HubDeJogos.Hub.Views
{
    internal static class Tutoriais
    {
        public readonly static string[] ExplicacoesXadrez = new string[4]
        {
          "\n  Olá, seja bem-vindo ao Xadrez do Hub de Jogos!\n" +
            "  Para jogar é muito simples! Vamos aos detalhes:\n\n" +
            "  Assim como no xadrez tradicional, as peças brancas\n" +
            "  começam e os turnos se alternam entre brancas e pretas.\n" +
            "  Mas, diferente do tradicional, aqui as peças são\n" +
            "  representadas por letras:\n" +
            "  | 'P' = Peão  | 'R' = Torre | 'N' = Cavalo |\n" +
            "  | 'B' = Bispo | 'Q'= Rainha | 'K'= Rei |\n\n" +
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

            "\n  Ah, e todas as partidas são registradas no formato pgn!" +
            "  Elas ficam na pasta Xadrez>Repositories>Data\n\n" +
            "  E essas são todas as informações que precisa saber!\n" +
            "  Agora, mesmo que não conheça os movimentos das peças,\n" +
            "  é possível jogar xadrez facilmente!\n" +
            "  Espero aproveite e se divirta! :)"
        };
        public readonly static string[] ExplicacoesJogoDaVelha = new string[4]
        {
          "\n  Olá, bem-vindo ao Jogo da Velha do Hub de Jogos!\n" +
            "  Para jogar é muito simples! Vamos aos detalhes:\n\n" +
            "  Antes de começar a partida podemos escolher um\n" +
            "  tabuleiro de 3x3 até 10x10. Este tutorial usará\n" +
            "  o tabuleiro 3x3 como base para os exemplos.\n" +
            "  Agora, vamos ver como o tabuleiro do Hub parece! ",

          "\n  Assim como no Jogo Da Velha tradicional os símbolos usados\n" +
            "  para marcar o tabuleiro são 'X' e 'O'. Mas, para marcar\n" +
            "  o tabuleiro aqui é um pouco diferente, temos que informar\n" +
            "  a posição que queremos, no caso deste tabuleiro as posições\n" +
            "  vão de 1 até 9.\n\n" +
            "  Vamos fazer uma jogada a seguir!",

            "\n  O jogo continua até um dos jogadores fazer uma combinação\n" +
              "  de 3 caracteres ('X' ou 'O') seguidos ou até não existir\n" +
              "  mais jogadas possíveis, resultando assim em um empate ou 'Velha'.\n" +
              "  A combinação dos caracteres pode ser nas linhas, nas colunas ou\n" +
              "  nas diagonais.\n" +
              "  A seguir um exemplo onde o 'X' ganha.",

            "\n  Como pôde ver, é um jogo bem simples!\n" +
            "  Espero que aproveite e se divirta com ele! :)"
        };
        public readonly static string ArteFinalTutorialXadrez = @"
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

        private readonly static string _jogodaVelhaComNumeros = @"
     ____      |  _______   |    _______
    |    |     | |       |  |   |       |
     |   |     | |____   |  |   |___    |
     |   |     |  ____|  |  |    ___|   |
     |   |     | |  _____|  |   |___    |
     |   |     | |  |____   |    ___|   |
     |___|     | |_______|  |   |_______|
  _____________|____________|_______________
     _   __    |   _______  |    ___
    | | |  |   |  |   ____| |   |   |
    | |_|  |   |  |  |____  |   |   |___
    |__    |   |  |_____  | |   |    _  |
       |   |   |   _____| | |   |   |_| |
       |___|   |  |_______| |   |_______|
  _____________|____________|_______________
    _______    |    _____   |    _______
   |       |   |   |  _  |  |   |  _    |
   |___    |   |   | |_| |  |   | |_|   |
       |   |   |  |   _   | |   |___    |
       |   |   |  |  |_|  | |       |   |
       |___|   |  |_______| |       |___|
               |            |               ";
        private readonly static string _jogoDaVelhaPrimeiraJogada = @"
               |  _______   |    _______
    __   __    | |       |  |   |       |
   |  |_|  |   | |____   |  |   |___    |
   |       |   |  ____|  |  |    ___|   |
    |     |    | |  _____|  |   |___    |
   |   _   |   | |  |____   |    ___|   |
   |__| |__|   | |_______|  |   |_______|
  _____________|____________|_______________
     _   __    |   _______  |    ___
    | | |  |   |  |   ____| |   |   |
    | |_|  |   |  |  |____  |   |   |___
    |__    |   |  |_____  | |   |    _  |
       |   |   |   _____| | |   |   |_| |
       |___|   |  |_______| |   |_______|
  _____________|____________|_______________
    _______    |    _____   |    _______
   |       |   |   |  _  |  |   |  _    |
   |___    |   |   | |_| |  |   | |_|   |
       |   |   |  |   _   | |   |___    |
       |   |   |  |  |_|  | |       |   |
       |___|   |  |_______| |       |___|
               |            |             ";
       private readonly static string _jogoDaVelhaSegundaJogada = @"
               |  _______   |    _______
    __   __    | |       |  |   |       |
   |  |_|  |   | |____   |  |   |___    |
   |       |   |  ____|  |  |    ___|   |
    |     |    | |  _____|  |   |___    |
   |   _   |   | |  |____   |    ___|   |
   |__| |__|   | |_______|  |   |_______|
  _____________|____________|_______________
     _   __    |   _______  |    ___
    | | |  |   |  |       | |   |   |
    | |_|  |   |  |   _   | |   |   |___
    |__    |   |  |  |_|  | |   |    _  |
       |   |   |  |       | |   |   |_| |
       |___|   |  |_______| |   |_______|
  _____________|____________|_______________
    _______    |    _____   |    _______
   |       |   |   |  _  |  |   |  _    |
   |___    |   |   | |_| |  |   | |_|   |
       |   |   |  |   _   | |   |___    |
       |   |   |  |  |_|  | |       |   |
       |___|   |  |_______| |       |___|";
        private readonly static string _jogoDaVelhaTerceiraJogada = @"
               |  _______   |    _______
    __   __    | |       |  |   |       |
   |  |_|  |   | |____   |  |   |___    |
   |       |   |  ____|  |  |    ___|   |
    |     |    | |  _____|  |   |___    |
   |   _   |   | |  |____   |    ___|   |
   |__| |__|   | |_______|  |   |_______|
  _____________|____________|_______________
    __   __    |   _______  |    ___
   |  |_|  |   |  |       | |   |   |
   |       |   |  |   _   | |   |   |___
    |     |    |  |  |_|  | |   |    _  |
   |   _   |   |  |       | |   |   |_| |
   |__| |__|   |  |_______| |   |_______|
  _____________|____________|_______________
    _______    |    _____   |    _______
   |       |   |   |  _  |  |   |  _    |
   |___    |   |   | |_| |  |   | |_|   |
       |   |   |  |   _   | |   |___    |
       |   |   |  |  |_|  | |       |   |
       |___|   |  |_______| |       |___|";
        private readonly static string _jogoDaVelhaQuartaJogada = @"
               |  _______   |    
    __   __    | |       |  |    _______
   |  |_|  |   | |____   |  |   |       |
   |       |   |  ____|  |  |   |   _   |
    |     |    | |  _____|  |   |  |_|  |
   |   _   |   | |  |____   |   |       |
   |__| |__|   | |_______|  |   |_______|
  _____________|____________|_______________
    __   __    |   _______  |    ___
   |  |_|  |   |  |       | |   |   |
   |       |   |  |   _   | |   |   |___
    |     |    |  |  |_|  | |   |    _  |
   |   _   |   |  |       | |   |   |_| |
   |__| |__|   |  |_______| |   |_______|
  _____________|____________|_______________
    _______    |    _____   |    _______
   |       |   |   |  _  |  |   |  _    |
   |___    |   |   | |_| |  |   | |_|   |
       |   |   |  |   _   | |   |___    |
       |   |   |  |  |_|  | |       |   |
       |___|   |  |_______| |       |___|";
        private readonly static string _jogoDaVelhaPartidaFinalizada = @"
               |  _______   |    
    __   __    | |       |  |    _______
   |  |_|  |   | |____   |  |   |       |
   |       |   |  ____|  |  |   |   _   |
    |     |    | |  _____|  |   |  |_|  |
   |   _   |   | |  |____   |   |       |
   |__| |__|   | |_______|  |   |_______|
  _____________|____________|_______________
    __   __    |   _______  |    ___
   |  |_|  |   |  |       | |   |   |
   |       |   |  |   _   | |   |   |___
    |     |    |  |  |_|  | |   |    _  |
   |   _   |   |  |       | |   |   |_| |
   |__| |__|   |  |_______| |   |_______|
  _____________|____________|_______________
    __   __    |    _____   |    _______
   |  |_|  |   |   |  _  |  |   |  _    |
   |       |   |   | |_| |  |   | |_|   |
    |     |    |  |   _   | |   |___    |
   |   _   |   |  |  |_|  | |       |   |
   |__| |__|   |  |_______| |       |___|";
       
        private readonly static string _vencedor = @"
   __   __  _______  __    _  _______  _______  ______   _______  ______      ___     __   __ 
  |  | |  ||       ||  |  | ||       ||       ||      | |       ||    _ |    |   |   |  |_|  |
  |  |_|  ||    ___||   |_| ||       ||    ___||  _    ||   _   ||   | ||    |___|   |       |
  |       ||   |___ |       ||       ||   |___ | | |   ||  | |  ||   |_||_    ___    |       |
  |       ||    ___||  _    ||      _||    ___|| |_|   ||  |_|  ||    __  |  |   |    |     | 
   |     | |   |___ | | |   ||     |_ |   |___ |       ||       ||   |  | |  |___|   |   _   |
    |___|  |_______||_|  |__||_______||_______||______| |_______||___|  |_|          |__| |__|";
        public readonly static string[] JogoDaVelhaAnimação = new string[7]
        { _jogodaVelhaComNumeros, 
          _jogoDaVelhaPrimeiraJogada,
          _jogoDaVelhaSegundaJogada,
          _jogoDaVelhaTerceiraJogada,
          _jogoDaVelhaQuartaJogada,
          _jogoDaVelhaPartidaFinalizada,
          _vencedor
        };
    }
}
