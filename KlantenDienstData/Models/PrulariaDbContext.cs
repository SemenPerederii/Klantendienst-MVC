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

    public virtual DbSet<Adres> Adressens { get; set; }

    public virtual DbSet<Artikel> Artikelen { get; set; }

    public virtual DbSet<ArtikelLeveranciersInfolijn> Artikelleveranciersinfolijnen { get; set; }

    public virtual DbSet<Bestellijn> Bestellijnen { get; set; }

    public virtual DbSet<Bestelling> Bestellingen { get; set; }

    public virtual DbSet<Bestellingsstatus> Bestellingsstatussen { get; set; }

    public virtual DbSet<betaalwijze> betaalwijzes { get; set; }

    public virtual DbSet<Categorie> categorieens { get; set; }

    public virtual DbSet<Chatgesprek> chatgesprekkens { get; set; }

    public virtual DbSet<Chatgespreklijn> chatgespreklijnens { get; set; }

    public virtual DbSet<Contactpersoon> contactpersonens { get; set; }

    public virtual DbSet<Eventwachtrijartikel> eventwachtrijartikelens { get; set; }

    public virtual DbSet<GebruikersAccount> gebruikersaccounts { get; set; }

    public virtual DbSet<InkomendeLevering> inkomendeleveringens { get; set; }

    public virtual DbSet<InkomendeLeveringslijn> inkomendeleveringslijnens { get; set; }

    public virtual DbSet<Klant> klantens { get; set; }

    public virtual DbSet<KlantReview> klantenreviews { get; set; }

    public virtual DbSet<leverancier> leveranciers { get; set; }

    public virtual DbSet<MagazijnPlaats> magazijnplaatsens { get; set; }

    public virtual DbSet<NatuurlijkePersoon> natuurlijkepersonens { get; set; }

    public virtual DbSet<PersoneelsLid> personeelsledens { get; set; }

    public virtual DbSet<personeelslidaccount> personeelslidaccounts { get; set; }

    public virtual DbSet<Plaats> plaatsens { get; set; }

    public virtual DbSet<RechtsPersoon> rechtspersonens { get; set; }

    public virtual DbSet<SecurityGroep> securitygroepens { get; set; }

    public virtual DbSet<Toonpersoneelsledenmetsecuritygroep> toonpersoneelsledenmetsecuritygroepens { get; set; }

    public virtual DbSet<UitgaandeLevering> uitgaandeleveringens { get; set; }

    public virtual DbSet<UitgaandeLeveringsStatus> uitgaandeleveringsstatussens { get; set; }

    public virtual DbSet<veelgesteldevragenartikel> veelgesteldevragenartikels { get; set; }

    public virtual DbSet<wishlistitem> wishlistitems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=prulariacom;user=root;password=seemeen.p", ServerVersion.Parse("8.0.44-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<actiecode>(entity =>
        {
            entity.HasKey(e => e.actiecodeId).HasName("PRIMARY");

            entity.Property(e => e.naam).HasMaxLength(45);
        });

        modelBuilder.Entity<Adres>(entity =>
        {
            entity.HasKey(e => e.adresId).HasName("PRIMARY");

            entity.ToTable("adressen");

            entity.HasIndex(e => e.plaatsId, "fk_Adressen_Plaatsen_idx");

            entity.Property(e => e.actief)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.bus).HasMaxLength(5);
            entity.Property(e => e.huisNummer).HasMaxLength(5);
            entity.Property(e => e.straat).HasMaxLength(100);

            entity.HasOne(d => d.plaats).WithMany(p => p.adressens)
                .HasForeignKey(d => d.plaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Adressen_Plaatsen");
        });

        modelBuilder.Entity<Artikel>(entity =>
        {
            entity.HasKey(e => e.artikelId).HasName("PRIMARY");

            entity.ToTable("artikelen");

            entity.HasIndex(e => e.ean, "ean_UNIQUE").IsUnique();

            entity.HasIndex(e => e.leveranciersId, "fk_Artikelen_Leveranciers");

            entity.Property(e => e.beschrijving).HasMaxLength(255);
            entity.Property(e => e.ean).HasMaxLength(13);
            entity.Property(e => e.levertijd).HasDefaultValueSql("'1'");
            entity.Property(e => e.naam).HasMaxLength(45);
            entity.Property(e => e.prijs).HasPrecision(18, 5);

            entity.HasOne(d => d.leveranciers).WithMany(p => p.artikelens)
                .HasForeignKey(d => d.leveranciersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Artikelen_Leveranciers");
        });

        modelBuilder.Entity<ArtikelLeveranciersInfolijn>(entity =>
        {
            entity.HasKey(e => new { e.artikelLeveranciersInfoLijnId, e.artikelId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("artikelleveranciersinfolijnen");

            entity.HasIndex(e => e.artikelId, "fk_ArtikelLeveranciersInfoLijnen_Artikelen1_idx");

            entity.Property(e => e.artikelLeveranciersInfoLijnId).ValueGeneratedOnAdd();
            entity.Property(e => e.antwoord).HasMaxLength(255);
            entity.Property(e => e.vraag).HasMaxLength(255);

            entity.HasOne(d => d.artikel).WithMany(p => p.artikelleveranciersinfolijnens)
                .HasForeignKey(d => d.artikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ArtikelLeveranciersInfoLijnen_Artikelen1");
        });

        modelBuilder.Entity<Bestellijn>(entity =>
        {
            entity.HasKey(e => e.bestellijnId).HasName("PRIMARY");

            entity.ToTable("bestellijnen");

            entity.HasIndex(e => e.artikelId, "fk_Bestellijnen_Artikelen1_idx");

            entity.HasIndex(e => e.bestelId, "fk_Bestellijnen_Bestellingen1_idx");

            entity.HasOne(d => d.artikel).WithMany(p => p.bestellijnens)
                .HasForeignKey(d => d.artikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellijnen_Artikelen1");

            entity.HasOne(d => d.bestel).WithMany(p => p.bestellijnens)
                .HasForeignKey(d => d.bestelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellijnen_Bestellingen1");
        });

        modelBuilder.Entity<Bestelling>(entity =>
        {
            entity.HasKey(e => e.bestelId).HasName("PRIMARY");

            entity.ToTable("bestellingen");

            entity.HasIndex(e => e.facturatieAdresId, "fk_Bestellingen_Adressen1_idx");

            entity.HasIndex(e => e.leveringsAdresId, "fk_Bestellingen_Adressen2_idx");

            entity.HasIndex(e => e.bestellingsStatusId, "fk_Bestellingen_BestellingsStatussen1_idx");

            entity.HasIndex(e => e.betaalwijzeId, "fk_Bestellingen_Betaalwijzes1_idx");

            entity.HasIndex(e => e.klantId, "fk_Bestellingen_Klanten1_idx");

            entity.Property(e => e.bedrijfsnaam).HasMaxLength(45);
            entity.Property(e => e.besteldatum).HasColumnType("datetime");
            entity.Property(e => e.betalingscode).HasMaxLength(45);
            entity.Property(e => e.btwNummer).HasMaxLength(45);
            entity.Property(e => e.familienaam).HasMaxLength(45);
            entity.Property(e => e.terugbetalingscode).HasMaxLength(45);
            entity.Property(e => e.voornaam).HasMaxLength(45);

            entity.HasOne(d => d.bestellingsStatus).WithMany(p => p.bestellingens)
                .HasForeignKey(d => d.bestellingsStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_BestellingsStatussen1");

            entity.HasOne(d => d.betaalwijze).WithMany(p => p.bestellingens)
                .HasForeignKey(d => d.betaalwijzeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Betaalwijzes1");

            entity.HasOne(d => d.facturatieAdres).WithMany(p => p.bestellingenfacturatieAdres)
                .HasForeignKey(d => d.facturatieAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Adressen1");

            entity.HasOne(d => d.klant).WithMany(p => p.bestellingens)
                .HasForeignKey(d => d.klantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Klanten1");

            entity.HasOne(d => d.leveringsAdres).WithMany(p => p.bestellingenleveringsAdres)
                .HasForeignKey(d => d.leveringsAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Adressen2");
        });

        modelBuilder.Entity<Bestellingsstatus>(entity =>
        {
            entity.HasKey(e => e.bestellingsStatusId).HasName("PRIMARY");

            entity.ToTable("bestellingsstatussen");

            entity.Property(e => e.naam).HasMaxLength(45);
        });

        modelBuilder.Entity<betaalwijze>(entity =>
        {
            entity.HasKey(e => e.betaalwijzeId).HasName("PRIMARY");

            entity.Property(e => e.naam).HasMaxLength(45);
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.categorieId).HasName("PRIMARY");

            entity.ToTable("categorieen");

            entity.HasIndex(e => e.hoofdCategorieId, "fk_Categorieen_Categorieen1_idx");

            entity.Property(e => e.naam).HasMaxLength(45);

            entity.HasOne(d => d.hoofdCategorie).WithMany(p => p.InversehoofdCategorie)
                .HasForeignKey(d => d.hoofdCategorieId)
                .HasConstraintName("fk_Categorieen_Categorieen1");

            entity.HasMany(d => d.artikels).WithMany(p => p.categories)
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
            entity.HasKey(e => e.chatgesprekId).HasName("PRIMARY");

            entity.ToTable("chatgesprekken");

            entity.HasIndex(e => e.gebruikersAccountId, "fk_ChatGesprekken_GebruikersAccounts1_idx");

            entity.HasOne(d => d.gebruikersAccount).WithMany(p => p.chatgesprekkens)
                .HasForeignKey(d => d.gebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ChatGesprekken_GebruikersAccounts1");
        });

        modelBuilder.Entity<Chatgespreklijn>(entity =>
        {
            entity.HasKey(e => e.chatgesprekLijnId).HasName("PRIMARY");

            entity.ToTable("chatgespreklijnen");

            entity.HasIndex(e => e.chatgesprekId, "fk_ChatgesprekLijnen_ChatGesprekken1_idx");

            entity.HasIndex(e => e.gebruikersAccountId, "fk_ChatgesprekLijnen_GebruikersAccounts1_idx");

            entity.HasIndex(e => e.personeelslidAccountId, "fk_ChatgesprekLijnen_PersoneelslidAccounts1_idx");

            entity.Property(e => e.bericht).HasMaxLength(255);
            entity.Property(e => e.gebruikersAccountId).HasDefaultValueSql("'0'");
            entity.Property(e => e.personeelslidAccountId).HasDefaultValueSql("'0'");
            entity.Property(e => e.tijdstip)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.chatgesprek).WithMany(p => p.chatgespreklijnens)
                .HasForeignKey(d => d.chatgesprekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ChatgesprekLijnen_ChatGesprekken1");

            entity.HasOne(d => d.gebruikersAccount).WithMany(p => p.chatgespreklijnens)
                .HasForeignKey(d => d.gebruikersAccountId)
                .HasConstraintName("fk_ChatgesprekLijnen_GebruikersAccounts1");

            entity.HasOne(d => d.personeelslidAccount).WithMany(p => p.chatgespreklijnens)
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

            entity.HasOne(d => d.ontvangerPersoneelslid).WithMany(p => p.inkomendeleveringens)
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

            entity.HasOne(d => d.bestellijn).WithMany(p => p.klantenreviews)
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
            entity.HasKey(e => e.magazijnPlaatsId).HasName("PRIMARY");

            entity.ToTable("magazijnplaatsen");

            entity.HasIndex(e => e.artikelId, "fk_MagazijnPlaatsen_Artikelen1_idx");

            entity.HasIndex(e => new { e.rij, e.rek }, "uinx_rijrek").IsUnique();

            entity.Property(e => e.rij)
                .HasMaxLength(1)
                .IsFixedLength();

            entity.HasOne(d => d.artikel).WithMany(p => p.magazijnplaatsens)
                .HasForeignKey(d => d.artikelId)
                .HasConstraintName("fk_MagazijnPlaatsen_Artikelen1");
        });

        modelBuilder.Entity<NatuurlijkePersoon>(entity =>
        {
            entity.HasKey(e => e.klantId).HasName("PRIMARY");

            entity.ToTable("natuurlijkepersonen");

            entity.HasIndex(e => e.gebruikersAccountId, "fk_NatuurlijkePersonen_gebruikersAccountId_idx");

            entity.HasIndex(e => e.klantId, "fk_PrivateKlanten_Klanten1_idx");

            entity.Property(e => e.klantId).ValueGeneratedNever();
            entity.Property(e => e.familienaam).HasMaxLength(45);
            entity.Property(e => e.voornaam).HasMaxLength(45);

            entity.HasOne(d => d.gebruikersAccount).WithMany(p => p.natuurlijkepersonens)
                .HasForeignKey(d => d.gebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_NatuurlijkePersonen_Gebruikersnamen1");

            entity.HasOne(d => d.klant).WithOne(p => p.natuurlijkepersonen)
                .HasForeignKey<NatuurlijkePersoon>(d => d.klantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PrivateKlanten_Klanten1");
        });

        modelBuilder.Entity<PersoneelsLid>(entity =>
        {
            entity.HasKey(e => e.personeelslidId).HasName("PRIMARY");

            entity.ToTable("personeelsleden");

            entity.HasIndex(e => e.personeelslidAccountId, "fk_Personeelsleden_PersoneelslidAccounts1_idx");

            entity.Property(e => e.familienaam).HasMaxLength(45);
            entity.Property(e => e.inDienst)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.voornaam).HasMaxLength(45);

            entity.HasOne(d => d.personeelslidAccount).WithMany(p => p.personeelsledens)
                .HasForeignKey(d => d.personeelslidAccountId)
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

            entity.HasOne(d => d.bestel).WithMany(p => p.uitgaandeleveringens)
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
