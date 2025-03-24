# Pre-inserimento-gruppo-XR


## Flusso e Responsabilità

- **APIService:**  
  Fornisce metodi generici per le chiamate HTTP e funge da punto di accesso centrale per l'intero progetto. 
 
- **API Clients (ComponentAPIClient e DeviceAPIClient):**  
  Utilizzano l’APIService per effettuare richieste specifiche, supportando sia chiamate simulate che reali. 
 
- **Models:**  
  Definiscono la struttura dei dati attesi dalle risposte API (JSON).  

- **UI Layer:**
  - **PanelManager:**  
    Gestisce la visualizzazione di due pannelli distinti: uno per lo spinner (loading) e uno per le informazioni finali.

  - **UIController:**  
    Coordina le chiamate API e aggiorna la UI in base alle risposte ricevute.

- **Poke Interaction:**  
  Rileva l’interazione dell’utente e invoca il metodo `RequestData()` del UIController.

## Utilizzo

- **Movimento**
Teleport attraverso Microgestures

- **Esecuzione:**  
   - L'interazione utente (gestita da toccando gli oggetti o direttamente dal `UIController`) invoca `RequestData()`.
   - Il `UIController` attiva il pannello Spinner e chiede i dati tramite il client API.
   - Al completamento della chiamata, il `UIController` aggiorna l’InfoPanel tramite il `PanelManager` con i dati ottenuti o un messaggio di errore.


