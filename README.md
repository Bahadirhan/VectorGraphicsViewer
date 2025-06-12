# VectorGraphicsViewer

A modular and extensible WPF application to load, display, and interact with vector shapes (lines, circles, triangles) defined in external JSON files. Built using MVVM and a layered architecture for maintainability and flexibility.

---

## ✨ Features

- Load vector shapes from JSON files
- Display lines, circles, and triangles with ARGB colors
- Fit-to-screen scaling for large coordinate spaces
- Zoom in/out using mouse wheel
- Click to select shapes with visual highlight
- Move selected shapes using keyboard arrows
- Support for transparency and fill/border rendering

---

## 🧱 Architecture

Follows Clean Architecture with 4 layers:

### 1. **Domain**

- Core abstractions: `IShape`, `LineShape`, `CircleShape`, `TriangleShape`
- Contains geometry, color, hit-test, and drawing logic

### 2. **Application**

- Interfaces and services (e.g., `IShapeService`)
- Bridges UI and file reading/parsing logic

### 3. **Infrastructure**

- File handling (`ShapeFileReader`)
- JSON parsing (`JsonShapeParser`)
- Easily extendable to support new formats like XML

### 4. **Presentation**

- WPF UI (ViewModels, XAML Views)
- `ShapeViewerViewModel` as main logic handler
- `ShapeView` for custom drawing and selection UI

---

## ▶️ How to Run

1. Build the solution in Visual Studio (Target: .NET Framework 4.8)
2. Run the application (MainWindow\.xaml)
3. Click "Load File" to select a JSON file with shapes
4. Interact using mouse and keyboard

---

## 📄 JSON Input Format

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

- Coordinates are in Cartesian space (Y axis up)
- Colors are in ARGB format: Alpha; Red; Green; Blue
- If `filled` is true, the shape is rendered with border and fill

---

## 🖱️ User Interaction

| Action              | Behavior            |
| ------------------- | ------------------- |
| Scroll wheel        | Zoom in/out         |
| Left click on shape | Select shape        |
| Arrow keys          | Move selected shape |

---

## ⚙️ Extensibility

| Extension                   | How to implement                                            |
| --------------------------- | ----------------------------------------------------------- |
| New shape (e.g., Rectangle) | Implement `IShape` and update parser                        |
| New format (e.g., XML)      | Create new `IShapeParser` and inject into `ShapeFileReader` |
| New selection behavior      | Extend `ShapeView` and ViewModel logic                      |

---

## 🧠 Design Patterns Used

- **MVVM** – separation of concerns between UI and logic
- **Command Pattern** – via custom `RelayCommand`
- **Dependency Injection** – `ShapeService`, `IShapeParser` abstractions

---

## 📁 Project Structure

- `/Domain` – business logic & shape contracts
- `/Application` – services & interfaces
- `/Infrastructure` – file I/O & parsing
- `/Presentation` – WPF Views, ViewModels, and Controls

---

## 👨‍💻 Author

Bahadirhan Keles

