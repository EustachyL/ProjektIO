USE [EdukuJez]
GO
/****** Object:  Table [dbo].[Grupy]    Script Date: 11.11.2023 16:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupy](
	[ID_Grupy] [int] IDENTITY(1,1) NOT NULL,
	[Nazwa] [varchar](255) NOT NULL,
	[Grupy_Nadrzedna] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Grupy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lekcje]    Script Date: 11.11.2023 16:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lekcje](
	[ID_Grupy] [int] NULL,
	[Dzien] [varchar](20) NULL,
	[Godzina] [time](7) NULL,
	[ID_Przedmiotu] [int] NULL,
	[ID_Prowadzacego] [int] NULL,
	[ID_Lekcji] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Lekcji] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Listy_Uprawnien]    Script Date: 11.11.2023 16:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Listy_Uprawnien](
	[ID_Grupy] [int] NOT NULL,
	[ID_Uprawnienia] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Grupy] ASC,
	[ID_Uprawnienia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oceny]    Script Date: 11.11.2023 16:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oceny](
	[ID_Ucznia] [int] IDENTITY(1,1) NOT NULL,
	[Ocena] [int] NULL,
	[Waga_Oceny] [int] NULL,
	[ID_Przedmiotu] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Ucznia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Przedmioty]    Script Date: 11.11.2023 16:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Przedmioty](
	[ID_Przedmiotu] [int] IDENTITY(1,1) NOT NULL,
	[Przedmiot] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Przedmiotu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Uprawnienia]    Script Date: 11.11.2023 16:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Uprawnienia](
	[Typ] [varchar](255) NULL,
	[ID_Uprawnienia] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Uprawnienia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Uzytkownicy]    Script Date: 11.11.2023 16:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Uzytkownicy](
	[ID_Uzytkownika] [int] IDENTITY(1,1) NOT NULL,
	[Imie] [varchar](255) NOT NULL,
	[Nazwisko] [varchar](255) NOT NULL,
	[Grupy] [varchar](255) NOT NULL,
	[Loginy] [varchar](255) NOT NULL,
	[Haslo] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Uzytkownika] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Uzytkownicy_Grupy]    Script Date: 11.11.2023 16:31:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Uzytkownicy_Grupy](
	[ID_Uzytkownika] [int] NOT NULL,
	[ID_Grupy] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Uzytkownika] ASC,
	[ID_Grupy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Grupy]  WITH CHECK ADD  CONSTRAINT [FK_Grupy_Grupy] FOREIGN KEY([Grupy_Nadrzedna])
REFERENCES [dbo].[Grupy] ([ID_Grupy])
GO
ALTER TABLE [dbo].[Grupy] CHECK CONSTRAINT [FK_Grupy_Grupy]
GO
ALTER TABLE [dbo].[Lekcje]  WITH CHECK ADD  CONSTRAINT [FK_Lekcje_Grupy] FOREIGN KEY([ID_Grupy])
REFERENCES [dbo].[Grupy] ([ID_Grupy])
GO
ALTER TABLE [dbo].[Lekcje] CHECK CONSTRAINT [FK_Lekcje_Grupy]
GO
ALTER TABLE [dbo].[Lekcje]  WITH CHECK ADD  CONSTRAINT [FK_Lekcje_Przedmioty] FOREIGN KEY([ID_Przedmiotu])
REFERENCES [dbo].[Przedmioty] ([ID_Przedmiotu])
GO
ALTER TABLE [dbo].[Lekcje] CHECK CONSTRAINT [FK_Lekcje_Przedmioty]
GO
ALTER TABLE [dbo].[Lekcje]  WITH CHECK ADD  CONSTRAINT [FK_Lekcje_Uzytkownicy] FOREIGN KEY([ID_Prowadzacego])
REFERENCES [dbo].[Uzytkownicy] ([ID_Uzytkownika])
GO
ALTER TABLE [dbo].[Lekcje] CHECK CONSTRAINT [FK_Lekcje_Uzytkownicy]
GO
ALTER TABLE [dbo].[Listy_Uprawnien]  WITH CHECK ADD  CONSTRAINT [FK_Listy_Uprawnien_Grupy] FOREIGN KEY([ID_Grupy])
REFERENCES [dbo].[Grupy] ([ID_Grupy])
GO
ALTER TABLE [dbo].[Listy_Uprawnien] CHECK CONSTRAINT [FK_Listy_Uprawnien_Grupy]
GO
ALTER TABLE [dbo].[Listy_Uprawnien]  WITH CHECK ADD  CONSTRAINT [FK_Listy_Uprawnien_Uprawnienia] FOREIGN KEY([ID_Uprawnienia])
REFERENCES [dbo].[Uprawnienia] ([ID_Uprawnienia])
GO
ALTER TABLE [dbo].[Listy_Uprawnien] CHECK CONSTRAINT [FK_Listy_Uprawnien_Uprawnienia]
GO
ALTER TABLE [dbo].[Oceny]  WITH CHECK ADD  CONSTRAINT [FK_Oceny_Przedmioty] FOREIGN KEY([ID_Przedmiotu])
REFERENCES [dbo].[Przedmioty] ([ID_Przedmiotu])
GO
ALTER TABLE [dbo].[Oceny] CHECK CONSTRAINT [FK_Oceny_Przedmioty]
GO
ALTER TABLE [dbo].[Oceny]  WITH CHECK ADD  CONSTRAINT [FK_Oceny_Uzytkownicy] FOREIGN KEY([ID_Ucznia])
REFERENCES [dbo].[Uzytkownicy] ([ID_Uzytkownika])
GO
ALTER TABLE [dbo].[Oceny] CHECK CONSTRAINT [FK_Oceny_Uzytkownicy]
GO
ALTER TABLE [dbo].[Uzytkownicy_Grupy]  WITH CHECK ADD  CONSTRAINT [FK_Uzytkownicy_Grupy_Grupy] FOREIGN KEY([ID_Grupy])
REFERENCES [dbo].[Grupy] ([ID_Grupy])
GO
ALTER TABLE [dbo].[Uzytkownicy_Grupy] CHECK CONSTRAINT [FK_Uzytkownicy_Grupy_Grupy]
GO
ALTER TABLE [dbo].[Uzytkownicy_Grupy]  WITH CHECK ADD  CONSTRAINT [FK_Uzytkownicy_Grupy_Uzytkownicy] FOREIGN KEY([ID_Uzytkownika])
REFERENCES [dbo].[Uzytkownicy] ([ID_Uzytkownika])
GO
ALTER TABLE [dbo].[Uzytkownicy_Grupy] CHECK CONSTRAINT [FK_Uzytkownicy_Grupy_Uzytkownicy]
GO
ALTER TABLE [dbo].[Lekcje]  WITH CHECK ADD CHECK  (([Dzien]='piątek' OR [Dzien]='czwartek' OR [Dzien]='środa' OR [Dzien]='wtorek' OR [Dzien]='poniedziałek'))
GO
