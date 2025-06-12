# VectorGraphicsViewer - Teknik Doküman

## Proje Amacı  
VectorGraphicsViewer, JSON formatında vektör şekillerini (Çizgi, Daire, Üçgen) okuyup WPF arayüzünde görüntüleyen masaüstü bir uygulamadır. Proje, .NET Framework 4.8 üzerinde MVVM mimarisi kullanılarak geliştirilmiştir.

## Mimari Yapı  
Proje, Clean Architecture prensiplerine uygun olarak 4 ana katmana ayrılmıştır:

- **Domain**  
  Saf şekil soyutlamalarını içerir:  
  - `IShape` arayüzü  
  - `LineShape`, `CircleShape`, `TriangleShape` sınıfları

- **Infrastructure**  
  Dosya okuma ve ayrıştırma mantığını içerir:  
  - `JsonShapeParser` (JSON’dan şekilleri ayrıştırır)  
  - `ShapeFileReader` (dosya içeriğini okur)

- **Application**  
  UI’ya sunulan uygulama servisleri:  
  - `IShapeService` arayüzü  
  - `ShapeService` implementasyonu

- **Presentation (UI)**  
  WPF Görünümleri, ViewModel’ler ve Kontroller:  
  - `ShapeViewerViewModel`  
  - `ShapeView` (UserControl)  
  - `MainWindow.xaml`

## Özellikler  
- JSON dosyasından şekil yükleme  
- Ekrana sığdırma ve yakınlaştırma (zoom) fonksiyonu  
- Tek tıklama ile seçim desteği  
- Klavye ok tuşlarıyla seçili şekli hareket ettirme  
- ARGB renk ve saydamlık desteği

## JSON Veri Formatı  
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
