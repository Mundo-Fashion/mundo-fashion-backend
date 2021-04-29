﻿using MundoFashion.Application.Services.Base;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
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

        public async Task CancelarSolicitacao(Guid solicitacaoId)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            solicitacao.CancelarSolicitacao();
            
            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }


        public async Task AdicionarProposta(Guid solicitacaoId, Proposta proposta)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            solicitacao.AdicionarProposta(proposta);

            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }

        public async Task AtualizarProposta(Guid solicitacaoId, Proposta proposta)
        {
            Solicitacao solicitacao = await _solicitacaoRepository.ObterSolicitacaoPorId(solicitacaoId).ConfigureAwait(false);

            if (solicitacao is null)
            {
                Notificar("Solicitação não encontrada.");
                return;
            }

            if(!solicitacao.PossuiProposta())
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

            if (!solicitacao.PossuiProposta())
            {
                Notificar("Solicitação não possui proposta.");
                return;
            }

            solicitacao.Proposta?.AceitarProposta();

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

            if (!solicitacao.PossuiProposta())
            {
                Notificar("Solicitação não possui proposta.");
                return;
            }

            solicitacao.Proposta?.RecusarProposta();

            _solicitacaoRepository.AtualizarProposta(solicitacao.Proposta);
            _solicitacaoRepository.AtualizarSolicitacao(solicitacao);
            await _solicitacaoRepository.Commit().ConfigureAwait(false);
        }
    }
}