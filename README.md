# RDW CO<sub>2</sub> Proof of Concept

## Opdracht
De afstudeeropdracht was om onderzoek te doen naar het meten en berekenen van CO<sub>2</sub> uitstoot van taxivoertuigen zodat wagenparkbeheerders beter inzicht kunnen krijgen in de uitstoot van hun wagenpark. Er is nog niet voldoende informatie bekend om hier een gerichte oplossing voor te bedenken. Er zijn verschillende vragen die gesteld kunnen worden waaronder: “Hoe milieuvriendelijk rijdt de chauffeur nou eigenlijk?” 

Er zijn ook andere gegevens die toepasselijk zijn om antwoord te geven op deze vraag, maar het idee is om CO2-uitstoot per voertuig, per dienst en per gereden kilometer in een overzicht te zetten en van een gegeven periode te laten zien.

## Uitvoering
De Rijksdient voor het Wegverkeer (RDW) biedt via een Open Data API toegang tot voertuiggegevens, waaronder de officiële CO2-uitstoot. In de POC is een technische koppeling gerealiseerd met de RDW Open Data API. Het doel: onderzoeken of deze koppeling een betrouwbare, reproduceerbare en efficiënte methode biedt om CO2-gerelateerde gegevens te ontsluiten en om dit te koppelen met CabmanData, het systeem ontwikkeld door Euphoria Mobility, het bedrijf waar ik stage heb gelopen en mijn opdrachtgever.

## Doelstellingen
De focus ligt op:
1. Het berekenen van uitstoot op basis van RDW-data
2. Het omgaan met ontbrekende of inconsistente gegevens
3. Het technisch inrichten van een herbruikbare service
4. Het beoordelen van de bruikbaarheid in een operationele omgeving

## Werkwijze
Afhankelijk van de beschikbare gegevens worden er twee methoden toegepast:
- Directe uitstootberekening (voorkeur)
  - Gebruik van de CO2-uitstoot in gram per kilometer (g/km) en vermenigvuldiging met de gereden afstand.
- Indirecte berekening via brandstofverbruik
  - Als g/km ontbreekt, wordt de uitstoot berekend via liters per 100 km (l/100km) en een vooraf ingestelde uitstootwaarde per liter.
