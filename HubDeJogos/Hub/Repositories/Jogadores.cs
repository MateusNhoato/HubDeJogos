using HubDeJogos.Models;
using Newtonsoft.Json;

namespace HubDeJogos.Hub.Repositories
{
    public class Jogadores
    {
        private static string _pathJogadoresJson = @"..\..\..\Hub\Repositories\Data\DadosDosJogadores.Json";
        public List<Jogador> ListaDeJogadores { get; private set; }

        public Jogadores() { }
        
        public void CarregarListaDeJogadores()
        {          
            try
            {               
                string stringJson = File.ReadAllText(_pathJogadoresJson);               
                ListaDeJogadores = JsonConvert.DeserializeObject<List<Jogador>>(stringJson);
            }
            catch (FileNotFoundException)
            {
                File.Create(_pathJogadoresJson).Close();
                ListaDeJogadores = new List<Jogador>();
            }               
        }

        public void SalvarJogadores()
        {
            
            string json = JsonConvert.SerializeObject(ListaDeJogadores);
            try
            {
                File.WriteAllText(_pathJogadoresJson, json);
            }
            catch(FileNotFoundException)
            {
                File.Create(_pathJogadoresJson).Close();
            }
        }

        public void SalvarJogadores(List<Jogador> jogadores)
        {
            
            string json = JsonConvert.SerializeObject(jogadores);
            try
            {
                File.WriteAllText(_pathJogadoresJson, json);
            }
            catch (FileNotFoundException)
            {
                File.Create(_pathJogadoresJson).Close();
            }
        }
    }
}
