using MundoFashion.Application.Services.Base;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Servicos;
using MundoFashion.Domain.Validacoes;
using System;
using System.Threading.Tasks;

namespace MundoFashion.Application.Services
{
    public class EmpresaServices : BaseServices
    {
        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaServices(Notificador notificador, IEmpresaRepository empresaRepository) : base(notificador)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<bool> AdicionarSolicitacaoEmpresa(Guid empresaId, Solicitacao solicitacao)
        {
            if (!Validar<Solicitacao, SolicitacaoValidator>(solicitacao)) return false;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(empresaId).ConfigureAwait(false);

            if (empresa is null)
            {
                Notificar("Empresa não encontrada na base de dados.");
                return false;
            }

            empresa.AdicionarSolicitacao(solicitacao);

            _empresaRepository.AtualizarEmpresa(empresa);
            _empresaRepository.AdicionarSolicitacao(solicitacao);

            return await _empresaRepository.Commit().ConfigureAwait(false);
        }

        public async Task InativarServicoEmpresa(Guid empresaId)
        {
            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(empresaId).ConfigureAwait(false);

            if (empresa is null)
            {
                Notificar("Empresa não encontrada na base de dados.");
                return;
            }

            empresa.InativarServico();

            _empresaRepository.AtualizarEmpresa(empresa);

            await _empresaRepository.Commit().ConfigureAwait(false);
        }

        public async Task InativarEmpresa(Guid empresaId)
        {
            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(empresaId).ConfigureAwait(false);

            if (empresa is null)
            {
                Notificar("Empresa não encontrada na base de dados.");
                return;
            }

            empresa.Inativate();

            _empresaRepository.AtualizarEmpresa(empresa);

            await _empresaRepository.Commit().ConfigureAwait(false);
        }

        public async Task CriarServicoEmpresa(Guid empresaId, ServicoEstampa servico)
        {
            if (!Validar<ServicoEstampa, ServicoEstampaValidator>(servico)) return;

            Empresa empresa = await _empresaRepository.ObterEmpresaPorId(empresaId).ConfigureAwait(false);

            if (empresa is null)
            {
                Notificar("Empresa não encontrada na base de dados.");
                return;
            }

            empresa.AdicionarServico(servico);

            _empresaRepository.AtualizarEmpresa(empresa);
            _empresaRepository.AdicionarServico(servico);

            await _empresaRepository.Commit().ConfigureAwait(false);
        }
    }
}
