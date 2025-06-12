# VectorGraphicsViewer

Eine modulare und erweiterbare WPF-Anwendung zum Laden, Anzeigen und Interagieren mit Vektorformen (Linien, Kreise, Dreiecke), die in externen JSON-Dateien definiert sind. Entwickelt mit MVVM und einer geschichteten Architektur für Wartbarkeit und Flexibilität.

---

## ✨ Funktionen

- Vektorformen aus JSON-Dateien laden
- Linien, Kreise und Dreiecke mit ARGB-Farben anzeigen
- Skalierung zum Anpassen an den Bildschirm für große Koordinatenbereiche
- Zoom mit dem Mausrad ein- und auszoomen
- Klick zum Auswählen von Formen mit visueller Hervorhebung
- Verschieben ausgewählter Formen mit den Pfeiltasten
- Unterstützung für Transparenz und Füll-/Randdarstellung

---

## 🧱 Architektur

Folgt der Clean Architecture mit 4 Schichten:

### 1. **Domain**

- Kernabstraktionen: `IShape`, `LineShape`, `CircleShape`, `TriangleShape`
- Beinhaltet Geometrie-, Farb-, Treffer- und Zeichenlogik

### 2. **Application**

- Schnittstellen und Services (z.B. `IShapeService`)
- Verbindet UI mit Datei-Lese-/Parsing-Logik

### 3. **Infrastructure**

- Dateioperationen (`ShapeFileReader`)
- JSON-Parsing (`JsonShapeParser`)
- Einfach erweiterbar für neue Formate wie XML

### 4. **Presentation**

- WPF UI (ViewModels, XAML-Views)
- `ShapeViewerViewModel` als Hauptlogik-Handler
- `ShapeView` für benutzerdefinierte Zeichnung und Auswahl-UI

---

## ▶️ So starten

1. Lösung in Visual Studio bauen (Ziel: .NET Framework 4.8)  
2. Anwendung starten (MainWindow.xaml)  
3. Auf "Datei laden" klicken und eine JSON-Datei mit Formen auswählen  
4. Mit Maus und Tastatur interagieren  

---

## 📄 JSON Eingabeformat
```json
[
  {
    "type": "line",
    "a": "0; 0",
    "b": "100; 50",
    "color": "255; 0; 255; 0"
  },
  {
    "type": "circle",
    "center": "100; 150",
    "radius": 30,
    "color": "128; 255; 0; 0",
    "filled": true
  },
  {
    "type": "triangle",
    "a": "10; 10",
    "b": "50; 80",
    "c": "90; 20",
    "color": "200; 0; 255; 0",
    "filled": false
  }
]
```
- Koordinaten im kartesischen Raum (Y-Achse nach oben)  
- Farben im ARGB-Format: Alpha; Rot; Grün; Blau  
- Wenn `filled` true ist, wird die Form mit Rand und Füllung dargestellt  

---

## 🖱️ Benutzerinteraktion

| Aktion             | Verhalten                   |
| ------------------ | ---------------------------|
| Mausrad scrollen   | Rein- und rauszoomen       |
| Linksklick auf Form| Form auswählen             |
| Pfeiltasten        | Ausgewählte Form bewegen   |

---

## ⚙️ Erweiterbarkeit

| Erweiterung                 | Umsetzung                                              |
| -------------------------- | -----------------------------------------------------|
| Neue Form (z.B. Rechteck)  | `IShape` implementieren und Parser aktualisieren      |
| Neues Format (z.B. XML)    | Neuen `IShapeParser` erstellen und in `ShapeFileReader` injizieren |
| Neue Auswahlverhalten      | `ShapeView` und ViewModel-Logik erweitern             |

---

## 🧠 Verwendete Designmuster

- **MVVM** – Trennung von UI und Logik  
- **Command Pattern** – über benutzerdefinierte `RelayCommand`  
- **Dependency Injection** – `ShapeService`, `IShapeParser` Abstraktionen  

---

## 📁 Projektstruktur

- `/Domain` – Geschäftslogik & Formverträge  
- `/Application` – Services & Schnittstellen  
- `/Infrastructure` – Datei-I/O & Parsing  
- `/Presentation` – WPF Views, ViewModels und Controls  

---

## 👨‍💻 Autor

Bahadirhan Keles
kelesbahadirhan@gmail.com
