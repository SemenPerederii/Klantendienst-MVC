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

    public virtual DbSet<InkomendeLevering> InkomendeLeveringen { get; set; }

    public virtual DbSet<InkomendeLeveringslijn> Inkomendeleveringslijnen { get; set; }

    public virtual DbSet<Klant> Klanten { get; set; }

    public virtual DbSet<KlantReview> KlantenReviews { get; set; }

    public virtual DbSet<Leverancier> Leveranciers { get; set; }

    public virtual DbSet<MagazijnPlaats> MagazijnPlaatsen { get; set; }

    public virtual DbSet<NatuurlijkePersoon> NatuurlijkePersonen { get; set; }

    public virtual DbSet<Personeelslid> Personeelsleden { get; set; }

    public virtual DbSet<PersoneelslidAccount> PersoneelslidAccounts { get; set; }

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
                .HasForeignKey(d => d.PlaatsId)
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

            entity.HasOne(d => d.Artikel).WithMany(p => p.Artikelleveranciersinfolijnen)
                .HasForeignKey(d => d.ArtikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ArtikelLeveranciersInfoLijnen_Artikelen1");
        });

        modelBuilder.Entity<Bestellijn>(entity =>
        {
            entity.HasKey(e => e.BestellijnId).HasName("PRIMARY");

            entity.ToTable("bestellijnen");

            entity.HasIndex(e => e.ArtikelId, "fk_Bestellijnen_Artikelen1_idx");

            entity.HasIndex(e => e.BestelId, "fk_Bestellijnen_Bestellingen1_idx");

            entity.HasOne(d => d.Artikel).WithMany(p => p.Bestellijnen)
                .HasForeignKey(d => d.ArtikelId)
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

            entity.HasOne(d => d.Betaalwijze).WithMany(p => p.Bestellingen)
                .HasForeignKey(d => d.BetaalwijzeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Betaalwijzes1");

            entity.HasOne(d => d.FacturatieAdres).WithMany(p => p.BestellingenfacturatieAdres)
                .HasForeignKey(d => d.FacturatieAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Adressen1");

            entity.HasOne(d => d.Klant).WithMany(p => p.Bestellingen)
                .HasForeignKey(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Klanten1");

            entity.HasOne(d => d.LeveringsAdres).WithMany(p => p.BestellingenleveringsAdres)
                .HasForeignKey(d => d.LeveringsAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Adressen2");
        });

        modelBuilder.Entity<Bestellingsstatus>(entity =>
        {
            entity.HasKey(e => e.BestellingsStatusId).HasName("PRIMARY");

            entity.ToTable("bestellingsstatussen");

            entity.Property(e => e.Naam).HasMaxLength(45);
        });

        modelBuilder.Entity<Betaalwijze>(entity =>
        {
            entity.HasKey(e => e.BetaalwijzeId).HasName("PRIMARY");

            entity.Property(e => e.Naam).HasMaxLength(45);
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

            entity.HasMany(d => d.Artikelen).WithMany(p => p.Categorieën)
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

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.Chatgesprekken)
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

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.Chatgespreklijnen)
                .HasForeignKey(d => d.GebruikersAccountId)
                .HasConstraintName("fk_ChatgesprekLijnen_GebruikersAccounts1");

            entity.HasOne(d => d.PersoneelslidAccount).WithMany(p => p.Chatgespreklijnen)
                .HasForeignKey(d => d.PersoneelslidAccountId)
                .HasConstraintName("fk_ChatgesprekLijnen_PersoneelslidAccounts1");
        });

        modelBuilder.Entity<Contactpersoon>(entity =>
        {
            entity.HasKey(e => e.ContactpersoonId).HasName("PRIMARY");

            entity.ToTable("contactpersonen");

            entity.HasIndex(e => e.GebruikersAccountId, "fk_Contactpersonen_GebruikersAccounts_idx");

            entity.HasIndex(e => e.KlantId, "fk_Contactpersonen_Rechtspersonen1_idx");

            entity.Property(e => e.Familienaam).HasMaxLength(45);
            entity.Property(e => e.Functie).HasMaxLength(45);
            entity.Property(e => e.Voornaam).HasMaxLength(45);

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.Contactpersonen)
                .HasForeignKey(d => d.GebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Contactpersonen_GebruikersAccounts1");

            entity.HasOne(d => d.Klant).WithMany(p => p.Contactpersonen)
                .HasForeignKey(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Contactpersonen_Rechtspersonen1");
        });

        modelBuilder.Entity<EventwachtrijArtikel>(entity =>
        {
            entity.HasKey(e => e.ArtikelId).HasName("PRIMARY");

            entity.ToTable("eventwachtrijartikelen");

            entity.Property(e => e.ArtikelId).ValueGeneratedNever();
        });

        modelBuilder.Entity<GebruikersAccount>(entity =>
        {
            entity.HasKey(e => e.GebruikersAccountId).HasName("PRIMARY");

            entity.HasIndex(e => e.Emailadres, "gebrukersnaam_UNIQUE").IsUnique();

            entity.Property(e => e.Emailadres).HasMaxLength(45);
            entity.Property(e => e.Paswoord).HasMaxLength(255);
        });

        modelBuilder.Entity<InkomendeLevering>(entity =>
        {
            entity.HasKey(e => e.InkomendeLeveringsId).HasName("PRIMARY");

            entity.ToTable("inkomendeleveringen");

            entity.HasIndex(e => e.LeveranciersId, "fk_InkomendeLeveringen_Leveranciers1");

            entity.HasIndex(e => e.OntvangerPersoneelslidId, "fk_InkomendeLeveringen_Personeelsleden1_idx");

            entity.Property(e => e.LeveringsbonNummer).HasMaxLength(45);

            entity.HasOne(d => d.Leveranciers).WithMany(p => p.Inkomendeleveringen)
                .HasForeignKey(d => d.LeveranciersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeveringen_Leveranciers1");

            entity.HasOne(d => d.OntvangerPersoneelslid).WithMany(p => p.Inkomendeleveringen)
                .HasForeignKey(d => d.OntvangerPersoneelslidId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeveringen_Personeelsleden1");
        });

        modelBuilder.Entity<InkomendeLeveringslijn>(entity =>
        {
            entity.HasKey(e => new { e.InkomendeLeveringsId, e.ArtikelId, e.MagazijnPlaatsId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("inkomendeleveringslijnen");

            entity.HasIndex(e => e.ArtikelId, "fk_InkomendeLeverongsLijnen_Artikelen1_idx");

            entity.HasIndex(e => e.MagazijnPlaatsId, "fk_InkomendeLeverongsLijnen_MagazijnPlaatsen1_idx");

            entity.HasOne(d => d.Artikel).WithMany(p => p.Inkomendeleveringslijnen)
                .HasForeignKey(d => d.ArtikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeverongsLijnen_Artikelen1");

            entity.HasOne(d => d.InkomendeLevering).WithMany(p => p.Inkomendeleveringslijnen)
                .HasForeignKey(d => d.InkomendeLeveringsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeverongsLijnen_InkomendeLeveringen1");
        });

        modelBuilder.Entity<Klant>(entity =>
        {
            entity.HasKey(e => e.KlantId).HasName("PRIMARY");

            entity.ToTable("klanten");

            entity.HasIndex(e => e.FacturatieAdresId, "fk_Klanten_Adressen1_idx");

            entity.HasIndex(e => e.LeveringsAdresId, "fk_Klanten_Adressen2_idx");

            entity.HasOne(d => d.FacturatieAdres).WithMany(p => p.KlantenfacturatieAdres)
                .HasForeignKey(d => d.FacturatieAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Klanten_Adressen1");

            entity.HasOne(d => d.LeveringsAdres).WithMany(p => p.KlantenleveringsAdres)
                .HasForeignKey(d => d.LeveringsAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Klanten_Adressen2");
        });

        modelBuilder.Entity<KlantReview>(entity =>
        {
            entity.HasKey(e => e.KlantenReviewId).HasName("PRIMARY");

            entity.HasIndex(e => e.BestellijnId, "fk_KlantenReviews_Bestellijnen1_idx");

            entity.Property(e => e.Commentaar).HasMaxLength(255);
            entity.Property(e => e.Nickname).HasMaxLength(45);

            entity.HasOne(d => d.Bestellijn).WithMany(p => p.Klantenreviews)
                .HasForeignKey(d => d.BestellijnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_KlantenReviews_Bestellijnen1");
        });

        modelBuilder.Entity<Leverancier>(entity =>
        {
            entity.HasKey(e => e.LeveranciersId).HasName("PRIMARY");

            entity.HasIndex(e => e.PlaatsId, "fk_Leveranciers_Plaatsen1_idx");

            entity.Property(e => e.BTWNummer).HasMaxLength(45);
            entity.Property(e => e.Bus).HasMaxLength(5);
            entity.Property(e => e.FamilienaamContactpersoon).HasMaxLength(45);
            entity.Property(e => e.HuisNummer).HasMaxLength(5);
            entity.Property(e => e.Naam).HasMaxLength(45);
            entity.Property(e => e.Straat).HasMaxLength(45);
            entity.Property(e => e.VoornaamContactpersoon).HasMaxLength(45);

            entity.HasOne(d => d.Plaats).WithMany(p => p.Leveranciers)
                .HasForeignKey(d => d.PlaatsId)
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

            entity.HasOne(d => d.artikel).WithMany(p => p.Magazijnplaatsen)
                .HasForeignKey(d => d.ArtikelId)
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

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.Natuurlijkepersonen)
                .HasForeignKey(d => d.GebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_NatuurlijkePersonen_Gebruikersnamen1");

            entity.HasOne(d => d.Klant).WithOne(p => p.Natuurlijkepersonen)
                .HasForeignKey<NatuurlijkePersoon>(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PrivateKlanten_Klanten1");
        });

        modelBuilder.Entity<Personeelslid>(entity =>
        {
            entity.HasKey(e => e.PersoneelslidId).HasName("PRIMARY");

            entity.ToTable("personeelsleden");

            entity.HasIndex(e => e.PersoneelslidAccountId, "fk_Personeelsleden_PersoneelslidAccounts1_idx");

            entity.Property(e => e.Familienaam).HasMaxLength(45);
            entity.Property(e => e.InDienst)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.Voornaam).HasMaxLength(45);

            entity.HasOne(d => d.PersoneelslidAccount).WithMany(p => p.Personeelsleden)
                .HasForeignKey(d => d.PersoneelslidAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Personeelsleden_PersoneelslidAccounts1");

            entity.HasMany(d => d.SecurityGroepen).WithMany(p => p.Personeelsleden)
                .UsingEntity<Dictionary<string, object>>(
                    "personeelslidsecuritygroepen",
                    r => r.HasOne<SecurityGroep>().WithMany()
                        .HasForeignKey("securityGroepId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_PersoneelslidSecurityGroepen_SecurityGroepen1"),
                    l => l.HasOne<Personeelslid>().WithMany()
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

        modelBuilder.Entity<PersoneelslidAccount>(entity =>
        {
            entity.HasKey(e => e.PersoneelslidAccountId).HasName("PRIMARY");

            entity.HasIndex(e => e.Emailadres, "emailadres_UNIQUE").IsUnique();

            entity.Property(e => e.Emailadres).HasMaxLength(45);
            entity.Property(e => e.Paswoord).HasMaxLength(255);
        });

        modelBuilder.Entity<Plaats>(entity =>
        {
            entity.HasKey(e => e.PlaatsId).HasName("PRIMARY");

            entity.ToTable("plaatsen");

            entity.Property(e => e.PlaatsNaam).HasMaxLength(150);
            entity.Property(e => e.Postcode).HasMaxLength(4);
        });

        modelBuilder.Entity<RechtsPersoon>(entity =>
        {
            entity.HasKey(e => e.KlantId).HasName("PRIMARY");

            entity.ToTable("rechtspersonen");

            entity.Property(e => e.KlantId).ValueGeneratedNever();
            entity.Property(e => e.BTWNummer).HasMaxLength(10);
            entity.Property(e => e.Naam).HasMaxLength(45);

            entity.HasOne(d => d.Klant).WithOne(p => p.Rechtspersonen)
                .HasForeignKey<RechtsPersoon>(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Rechtspersonen_Klanten1");
        });

        modelBuilder.Entity<SecurityGroep>(entity =>
        {
            entity.HasKey(e => e.SecurityGroepId).HasName("PRIMARY");

            entity.ToTable("securitygroepen");

            entity.Property(e => e.Naam).HasMaxLength(45);
        });

        modelBuilder.Entity<Toonpersoneelsledenmetsecuritygroep>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("toonpersoneelsledenmetsecuritygroepen");

            entity.Property(e => e.Emailadres).HasMaxLength(45);
            entity.Property(e => e.Familienaam).HasMaxLength(45);
            entity.Property(e => e.Naam).HasMaxLength(45);
            entity.Property(e => e.Voornaam).HasMaxLength(45);
        });

        modelBuilder.Entity<UitgaandeLevering>(entity =>
        {
            entity.HasKey(e => e.UitgaandeLeveringsId).HasName("PRIMARY");

            entity.ToTable("uitgaandeleveringen");

            entity.HasIndex(e => e.BestelId, "fk_UitgaandeLeveringen_Bestellingen1_idx");

            entity.HasIndex(e => e.KlantId, "fk_UitgaandeLeveringen_Klanten1_idx");

            entity.HasIndex(e => e.UitgaandeLeveringsStatusId, "fk_UitgaandeLeveringen_UitgaandeLeveringsStatussn1_idx");

            entity.Property(e => e.Trackingcode).HasMaxLength(45);

            entity.HasOne(d => d.Bestelling).WithMany(p => p.Uitgaandeleveringen)
                .HasForeignKey(d => d.BestelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UitgaandeLeveringen_Bestellingen1");

            entity.HasOne(d => d.Klant).WithMany(p => p.Uitgaandeleveringen)
                .HasForeignKey(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UitgaandeLeveringen_Klanten1");

            entity.HasOne(d => d.UitgaandeLeveringsStatus).WithMany(p => p.Uitgaandeleveringen)
                .HasForeignKey(d => d.UitgaandeLeveringsStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UitgaandeLeveringen_UitgaandeLeveringsStatussn1");
        });

        modelBuilder.Entity<UitgaandeLeveringsStatus>(entity =>
        {
            entity.HasKey(e => e.UitgaandeLeveringsStatusId).HasName("PRIMARY");

            entity.ToTable("uitgaandeleveringsstatussen");

            entity.Property(e => e.Naam).HasMaxLength(45);
        });

        modelBuilder.Entity<VeelgesteldevragenArtikel>(entity =>
        {
            entity.HasKey(e => e.VeelgesteldeVragenArtikelId).HasName("PRIMARY");

            entity.HasIndex(e => e.ArtikelId, "fk_VeelgesteldeVragenArtikels_Artikelen1_idx");

            entity.Property(e => e.Antwoord).HasMaxLength(255);
            entity.Property(e => e.Vraag).HasMaxLength(255);

            entity.HasOne(d => d.Artikel).WithMany(p => p.Veelgesteldevragenartikels)
                .HasForeignKey(d => d.ArtikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VeelgesteldeVragenArtikels_Artikelen1");
        });

        modelBuilder.Entity<Wishlistitem>(entity =>
        {
            entity.HasKey(e => new { e.WishListItemId, e.GebruikersAccountId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasIndex(e => e.ArtikelId, "fk_WishListItems_Artikelen1_idx");

            entity.HasIndex(e => e.GebruikersAccountId, "fk_WishListItems_GebruikersAccounts1_idx");

            entity.Property(e => e.WishListItemId).ValueGeneratedOnAdd();
            entity.Property(e => e.Aantal).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.Artikel).WithMany(p => p.Wishlistitems)
                .HasForeignKey(d => d.ArtikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WishListItems_Artikelen1");

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.Wishlistitems)
                .HasForeignKey(d => d.GebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WishListItems_GebruikersAccounts1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
