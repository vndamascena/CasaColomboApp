using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace CasaColomboApp.Domain.Services
{
    public class ProdutoPisoDomainService : IProdutoPisoDomainService
    {
        private readonly IProdutoPisoRepository? _produtoPisoRepository;
        private readonly ILoteRepository? _loteRepository;
        private readonly IVendaRepository? _vendaRepository;

        public ProdutoPisoDomainService(IProdutoPisoRepository? produtoPisoRepository, ILoteRepository? loteRepository, IVendaRepository? vendaRepository)
        {
            _produtoPisoRepository = produtoPisoRepository;
            _loteRepository = loteRepository;
            _vendaRepository = vendaRepository;
        }

        public ProdutoPiso Cadastrar(ProdutoPiso produtoPiso, List<Lote> lotes, string matricula)
        {

            if (produtoPiso == null)
                throw new ArgumentNullException(nameof(produtoPiso));

            if (lotes == null || !lotes.Any())
                throw new ArgumentException("A lista de lotes não pode estar vazia.");

            // Inicialize a lista de lotes se ainda não estiver inicializada
            if (produtoPiso.Lote == null)
                produtoPiso.Lote = new List<Lote>();

            // Se o produto ainda não tiver nenhum lote, considere o primeiro lote como o principal
            if (!produtoPiso.Lote.Any())
            {
                produtoPiso.Lote.Add(lotes.First()); // Adicione o primeiro lote à lista de lotes do produto
                lotes.RemoveAt(0); // Remova o primeiro lote da lista de lotes
            }

            // Associar os lotes restantes ao produto
            foreach (var lote in lotes)
            {
                // Verificar se já existe um lote com o mesmo númeroLote
                var loteExistente = produtoPiso.Lote.FirstOrDefault(l => l.NumeroLote == lote.NumeroLote);

                if (loteExistente == null)
                {
                    // Se não existir, adicionar o lote à lista
                    produtoPiso.Lote.Add(lote);
                }
                else
                {
                    // Se existir, atualizar a quantidade do lote existente
                    loteExistente.QuantidadeLote = lote.QuantidadeLote;
                }
            }
            foreach (var lote in produtoPiso.Lote)
            {
                lote.UsuarioId = matricula;
                lote.NomeProduto = produtoPiso.Nome;
                lote.Marca = produtoPiso.Marca;

            }
            // Calcula a quantidade total dos lotes, excluindo o primeiro lote
            int quantidadeTotalLotes = produtoPiso.Lote.Skip(1).Sum(l => l.QuantidadeLote);

            // Atribui a quantidade total dos lotes à propriedade Quantidade do produto
            produtoPiso.Quantidade = quantidadeTotalLotes;

            try
            {
                // Cadastre o produto com os lotes
                _produtoPisoRepository.Add(produtoPiso); // Salva o produto no banco de dados
                produtoPiso = _produtoPisoRepository?.GetById(produtoPiso.Id);
                return produtoPiso; // Retorna o produto após ser salvo, caso precise usar mais tarde
            }
            catch (Exception ex)
            {
                // Registre a exceção para fins de diagnóstico
                Console.WriteLine($"Erro ao salvar o produto: {ex}");
                throw; // Re-throw para que a exceção seja tratada no nível superior
            }
        }


        public ProdutoPiso Atualizar(ProdutoPiso produtoPiso, string matricula)
        {
            var registro = ObterPorId(produtoPiso.Id);

            if (registro == null)
                throw new ApplicationException("Produto não encontrado para edição.");

            // Remove os lotes excluídos do produto
            produtoPiso.Lote.RemoveAll(l => l.QuantidadeLote == 0 && string.IsNullOrEmpty(l.NumeroLote));

            // Constrói uma nova lista de lotes para o produto atualizado
            var lotesAtualizados = new List<Lote>();
            foreach (var lote in produtoPiso.Lote)
            {
                var loteExistente = _loteRepository.ObterPorId(lote.Id);

                if (loteExistente != null)
                {
                    // Se os campos QtdEntrada e NomeProduto não forem fornecidos, mantenha os valores atuais
                    loteExistente.NumeroLote = lote.NumeroLote;
                    loteExistente.QuantidadeLote = lote.QuantidadeLote;
                    loteExistente.Ala = lote.Ala;
                    loteExistente.Codigo = lote.Codigo;
                    loteExistente.NomeProduto = string.IsNullOrEmpty(lote.NomeProduto) ? loteExistente.NomeProduto : lote.NomeProduto;
                    loteExistente.QtdEntrada = lote.QtdEntrada == 0 ? loteExistente.QtdEntrada : lote.QtdEntrada;
                    loteExistente.DataUltimaAlteracao = DateTime.Now;
                    loteExistente.UsuarioId = matricula;
                    loteExistente.Marca = string.IsNullOrEmpty(lote.Marca) ? loteExistente.Marca : lote.Marca;

                    lotesAtualizados.Add(loteExistente);
                }
                else
                {
                    // Adiciona um novo lote com status Ativo
                    lotesAtualizados.Add(new Lote
                    {
                        NumeroLote = lote.NumeroLote,
                        QuantidadeLote = lote.QuantidadeLote,
                        Ala = lote.Ala,
                        Codigo = lote.Codigo,
                        NomeProduto = lote.NomeProduto,
                        Ativo = true, // Define o novo lote como ativo
                        DataUltimaAlteracao = DateTime.Now,
                        DataEntrada = DateTime.Now,
                        QtdEntrada = lote.QtdEntrada,
                        UsuarioId = matricula,
                        Marca = lote.Marca,
                    });
                }
            }

            // Identifica lotes que foram removidos do produto e devem ser desativados

            var produtoAtualizado = new ProdutoPiso
            {
                Id = produtoPiso.Id,
                Nome = produtoPiso.Nome,
                Marca = produtoPiso.Marca,
                Pei = produtoPiso.Pei,
                Quantidade = lotesAtualizados.Sum(l => l.QuantidadeLote),
                Lote = lotesAtualizados,
                Descricao = produtoPiso.Descricao,
                PrecoCaixa = produtoPiso.PrecoCaixa,
                MetroQCaixa = produtoPiso.MetroQCaixa,
                PrecoMetroQ = produtoPiso.PrecoMetroQ,
                PecasCaixa = produtoPiso.PecasCaixa,
                Ativo = registro.Ativo,
                ImagemUrl = registro.ImagemUrl,
                DataHoraCadastro = registro.DataHoraCadastro,
                DataHoraAlteracao = DateTime.Now,
                DepositoId = registro.DepositoId,
                FornecedorId = registro.FornecedorId,
                CategoriaId = registro.CategoriaId,
            };

            if (registro.DataHoraCadastro == null)
            {
                produtoAtualizado.DataHoraCadastro = DateTime.Now;
            }

            _produtoPisoRepository?.Update(produtoAtualizado);
            return _produtoPisoRepository?.GetById(produtoPiso.Id);
        }








        public ProdutoPiso Inativar(int id)
        {

            var produtoPiso = ObterPorId(id);

            if (produtoPiso == null)
                throw new ApplicationException("Produto não encontrado para exclusão.");

            produtoPiso.Ativo = false;
            produtoPiso.DataHoraAlteracao = DateTime.Now;

            _produtoPisoRepository?.Update(produtoPiso);

            return produtoPiso;
        }

        public List<ProdutoPiso> Consultar()
        {
            var produtosPiso = _produtoPisoRepository?.GetAll(true);

            if (produtosPiso == null)
                return new List<ProdutoPiso>();

            // A lógica do foreach é mantida para manipular os lotes, mas sem novas consultas ao banco
            foreach (var produto in produtosPiso)
            {
                // Certifica-se de que o produto tenha lotes
                if (produto.Lote != null && produto.Lote.Count > 1)
                {
                    // Remove o primeiro lote, se houver mais de um
                    produto.Lote.RemoveAt(0);
                }

                // Soma a quantidade de todos os lotes restantes
                produto.Quantidade = produto.Lote?.Sum(l => l.QuantidadeLote) ?? 0;
            }

            // Retorna apenas os produtos que estão ativos
            return produtosPiso.Where(p => p.Ativo).ToList();
        }



        public ProdutoPiso ObterPorId(int id)
        {
            var produtoPiso = _produtoPisoRepository?.GetById(id);

            if (produtoPiso != null)
            {
                // Filtra os lotes inativos
                produtoPiso.Lote = produtoPiso.Lote.Where(l => l.Ativo).ToList();
            }

            return produtoPiso;
        }


      
        public List<Lote> ConsultarLote(int produtoPisoId)
        {
            var lotes = _produtoPisoRepository.GetLotesByProdutoId(produtoPisoId);
            return lotes.Where(l => l.Ativo).ToList();
        }

        public void ExcluirLote(int produtoPisoId, int loteId)
        {
            // Verifica se o lote pertence ao produto
            var lote = _loteRepository.ObterPorId(loteId);
            if (lote == null)
            {
                throw new ApplicationException("O lote especificado não foi encontrado.");
            }

            if (lote.ProdutoPisoId != produtoPisoId)
            {
                throw new ApplicationException("O lote não pertence ao produto especificado.");
            }

            // Inativa o lote em vez de removê-lo
            lote.Ativo = false;
            _loteRepository.Update(lote);

        }

        public void ConfirmarVenda(int loteId, int quantidadeVendida, string matricula)
        {
            // Obter o lote pelo ID
            var lote = _loteRepository.ObterPorId(loteId);

            if (lote == null)
            {
                throw new ApplicationException("Lote não encontrado.");
            }

            // Criar e salvar a venda
            var venda = new Venda
            {
                LoteId = lote.Id,
                Quantidade = quantidadeVendida,
                DataVenda = DateTime.Now,
                NumeroLote = lote.NumeroLote,
                UsuarioId  = matricula,
                Codigo = lote.Codigo,
                Nomeproduto = lote.NomeProduto,
                Marca = lote.Marca,
              
                
            };

            _vendaRepository.Add(venda);

            // Atualizar a quantidade vendida do lote, permitindo quantidade negativa
            lote.QuantidadeLote -= quantidadeVendida;

            // Atualizar o lote no banco de dados
            _loteRepository.Update(lote);

            // Obter o produto associado ao lote
            var produtoPiso = _produtoPisoRepository.GetById(lote.ProdutoPisoId);

            if (produtoPiso == null)
            {
                throw new ApplicationException("Produto não encontrado.");
            }

            // Recalcular a quantidade total do produto, excluindo lotes com quantidade negativa
            produtoPiso.Quantidade = _produtoPisoRepository.GetLotesByProdutoId(produtoPiso.Id)
                                                    .Where(l => l.QuantidadeLote >= 0)
                                                    .Sum(l => l.QuantidadeLote);

            // Atualizar o produto no banco de dados
            _produtoPisoRepository.Update(produtoPiso);
        }






    }
}
