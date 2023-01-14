using HubDeJogos.Models;
using HubDeJogos.Views;
namespace HubDeJogos.Controllers
{
    public class Hub
    {
        private readonly List<Jogador> _jogadores;

        public Hub(List<Jogador> jogadores)
        {
            _jogadores = jogadores;
        }

        public void MenuDoHub()
        {
            
            string opcao = "";
            do
            {               
                Tela.ImprimirMenuDoHub();
                

            } while (opcao != "0") ;


        }

        public void EscolherJogos()
        {

        }
        
        public void RegistrarJogador()
        {

        }
    }
}
