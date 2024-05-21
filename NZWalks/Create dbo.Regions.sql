USE [NZWalks]
GO

/****** Object: Table [dbo].[Regions] Script Date: 21-05-2024 12:16:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Regions] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (MAX)   NOT NULL,
    [Area]       NVARCHAR (MAX)   NOT NULL,
    [Code]       NVARCHAR (MAX)   NOT NULL,
    [Latitude]   FLOAT (53)       NOT NULL,
    [Longitude]  FLOAT (53)       NOT NULL,
    [Population] BIGINT           NOT NULL
);

select * from Regions;
Delete from Regions where Id ='4C29B6D4-5D3E-44B2-8A6F-0E1F8D8E7B6A';
