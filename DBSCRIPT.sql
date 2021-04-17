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

CREATE TABLE [Vendor] (
    [VendorId] int NOT NULL IDENTITY,
    [VendorName] nvarchar(max) NULL,
    [VendorRating] int NOT NULL,
    [DeliveryCharge] float NOT NULL,
    CONSTRAINT [PK_Vendor] PRIMARY KEY ([VendorId])
);
GO

CREATE TABLE [Cart] (
    [cartid] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [CustomerName] nvarchar(max) NULL,
    [ProductId] int NOT NULL,
    [Price] int NOT NULL,
    [Desciption] nvarchar(max) NULL,
    [Zipcode] int NOT NULL,
    [DeliveryDate] datetime2 NOT NULL,
    [vendorobjVendorId] int NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY ([cartid]),
    CONSTRAINT [FK_Cart_Vendor_vendorobjVendorId] FOREIGN KEY ([vendorobjVendorId]) REFERENCES [Vendor] ([VendorId]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Cart_vendorobjVendorId] ON [Cart] ([vendorobjVendorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210405051949_init', N'5.0.4');
GO

COMMIT;
GO

