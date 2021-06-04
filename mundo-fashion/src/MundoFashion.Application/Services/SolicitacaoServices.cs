using MundoFashion.Application.Services.Base;
using MundoFashion.Core.Enums.Solicitacao;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Validacoes;
using System;
using System.Threading.Tasks;

namespace MundoFashion.Application.Services
{
    public class SolicitacaoServices : BaseServices
    {
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        public SolicitacaoServices(Notificador notificador, ISolicitacaoRepository solicitacaoRepository) : base(notificador)
        {
            _solicitacaoRepository = solicitacaoRepository;
        }

        public async Task AdicionarSolicitacao(Solicitacao solicitacao, DetalhesSolicitacao detalhesSolicitacao)
        {
            if (!Validar<Solicitacao, SolicitacaoValidator>(solicitacao)) return;
            if (!Validar<DetalhesSolicitacao, DetalhesSolicitacaoValidator>(detalhesSolicitacao)) return;

            _solicitacaoRepository.AdicionarSolicitacao(solicitacao);
            _solicitacaoRepository.AdicionarDetalhesSolicitacao(detalhesSolicitacao);

            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task CancelarSolicitacao(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            if (solicitacao.Status == StatusSolicitacao.Entregue || solicitacao.Status == StatusSolicitacao.Finalizado)
            {
                Notificar("Não é possível cancelar solicitação entregues ou já finalizadas.");
                return;
            }

            if (solicitacao.Status != StatusSolicitacao.Cancelada)
                solicitacao.Cancelar();

            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task AceitarSolicitacao(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            solicitacao.AceitarSolicitacao();
            solicitacao.IniciarNegociacao();

            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task RecusarSolicitacao(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            if (solicitacao.Status == StatusSolicitacao.Entregue || solicitacao.Status == StatusSolicitacao.Finalizado)
            {
                Notificar("Não é possível recusar solicitação entregues ou já finalizadas.");
                return;
            }

            solicitacao.RecusarSolicitacao();

            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }


        public async Task AdicionarProposta(Guid solicitacaoId, Proposta proposta)
        {
            if (!Validar<Proposta, PropostaValidator>(proposta)) return;

            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            if (!solicitacao.Aceita)
            {
                Notificar("Solicitação ainda não aceita pelo prestador.");
                return;
            }

            solicitacao.AdicionarProposta(proposta);
            solicitacao.AnalisarProposta();

            _solicitacaoRepository.AdicionarProposta(proposta);
            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task AtualizarProposta(Guid solicitacaoId, Proposta proposta)
        {
            if (!Validar<Proposta, PropostaValidator>(proposta)) return;

            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            if (!solicitacao.Aceita)
            {
                Notificar("Solicitação ainda não aceita pelo prestador.");
                return;
            }

            if (!solicitacao.PossuiProposta())
            {
                Notificar("Solicitação não possui proposta.");
                return;
            }

            solicitacao.AtualizarValorProposta(proposta.Valor);

            _solicitacaoRepository.AtualizarProposta(solicitacao.Proposta);
            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task AceitarProposta(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            if (!solicitacao.Aceita)
            {
                Notificar("Solicitação ainda não aceita pelo prestador.");
                return;
            }

            if (!solicitacao.PossuiProposta())
            {
                Notificar("Solicitação não possui proposta.");
                return;
            }

            solicitacao.Proposta?.AceitarProposta();
            solicitacao.Negociado();

            _solicitacaoRepository.AtualizarProposta(solicitacao.Proposta);
            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task RecusarProposta(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            if (!solicitacao.Aceita)
            {
                Notificar("Solicitação ainda não aceita pelo prestador.");
                return;
            }

            if (!solicitacao.PossuiProposta())
            {
                Notificar("Solicitação não possui proposta.");
                return;
            }

            solicitacao.Proposta?.RecusarProposta();
            solicitacao.IniciarNegociacao();

            _solicitacaoRepository.AtualizarProposta(solicitacao.Proposta);
            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task PagarSolicitacao(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            if (!solicitacao.Aceita)
            {
                Notificar("Solicitação ainda não aceita pelo prestador.");
                return;
            }

            solicitacao.Pagar();

            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task EntregarSolicitacao(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            if (!solicitacao.Aceita)
            {
                Notificar("Solicitação ainda não aceita pelo prestador.");
                return;
            }

            solicitacao.Entregue();

            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task FinalizarSolicitacao(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            if (!solicitacao.Aceita)
            {
                Notificar("Solicitação ainda não aceita pelo prestador.");
                return;
            }

            solicitacao.Finalizar();

            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task AdicionarMensagem(Guid solicitacaoId, Mensagem mensagem)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            solicitacao.AdicionarMensagem(mensagem);

            _solicitacaoRepository.AdicionarMensagem(mensagem);
            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);

            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }
    }
}
