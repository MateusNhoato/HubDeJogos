namespace HubDeJogos.Views
{
    public class Tela
    {
        private readonly static string Hub = @"
 __   __  __   __  _______    ______   _______        ___  _______  _______  _______  _______ 
|  | |  ||  | |  ||  _    |  |      | |       |      |   ||       ||       ||       ||       |
|  |_|  ||  | |  || |_|   |  |  _    ||    ___|      |   ||   _   ||    ___||   _   ||  _____|
|       ||  |_|  ||       |  | | |   ||   |___       |   ||  | |  ||   | __ |  | |  || |_____ 
|       ||       ||  _   |   | |_|   ||    ___|   ___|   ||  |_|  ||   ||  ||  |_|  ||_____  |
|   _   ||       || |_|   |  |       ||   |___   |       ||       ||   |_| ||       | _____| |
|__| |__||_______||_______|  |______| |_______|  |_______||_______||_______||_______||_______|";



        public static void ImprimirMenuDoHub()
        {
            Console.Clear();
            Console.WriteLine(Hub + "\n\n");
            Console.WriteLine("1- Acessar Jogos");
            Console.WriteLine("2- Registrar Novo Jogador");
            Console.WriteLine("0- Sair");
            Console.Write("\nDigite a opção desejada: ");
        }
    }
}
