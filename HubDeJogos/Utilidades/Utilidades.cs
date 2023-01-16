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
    }
}
