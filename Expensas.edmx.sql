
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/16/2017 15:17:48
-- Generated from EDMX file: D:\Sistemmas\Working Copy\Consorcios\DAO\Expensas.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Expensas];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Expensas_Consorcios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expensas] DROP CONSTRAINT [FK_Expensas_Consorcios];
GO
IF OBJECT_ID(N'[dbo].[FK_ExpensasDetalle_Expensas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExpensasDetalle] DROP CONSTRAINT [FK_ExpensasDetalle_Expensas];
GO
IF OBJECT_ID(N'[dbo].[FK_Gastos_TipoGastos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Gastos] DROP CONSTRAINT [FK_Gastos_TipoGastos];
GO
IF OBJECT_ID(N'[dbo].[FK_GastosExtDetalle_Expensas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GastosExtDetalle] DROP CONSTRAINT [FK_GastosExtDetalle_Expensas];
GO
IF OBJECT_ID(N'[dbo].[FK_Pagos_UF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pagos] DROP CONSTRAINT [FK_Pagos_UF];
GO
IF OBJECT_ID(N'[dbo].[FK_UnidadesFuncionales_Consorcios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UnidadesFuncionales] DROP CONSTRAINT [FK_UnidadesFuncionales_Consorcios];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Consorcios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Consorcios];
GO
IF OBJECT_ID(N'[dbo].[Expensas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Expensas];
GO
IF OBJECT_ID(N'[dbo].[ExpensasDetalle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExpensasDetalle];
GO
IF OBJECT_ID(N'[dbo].[Gastos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Gastos];
GO
IF OBJECT_ID(N'[dbo].[GastosExtDetalle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GastosExtDetalle];
GO
IF OBJECT_ID(N'[dbo].[Pagos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pagos];
GO
IF OBJECT_ID(N'[dbo].[TipoGastos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoGastos];
GO
IF OBJECT_ID(N'[dbo].[UnidadesFuncionales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UnidadesFuncionales];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Consorcios'
CREATE TABLE [dbo].[Consorcios] (
    [ID] nvarchar(50)  NOT NULL,
    [Direccion] nvarchar(50)  NOT NULL,
    [UltimaExpensa] nvarchar(50)  NULL,
    [PrimerVencimiento] int  NULL,
    [SegundoVencimiento] int  NULL,
    [InteresSegundoVencimiento] decimal(18,2)  NULL
);
GO

-- Creating table 'Expensas'
CREATE TABLE [dbo].[Expensas] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Periodo] nvarchar(50)  NOT NULL,
    [Total_Gastos] decimal(18,2)  NULL,
    [PeriodoNumerico] int  NULL,
    [Estado] nvarchar(50)  NULL,
    [Consorcios_ID] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ExpensasDetalle'
CREATE TABLE [dbo].[ExpensasDetalle] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Detalle] nvarchar(50)  NULL,
    [Importe] decimal(18,2)  NULL,
    [TipoGasto_ID] int  NULL,
    [Expensas_ID] decimal(18,0)  NULL
);
GO

-- Creating table 'Gastos'
CREATE TABLE [dbo].[Gastos] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Detalle] nvarchar(50)  NULL,
    [Importe] decimal(18,2)  NULL,
    [TipoGastos_ID] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'TipoGastos'
CREATE TABLE [dbo].[TipoGastos] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Detalle] nvarchar(50)  NULL
);
GO

-- Creating table 'GastosExtDetalle'
CREATE TABLE [dbo].[GastosExtDetalle] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Detalle] nvarchar(50)  NULL,
    [Importe] decimal(18,2)  NULL,
    [Expensas_ID] decimal(18,0)  NULL
);
GO

-- Creating table 'UnidadesFuncionales'
CREATE TABLE [dbo].[UnidadesFuncionales] (
    [UF] nvarchar(5)  NOT NULL,
    [Dueño] nvarchar(50)  NULL,
    [Coeficiente] decimal(18,2)  NULL,
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Consorcios_ID] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Pagos'
CREATE TABLE [dbo].[Pagos] (
    [ID] decimal(18,0) IDENTITY(1,1) NOT NULL,
    [Coeficiente] decimal(18,2)  NOT NULL,
    [ImportePago1] decimal(18,2)  NOT NULL,
    [FechaPago1] datetime  NOT NULL,
    [ImportePago2] decimal(18,2)  NOT NULL,
    [FechaPago2] datetime  NOT NULL,
    [ImporteExtraordinario] decimal(18,2)  NOT NULL,
    [ImporteGastoParticular] decimal(18,0)  NOT NULL,
    [ImportePagado] decimal(18,2)  NULL,
    [FechaPagado] datetime  NULL,
    [Estado] nvarchar(50)  NULL,
    [Periodo] int  NULL,
    [DetalleGastoParticular] nvarchar(50)  NULL,
    [UnidadesFuncionales_ID] decimal(18,0)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Consorcios'
ALTER TABLE [dbo].[Consorcios]
ADD CONSTRAINT [PK_Consorcios]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Expensas'
ALTER TABLE [dbo].[Expensas]
ADD CONSTRAINT [PK_Expensas]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ExpensasDetalle'
ALTER TABLE [dbo].[ExpensasDetalle]
ADD CONSTRAINT [PK_ExpensasDetalle]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Gastos'
ALTER TABLE [dbo].[Gastos]
ADD CONSTRAINT [PK_Gastos]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TipoGastos'
ALTER TABLE [dbo].[TipoGastos]
ADD CONSTRAINT [PK_TipoGastos]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GastosExtDetalle'
ALTER TABLE [dbo].[GastosExtDetalle]
ADD CONSTRAINT [PK_GastosExtDetalle]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UnidadesFuncionales'
ALTER TABLE [dbo].[UnidadesFuncionales]
ADD CONSTRAINT [PK_UnidadesFuncionales]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Pagos'
ALTER TABLE [dbo].[Pagos]
ADD CONSTRAINT [PK_Pagos]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Consorcios_ID] in table 'Expensas'
ALTER TABLE [dbo].[Expensas]
ADD CONSTRAINT [FK_Expensas_Consorcios]
    FOREIGN KEY ([Consorcios_ID])
    REFERENCES [dbo].[Consorcios]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Expensas_Consorcios'
CREATE INDEX [IX_FK_Expensas_Consorcios]
ON [dbo].[Expensas]
    ([Consorcios_ID]);
GO

-- Creating foreign key on [Expensas_ID] in table 'ExpensasDetalle'
ALTER TABLE [dbo].[ExpensasDetalle]
ADD CONSTRAINT [FK_ExpensasDetalle_Expensas]
    FOREIGN KEY ([Expensas_ID])
    REFERENCES [dbo].[Expensas]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExpensasDetalle_Expensas'
CREATE INDEX [IX_FK_ExpensasDetalle_Expensas]
ON [dbo].[ExpensasDetalle]
    ([Expensas_ID]);
GO

-- Creating foreign key on [TipoGastos_ID] in table 'Gastos'
ALTER TABLE [dbo].[Gastos]
ADD CONSTRAINT [FK_Gastos_TipoGastos]
    FOREIGN KEY ([TipoGastos_ID])
    REFERENCES [dbo].[TipoGastos]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Gastos_TipoGastos'
CREATE INDEX [IX_FK_Gastos_TipoGastos]
ON [dbo].[Gastos]
    ([TipoGastos_ID]);
GO

-- Creating foreign key on [Expensas_ID] in table 'GastosExtDetalle'
ALTER TABLE [dbo].[GastosExtDetalle]
ADD CONSTRAINT [FK_GastosExtDetalle_Expensas]
    FOREIGN KEY ([Expensas_ID])
    REFERENCES [dbo].[Expensas]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GastosExtDetalle_Expensas'
CREATE INDEX [IX_FK_GastosExtDetalle_Expensas]
ON [dbo].[GastosExtDetalle]
    ([Expensas_ID]);
GO

-- Creating foreign key on [Consorcios_ID] in table 'UnidadesFuncionales'
ALTER TABLE [dbo].[UnidadesFuncionales]
ADD CONSTRAINT [FK_UnidadesFuncionales_Consorcios]
    FOREIGN KEY ([Consorcios_ID])
    REFERENCES [dbo].[Consorcios]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UnidadesFuncionales_Consorcios'
CREATE INDEX [IX_FK_UnidadesFuncionales_Consorcios]
ON [dbo].[UnidadesFuncionales]
    ([Consorcios_ID]);
GO

-- Creating foreign key on [UnidadesFuncionales_ID] in table 'Pagos'
ALTER TABLE [dbo].[Pagos]
ADD CONSTRAINT [FK_Pagos_UF]
    FOREIGN KEY ([UnidadesFuncionales_ID])
    REFERENCES [dbo].[UnidadesFuncionales]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Pagos_UF'
CREATE INDEX [IX_FK_Pagos_UF]
ON [dbo].[Pagos]
    ([UnidadesFuncionales_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------