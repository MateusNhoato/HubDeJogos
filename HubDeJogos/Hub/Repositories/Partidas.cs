using HubDeJogos.Repositories;
using Newtonsoft.Json;

namespace HubDeJogos.Hub.Repositories
{
    public static class Partidas
    {
        private static string _path = @"..\..\..\Hub\Repositories\Dados\RegistroDePartidas.Json";
        public static List<Partida> HistoricoDePartidas { get; private set; } = new();

        public static void CarregarPartidas()
        {
            try
            {
                string stringJson = File.ReadAllText(_path);
                if (!(string.IsNullOrEmpty(stringJson)))
                    HistoricoDePartidas = JsonConvert.DeserializeObject<List<Partida>>(stringJson);
            }
            catch (FileNotFoundException)
            {
                File.Create(_path).Close();
            }
        }

        public static void SalvarPartidas()
        {

            string json = JsonConvert.SerializeObject(HistoricoDePartidas);
            try
            {
                File.WriteAllText(_path, json);
            }
            catch (FileNotFoundException)
            {
                File.Create(_path).Close();
                File.WriteAllText(_path, json);
            }
        }


    }
}
