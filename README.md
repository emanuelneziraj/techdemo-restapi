# TechDemo: Einführung in REST APIs mit .NET und Postman

Dieses Projekt demonstriert die Erstellung einer einfachen RESTful API mit C# und .NET 8 unter Verwendung von Visual Studio Code. Die API ermöglicht grundlegende CRUD-Operationen (Create, Read, Update, Delete) auf einer Ressource namens "Item". Postman wird zur Testung der API-Endpunkte eingesetzt.

## Voraussetzungen

- **.NET 8 SDK**: Stellen Sie sicher, dass das [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.404-macos-arm64-installer) auf Ihrem System installiert ist.
- **Visual Studio Code**: Installieren Sie die neueste Version von [Visual Studio Code](https://code.visualstudio.com/download).
- **Postman**: Laden Sie [Postman](https://www.postman.com/downloads/) herunter und installieren Sie es, um die API-Endpunkte zu testen.

## Projektstruktur
```
techdemo-restapi/
├── Controllers/
│   └── ItemsController.cs
├── Models/
│   └── Item.cs
├── Properties/
│   └── launchSettings.json
├── .gitignore
├── appsettings.Development.json
├── appsettings.json
├── Präsentation-REST-API.pptx
├── Program.cs
├── README.md
├── techdemo-restapi.csproj
├── techdemo-restapi.http
└── techdemo-restapi.sln
```

## Einrichtung und Ausführung

1. **Projekt erstellen**:
   Öffnen Sie das Terminal und führen Sie den folgenden Befehl aus, um ein neues Web-API-Projekt zu erstellen:

   ```
   dotnet new webapi -n TechDemo-RestAPI
   ```
    Projektverzeichnis öffnen: Navigieren Sie in das erstellte Projektverzeichnis:
    ```
    cd TechDemo-RestAPI
    ```

    Modelldefinition: Erstellen Sie im Ordner Models die Datei Item.cs mit folgendem Inhalt:

    ```
    using System.ComponentModel.DataAnnotations;

    namespace TechDemo_RestAPI.Models
    {
        public class Item
        {
            [Key]
            public Guid Id { get; set; } = Guid.NewGuid();
            public string? Name { get; set; }
            public decimal Price { get; set; }
        }
    }
    ```

2. **Controller erstellen**: Erstellen Sie im Ordner Controllers die Datei ItemsController.cs mit folgendem Inhalt:

    ```
    using Microsoft.AspNetCore.Mvc;
    using TechDemo_RestAPI.Models;

    namespace TechDemo_RestAPI.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ItemsController : ControllerBase
        {
            private static readonly List<Item> Items = new();

            [HttpGet]
            public ActionResult<IEnumerable<Item>> GetItems()
            {
                return Ok(Items);
            }

            [HttpGet("{id}")]
            public ActionResult<Item> GetItem(Guid id)
            {
                var item = Items.FirstOrDefault(i => i.Id == id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }

            [HttpPost]
            public ActionResult<Item> CreateItem(Item item)
            {
                item.Id = Guid.NewGuid();
                Items.Add(item);
                return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
            }

            [HttpPut("{id}")]
            public IActionResult UpdateItem(Guid id, Item updatedItem)
            {
                var item = Items.FirstOrDefault(i => i.Id == id);
                if (item == null)
                {
                    return NotFound();
                }
                item.Name = updatedItem.Name;
                item.Price = updatedItem.Price;
                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteItem(Guid id)
            {
                var item = Items.FirstOrDefault(i => i.Id == id);
                if (item == null)
                {
                    return NotFound();
                }
                Items.Remove(item);
                return NoContent();
            }
        }
    }

    ```
3. **Programmkonfiguration**: Stellen Sie sicher, dass die Datei Program.cs wie folgt konfiguriert ist:
    ```
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

    ```
4. **Projekt ausführen**: Starten Sie die Anwendung mit dem folgenden Befehl:

    ```
    dotnet run
    ```
Die API ist nun unter https://localhost:5212 erreichbar.
## API-Endpunkte

- **GET** /api/items: Gibt eine Liste aller Items zurück.
- **GET** /api/items/{id}: Gibt ein spezifisches Item basierend auf der ID zurück.
- **POST** /api/items: Erstellt ein neues Item.
- **PUT** /api/items/{id}: Aktualisiert ein bestehendes Item.
- **DELETE** /api/items/{id}: Löscht ein Item basierend auf der ID.

## Testen mit Postman

Postman öffnen: Starten Sie Postman.
Neue Anfrage erstellen: Klicken Sie auf "New" und wählen Sie "Request".
Anfrage konfigurieren:
1. Wählen Sie die HTTP-Methode (GET, POST, etc.).
2. Geben Sie die URL des Endpunkts ein, z.B. https://localhost:5001/api/items.
3. Für POST- und PUT-Anfragen:
Wechseln Sie zum Tab "Body".
Wählen Sie "raw" und "JSON" aus.
Geben Sie die JSON-Daten ein, z.B.:
    ```
    {
        "name": "Beispiel Item",
        "price": 9.99
    }
    ```
4. Anfrage senden: Klicken Sie auf "Send", um die Anfrage abzuschicken und die Antwort zu überprüfen.

## Hinweise

- **HTTPS-Zertifikat**: Beim ersten Start der Anwendung wird möglicherweise ein selbstsigniertes Zertifikat erstellt. Ihr Browser oder Postman könnte eine Warnung anzeigen. Akzeptieren Sie das Zertifikat, um fortzufahren.
- **Datenpersistenz**: In diesem Beispiel werden die Daten in einer In-Memory-Liste gespeichert. Bei einem Neustart der Anwendung gehen die Daten verloren. Für eine dauerhafte Speicherung sollten Sie eine Datenbank wie SQL Server oder SQLite integrieren.