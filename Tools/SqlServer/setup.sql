CREATE DATABASE [LojaVirtual.Catalogo]
GO

USE [LojaVirtual.Catalogo]
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categorias] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Produtos] (
    [Id] int NOT NULL IDENTITY,
    [CategoriaId] int NOT NULL,
    [Nome] nvarchar(50) NOT NULL,
    [Descricao] nvarchar(2000) NOT NULL,
    [Estoque] int NOT NULL,
    [Preco] decimal(18,2) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [DataAtualizacao] datetime2 NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produtos_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Imagens] (
    [Id] int NOT NULL IDENTITY,
    [ProdutoId] int NOT NULL,
    [Local] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_Imagens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Imagens_Produtos_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produtos] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Categorias_Nome] ON [Categorias] ([Nome]);
GO

CREATE INDEX [IX_Imagens_ProdutoId] ON [Imagens] ([ProdutoId]);
GO

CREATE INDEX [IX_Produtos_CategoriaId] ON [Produtos] ([CategoriaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240509184409_Inicial', N'8.0.5');
GO

COMMIT;
GO

CREATE DATABASE [LojaVirtual.Clientes]
GO

USE [LojaVirtual.Clientes]
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Clientes] (
    [Id] int NOT NULL IDENTITY,
    [Usuario] nvarchar(10) NOT NULL,
    [Senha] nvarchar(64) NOT NULL,
    [Nome] nvarchar(25) NOT NULL,
    [Sobrenome] nvarchar(25) NOT NULL,
    [Cpf] nvarchar(14) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [DataAtualizacao] datetime2 NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Senha] CHECK (LEN(Senha) = 64)
);
GO

CREATE TABLE [Emails] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [EmailEndereco] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Emails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Emails_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Enderecos] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [EnderecoNome] nvarchar(25) NOT NULL,
    [Cep] nvarchar(9) NOT NULL,
    [Logradouro] nvarchar(150) NOT NULL,
    [Numero] nvarchar(10) NOT NULL,
    [Complemento] nvarchar(150) NULL,
    [Cidade] nvarchar(25) NOT NULL,
    [Bairro] nvarchar(25) NOT NULL,
    [Uf] nvarchar(2) NOT NULL,
    CONSTRAINT [PK_Enderecos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Enderecos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Telefones] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [Numero] nvarchar(15) NOT NULL,
    CONSTRAINT [PK_Telefones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Telefones_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Tokens] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [BearerToken] nvarchar(max) NOT NULL,
    [Validade] datetime2 NOT NULL,
    [RefreshToken] nvarchar(64) NULL,
    CONSTRAINT [PK_Tokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tokens_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Clientes_Cpf] ON [Clientes] ([Cpf]);
GO

CREATE UNIQUE INDEX [IX_Clientes_Usuario] ON [Clientes] ([Usuario]);
GO

CREATE INDEX [IX_Emails_ClienteId] ON [Emails] ([ClienteId]);
GO

CREATE INDEX [IX_Enderecos_ClienteId] ON [Enderecos] ([ClienteId]);
GO

CREATE INDEX [IX_Telefones_ClienteId] ON [Telefones] ([ClienteId]);
GO

CREATE UNIQUE INDEX [IX_Tokens_ClienteId] ON [Tokens] ([ClienteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240724191217_Inicial', N'8.0.5');
GO

COMMIT;
GO

CREATE DATABASE [LojaVirtual.Colaboradores]
GO

USE [LojaVirtual.Colaboradores]
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Colaboradores] (
    [Id] int NOT NULL IDENTITY,
    [Usuario] nvarchar(10) NOT NULL,
    [Senha] nvarchar(64) NOT NULL,
    [Nome] nvarchar(25) NULL,
    [Sobrenome] nvarchar(25) NULL,
    [DataCadastro] datetime2 NOT NULL,
    [DataAtualizacao] datetime2 NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Colaboradores] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Senha] CHECK (LEN(Senha) = 64)
);
GO

CREATE TABLE [Permissoes] (
    [Id] int NOT NULL IDENTITY,
    [ColaboradorId] int NOT NULL,
    [VisualizarColaborador] bit NOT NULL,
    [AdicionarColaborador] bit NOT NULL,
    [EditarColaborador] bit NOT NULL,
    [ExcluirColaborador] bit NOT NULL,
    [VisualizarCliente] bit NOT NULL,
    [VisualizarCategoria] bit NOT NULL,
    [AdicionarCategoria] bit NOT NULL,
    [EditarCategoria] bit NOT NULL,
    [ExcluirCategoria] bit NOT NULL,
    [VisualizarProduto] bit NOT NULL,
    [AdicionarProduto] bit NOT NULL,
    [EditarProduto] bit NOT NULL,
    [ExcluirProduto] bit NOT NULL,
    [VisualizarPedido] bit NOT NULL,
    [AdicionarSituacaoPedido] bit NOT NULL,
    CONSTRAINT [PK_Permissoes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Permissoes_Colaboradores_ColaboradorId] FOREIGN KEY ([ColaboradorId]) REFERENCES [Colaboradores] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Tokens] (
    [Id] int NOT NULL IDENTITY,
    [ColaboradorId] int NOT NULL,
    [BearerToken] nvarchar(max) NOT NULL,
    [Validade] datetime2 NOT NULL,
    [RefreshToken] nvarchar(64) NULL,
    CONSTRAINT [PK_Tokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tokens_Colaboradores_ColaboradorId] FOREIGN KEY ([ColaboradorId]) REFERENCES [Colaboradores] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ativo', N'DataAtualizacao', N'DataCadastro', N'Nome', N'Senha', N'Sobrenome', N'Usuario') AND [object_id] = OBJECT_ID(N'[Colaboradores]'))
    SET IDENTITY_INSERT [Colaboradores] ON;
INSERT INTO [Colaboradores] ([Id], [Ativo], [DataAtualizacao], [DataCadastro], [Nome], [Senha], [Sobrenome], [Usuario])
VALUES (1, CAST(1 AS bit), NULL, '0001-01-01T00:00:00.0000000', NULL, N'ee5eec2a6355d4708e985fa8bc9e7b0f161fa825b106de4e899534049e4553de', NULL, N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ativo', N'DataAtualizacao', N'DataCadastro', N'Nome', N'Senha', N'Sobrenome', N'Usuario') AND [object_id] = OBJECT_ID(N'[Colaboradores]'))
    SET IDENTITY_INSERT [Colaboradores] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AdicionarCategoria', N'AdicionarColaborador', N'AdicionarProduto', N'AdicionarSituacaoPedido', N'ColaboradorId', N'EditarCategoria', N'EditarColaborador', N'EditarProduto', N'ExcluirCategoria', N'ExcluirColaborador', N'ExcluirProduto', N'VisualizarCategoria', N'VisualizarCliente', N'VisualizarColaborador', N'VisualizarPedido', N'VisualizarProduto') AND [object_id] = OBJECT_ID(N'[Permissoes]'))
    SET IDENTITY_INSERT [Permissoes] ON;
INSERT INTO [Permissoes] ([Id], [AdicionarCategoria], [AdicionarColaborador], [AdicionarProduto], [AdicionarSituacaoPedido], [ColaboradorId], [EditarCategoria], [EditarColaborador], [EditarProduto], [ExcluirCategoria], [ExcluirColaborador], [ExcluirProduto], [VisualizarCategoria], [VisualizarCliente], [VisualizarColaborador], [VisualizarPedido], [VisualizarProduto])
VALUES (1, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), 1, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AdicionarCategoria', N'AdicionarColaborador', N'AdicionarProduto', N'AdicionarSituacaoPedido', N'ColaboradorId', N'EditarCategoria', N'EditarColaborador', N'EditarProduto', N'ExcluirCategoria', N'ExcluirColaborador', N'ExcluirProduto', N'VisualizarCategoria', N'VisualizarCliente', N'VisualizarColaborador', N'VisualizarPedido', N'VisualizarProduto') AND [object_id] = OBJECT_ID(N'[Permissoes]'))
    SET IDENTITY_INSERT [Permissoes] OFF;
GO

CREATE UNIQUE INDEX [IX_Colaboradores_Usuario] ON [Colaboradores] ([Usuario]);
GO

CREATE UNIQUE INDEX [IX_Permissoes_ColaboradorId] ON [Permissoes] ([ColaboradorId]);
GO

CREATE UNIQUE INDEX [IX_Tokens_ColaboradorId] ON [Tokens] ([ColaboradorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240724191308_Inicial', N'8.0.5');
GO

COMMIT;
GO

CREATE DATABASE [LojaVirtual.Pedidos]
GO

USE [LojaVirtual.Pedidos]
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Pedidos] (
    [Id] int NOT NULL IDENTITY,
    [QuantidadeItens] int NOT NULL,
    [ValorTotal] decimal(18,2) NOT NULL,
    [TipoPagamento] int NOT NULL,
    CONSTRAINT [PK_Pedidos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Clientes] (
    [Id] int NOT NULL IDENTITY,
    [PedidoId] int NOT NULL,
    [ClienteId] int NOT NULL,
    [Nome] nvarchar(25) NOT NULL,
    [Sobrenome] nvarchar(25) NOT NULL,
    [Cpf] nvarchar(14) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [Telefone] nvarchar(15) NOT NULL,
    [EnderecoNome] nvarchar(25) NOT NULL,
    [Cep] nvarchar(9) NOT NULL,
    [Logradouro] nvarchar(150) NOT NULL,
    [EnderecoNumero] nvarchar(10) NOT NULL,
    [Complemento] nvarchar(150) NULL,
    [Cidade] nvarchar(25) NOT NULL,
    [Bairro] nvarchar(25) NOT NULL,
    [Uf] nvarchar(2) NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Clientes_Pedidos_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedidos] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PedidoItens] (
    [Id] int NOT NULL IDENTITY,
    [PedidoId] int NOT NULL,
    [ProdutoId] int NOT NULL,
    [Nome] nvarchar(50) NOT NULL,
    [Quantidade] int NOT NULL,
    [Preco] decimal(18,2) NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    [Imagem] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_PedidoItens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PedidoItens_Pedidos_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedidos] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SituacoesPedido] (
    [Id] int NOT NULL IDENTITY,
    [PedidoId] int NOT NULL,
    [TipoSituacaoPedido] int NOT NULL,
    [Data] datetime2 NOT NULL,
    [Mensagem] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_SituacoesPedido] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SituacoesPedido_Pedidos_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedidos] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Clientes_PedidoId] ON [Clientes] ([PedidoId]);
GO

CREATE INDEX [IX_PedidoItens_PedidoId] ON [PedidoItens] ([PedidoId]);
GO

CREATE INDEX [IX_SituacoesPedido_PedidoId] ON [SituacoesPedido] ([PedidoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240509184516_Inicial', N'8.0.5');
GO

COMMIT;
GO
