# JustPlay
Applicazione TypeScript, React, C# con SQL Server
## Introduzione
JustPlay è un'applicazione web. 
Questa permette di consultare una lista di videogiochi, di fare una ricerca e di visualizzarne i dettagli in una scheda apposita. 
Da anonimi è possibile solo fare la ricerca e consultare le schede, mentre l'utente che ha fatto il login può anche accedere alla sua pagina personale e aggiungere i videogiochi nella Wishlist o nei Posseduti dalla scheda del videogioco. 
Inoltre, se l'utente ha fatto il login e dispone dei permessi, è possibile aggiungere videogiochi, modificarli e cancellarli.
## Tecnologie
- Typescript
- React
- C#
- SQL Server
## Setup
Nella repository è presente un docker compose, che si occuperà di far partire il progetto in maniera ottimale. Per avviare il progetto in questo modo è necessario avere Docker installato e poi aprire il terminale Powershell nella cartella generale della repository e scrivere "docker-compose up -d". In questo modo verranno scaricate automaticamente le immagini e il progetto verrà caricato nei container. Andando all'indirizzo "localhost" sul browser sarà possibile interagire con il progetto.

#### Importante:
Per testare le funzioni da admin fare l'accesso con i seguenti dati: 
username: test@test.com
password: Test123@@
