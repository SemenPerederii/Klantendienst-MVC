using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace KlantenDienstData.Models;

public partial class PrulariaDbContext : DbContext
{
    public PrulariaDbContext()
    {
    }

    public PrulariaDbContext(DbContextOptions<PrulariaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actiecode> Actiecodes { get; set; }

    public virtual DbSet<Adres> Adressen { get; set; }

    public virtual DbSet<Artikel> Artikelen { get; set; }

    public virtual DbSet<ArtikelLeveranciersInfolijn> Artikelleveranciersinfolijnen { get; set; }

    public virtual DbSet<Bestellijn> Bestellijnen { get; set; }

    public virtual DbSet<Bestelling> Bestellingen { get; set; }

    public virtual DbSet<Bestellingsstatus> Bestellingsstatussen { get; set; }

    public virtual DbSet<Betaalwijze> Betaalwijzen { get; set; }

    public virtual DbSet<Categorie> Categorieen { get; set; }

    public virtual DbSet<Chatgesprek> Chatgesprekken { get; set; }

    public virtual DbSet<Chatgespreklijn> Chatgespreklijnen { get; set; }

    public virtual DbSet<Contactpersoon> Contactpersonen { get; set; }

    public virtual DbSet<EventwachtrijArtikel> EventwachtrijArtikelen { get; set; }

    public virtual DbSet<GebruikersAccount> GebruikersAccounts { get; set; }

    public virtual DbSet<InkomendeLevering> InkomendeLeveringens { get; set; }

    public virtual DbSet<InkomendeLeveringslijn> Inkomendeleveringslijnens { get; set; }

    public virtual DbSet<Klant> klantens { get; set; }

    public virtual DbSet<KlantReview> KlantenReviews { get; set; }

    public virtual DbSet<Leverancier> Leveranciers { get; set; }

    public virtual DbSet<MagazijnPlaats> MagazijnPlaatsen { get; set; }

    public virtual DbSet<NatuurlijkePersoon> NatuurlijkePersonen { get; set; }

    public virtual DbSet<PersoneelsLid> PersoneelsLeden { get; set; }

    public virtual DbSet<PersoneelsLidAccount> PersoneelsLidAccounts { get; set; }

    public virtual DbSet<Plaats> Plaatsen { get; set; }

    public virtual DbSet<RechtsPersoon> RechtsPersonen { get; set; }

    public virtual DbSet<SecurityGroep> SecurityGroepen { get; set; }

    public virtual DbSet<Toonpersoneelsledenmetsecuritygroep> Toonpersoneelsledenmetsecuritygroepen { get; set; }

    public virtual DbSet<UitgaandeLevering> UitgaandeLeveringen { get; set; }

    public virtual DbSet<UitgaandeLeveringsStatus> UitgaandeLeveringsStatussen { get; set; }

    public virtual DbSet<VeelgesteldevragenArtikel> VeelgesteldevragenArtikels { get; set; }

    public virtual DbSet<Wishlistitem> Wishlistitems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Actiecode>(entity =>
        {
            entity.HasKey(e => e.ActiecodeId).HasName("PRIMARY");

            entity.Property(e => e.Naam).HasMaxLength(45);
        });

        modelBuilder.Entity<Adres>(entity =>
        {
            entity.HasKey(e => e.AdresId).HasName("PRIMARY");

            entity.ToTable("adressen");

            entity.HasIndex(e => e.PlaatsId, "fk_Adressen_Plaatsen_idx");

            entity.Property(e => e.Actief)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.Bus).HasMaxLength(5);
            entity.Property(e => e.HuisNummer).HasMaxLength(5);
            entity.Property(e => e.Straat).HasMaxLength(100);

            entity.HasOne(d => d.Plaats).WithMany(p => p.Adressen)
                .HasForeignKey(d => d.plaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Adressen_Plaatsen");
        });

        modelBuilder.Entity<Artikel>(entity =>
        {
            entity.HasKey(e => e.ArtikelId).HasName("PRIMARY");

            entity.ToTable("artikelen");

            entity.HasIndex(e => e.EAN, "ean_UNIQUE").IsUnique();

            entity.HasIndex(e => e.LeveranciersId, "fk_Artikelen_Leveranciers");

            entity.Property(e => e.Beschrijving).HasMaxLength(255);
            entity.Property(e => e.EAN).HasMaxLength(13);
            entity.Property(e => e.Levertijd).HasDefaultValueSql("'1'");
            entity.Property(e => e.Naam).HasMaxLength(45);
            entity.Property(e => e.Prijs).HasPrecision(18, 5);

            entity.HasOne(d => d.Leveranciers).WithMany(p => p.Artikelen)
                .HasForeignKey(d => d.LeveranciersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Artikelen_Leveranciers");
        });

        modelBuilder.Entity<ArtikelLeveranciersInfolijn>(entity =>
        {
            entity.HasKey(e => new { e.ArtikelLeveranciersInfoLijnId, e.ArtikelId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("artikelleveranciersinfolijnen");

            entity.HasIndex(e => e.ArtikelId, "fk_ArtikelLeveranciersInfoLijnen_Artikelen1_idx");

            entity.Property(e => e.ArtikelLeveranciersInfoLijnId).ValueGeneratedOnAdd();
            entity.Property(e => e.Antwoord).HasMaxLength(255);
            entity.Property(e => e.Vraag).HasMaxLength(255);

            entity.HasOne(d => d.Artikel).WithMany(p => p.artikelleveranciersinfolijnens)
                .HasForeignKey(d => d.artikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ArtikelLeveranciersInfoLijnen_Artikelen1");
        });

        modelBuilder.Entity<Bestellijn>(entity =>
        {
            entity.HasKey(e => e.BestellijnId).HasName("PRIMARY");

            entity.ToTable("bestellijnen");

            entity.HasIndex(e => e.ArtikelId, "fk_Bestellijnen_Artikelen1_idx");

            entity.HasIndex(e => e.BestelId, "fk_Bestellijnen_Bestellingen1_idx");

            entity.HasOne(d => d.Artikel).WithMany(p => p.bestellijnens)
                .HasForeignKey(d => d.artikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellijnen_Artikelen1");

            entity.HasOne(d => d.Bestel).WithMany(p => p.Bestellijnen)
                .HasForeignKey(d => d.BestelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellijnen_Bestellingen1");
        });

        modelBuilder.Entity<Bestelling>(entity =>
        {
            entity.HasKey(e => e.BestelId).HasName("PRIMARY");

            entity.ToTable("bestellingen");

            entity.HasIndex(e => e.FacturatieAdresId, "fk_Bestellingen_Adressen1_idx");

            entity.HasIndex(e => e.LeveringsAdresId, "fk_Bestellingen_Adressen2_idx");

            entity.HasIndex(e => e.BestellingsStatusId, "fk_Bestellingen_BestellingsStatussen1_idx");

            entity.HasIndex(e => e.BetaalwijzeId, "fk_Bestellingen_Betaalwijzes1_idx");

            entity.HasIndex(e => e.KlantId, "fk_Bestellingen_Klanten1_idx");

            entity.Property(e => e.Bedrijfsnaam).HasMaxLength(45);
            entity.Property(e => e.Besteldatum).HasColumnType("datetime");
            entity.Property(e => e.Betalingscode).HasMaxLength(45);
            entity.Property(e => e.BTWNummer).HasMaxLength(45);
            entity.Property(e => e.Familienaam).HasMaxLength(45);
            entity.Property(e => e.Terugbetalingscode).HasMaxLength(45);
            entity.Property(e => e.Voornaam).HasMaxLength(45);

            entity.HasOne(d => d.BestellingsStatus).WithMany(p => p.Bestellingen)
                .HasForeignKey(d => d.BestellingsStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_BestellingsStatussen1");

            entity.HasOne(d => d.Betaalwijze).WithMany(p => p.bestellingens)
                .HasForeignKey(d => d.BetaalwijzeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Betaalwijzes1");

            entity.HasOne(d => d.FacturatieAdres).WithMany(p => p.bestellingenfacturatieAdres)
                .HasForeignKey(d => d.facturatieAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Adressen1");

            entity.HasOne(d => d.Klant).WithMany(p => p.bestellingens)
                .HasForeignKey(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Klanten1");

            entity.HasOne(d => d.LeveringsAdres).WithMany(p => p.bestellingenleveringsAdres)
                .HasForeignKey(d => d.leveringsAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Adressen2");
        });

        modelBuilder.Entity<Bestellingsstatus>(entity =>
        {
            entity.HasKey(e => e.BestellingsStatusId).HasName("PRIMARY");

            entity.ToTable("bestellingsstatussen");

            entity.Property(e => e.Naam).HasMaxLength(45);
        });

        modelBuilder.Entity<betaalwijze>(entity =>
        {
            entity.HasKey(e => e.betaalwijzeId).HasName("PRIMARY");

            entity.Property(e => e.naam).HasMaxLength(45);
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.CategorieId).HasName("PRIMARY");

            entity.ToTable("categorieen");

            entity.HasIndex(e => e.HoofdCategorieId, "fk_Categorieen_Categorieen1_idx");

            entity.Property(e => e.Naam).HasMaxLength(45);

            entity.HasOne(d => d.HoofdCategorie).WithMany(p => p.InversehoofdCategorie)
                .HasForeignKey(d => d.HoofdCategorieId)
                .HasConstraintName("fk_Categorieen_Categorieen1");

            entity.HasMany(d => d.Artikelen).WithMany(p => p.categories)
                .UsingEntity<Dictionary<string, object>>(
                    "artikelcategorieen",
                    r => r.HasOne<Artikel>().WithMany()
                        .HasForeignKey("artikelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ArtikelCategorieen_Artikelen1"),
                    l => l.HasOne<Categorie>().WithMany()
                        .HasForeignKey("categorieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ArtikelCategorieen_Categorieen1"),
                    j =>
                    {
                        j.HasKey("categorieId", "artikelId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("artikelcategorieen");
                        j.HasIndex(new[] { "artikelId" }, "fk_ArtikelCategorieen_Artikelen1_idx");
                    });
        });

        modelBuilder.Entity<Chatgesprek>(entity =>
        {
            entity.HasKey(e => e.ChatgesprekId).HasName("PRIMARY");

            entity.ToTable("chatgesprekken");

            entity.HasIndex(e => e.GebruikersAccountId, "fk_ChatGesprekken_GebruikersAccounts1_idx");

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.chatgesprekkens)
                .HasForeignKey(d => d.GebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ChatGesprekken_GebruikersAccounts1");
        });

        modelBuilder.Entity<Chatgespreklijn>(entity =>
        {
            entity.HasKey(e => e.ChatgesprekLijnId).HasName("PRIMARY");

            entity.ToTable("chatgespreklijnen");

            entity.HasIndex(e => e.ChatgesprekId, "fk_ChatgesprekLijnen_ChatGesprekken1_idx");

            entity.HasIndex(e => e.GebruikersAccountId, "fk_ChatgesprekLijnen_GebruikersAccounts1_idx");

            entity.HasIndex(e => e.PersoneelslidAccountId, "fk_ChatgesprekLijnen_PersoneelslidAccounts1_idx");

            entity.Property(e => e.Bericht).HasMaxLength(255);
            entity.Property(e => e.GebruikersAccountId).HasDefaultValueSql("'0'");
            entity.Property(e => e.PersoneelslidAccountId).HasDefaultValueSql("'0'");
            entity.Property(e => e.Tijdstip)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.Chatgesprek).WithMany(p => p.Chatgespreklijnen)
                .HasForeignKey(d => d.ChatgesprekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ChatgesprekLijnen_ChatGesprekken1");

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.chatgespreklijnens)
                .HasForeignKey(d => d.GebruikersAccountId)
                .HasConstraintName("fk_ChatgesprekLijnen_GebruikersAccounts1");

            entity.HasOne(d => d.PersoneelsLidAccount).WithMany(p => p.chatgespreklijnens)
                .HasForeignKey(d => d.personeelslidAccountId)
                .HasConstraintName("fk_ChatgesprekLijnen_PersoneelslidAccounts1");
        });

        modelBuilder.Entity<Contactpersoon>(entity =>
        {
            entity.HasKey(e => e.contactpersoonId).HasName("PRIMARY");

            entity.ToTable("contactpersonen");

            entity.HasIndex(e => e.gebruikersAccountId, "fk_Contactpersonen_GebruikersAccounts_idx");

            entity.HasIndex(e => e.klantId, "fk_Contactpersonen_Rechtspersonen1_idx");

            entity.Property(e => e.familienaam).HasMaxLength(45);
            entity.Property(e => e.functie).HasMaxLength(45);
            entity.Property(e => e.voornaam).HasMaxLength(45);

            entity.HasOne(d => d.gebruikersAccount).WithMany(p => p.contactpersonens)
                .HasForeignKey(d => d.gebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Contactpersonen_GebruikersAccounts1");

            entity.HasOne(d => d.klant).WithMany(p => p.contactpersonens)
                .HasForeignKey(d => d.klantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Contactpersonen_Rechtspersonen1");
        });

        modelBuilder.Entity<Eventwachtrijartikel>(entity =>
        {
            entity.HasKey(e => e.artikelId).HasName("PRIMARY");

            entity.ToTable("eventwachtrijartikelen");

            entity.Property(e => e.artikelId).ValueGeneratedNever();
        });

        modelBuilder.Entity<GebruikersAccount>(entity =>
        {
            entity.HasKey(e => e.gebruikersAccountId).HasName("PRIMARY");

            entity.HasIndex(e => e.emailadres, "gebrukersnaam_UNIQUE").IsUnique();

            entity.Property(e => e.emailadres).HasMaxLength(45);
            entity.Property(e => e.paswoord).HasMaxLength(255);
        });

        modelBuilder.Entity<InkomendeLevering>(entity =>
        {
            entity.HasKey(e => e.inkomendeLeveringsId).HasName("PRIMARY");

            entity.ToTable("inkomendeleveringen");

            entity.HasIndex(e => e.leveranciersId, "fk_InkomendeLeveringen_Leveranciers1");

            entity.HasIndex(e => e.ontvangerPersoneelslidId, "fk_InkomendeLeveringen_Personeelsleden1_idx");

            entity.Property(e => e.leveringsbonNummer).HasMaxLength(45);

            entity.HasOne(d => d.leveranciers).WithMany(p => p.inkomendeleveringens)
                .HasForeignKey(d => d.leveranciersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeveringen_Leveranciers1");

            entity.HasOne(d => d.ontvangerPersoneelslid).WithMany(p => p.Inkomendeleveringens)
                .HasForeignKey(d => d.ontvangerPersoneelslidId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeveringen_Personeelsleden1");
        });

        modelBuilder.Entity<InkomendeLeveringslijn>(entity =>
        {
            entity.HasKey(e => new { e.inkomendeLeveringsId, e.artikelId, e.magazijnPlaatsId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("inkomendeleveringslijnen");

            entity.HasIndex(e => e.artikelId, "fk_InkomendeLeverongsLijnen_Artikelen1_idx");

            entity.HasIndex(e => e.magazijnPlaatsId, "fk_InkomendeLeverongsLijnen_MagazijnPlaatsen1_idx");

            entity.HasOne(d => d.artikel).WithMany(p => p.inkomendeleveringslijnens)
                .HasForeignKey(d => d.artikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeverongsLijnen_Artikelen1");

            entity.HasOne(d => d.inkomendeLeverings).WithMany(p => p.inkomendeleveringslijnens)
                .HasForeignKey(d => d.inkomendeLeveringsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeverongsLijnen_InkomendeLeveringen1");
        });

        modelBuilder.Entity<Klant>(entity =>
        {
            entity.HasKey(e => e.klantId).HasName("PRIMARY");

            entity.ToTable("klanten");

            entity.HasIndex(e => e.facturatieAdresId, "fk_Klanten_Adressen1_idx");

            entity.HasIndex(e => e.leveringsAdresId, "fk_Klanten_Adressen2_idx");

            entity.HasOne(d => d.facturatieAdres).WithMany(p => p.klantenfacturatieAdres)
                .HasForeignKey(d => d.facturatieAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Klanten_Adressen1");

            entity.HasOne(d => d.leveringsAdres).WithMany(p => p.klantenleveringsAdres)
                .HasForeignKey(d => d.leveringsAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Klanten_Adressen2");
        });

        modelBuilder.Entity<KlantReview>(entity =>
        {
            entity.HasKey(e => e.klantenReviewId).HasName("PRIMARY");

            entity.HasIndex(e => e.bestellijnId, "fk_KlantenReviews_Bestellijnen1_idx");

            entity.Property(e => e.commentaar).HasMaxLength(255);
            entity.Property(e => e.nickname).HasMaxLength(45);

            entity.HasOne(d => d.bestellijn).WithMany(p => p.Klantenreviews)
                .HasForeignKey(d => d.bestellijnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_KlantenReviews_Bestellijnen1");
        });

        modelBuilder.Entity<leverancier>(entity =>
        {
            entity.HasKey(e => e.leveranciersId).HasName("PRIMARY");

            entity.HasIndex(e => e.plaatsId, "fk_Leveranciers_Plaatsen1_idx");

            entity.Property(e => e.btwNummer).HasMaxLength(45);
            entity.Property(e => e.bus).HasMaxLength(5);
            entity.Property(e => e.familienaamContactpersoon).HasMaxLength(45);
            entity.Property(e => e.huisNummer).HasMaxLength(5);
            entity.Property(e => e.naam).HasMaxLength(45);
            entity.Property(e => e.straat).HasMaxLength(45);
            entity.Property(e => e.voornaamContactpersoon).HasMaxLength(45);

            entity.HasOne(d => d.plaats).WithMany(p => p.leveranciers)
                .HasForeignKey(d => d.plaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Leveranciers_Plaatsen1");
        });

        modelBuilder.Entity<MagazijnPlaats>(entity =>
        {
            entity.HasKey(e => e.MagazijnPlaatsId).HasName("PRIMARY");

            entity.ToTable("magazijnplaatsen");

            entity.HasIndex(e => e.ArtikelId, "fk_MagazijnPlaatsen_Artikelen1_idx");

            entity.HasIndex(e => new { e.Rij, e.Rek }, "uinx_rijrek").IsUnique();

            entity.Property(e => e.Rij)
                .HasMaxLength(1)
                .IsFixedLength();

            entity.HasOne(d => d.artikel).WithMany(p => p.magazijnplaatsens)
                .HasForeignKey(d => d.artikelId)
                .HasConstraintName("fk_MagazijnPlaatsen_Artikelen1");
        });

        modelBuilder.Entity<NatuurlijkePersoon>(entity =>
        {
            entity.HasKey(e => e.KlantId).HasName("PRIMARY");

            entity.ToTable("natuurlijkepersonen");

            entity.HasIndex(e => e.GebruikersAccountId, "fk_NatuurlijkePersonen_gebruikersAccountId_idx");

            entity.HasIndex(e => e.KlantId, "fk_PrivateKlanten_Klanten1_idx");

            entity.Property(e => e.KlantId).ValueGeneratedNever();
            entity.Property(e => e.Familienaam).HasMaxLength(45);
            entity.Property(e => e.Voornaam).HasMaxLength(45);

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.natuurlijkepersonens)
                .HasForeignKey(d => d.GebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_NatuurlijkePersonen_Gebruikersnamen1");

            entity.HasOne(d => d.Klant).WithOne(p => p.natuurlijkepersonen)
                .HasForeignKey<NatuurlijkePersoon>(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PrivateKlanten_Klanten1");
        });

        modelBuilder.Entity<PersoneelsLid>(entity =>
        {
            entity.HasKey(e => e.PersoneelslidId).HasName("PRIMARY");

            entity.ToTable("personeelsleden");

            entity.HasIndex(e => e.PersoneelslidAccountId, "fk_Personeelsleden_PersoneelslidAccounts1_idx");

            entity.Property(e => e.Familienaam).HasMaxLength(45);
            entity.Property(e => e.InDienst)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.Voornaam).HasMaxLength(45);

            entity.HasOne(d => d.personeelslidAccount).WithMany(p => p.personeelsledens)
                .HasForeignKey(d => d.PersoneelslidAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Personeelsleden_PersoneelslidAccounts1");

            entity.HasMany(d => d.securityGroeps).WithMany(p => p.personeelslids)
                .UsingEntity<Dictionary<string, object>>(
                    "personeelslidsecuritygroepen",
                    r => r.HasOne<SecurityGroep>().WithMany()
                        .HasForeignKey("securityGroepId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_PersoneelslidSecurityGroepen_SecurityGroepen1"),
                    l => l.HasOne<PersoneelsLid>().WithMany()
                        .HasForeignKey("personeelslidId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_PersoneelslidSecurityGroepen_Personeelsleden1"),
                    j =>
                    {
                        j.HasKey("personeelslidId", "securityGroepId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("personeelslidsecuritygroepen");
                        j.HasIndex(new[] { "securityGroepId" }, "fk_PersoneelslidSecurityGroepen_SecurityGroepen1_idx");
                    });
        });

        modelBuilder.Entity<personeelslidaccount>(entity =>
        {
            entity.HasKey(e => e.personeelslidAccountId).HasName("PRIMARY");

            entity.HasIndex(e => e.emailadres, "emailadres_UNIQUE").IsUnique();

            entity.Property(e => e.emailadres).HasMaxLength(45);
            entity.Property(e => e.paswoord).HasMaxLength(255);
        });

        modelBuilder.Entity<Plaats>(entity =>
        {
            entity.HasKey(e => e.plaatsId).HasName("PRIMARY");

            entity.ToTable("plaatsen");

            entity.Property(e => e.plaats).HasMaxLength(150);
            entity.Property(e => e.postcode).HasMaxLength(4);
        });

        modelBuilder.Entity<RechtsPersoon>(entity =>
        {
            entity.HasKey(e => e.klantId).HasName("PRIMARY");

            entity.ToTable("rechtspersonen");

            entity.Property(e => e.klantId).ValueGeneratedNever();
            entity.Property(e => e.btwNummer).HasMaxLength(10);
            entity.Property(e => e.naam).HasMaxLength(45);

            entity.HasOne(d => d.klant).WithOne(p => p.rechtspersonen)
                .HasForeignKey<RechtsPersoon>(d => d.klantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Rechtspersonen_Klanten1");
        });

        modelBuilder.Entity<SecurityGroep>(entity =>
        {
            entity.HasKey(e => e.securityGroepId).HasName("PRIMARY");

            entity.ToTable("securitygroepen");

            entity.Property(e => e.naam).HasMaxLength(45);
        });

        modelBuilder.Entity<Toonpersoneelsledenmetsecuritygroep>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("toonpersoneelsledenmetsecuritygroepen");

            entity.Property(e => e.emailadres).HasMaxLength(45);
            entity.Property(e => e.familienaam).HasMaxLength(45);
            entity.Property(e => e.naam).HasMaxLength(45);
            entity.Property(e => e.voornaam).HasMaxLength(45);
        });

        modelBuilder.Entity<UitgaandeLevering>(entity =>
        {
            entity.HasKey(e => e.uitgaandeLeveringsId).HasName("PRIMARY");

            entity.ToTable("uitgaandeleveringen");

            entity.HasIndex(e => e.bestelId, "fk_UitgaandeLeveringen_Bestellingen1_idx");

            entity.HasIndex(e => e.klantId, "fk_UitgaandeLeveringen_Klanten1_idx");

            entity.HasIndex(e => e.uitgaandeLeveringsStatusId, "fk_UitgaandeLeveringen_UitgaandeLeveringsStatussn1_idx");

            entity.Property(e => e.trackingcode).HasMaxLength(45);

            entity.HasOne(d => d.bestel).WithMany(p => p.Uitgaandeleveringen)
                .HasForeignKey(d => d.bestelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UitgaandeLeveringen_Bestellingen1");

            entity.HasOne(d => d.klant).WithMany(p => p.uitgaandeleveringens)
                .HasForeignKey(d => d.klantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UitgaandeLeveringen_Klanten1");

            entity.HasOne(d => d.uitgaandeLeveringsStatus).WithMany(p => p.uitgaandeleveringens)
                .HasForeignKey(d => d.uitgaandeLeveringsStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UitgaandeLeveringen_UitgaandeLeveringsStatussn1");
        });

        modelBuilder.Entity<UitgaandeLeveringsStatus>(entity =>
        {
            entity.HasKey(e => e.uitgaandeLeveringsStatusId).HasName("PRIMARY");

            entity.ToTable("uitgaandeleveringsstatussen");

            entity.Property(e => e.naam).HasMaxLength(45);
        });

        modelBuilder.Entity<veelgesteldevragenartikel>(entity =>
        {
            entity.HasKey(e => e.veelgesteldeVragenArtikelId).HasName("PRIMARY");

            entity.HasIndex(e => e.artikelId, "fk_VeelgesteldeVragenArtikels_Artikelen1_idx");

            entity.Property(e => e.antwoord).HasMaxLength(255);
            entity.Property(e => e.vraag).HasMaxLength(255);

            entity.HasOne(d => d.artikel).WithMany(p => p.veelgesteldevragenartikels)
                .HasForeignKey(d => d.artikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VeelgesteldeVragenArtikels_Artikelen1");
        });

        modelBuilder.Entity<wishlistitem>(entity =>
        {
            entity.HasKey(e => new { e.wishListItemId, e.gebruikersAccountId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasIndex(e => e.artikelId, "fk_WishListItems_Artikelen1_idx");

            entity.HasIndex(e => e.gebruikersAccountId, "fk_WishListItems_GebruikersAccounts1_idx");

            entity.Property(e => e.wishListItemId).ValueGeneratedOnAdd();
            entity.Property(e => e.aantal).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.artikel).WithMany(p => p.wishlistitems)
                .HasForeignKey(d => d.artikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WishListItems_Artikelen1");

            entity.HasOne(d => d.gebruikersAccount).WithMany(p => p.wishlistitems)
                .HasForeignKey(d => d.gebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WishListItems_GebruikersAccounts1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
