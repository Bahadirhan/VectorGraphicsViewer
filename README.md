# VectorGraphicsViewer - Technical Document

## Project Purpose  
VectorGraphicsViewer is a desktop application that reads vector shapes (Line, Circle, Triangle) in JSON format and displays them on a WPF interface. The project is developed on .NET Framework 4.8 using the MVVM architecture.

## Architectural Structure  
The project is divided into 4 main layers following Clean Architecture principles:

- **Domain**  
  Contains pure shape abstractions:  
  - `IShape` interface  
  - `LineShape`, `CircleShape`, `TriangleShape` classes

- **Infrastructure**  
  Contains file reading and parsing logic:  
  - `JsonShapeParser` (parses shapes from JSON)  
  - `ShapeFileReader` (reads file content)

- **Application**  
  Application services exposed to UI:  
  - `IShapeService` interface  
  - `ShapeService` implementation

- **Presentation (UI)**  
  WPF Views, ViewModels, and Controls:  
  - `ShapeViewerViewModel`  
  - `ShapeView` (UserControl)  
  - `MainWindow.xaml`

## Features  
- Loading shapes from JSON file  
- Fit-to-screen and zoom functionality  
- Selection support (single click)  
- Moving selected shape using keyboard arrows  
- ARGB color and transparency support

## JSON Data Format  
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
