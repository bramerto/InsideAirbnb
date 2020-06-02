# NotS-WAPP
InsideAirbnb app for NotS Windows Applications class assignment (April - June 2020)

## Use Cases
- [X] Registeren en inloggen. (must)
- [X] Filter op prijs. (must)
- [X] Filter op buurt. (must)
- [X] Filter op review. (must)
- [X] Locaties van zoekresultaat zichtbaar op kaart. (could)
- [X] Kaart is clickable, details rechts op pagina, maakt gebruik van de mapbox API. (must)
- [X] Layout idem als insideairbnb.com. (could)
- [ ] Details per item waarop gefiltered is: #overnachtingen, #opbrengst in de maand. (must)
- [ ] Er moeten rollen toegevoegd en toegekend worden aan geregistreerde gebruikers. (must)
- [ ] Admin panel met resultaten zoals trends, totalen, gemiddelden, etc. worden weergegeven in charts. Denk daarbij aan bv. Gemiddelde beschikbaarheid per maand, gemiddelde beschikbaarheid per buurt, overzicht van gemiddelde huurprijs per buurt.

## Other Functional Requirements
- [X] Redirect naar Map na Signout / Signin
- [X] Default page is Map
- [X] Ontwikkelt met de laatste Microsoft ASP.Net Core versie.
- [ ] Wordt gehost op het Azure Cloud Platform (SQL Server for Azure, Redis cache voor Azure, Azure AD B2C en Azure app service).
- [X] Maakt gebruik van ASP.Net Razor pages of MVC. Alleen in overleg met docent als er goede redenen zijn om hiervan af te wijken.
- [X] Maakt gebruik van MSSQL Server (versie van Azure).
- [ ] De applicatie moet veilig zijn. Gebruik de OWASP top 5 om de meest voorkomende onveiligheden op te sporen en af te dichten.
- [ ] De applicatie is aantoonbaar highly-scalable. Er worden daarvoor performance tests als bewijsmateriaal opgeleverd (voor de performance-verbeteringen en daarna).
- [X] Authenticatie en autorisatie via Azure B2C (Authentication As A Service).
- [ ] Caching service via Redis.
