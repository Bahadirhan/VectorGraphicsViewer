# VectorGraphicsViewer - Technisches Dokument

## Projektzweck  
VectorGraphicsViewer ist eine Desktop-Anwendung, die Vektorformen (Linie, Kreis, Dreieck) im JSON-Format liest und sie auf einer WPF-Oberfläche anzeigt. Das Projekt wurde mit dem .NET Framework 4.8 unter Verwendung der MVVM-Architektur entwickelt.

## Architektonische Struktur  
Das Projekt ist in 4 Hauptschichten unterteilt, die den Prinzipien der Clean Architecture folgen:

- **Domain**  
  Enthält reine Formabstraktionen:  
  - `IShape` Schnittstelle  
  - `LineShape`, `CircleShape`, `TriangleShape` Klassen

- **Infrastructure**  
  Enthält Logik zum Lesen und Parsen von Dateien:  
  - `JsonShapeParser` (parst Formen aus JSON)  
  - `ShapeFileReader` (liest Dateiinhalte)

- **Application**  
  Anwendungsschichten, die der UI Dienste bereitstellen:  
  - `IShapeService` Schnittstelle  
  - `ShapeService` Implementierung

- **Presentation (UI)**  
  WPF-Views, ViewModels und Controls:  
  - `ShapeViewerViewModel`  
  - `ShapeView` (UserControl)  
  - `MainWindow.xaml`

## Funktionen  
- Laden von Formen aus einer JSON-Datei  
- Bildschirmfüllende Skalierung und Zoomfunktion  
- Unterstützung der Auswahl (einfacher Klick)  
- Verschieben der ausgewählten Form mit den Pfeiltasten  
- ARGB-Farb- und Transparenzunterstützung

## JSON-Datenformat  
```json
[
  {
    "type": "circle",
    "center": "100; 150",
    "radius": 30,
    "color": "128; 255; 0; 0",
    "filled": true
  },
  {
    "type": "line",
    "a": "0; 0",
    "b": "100; 100",
    "color": "255; 0; 0; 255"
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
