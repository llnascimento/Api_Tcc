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

CREATE TABLE [Tarefas] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(255) NOT NULL,
    [Descricao] nvarchar(1000) NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Tarefas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(255) NOT NULL,
    [Email] nvarchar(150) NOT NULL,
    [Senha] nvarchar(32) NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230308011848_InitialDB', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Usuarios] ADD [SexoUsuario] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Tarefas] ADD [PrioriddaeTarefa] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Tarefas] ADD [TipoTarefa] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Tarefas] ADD [UsuarioId] int NULL;
GO

CREATE INDEX [IX_Tarefas_UsuarioId] ON [Tarefas] ([UsuarioId]);
GO

ALTER TABLE [Tarefas] ADD CONSTRAINT [FK_Tarefas_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230315010937_VinculoTarefaUsuario', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'SexoUsuario');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Usuarios] ALTER COLUMN [SexoUsuario] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230506141349_Atualizacao', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Nome', N'Senha', N'SexoUsuario') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] ON;
INSERT INTO [Usuarios] ([Id], [Email], [Nome], [Senha], [SexoUsuario])
VALUES (1, N'Usuario@gmail.com', N'UsuarioAdmin', N'1233456', N'M');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Nome', N'Senha', N'SexoUsuario') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230506143538_CriacaoUsuario', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230510195834_AjusteNaAutenticacao', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Tarefas] DROP CONSTRAINT [FK_Tarefas_Usuarios_UsuarioId];
GO

DROP INDEX [IX_Tarefas_UsuarioId] ON [Tarefas];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefas]') AND [c].[name] = N'PrioriddaeTarefa');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Tarefas] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Tarefas] DROP COLUMN [PrioriddaeTarefa];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefas]') AND [c].[name] = N'TipoTarefa');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Tarefas] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Tarefas] DROP COLUMN [TipoTarefa];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefas]') AND [c].[name] = N'UsuarioId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Tarefas] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Tarefas] ALTER COLUMN [UsuarioId] int NOT NULL;
ALTER TABLE [Tarefas] ADD DEFAULT 0 FOR [UsuarioId];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefas]') AND [c].[name] = N'Status');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Tarefas] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Tarefas] ALTER COLUMN [Status] nvarchar(max) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefas]') AND [c].[name] = N'Descricao');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Tarefas] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Tarefas] ALTER COLUMN [Descricao] nvarchar(1000) NOT NULL;
ALTER TABLE [Tarefas] ADD DEFAULT N'' FOR [Descricao];
GO

ALTER TABLE [Tarefas] ADD [DataTermino] nvarchar(6) NOT NULL DEFAULT N'';
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataTermino', N'Descricao', N'Nome', N'Status', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Tarefas]'))
    SET IDENTITY_INSERT [Tarefas] ON;
INSERT INTO [Tarefas] ([Id], [DataTermino], [Descricao], [Nome], [Status], [UsuarioId])
VALUES (1, N'09/04/2024', N'Teste Api', N'Tarefa Teste', N'Pendente', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataTermino', N'Descricao', N'Nome', N'Status', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Tarefas]'))
    SET IDENTITY_INSERT [Tarefas] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230610163756_UltimaAtualizacao', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DELETE FROM [Tarefas]
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230610171125_Ultima', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tarefas]') AND [c].[name] = N'DataTermino');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Tarefas] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Tarefas] ALTER COLUMN [DataTermino] nvarchar(max) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230610182106_Ajuste', N'6.0.0');
GO

COMMIT;
GO

