using HubDeJogos.Models;
using Newtonsoft.Json;

namespace HubDeJogos.Hub.Repositories
{
    public class Jogadores
    {
        private static string _path = @"..\..\..\Hub\Repositories\Dados\DadosDosJogadores.Json";
        public List<Jogador> ListaDeJogadores { get; private set; }

        public Jogadores() { }

        public void CarregarListaDeJogadores()
        {
            try
            {
                string stringJson = File.ReadAllText(_path);
                ListaDeJogadores = JsonConvert.DeserializeObject<List<Jogador>>(stringJson);
            }
            catch (FileNotFoundException)
            {
                File.Create(_path).Close();
                ListaDeJogadores = new List<Jogador>();
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(@"..\..\..\Hub\Repositories\Dados");
                File.Create(_path).Close();
                ListaDeJogadores = new List<Jogador>();
            }

        }


        public void SalvarJogadores(List<Jogador> jogadores)
        {

            string json = JsonConvert.SerializeObject(jogadores);
            try
            {
                File.WriteAllText(_path, json);
            }
            catch (FileNotFoundException)
            {
                File.Create(_path).Close();
                File.WriteAllText(_path, json);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(@"..\..\..\Hub\Repositories\Dados");
                File.Create(_path).Close();
                ListaDeJogadores = new List<Jogador>();
            }
        }
    }
}
