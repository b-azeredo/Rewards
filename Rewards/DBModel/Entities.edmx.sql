
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/05/2024 17:33:28
-- Generated from EDMX file: C:\Users\bazeredo\source\repos\Rewards\Rewards\DBModel\Entities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB-Projeto];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FORM] DROP CONSTRAINT [FK_3];
GO
IF OBJECT_ID(N'[dbo].[FK_4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PURCHASE] DROP CONSTRAINT [FK_4];
GO
IF OBJECT_ID(N'[dbo].[FK_4_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FORM] DROP CONSTRAINT [FK_4_1];
GO
IF OBJECT_ID(N'[dbo].[FK_4_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PURCHASE] DROP CONSTRAINT [FK_4_2];
GO
IF OBJECT_ID(N'[dbo].[FK_5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[REWARD_STOCK] DROP CONSTRAINT [FK_5];
GO
IF OBJECT_ID(N'[dbo].[FK_7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FILE] DROP CONSTRAINT [FK_7];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ACTIVITY]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ACTIVITY];
GO
IF OBJECT_ID(N'[dbo].[FILE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FILE];
GO
IF OBJECT_ID(N'[dbo].[FORM]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FORM];
GO
IF OBJECT_ID(N'[dbo].[PURCHASE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PURCHASE];
GO
IF OBJECT_ID(N'[dbo].[REWARD]', 'U') IS NOT NULL
    DROP TABLE [dbo].[REWARD];
GO
IF OBJECT_ID(N'[dbo].[REWARD_STOCK]', 'U') IS NOT NULL
    DROP TABLE [dbo].[REWARD_STOCK];
GO
IF OBJECT_ID(N'[dbo].[USER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[USER];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ACTIVITY'
CREATE TABLE [dbo].[ACTIVITY] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [NAME] varchar(500)  NOT NULL,
    [POINTS] int  NOT NULL,
    [LIMIT_PER_WEEK] int  NOT NULL,
    [ACTIVATED] bit  NOT NULL,
    [DESCRIPTION] varchar(8000)  NOT NULL
);
GO

-- Creating table 'FILE'
CREATE TABLE [dbo].[FILE] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FORM_ID] int  NOT NULL,
    [CONTENT] varbinary(max)  NOT NULL,
    [FILE_NAME] varchar(100)  NULL,
    [FILE_EXTENSION] varchar(5)  NULL
);
GO

-- Creating table 'FORM'
CREATE TABLE [dbo].[FORM] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [USER_ID] int  NOT NULL,
    [ACTIVITY_ID] int  NOT NULL,
    [DESCRIPTION] varchar(8000)  NOT NULL,
    [STATUS] bit  NULL,
    [CREATE_DATE] datetime  NOT NULL,
    [MANAGER_DATA_APROVED] datetime  NULL
);
GO

-- Creating table 'PURCHASE'
CREATE TABLE [dbo].[PURCHASE] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [USER_ID] int  NOT NULL,
    [REWARD_ID] int  NOT NULL,
    [PURCHASE_DATE] datetime  NOT NULL
);
GO

-- Creating table 'REWARD'
CREATE TABLE [dbo].[REWARD] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [NAME] varchar(50)  NOT NULL,
    [PRICE] int  NOT NULL,
    [IMAGE] varbinary(max)  NOT NULL,
    [ACTIVATED] bit  NOT NULL
);
GO

-- Creating table 'REWARD_STOCK'
CREATE TABLE [dbo].[REWARD_STOCK] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [REWARD_ID] int  NOT NULL,
    [STOCK] int  NOT NULL
);
GO

-- Creating table 'USER'
CREATE TABLE [dbo].[USER] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [NAME] varchar(100)  NOT NULL,
    [EMAIL] varchar(100)  NOT NULL,
    [ROLE] varchar(50)  NOT NULL,
    [MANAGER_EMAIL] varchar(100)  NULL,
    [PROFILE_IMAGE] varbinary(max)  NOT NULL,
    [ACTIVATED] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'ACTIVITY'
ALTER TABLE [dbo].[ACTIVITY]
ADD CONSTRAINT [PK_ACTIVITY]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'FILE'
ALTER TABLE [dbo].[FILE]
ADD CONSTRAINT [PK_FILE]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'FORM'
ALTER TABLE [dbo].[FORM]
ADD CONSTRAINT [PK_FORM]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PURCHASE'
ALTER TABLE [dbo].[PURCHASE]
ADD CONSTRAINT [PK_PURCHASE]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'REWARD'
ALTER TABLE [dbo].[REWARD]
ADD CONSTRAINT [PK_REWARD]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'REWARD_STOCK'
ALTER TABLE [dbo].[REWARD_STOCK]
ADD CONSTRAINT [PK_REWARD_STOCK]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'USER'
ALTER TABLE [dbo].[USER]
ADD CONSTRAINT [PK_USER]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ACTIVITY_ID] in table 'FORM'
ALTER TABLE [dbo].[FORM]
ADD CONSTRAINT [FK_4_1]
    FOREIGN KEY ([ACTIVITY_ID])
    REFERENCES [dbo].[ACTIVITY]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_4_1'
CREATE INDEX [IX_FK_4_1]
ON [dbo].[FORM]
    ([ACTIVITY_ID]);
GO

-- Creating foreign key on [FORM_ID] in table 'FILE'
ALTER TABLE [dbo].[FILE]
ADD CONSTRAINT [FK_7]
    FOREIGN KEY ([FORM_ID])
    REFERENCES [dbo].[FORM]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_7'
CREATE INDEX [IX_FK_7]
ON [dbo].[FILE]
    ([FORM_ID]);
GO

-- Creating foreign key on [USER_ID] in table 'FORM'
ALTER TABLE [dbo].[FORM]
ADD CONSTRAINT [FK_3]
    FOREIGN KEY ([USER_ID])
    REFERENCES [dbo].[USER]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_3'
CREATE INDEX [IX_FK_3]
ON [dbo].[FORM]
    ([USER_ID]);
GO

-- Creating foreign key on [USER_ID] in table 'PURCHASE'
ALTER TABLE [dbo].[PURCHASE]
ADD CONSTRAINT [FK_4]
    FOREIGN KEY ([USER_ID])
    REFERENCES [dbo].[USER]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_4'
CREATE INDEX [IX_FK_4]
ON [dbo].[PURCHASE]
    ([USER_ID]);
GO

-- Creating foreign key on [REWARD_ID] in table 'PURCHASE'
ALTER TABLE [dbo].[PURCHASE]
ADD CONSTRAINT [FK_4_2]
    FOREIGN KEY ([REWARD_ID])
    REFERENCES [dbo].[REWARD]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_4_2'
CREATE INDEX [IX_FK_4_2]
ON [dbo].[PURCHASE]
    ([REWARD_ID]);
GO

-- Creating foreign key on [REWARD_ID] in table 'REWARD_STOCK'
ALTER TABLE [dbo].[REWARD_STOCK]
ADD CONSTRAINT [FK_5]
    FOREIGN KEY ([REWARD_ID])
    REFERENCES [dbo].[REWARD]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_5'
CREATE INDEX [IX_FK_5]
ON [dbo].[REWARD_STOCK]
    ([REWARD_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------