USE master;
GO
IF DB_ID (N'Wypozyczalnia') IS NOT NULL
DROP DATABASE Wypozyczalnia;

CREATE DATABASE Wypozyczalnia 
COLLATE Polish_100_CI_AS; 

GO
USE [Wypozyczalnia]
GO
CREATE TABLE [dbo].[Samochody](
	[ID] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[ID_KlasaPojazdu] [int] NOT NULL,
	[Marka] [varchar](30) NOT NULL,
	[Model] [varchar](30) NOT NULL,
	[Generacja] [varchar](30) NOT NULL,
	[Rocznik] [smallint] NOT NULL,
	[MocKM] [smallint] NOT NULL,
	[CenaDoba] [decimal](18, 0) NOT NULL,
	[CenaDlugoTerm] [decimal](18, 0) NULL
)
GO

CREATE TABLE [dbo].[KlasyPojazdow](
	[ID] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Klasa] [varchar](30) NOT NULL,
	[LataPrawaJazdy] [smallint] NULL
	)
GO
CREATE TABLE [dbo].[Wypozyczone](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Samochod] [int] NOT NULL,
	[ID_Uzytkownik] [int] NOT NULL,
	[DataOdbioru] [date] NOT NULL,
	[DataZwrotu] [date] NOT NULL,
)
GO
CREATE TABLE [dbo].[Uzytkownicy](
	[ID] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Imie] [varchar](30) NOT NULL,
	[Nazwisko] [varchar](30) NOT NULL,
	[PESEL] [varchar](20) NOT NULL,
	[NrTelefonu] [int] NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[Haslo] [varchar](30) NOT NULL,
	[LataPrawaJazdy] [smallint] NULL,
	[CzyPracownik] [bit] NOT NULL
	)
GO

SET IDENTITY_INSERT [dbo].[KlasyPojazdow] ON 

INSERT [dbo].[KlasyPojazdow] ([ID], [Klasa], [LataPrawaJazdy]) VALUES (1, N'SuperSportowe', 5)
INSERT [dbo].[KlasyPojazdow] ([ID], [Klasa], [LataPrawaJazdy]) VALUES (2, N'Sportowe', 2)
INSERT [dbo].[KlasyPojazdow] ([ID], [Klasa], [LataPrawaJazdy]) VALUES (3, N'SuperLuksusowe', 5)
INSERT [dbo].[KlasyPojazdow] ([ID], [Klasa], [LataPrawaJazdy]) VALUES (4, N'Luksusowe', 2)
INSERT [dbo].[KlasyPojazdow] ([ID], [Klasa], [LataPrawaJazdy]) VALUES (5, N'Standardowe', 0)
SET IDENTITY_INSERT [dbo].[KlasyPojazdow] OFF
GO
SET IDENTITY_INSERT [dbo].[Uzytkownicy] ON 

INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (1, N'Wojtek', N'Maciejewski', N'40101579294', 601385534, N'obopoteny@wp.pl', 'tajnehaslo123', 15, 0)
INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (2, N'Czes³aw', N'Kamiñski', N'40062849397', 789265944, N'Axel8@gmail.com', 'tajnehaslo123', 20, 0)
INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (3, N'Klementyna', N'Grabowska', N'94062979143', 666626146, N'Rixsa@op.pl', 'tajnehaslo123', 8, 0)
INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (4, N'Kazimiera', N'Olszewska', N'00211554024', 672004017, N'Demoin243@gmail.com', 'tajnehaslo123', 5, 0)
INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (5, N'Berta', N'Rutkowska', N'88112807263', 607971144, N'clon0854@wp.pl', 'tajnehaslo123', 14, 0)
INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (6, N'W³odzis³aw', N'Nowakowski', N'73100783614', 788471264, N'buslig245@op.pl', 'tajnehaslo123', 2, 0)
INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (7, N'Serafina', N'Maciejewska', N'59060456362', 881915360, N'DarkSmerh@spoko.pl', 'tajnehaslo123', 44, 0)
INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (8, N'Kasia', N'Kalinowska', N'44120166567', 516981586, N'Mihailka25@gmail.com', 'tajnehaslo123', 25, 0)
INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (9, N'Fryderyk', N'Czarnecki', N'56092951713', 673237247, N'Lorenanna@gmail.com', 'tajnehaslo123', 16, 1)
INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (10, N'Karolina', N'Czerwinska', N'02280776400', 791288651, N'hero555@gmail.com', 'tajnehaslo123', 12, 1)
INSERT [dbo].[Uzytkownicy] ([ID], [Imie], [Nazwisko], [PESEL], [NrTelefonu], [Email], [Haslo], [LataPrawaJazdy], [CzyPracownik]) VALUES (11, N'a', N'a', N'00000000000', 100000000, N'a', N'a', NULL, 1)
SET IDENTITY_INSERT [dbo].[Uzytkownicy] OFF
GO

SET IDENTITY_INSERT [dbo].[Samochody] ON 

INSERT [dbo].[Samochody]([ID], [ID_KlasaPojazdu], [Marka], [Model], [Generacja], [Rocznik], [MocKM], [CenaDoba], [CenaDlugoTerm]) VALUES (1, 3, N'Mercedes-Benz', N'EQS', N'I', 2021, 333, CAST(1000 AS Decimal(18, 0)), CAST(900 AS Decimal(18, 0)))
INSERT [dbo].[Samochody]([ID], [ID_KlasaPojazdu], [Marka], [Model], [Generacja], [Rocznik], [MocKM], [CenaDoba], [CenaDlugoTerm]) VALUES (2, 1, N'Audi', N'E-Tron', N'I', 2021, 598, CAST(1050 AS Decimal(18, 0)), CAST(900 AS Decimal(18, 0)))
INSERT [dbo].[Samochody]([ID], [ID_KlasaPojazdu], [Marka], [Model], [Generacja], [Rocznik], [MocKM], [CenaDoba], [CenaDlugoTerm]) VALUES (3, 5, N'Fiat', N'500', N'3+1', 2021, 118, CAST(300 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)))
INSERT [dbo].[Samochody]([ID], [ID_KlasaPojazdu], [Marka], [Model], [Generacja], [Rocznik], [MocKM], [CenaDoba], [CenaDlugoTerm]) VALUES (4, 3, N'Mercedes-Benz', N'EQC', N'400', 2020, 408, CAST(770 AS Decimal(18, 0)), CAST(620 AS Decimal(18, 0)))
INSERT [dbo].[Samochody]([ID], [ID_KlasaPojazdu], [Marka], [Model], [Generacja], [Rocznik], [MocKM], [CenaDoba], [CenaDlugoTerm]) VALUES (5, 5, N'Nissan', N'Leaf', N'I', 2017, 150, CAST(250 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)))
INSERT [dbo].[Samochody]([ID], [ID_KlasaPojazdu], [Marka], [Model], [Generacja], [Rocznik], [MocKM], [CenaDoba], [CenaDlugoTerm]) VALUES (6, 5, N'Volkswagen', N'Passat', N'B8', 2018, 150, CAST(300 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)))
INSERT [dbo].[Samochody]([ID], [ID_KlasaPojazdu], [Marka], [Model], [Generacja], [Rocznik], [MocKM], [CenaDoba], [CenaDlugoTerm]) VALUES (7, 4, N'Lexus', N'IS 200t', N'III', 2016, 245, CAST(500 AS Decimal(18, 0)), CAST(450 AS Decimal(18, 0)))
INSERT [dbo].[Samochody]([ID], [ID_KlasaPojazdu], [Marka], [Model], [Generacja], [Rocznik], [MocKM], [CenaDoba], [CenaDlugoTerm]) VALUES (8, 3, N'Mercedes-Benz', N'Maybach', N'S650', 2020, 630, CAST(1200 AS Decimal(18, 0)), CAST(1050 AS Decimal(18, 0)))
INSERT [dbo].[Samochody]([ID], [ID_KlasaPojazdu], [Marka], [Model], [Generacja], [Rocznik], [MocKM], [CenaDoba], [CenaDlugoTerm]) VALUES (9, 2, N'Porsche', N'Boxster', N'981', 2012, 265, CAST(800 AS Decimal(18, 0)), CAST(700 AS Decimal(18, 0)))
INSERT [dbo].[Samochody]([ID], [ID_KlasaPojazdu], [Marka], [Model], [Generacja], [Rocznik], [MocKM], [CenaDoba], [CenaDlugoTerm]) VALUES (10, 1, N'Lamborghini', N'Aventador', N'S', 2018, 740, CAST(1100 AS Decimal(18, 0)), CAST(1000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Samochody] OFF
GO

GO
SET IDENTITY_INSERT [dbo].[Wypozyczone] ON 
INSERT [dbo].[Wypozyczone] ([ID], [ID_Samochod], [ID_Uzytkownik], [DataOdbioru], [DataZwrotu]) VALUES (1, 2, 1, CAST(N'2021-11-21' AS Date), CAST(N'2021-11-23' AS Date))
INSERT [dbo].[Wypozyczone] ([ID], [ID_Samochod], [ID_Uzytkownik], [DataOdbioru], [DataZwrotu]) VALUES (2, 1, 6, CAST(N'2021-12-24' AS Date), CAST(N'2021-12-30' AS Date))
INSERT [dbo].[Wypozyczone] ([ID], [ID_Samochod], [ID_Uzytkownik], [DataOdbioru], [DataZwrotu]) VALUES (3, 7, 2, CAST(N'2022-01-07' AS Date), CAST(N'2022-01-09' AS Date))
INSERT [dbo].[Wypozyczone] ([ID], [ID_Samochod], [ID_Uzytkownik], [DataOdbioru], [DataZwrotu]) VALUES (4, 8, 8, CAST(N'2022-01-07' AS Date), CAST(N'2022-01-14' AS Date))
INSERT [dbo].[Wypozyczone] ([ID], [ID_Samochod], [ID_Uzytkownik], [DataOdbioru], [DataZwrotu]) VALUES (5, 3, 7, CAST(N'2022-01-09' AS Date), CAST(N'2022-01-11' AS Date))
INSERT [dbo].[Wypozyczone] ([ID], [ID_Samochod], [ID_Uzytkownik], [DataOdbioru], [DataZwrotu]) VALUES (6, 5, 1, CAST(N'2022-01-09' AS Date), CAST(N'2022-01-11' AS Date))
INSERT [dbo].[Wypozyczone] ([ID], [ID_Samochod], [ID_Uzytkownik], [DataOdbioru], [DataZwrotu]) VALUES (7, 10, 4, CAST(N'2022-01-16' AS Date), CAST(N'2022-12-29' AS Date))
INSERT [dbo].[Wypozyczone] ([ID], [ID_Samochod], [ID_Uzytkownik], [DataOdbioru], [DataZwrotu]) VALUES (8, 2, 8, CAST(N'2022-04-26' AS Date), CAST(N'2022-05-02' AS Date))
INSERT [dbo].[Wypozyczone] ([ID], [ID_Samochod], [ID_Uzytkownik], [DataOdbioru], [DataZwrotu]) VALUES (9, 1, 6, CAST(N'2022-07-05' AS Date), CAST(N'2022-08-05' AS Date))
INSERT [dbo].[Wypozyczone] ([ID], [ID_Samochod], [ID_Uzytkownik], [DataOdbioru], [DataZwrotu]) VALUES (10, 8, 10, CAST(N'2022-09-05' AS Date), CAST(N'2022-10-15' AS Date))
SET IDENTITY_INSERT [dbo].[Wypozyczone] OFF

GO
ALTER TABLE [dbo].[Samochody]  WITH CHECK ADD FOREIGN KEY([ID_KlasaPojazdu])
REFERENCES [dbo].[KlasyPojazdow] ([ID])
GO
ALTER TABLE [dbo].[Wypozyczone]  WITH CHECK ADD FOREIGN KEY([ID_Uzytkownik])
REFERENCES [dbo].[Uzytkownicy] ([ID])
GO
ALTER TABLE [dbo].[Wypozyczone]  WITH CHECK ADD FOREIGN KEY([ID_Samochod])
REFERENCES [dbo].[Samochody] ([ID])
GO



GO
USE [master]
GO
ALTER DATABASE [Wypozyczalnia] SET  READ_WRITE 
GO