﻿// <auto-generated />
using System;
using LojaVirtual.Colaboradores.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LojaVirtual.Colaboradores.Migrations
{
    [DbContext(typeof(SqlServerContext))]
    partial class SqlServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LojaVirtual.Colaboradores.Models.Colaborador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Sobrenome")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("Usuario")
                        .IsUnique();

                    b.ToTable("Colaboradores", t =>
                        {
                            t.HasCheckConstraint("CK_Senha", "LEN(Senha) = 64");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ativo = true,
                            DataCadastro = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Senha = "f360ca3fef5aa0422ee9c2489a09bcb28efeeb751150ab6c2a08ca37a419cd46",
                            Usuario = "admin"
                        });
                });

            modelBuilder.Entity("LojaVirtual.Colaboradores.Models.Permissao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AdicionarCategoria")
                        .HasColumnType("bit");

                    b.Property<bool>("AdicionarColaborador")
                        .HasColumnType("bit");

                    b.Property<bool>("AdicionarProduto")
                        .HasColumnType("bit");

                    b.Property<bool>("AdicionarSituacaoPedido")
                        .HasColumnType("bit");

                    b.Property<int>("ColaboradorId")
                        .HasColumnType("int");

                    b.Property<bool>("EditarCategoria")
                        .HasColumnType("bit");

                    b.Property<bool>("EditarColaborador")
                        .HasColumnType("bit");

                    b.Property<bool>("EditarProduto")
                        .HasColumnType("bit");

                    b.Property<bool>("ExcluirCategoria")
                        .HasColumnType("bit");

                    b.Property<bool>("ExcluirColaborador")
                        .HasColumnType("bit");

                    b.Property<bool>("ExcluirProduto")
                        .HasColumnType("bit");

                    b.Property<bool>("VisualizarCategoria")
                        .HasColumnType("bit");

                    b.Property<bool>("VisualizarCliente")
                        .HasColumnType("bit");

                    b.Property<bool>("VisualizarColaborador")
                        .HasColumnType("bit");

                    b.Property<bool>("VisualizarPedido")
                        .HasColumnType("bit");

                    b.Property<bool>("VisualizarProduto")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ColaboradorId")
                        .IsUnique();

                    b.ToTable("Permissoes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdicionarCategoria = true,
                            AdicionarColaborador = true,
                            AdicionarProduto = true,
                            AdicionarSituacaoPedido = true,
                            ColaboradorId = 1,
                            EditarCategoria = true,
                            EditarColaborador = true,
                            EditarProduto = true,
                            ExcluirCategoria = true,
                            ExcluirColaborador = true,
                            ExcluirProduto = true,
                            VisualizarCategoria = true,
                            VisualizarCliente = true,
                            VisualizarColaborador = true,
                            VisualizarPedido = true,
                            VisualizarProduto = true
                        });
                });

            modelBuilder.Entity("LojaVirtual.Colaboradores.Models.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BearerToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ColaboradorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PeriodEnd")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("PeriodEnd");

                    b.Property<DateTime>("PeriodStart")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("PeriodStart");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("Validade")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ColaboradorId")
                        .IsUnique();

                    b.ToTable("Tokens");

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                            {
                                ttb.UseHistoryTable("TokensHistory");
                                ttb
                                    .HasPeriodStart("PeriodStart")
                                    .HasColumnName("PeriodStart");
                                ttb
                                    .HasPeriodEnd("PeriodEnd")
                                    .HasColumnName("PeriodEnd");
                            }));
                });

            modelBuilder.Entity("LojaVirtual.Colaboradores.Models.Permissao", b =>
                {
                    b.HasOne("LojaVirtual.Colaboradores.Models.Colaborador", "Colaborador")
                        .WithOne("Permissao")
                        .HasForeignKey("LojaVirtual.Colaboradores.Models.Permissao", "ColaboradorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colaborador");
                });

            modelBuilder.Entity("LojaVirtual.Colaboradores.Models.Token", b =>
                {
                    b.HasOne("LojaVirtual.Colaboradores.Models.Colaborador", "Colaborador")
                        .WithOne("Token")
                        .HasForeignKey("LojaVirtual.Colaboradores.Models.Token", "ColaboradorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colaborador");
                });

            modelBuilder.Entity("LojaVirtual.Colaboradores.Models.Colaborador", b =>
                {
                    b.Navigation("Permissao");

                    b.Navigation("Token");
                });
#pragma warning restore 612, 618
        }
    }
}
