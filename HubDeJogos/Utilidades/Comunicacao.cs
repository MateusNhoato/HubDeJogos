namespace Utilidades
{
    internal static class Comunicacao
    {
        public readonly static string Linha = @"                                                
   ____   ____   ____   ____   ____   ____   ____   ____
  |____| |____| |____| |____| |____| |____| |____| |____| ";

        public readonly static string MeiaLinha = @"                                                
   ____   ____   ____   ____
  |____| |____| |____| |____|";


        // função utilitária para comunicação com usuário        
        public static void AperteEnterParaContinuar()
        {
            Console.CursorVisible = false;
            Console.WriteLine(Linha);
            Console.Write("  Aperte enter para continuar ");
            Console.ReadLine();
            Console.Clear();
            Console.CursorVisible = true;
        }

        public static void ComunicacaoComUsuario(Efeito efeito, string? msg)
        {
            if (msg != null)
                Console.WriteLine($"\n\n  {msg}");
            Som.ReproduzirEfeito(efeito);
        }

        public static void Carregando()
        {
            Som.ReproduzirEfeito(Efeito.carregar);
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Console.Write("  . ");
            }
        }
    }
}
