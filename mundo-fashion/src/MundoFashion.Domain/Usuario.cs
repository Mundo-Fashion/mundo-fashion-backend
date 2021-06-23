using MundoFashion.Core;
using MundoFashion.Core.Constants;
using MundoFashion.Core.Interfaces;
using MundoFashion.Domain.Servicos;
using System;

namespace MundoFashion.Domain
{
    public class Usuario : Entity, IAggregateRoot
    {
        public string AvatarLink { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string Role { get; private set; }
        public string Cpf { get; private set; }
        public ServicoEstampa Servico { get; private set; }
        public Guid ServicoId { get; private set; }
        public string DescricaoPessoal { get; private set; }
        public string AlexaUserId { get; private set; }
        public bool UtilizaSuporteAlexa { get; private set; }

        private Usuario() { }
        public Usuario(string nome, string cpf, string email, string senha, string role)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Senha = senha;
            Role = role;
        }

        public void AtualizarNome(string nomeAtualizado)
        {
            if (!Nome.Equals(nomeAtualizado) && !string.IsNullOrWhiteSpace(nomeAtualizado))
                Nome = nomeAtualizado;
        }

        public void AtualizarSenha(string senhaAtualizada)
        {
            if (!Senha.Equals(senhaAtualizada) && !string.IsNullOrWhiteSpace(senhaAtualizada))
                Senha = senhaAtualizada;
        }

        public void AtualizarDescricaoPessoal(string descricaoPessoalAtualizada)
        {
            if (string.IsNullOrWhiteSpace(DescricaoPessoal) || !DescricaoPessoal.Equals(descricaoPessoalAtualizada))
                DescricaoPessoal = descricaoPessoalAtualizada;
        }

        public void SetarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || Cpf == cpf) return;

            AlterarRole(Roles.CLIENTE_PRESTADOR);
            Cpf = cpf;
        }

        public void AdicionarServico(ServicoEstampa servico)
        {
            AlterarRole(Roles.CLIENTE_PRESTADOR);
            ServicoId = servico.Id;
            servico.AssociarUsuarioPrestador(Id);
        }

        public void AtualizarServico(ServicoEstampa servico)
        {
            if (Servico == servico || servico is null) return;

            Servico.AtualizarTipoEstampa(servico.TipoEstampa);
            Servico.AtualizarTipoTecnicaEstampa(servico.Tecnica);
            Servico.AtualizarTipoTecnicaEstamparia(servico.TecnicaEstamparia);
            Servico.AtualizarTipoNicho(servico.Nicho);
            Servico.AtualizarTipoRapport(servico.TipoRapport);
            Servico.AtualizarDescricao(servico.Descricao);

            if (servico.Imagens?.Length > 0)
                Servico.RemoverImagens();

            foreach (var item in servico.Imagens)
                Servico.AdicionarImagem(item);
        }

        public void AlterarRole(string novaRole)
        {
            if (!Role.Equals(novaRole))
                Role = novaRole;
        }

        public void InativarServico()
        {
            if (PossuiServico())
                Servico.Inativate();
        }

        public bool PossuiServico()
            => !ServicoId.Equals(Guid.Empty);

        public void AssociarAlexaUserId(string alexaUserId)
           => AlexaUserId = alexaUserId;

        public void DesassociarAlexaUserId()
           => AlexaUserId = string.Empty;

        public void AtivarSuporteAlexa()
           => UtilizaSuporteAlexa = true;

        public void DesativarSuporteAlexa()
        {
            UtilizaSuporteAlexa = false;
            DesassociarAlexaUserId();
        }

        public void AtualizarAvatar(string novoLinkAvatar)
        {
            if (!string.IsNullOrWhiteSpace(novoLinkAvatar))
                AvatarLink = novoLinkAvatar;
        }
    }
}
