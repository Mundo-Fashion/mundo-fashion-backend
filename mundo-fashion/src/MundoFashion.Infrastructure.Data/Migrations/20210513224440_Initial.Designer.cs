﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MundoFashion.Infrastructure.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MundoFashion.Infrastructure.Data.Migrations
{
    [DbContext(typeof(MundoFashionContext))]
    [Migration("20210513224440_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("MundoFashion.Domain.DetalhesSolicitacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string[]>("Imagens")
                        .HasColumnType("text[]");

                    b.Property<int>("Nicho")
                        .HasColumnType("integer");

                    b.Property<string>("Observacoes")
                        .HasColumnType("text");

                    b.Property<Guid>("SolicitacaoId")
                        .HasColumnType("uuid");

                    b.Property<int>("Tecnica")
                        .HasColumnType("integer");

                    b.Property<int>("TecnicaEstamparia")
                        .HasColumnType("integer");

                    b.Property<int>("TipoEstampa")
                        .HasColumnType("integer");

                    b.Property<int>("TipoRapport")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SolicitacaoId")
                        .IsUnique();

                    b.ToTable("DetalhesSolicitacoes");
                });

            modelBuilder.Entity("MundoFashion.Domain.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Cnpj")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<Guid>("ServicoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("MundoFashion.Domain.Mensagem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Conteudo")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("EmissorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReceptorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SolicitacaoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SolicitacaoId");

                    b.ToTable("MensagensSolicitacao");
                });

            modelBuilder.Entity("MundoFashion.Domain.Proposta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Aceita")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("SolicitacaoId")
                        .HasColumnType("uuid");

                    b.Property<double>("Valor")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("SolicitacaoId")
                        .IsUnique();

                    b.ToTable("Propostas");
                });

            modelBuilder.Entity("MundoFashion.Domain.Servicos.ServicoEstampa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uuid");

                    b.Property<string[]>("Imagens")
                        .HasColumnType("text[]");

                    b.Property<int>("Nicho")
                        .HasColumnType("integer");

                    b.Property<int>("Tecnica")
                        .HasColumnType("integer");

                    b.Property<int>("TecnicaEstamparia")
                        .HasColumnType("integer");

                    b.Property<int>("TipoEstampa")
                        .HasColumnType("integer");

                    b.Property<int>("TipoRapport")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId")
                        .IsUnique();

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Servicos");
                });

            modelBuilder.Entity("MundoFashion.Domain.Solicitacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Aceita")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DetalhesId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PropostaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServicoId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("ServicoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Solicitacoes");
                });

            modelBuilder.Entity("MundoFashion.Domain.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cpf")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<Guid>("ServicoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MundoFashion.Domain.DetalhesSolicitacao", b =>
                {
                    b.HasOne("MundoFashion.Domain.Solicitacao", "Solicitacao")
                        .WithOne("Detalhes")
                        .HasForeignKey("MundoFashion.Domain.DetalhesSolicitacao", "SolicitacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Solicitacao");
                });

            modelBuilder.Entity("MundoFashion.Domain.Empresa", b =>
                {
                    b.HasOne("MundoFashion.Domain.Usuario", "Usuario")
                        .WithMany("Empresas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MundoFashion.Domain.Mensagem", b =>
                {
                    b.HasOne("MundoFashion.Domain.Solicitacao", "Solicitacao")
                        .WithMany("Mensagens")
                        .HasForeignKey("SolicitacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Solicitacao");
                });

            modelBuilder.Entity("MundoFashion.Domain.Proposta", b =>
                {
                    b.HasOne("MundoFashion.Domain.Solicitacao", "Solicitacao")
                        .WithOne("Proposta")
                        .HasForeignKey("MundoFashion.Domain.Proposta", "SolicitacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Solicitacao");
                });

            modelBuilder.Entity("MundoFashion.Domain.Servicos.ServicoEstampa", b =>
                {
                    b.HasOne("MundoFashion.Domain.Empresa", "Empresa")
                        .WithOne("Servico")
                        .HasForeignKey("MundoFashion.Domain.Servicos.ServicoEstampa", "EmpresaId");

                    b.HasOne("MundoFashion.Domain.Usuario", "Usuario")
                        .WithOne("Servico")
                        .HasForeignKey("MundoFashion.Domain.Servicos.ServicoEstampa", "UsuarioId");

                    b.Navigation("Empresa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MundoFashion.Domain.Solicitacao", b =>
                {
                    b.HasOne("MundoFashion.Domain.Empresa", "Empresa")
                        .WithMany("Solicitacoes")
                        .HasForeignKey("EmpresaId");

                    b.HasOne("MundoFashion.Domain.Servicos.ServicoEstampa", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MundoFashion.Domain.Usuario", "Usuario")
                        .WithMany("Solicitacoes")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Empresa");

                    b.Navigation("Servico");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MundoFashion.Domain.Empresa", b =>
                {
                    b.Navigation("Servico");

                    b.Navigation("Solicitacoes");
                });

            modelBuilder.Entity("MundoFashion.Domain.Solicitacao", b =>
                {
                    b.Navigation("Detalhes");

                    b.Navigation("Mensagens");

                    b.Navigation("Proposta");
                });

            modelBuilder.Entity("MundoFashion.Domain.Usuario", b =>
                {
                    b.Navigation("Empresas");

                    b.Navigation("Servico");

                    b.Navigation("Solicitacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
