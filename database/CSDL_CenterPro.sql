IF DB_ID(N'CenterProDb') IS NULL
    CREATE DATABASE [CenterProDb];
GO
USE [CenterProDb];
GO


/* Bao ve du lieu: neu DB da co schema cu, dung lai de nguoi dung tu quyet dinh. */
IF EXISTS (SELECT 1 FROM sys.tables WHERE [name] <> N'__EFMigrationsHistory')
   AND OBJECT_ID(N'dbo.GiangVien', N'U') IS NULL
    THROW 51010, 'CenterProDb da co du lieu/schema khac. Hay dung mot database trong moi hoac xoa database thu nghiem cu neu khong co du lieu can giu.', 1;

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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [GiangVien] (
        [MaGiangVien] int NOT NULL IDENTITY,
        [HoTen] nvarchar(150) NOT NULL,
        CONSTRAINT [PK_GiangVien] PRIMARY KEY ([MaGiangVien])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [PhongHoc] (
        [MaPhongHoc] int NOT NULL IDENTITY,
        [TenPhongHoc] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_PhongHoc] PRIMARY KEY ([MaPhongHoc])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [MaGiangVien] int NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUsers_GiangVien_MaGiangVien] FOREIGN KEY ([MaGiangVien]) REFERENCES [GiangVien] ([MaGiangVien]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [LopHoc] (
        [MaLopHoc] int NOT NULL IDENTITY,
        [TenLopHoc] nvarchar(150) NOT NULL,
        [MaGiangVien] int NOT NULL,
        CONSTRAINT [PK_LopHoc] PRIMARY KEY ([MaLopHoc]),
        CONSTRAINT [FK_LopHoc_GiangVien_MaGiangVien] FOREIGN KEY ([MaGiangVien]) REFERENCES [GiangVien] ([MaGiangVien]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [DeXuatLichDay] (
        [MaDeXuatLich] int NOT NULL IDENTITY,
        [MaLopHoc] int NOT NULL,
        [MaGiangVien] int NOT NULL,
        [ThoiGianBatDauDeXuat] datetime2 NOT NULL,
        [ThoiGianKetThucDeXuat] datetime2 NOT NULL,
        [TrangThai] nvarchar(24) NOT NULL,
        [ThoiGianGui] datetime2 NOT NULL,
        CONSTRAINT [PK_DeXuatLichDay] PRIMARY KEY ([MaDeXuatLich]),
        CONSTRAINT [CK_DeXuatLichDay_ThoiGian] CHECK ([ThoiGianKetThucDeXuat] > [ThoiGianBatDauDeXuat]),
        CONSTRAINT [FK_DeXuatLichDay_GiangVien_MaGiangVien] FOREIGN KEY ([MaGiangVien]) REFERENCES [GiangVien] ([MaGiangVien]) ON DELETE NO ACTION,
        CONSTRAINT [FK_DeXuatLichDay_LopHoc_MaLopHoc] FOREIGN KEY ([MaLopHoc]) REFERENCES [LopHoc] ([MaLopHoc]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE TABLE [LichHoc] (
        [MaLichHoc] int NOT NULL IDENTITY,
        [MaLopHoc] int NOT NULL,
        [MaPhongHoc] int NOT NULL,
        [MaDeXuatLich] int NULL,
        [ThoiGianBatDau] datetime2 NOT NULL,
        [ThoiGianKetThuc] datetime2 NOT NULL,
        CONSTRAINT [PK_LichHoc] PRIMARY KEY ([MaLichHoc]),
        CONSTRAINT [CK_LichHoc_ThoiGian] CHECK ([ThoiGianKetThuc] > [ThoiGianBatDau]),
        CONSTRAINT [FK_LichHoc_DeXuatLichDay_MaDeXuatLich] FOREIGN KEY ([MaDeXuatLich]) REFERENCES [DeXuatLichDay] ([MaDeXuatLich]) ON DELETE NO ACTION,
        CONSTRAINT [FK_LichHoc_LopHoc_MaLopHoc] FOREIGN KEY ([MaLopHoc]) REFERENCES [LopHoc] ([MaLopHoc]) ON DELETE NO ACTION,
        CONSTRAINT [FK_LichHoc_PhongHoc_MaPhongHoc] FOREIGN KEY ([MaPhongHoc]) REFERENCES [PhongHoc] ([MaPhongHoc]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (N''role-administrator'', N''role-administrator'', N''Administrator'', N''ADMINISTRATOR''),
    (N''role-lecturer'', N''role-lecturer'', N''Lecturer'', N''LECTURER''),
    (N''role-training-staff'', N''role-training-staff'', N''TrainingStaff'', N''TRAININGSTAFF'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaGiangVien', N'HoTen') AND [object_id] = OBJECT_ID(N'[GiangVien]'))
        SET IDENTITY_INSERT [GiangVien] ON;
    EXEC(N'INSERT INTO [GiangVien] ([MaGiangVien], [HoTen])
    VALUES (1, N''Nguyễn Minh An''),
    (2, N''Trần Thu Hà''),
    (3, N''Lê Quốc Bình'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaGiangVien', N'HoTen') AND [object_id] = OBJECT_ID(N'[GiangVien]'))
        SET IDENTITY_INSERT [GiangVien] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaPhongHoc', N'TenPhongHoc') AND [object_id] = OBJECT_ID(N'[PhongHoc]'))
        SET IDENTITY_INSERT [PhongHoc] ON;
    EXEC(N'INSERT INTO [PhongHoc] ([MaPhongHoc], [TenPhongHoc])
    VALUES (1, N''Phòng A''),
    (2, N''Phòng B'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaPhongHoc', N'TenPhongHoc') AND [object_id] = OBJECT_ID(N'[PhongHoc]'))
        SET IDENTITY_INSERT [PhongHoc] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaLopHoc', N'MaGiangVien', N'TenLopHoc') AND [object_id] = OBJECT_ID(N'[LopHoc]'))
        SET IDENTITY_INSERT [LopHoc] ON;
    EXEC(N'INSERT INTO [LopHoc] ([MaLopHoc], [MaGiangVien], [TenLopHoc])
    VALUES (1, 1, N''Lập trình C# 01''),
    (2, 2, N''Web căn bản 01''),
    (3, 1, N''Cơ sở dữ liệu 01'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaLopHoc', N'MaGiangVien', N'TenLopHoc') AND [object_id] = OBJECT_ID(N'[LopHoc]'))
        SET IDENTITY_INSERT [LopHoc] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaDeXuatLich', N'MaGiangVien', N'MaLopHoc', N'ThoiGianBatDauDeXuat', N'ThoiGianGui', N'ThoiGianKetThucDeXuat', N'TrangThai') AND [object_id] = OBJECT_ID(N'[DeXuatLichDay]'))
        SET IDENTITY_INSERT [DeXuatLichDay] ON;
    EXEC(N'INSERT INTO [DeXuatLichDay] ([MaDeXuatLich], [MaGiangVien], [MaLopHoc], [ThoiGianBatDauDeXuat], [ThoiGianGui], [ThoiGianKetThucDeXuat], [TrangThai])
    VALUES (1, 1, 3, ''2026-11-10T09:00:00.0000000'', ''2026-11-08T08:00:00.0000000'', ''2026-11-10T11:00:00.0000000'', N''Pending''),
    (2, 2, 2, ''2026-11-10T08:30:00.0000000'', ''2026-11-08T08:05:00.0000000'', ''2026-11-10T09:30:00.0000000'', N''Pending'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaDeXuatLich', N'MaGiangVien', N'MaLopHoc', N'ThoiGianBatDauDeXuat', N'ThoiGianGui', N'ThoiGianKetThucDeXuat', N'TrangThai') AND [object_id] = OBJECT_ID(N'[DeXuatLichDay]'))
        SET IDENTITY_INSERT [DeXuatLichDay] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaLichHoc', N'MaDeXuatLich', N'MaLopHoc', N'MaPhongHoc', N'ThoiGianBatDau', N'ThoiGianKetThuc') AND [object_id] = OBJECT_ID(N'[LichHoc]'))
        SET IDENTITY_INSERT [LichHoc] ON;
    EXEC(N'INSERT INTO [LichHoc] ([MaLichHoc], [MaDeXuatLich], [MaLopHoc], [MaPhongHoc], [ThoiGianBatDau], [ThoiGianKetThuc])
    VALUES (1, NULL, 1, 1, ''2026-11-10T08:00:00.0000000'', ''2026-11-10T10:00:00.0000000'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaLichHoc', N'MaDeXuatLich', N'MaLopHoc', N'MaPhongHoc', N'ThoiGianBatDau', N'ThoiGianKetThuc') AND [object_id] = OBJECT_ID(N'[LichHoc]'))
        SET IDENTITY_INSERT [LichHoc] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_AspNetUsers_MaGiangVien] ON [AspNetUsers] ([MaGiangVien]) WHERE [MaGiangVien] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE INDEX [IX_DeXuatLichDay_MaGiangVien_TrangThai] ON [DeXuatLichDay] ([MaGiangVien], [TrangThai]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE INDEX [IX_DeXuatLichDay_MaLopHoc] ON [DeXuatLichDay] ([MaLopHoc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_LichHoc_MaDeXuatLich] ON [LichHoc] ([MaDeXuatLich]) WHERE [MaDeXuatLich] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE INDEX [IX_LichHoc_MaLopHoc] ON [LichHoc] ([MaLopHoc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE INDEX [IX_LichHoc_MaPhongHoc_ThoiGianBatDau_ThoiGianKetThuc] ON [LichHoc] ([MaPhongHoc], [ThoiGianBatDau], [ThoiGianKetThuc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    CREATE INDEX [IX_LopHoc_MaGiangVien] ON [LopHoc] ([MaGiangVien]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261108084754_InitialCenterPro'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261108084754_InitialCenterPro', N'10.0.14');
END;

COMMIT;
GO


IF NOT EXISTS (SELECT 1 FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20261108180000_DoiTenCSDLThanhTiengViet')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261108180000_DoiTenCSDLThanhTiengViet', N'10.0.14');
END;
GO
