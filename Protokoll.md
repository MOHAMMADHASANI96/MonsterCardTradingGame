**Technische Schritte:

Bei diesem Projekt werden die Räume auf Anfrage in kleinere Einheiten aufgeteilt wie "get", "post","put" und "delete".
ProcessRequest-Methode wird durch eine BasicRouts Klasse geroutet, die BasicRouts selbst verwaltet eine Sammlung von Methoden und ist dafür verantwortlich, den richtigen ProcessRequest zu finden.
Bei der Entwicklung des Projekts war eine Authentifizierung erforderlich, so dass eine Autorisierungsklasse(Autorization) erstellt wurde.
Die Verarbeitung zwischen Server und Datenbank findet im Data-Ordner statt.
Innerhalb des Repository-Ordners haben wir Cardsrepository, StatsRepository, TradeRepository und UsersRepository, die alle von der NpgsqlConn-Klasse erben. Die Database-Klasse stellt eine Verbindung zur Datenbank her und erhält die String-Verbindungsparameter von der Settings-Klasse.


**Unit-Tests

Die geschrieben Unit-Tests umfassen die Teile "Cardsrepository","StatsRepository", "TradeRepository", "UsersRepository" hundertprozentig.
Außerdem wurden Tests für den Spielteil "Battle" entwickelt und insgesamt mehr als 30 Tests geschrieben.


**Gelernte Punkte:

Es ist äußerst wichtig, das Modell vor Beginn des Projekts genau zu entwerfen, da das Refactoring am Ende sehr viel Zeit kosten wird.

Arbeiten mit Datenbanken in C#

Tiefes Verständnis des http-Protokolls.

Unit- und Integrationstest.

Projekt kombiniert das theoretische Wissen über OOP, das wir im letzten Semester gelernt haben, und setzt es in die praktische Anwendung um. Allerdings reicht es nicht aus, etwas
Theorie zu kennen, reicht jedoch nicht aus, um es anzuwenden. Der Start des Projekts war der schwierigste Teil.


**Verbrachte Zeit:

Zu Beginn des Projekts habe ich einen udemy-Kurs "C# Programming " besucht und einige  Tutorials im Youtube geschaut.  

Ich möchte die Zeit, die ich für die Teilnahme am Kurs und Tutorials im Youtube aufgewendet habe, mit einbeziehen, weil ich glaube, dass ich diese Zeit für das Projekt investiert habe.

Kurs und Tutorials :                       ~ 25  STD

Datenbank-Design:                          ~ 5   STD    
 
Initial Projektanalyse Design:             ~ 10  STD   
 
Http-Server-Klassen:                       ~ 10  STD   
 
Implementierung von Repository-Klassen:    ~ 25  STD   
 
Datenbank-Implementierungen:               ~ 15  STD    
   
Unit-Tests:                                ~ 5   STD    
 
debugging von bugs:                        ~ 15  STD     
        
Gesamtzeit:                                ~ 110 STD



**GitHub:

https://github.com/H4S4NI/MonsterCardTradingGame.git