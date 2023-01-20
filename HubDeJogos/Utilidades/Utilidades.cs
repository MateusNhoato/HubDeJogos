namespace Utilidades
{
    internal static class Utilidades
    {
        public readonly static string Linha = @"                                                
   ____   ____   ____   ____   ____   ____   ____  
  |____| |____| |____| |____| |____| |____| |____| ";


        // função utilitária para comunicação com usuário        
        public static void AperteEnterParaContinuar()
        {
            Console.WriteLine(Linha);
            Console.Write("  Aperte enter para continuar ");
            Console.ReadLine();
            Console.Clear();
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
