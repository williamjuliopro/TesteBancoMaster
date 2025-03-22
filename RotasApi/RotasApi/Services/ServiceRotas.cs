using RotasApi.Model;
using RotasApi.Repository;
using System.Drawing;
using System.Threading.Tasks;

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

        public bool VerificaRotaCadastrada(string origem, string destino, decimal valor)
        {
            return _context.Rotas.Any(x => x.Origem == origem && x.Destino == destino && x.Valor == valor);
        }
        public async Task<List<Rotas>> SalvarRota(string origem, string destino, decimal valor)
        {                
            Rotas rota = new Rotas(origem, destino, valor);
            await _context.Rotas.AddAsync(rota);

            //persiste dados no banco
            _context.SaveChanges();

            List<Rotas> RotasDB = new List<Rotas>();
            RotasDB.Add(rota);

            return RotasDB;
        }

        // Função recursiva para encontrar o menor custo
        private void EncontrarRotaMaisBarata(string origem, string destino, List<Rotas> rotas, List<string> caminhoAtual, ref decimal menorCusto, ref List<string> melhorCaminho, decimal custoAtual)
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

        public List<string> BuscarMelhorRota(string origem, string destino, ref decimal menorCusto)
        {
            // Busca as rodas do banco      
            List<Rotas> rotas = BuscarRotas();

            // Variáveis para armazenar a melhor rota e seu custo
            //decimal menorCusto = -1;
            List<string> melhorCaminho = null;

            // Chamamos a função recursiva para encontrar o menor custo
            EncontrarRotaMaisBarata(origem, destino, rotas, new List<string>(), ref menorCusto, ref melhorCaminho, 0);

            return melhorCaminho;

        }


    }
}
