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
    [Migration("20210604145546_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
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

                    b.Property<string[]>("Imagens")
                        .HasColumnType("text[]");

                    b.Property<int>("Nicho")
                        .HasColumnType("integer");

                    b.Property<Guid>("PrestadorId")
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

                    b.HasIndex("PrestadorId")
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

                    b.Property<Guid?>("PropostaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServicoId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TomadorId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ServicoId");

                    b.HasIndex("TomadorId");

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
                    b.HasOne("MundoFashion.Domain.Usuario", "Prestador")
                        .WithOne("Servico")
                        .HasForeignKey("MundoFashion.Domain.Servicos.ServicoEstampa", "PrestadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prestador");
                });

            modelBuilder.Entity("MundoFashion.Domain.Solicitacao", b =>
                {
                    b.HasOne("MundoFashion.Domain.Servicos.ServicoEstampa", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MundoFashion.Domain.Usuario", "Tomador")
                        .WithMany()
                        .HasForeignKey("TomadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Servico");

                    b.Navigation("Tomador");
                });

            modelBuilder.Entity("MundoFashion.Domain.Solicitacao", b =>
                {
                    b.Navigation("Detalhes");

                    b.Navigation("Mensagens");

                    b.Navigation("Proposta");
                });

            modelBuilder.Entity("MundoFashion.Domain.Usuario", b =>
                {
                    b.Navigation("Servico");
                });
#pragma warning restore 612, 618
        }
    }
}
