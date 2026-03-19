# Klantendienst

**Project:** Klantendienst  
**Team:** VDAB (5 personen)  
**Technologie:** ASP.NET MVC, .NET Identity, Entity Framework  

## Beschrijving

Klantendienst is een interne webapplicatie voor het personeel, ontwikkeld in teamverband. Met de applicatie kunnen medewerkers:

- Categorieën en artikelen toevoegen, bewerken of verwijderen  
- De voorraadhoeveelheid van artikelen aanpassen  
- Inloggen en rechten beheren via een beveiligde autorisatie met .NET Identity  

Het project is ontwikkeld met **ASP.NET MVC** en gebruikt **Entity Framework** voor databasebeheer.

---

## Functies

- **Authenticatie & Autorisatie:** Gebruikersbeheer en beveiligde toegang via .NET Identity  
- **Productbeheer:** Artikelen en categorieën toevoegen, bewerken, verwijderen  
- **Voorraadbeheer:** Hoeveelheden van artikelen aanpassen  
- **Gebruikersvriendelijke interface:** Intuïtieve bediening voor personeel  

---

## Lokale installatie

Volg deze stappen om het project lokaal te draaien:

1. **Repository klonen**

git clone https://github.com/SemenPerederii/Klantendienst-MVC.git
cd Klantendienst-MVC

2. **Open het project in Visual Studio**
Dubbelklik op Klantendienst.sln of open via Visual Studio → Open Project/Solution

3. **Database updaten**

dotnet ef database update

4. **Project starten**
In Visual Studio: druk op F5 of klik op Start Debugging
Of via terminal:

dotnet run --project Klantendienst

5. **Open de applicatie in je browser**
http://localhost:5000
