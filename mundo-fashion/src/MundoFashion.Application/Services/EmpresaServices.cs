using MundoFashion.Application.Services.Base;
using MundoFashion.Core.Notifications;
using MundoFashion.Domain;
using MundoFashion.Domain.Repositories;
using MundoFashion.Domain.Validacoes;
using System;
using System.Threading.Tasks;

namespace MundoFashion.Application.Services
{
    public class EmpresaServices : BaseServices
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaServices(Notificador notificador, IEmpresaRepository empresaRepository, IUsuarioRepository usuarioRepository) : base(notificador)
        {
            _empresaRepository = empresaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task AdicionarSolicitacaoEmpresa(Guid empresaId, Solicitacao solicitacao)
        {
            if (!Validar<Solicitacao, SolicitacaoValidator>(solicitacao)) return;

            Empresa empresa = await _usuarioRepository.ObterEmpresaPorId(empresaId).ConfigureAwait(false);

            if (empresa is null)
            {
                Notificar("Empresa não encontrada na base de dados.");
                return;
            }

            empresa.AdicionarSolicitacao(solicitacao);

            _empresaRepository.AtualizarEmpresa(empresa);
            _empresaRepository.AdicionarSolicitacao(solicitacao);

            await _empresaRepository.Commit().ConfigureAwait(false);
        }

        public async Task InativarServicoEmpresa(Guid empresaId)
        {
            Empresa empresa = await _usuarioRepository.ObterEmpresaPorId(empresaId).ConfigureAwait(false);

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
            Empresa empresa = await _usuarioRepository.ObterEmpresaPorId(empresaId).ConfigureAwait(false);

            if (empresa is null)
            {
                Notificar("Empresa não encontrada na base de dados.");
                return;
            }

            empresa.Inativate();

            _empresaRepository.AtualizarEmpresa(empresa);

            await _empresaRepository.Commit().ConfigureAwait(false);
        }
    }
}
