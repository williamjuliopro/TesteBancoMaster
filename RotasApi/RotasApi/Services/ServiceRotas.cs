using RotasApi.Model;
using RotasApi.Repository;

namespace RotasApi.Services
{
    public class ServiceRotas
    {
        private readonly Context _context;
        public ServiceRotas(Context context)
        {
            _context = context;
        }

        public List<Rotas> BuscarRotas()
        {
            return _context.Rotas.ToList();
        }

        public List<Rotas> SalvarRota(string Origem, string Destino, decimal valor)
        {
            //Verifica se a rota já foi cadastrada para evitar duplicidade.
            var RotasDB = BuscarRotas();
            if (RotasDB.Where(x => x.Origem == Origem && x.Destino == Destino && x.Valor == valor).FirstOrDefault() != null)
            {
                throw new InvalidOperationException("Rota já cadastrada");                
            }

            Rotas rota = new Rotas(Origem, Destino, valor);
            _context.Rotas.AddAsync(rota);

            //persiste dados no banco
            _context.SaveChanges();

            RotasDB.Add(rota);

            return RotasDB;
        }

        // Função recursiva para encontrar o menor custo
        public static void EncontrarRotaMaisBarata(string origem, string destino, List<Rotas> rotas, List<string> caminhoAtual, ref decimal menorCusto, ref List<string> melhorCaminho, decimal custoAtual)
        {
            caminhoAtual.Add(origem);

            // Se chegamos ao destino, verificamos o custo
            if (origem == destino)
            {
                if (custoAtual < menorCusto || menorCusto == -1)
                {
                    menorCusto = custoAtual;
                    melhorCaminho = new List<string>(caminhoAtual);
                }
            }
            else
            {
                // Busca recursiva por cada rota possível a partir da origem
                foreach (var rota in rotas)
                {
                    if (rota.Origem == origem && !caminhoAtual.Contains(rota.Destino)) // Para evitar loops
                    {
                        EncontrarRotaMaisBarata(rota.Destino, destino, rotas, caminhoAtual, ref menorCusto, ref melhorCaminho, custoAtual + rota.Valor);
                    }
                }
            }

            // Retira a origem atual para continuar a busca
            caminhoAtual.RemoveAt(caminhoAtual.Count - 1);
        }

        public List<string> BuscarMelhorRoda(string origem, string destino, ref decimal menorCusto)
        {
            // Busca as rodas do banco      
            List<Rotas> rotas = BuscarRotas();

            // Variáveis para armazenar a melhor rota e seu custo
            //decimal menorCusto = -1;
            List<string> melhorCaminho = null;

            // Chamamos a função recursiva para encontrar o menor custo
            ServiceRotas.EncontrarRotaMaisBarata(origem, destino, rotas, new List<string>(), ref menorCusto, ref melhorCaminho, 0);

            return melhorCaminho;

        }


    }
}
