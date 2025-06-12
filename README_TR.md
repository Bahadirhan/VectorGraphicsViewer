# VectorGraphicsViewer

Harici JSON dosyalarında tanımlı vektör şekillerini (çizgiler, daireler, üçgenler) yüklemek, göstermek ve etkileşimde bulunmak için modüler ve genişletilebilir bir WPF uygulaması. Bakımı ve esnekliği için MVVM ve katmanlı mimari kullanılarak geliştirilmiştir.

---

## ✨ Özellikler

- Vektör şekillerini JSON dosyalarından yükleme
- Çizgiler, daireler ve üçgenleri ARGB renklerle gösterme
- Büyük koordinat alanları için ekrana sığdırma ölçeklemesi
- Fare tekerleği ile yakınlaştırma/uzaklaştırma
- Şekillere tıklayarak seçim yapma ve görsel vurgulama
- Seçili şekilleri klavye ok tuşlarıyla hareket ettirme
- Saydamlık ve dolgu/çerçeve çizimi desteği

---

## 🧱 Mimari

Bakımı ve genişletilebilirliği sağlamak için Clean Architecture ile 4 katman uygulanmıştır:

### 1. **Domain**

- Temel soyutlamalar: `IShape`, `LineShape`, `CircleShape`, `TriangleShape`
- Geometri, renk, hit-test ve çizim mantığı içerir

### 2. **Application**

- Arayüzler ve servisler (örn. `IShapeService`)
- UI ile dosya okuma/parsing mantığını köprüler

### 3. **Infrastructure**

- Dosya işlemleri (`ShapeFileReader`)
- JSON ayrıştırma (`JsonShapeParser`)
- XML gibi yeni formatların kolay eklenebilmesi için genişletilebilir

### 4. **Presentation**

- WPF UI (ViewModel’ler, XAML Görünümleri)
- Ana mantık için `ShapeViewerViewModel`
- Özel çizim ve seçim UI için `ShapeView`

---

## ▶️ Çalıştırma

1. Visual Studio’da projeyi derleyin (.NET Framework 4.8 hedeflenmiştir)
2. Uygulamayı çalıştırın (MainWindow.xaml)
3. "Dosya Yükle" butonuna tıklayarak JSON dosyası seçin
4. Fare ve klavye ile etkileşimde bulunun

---

## 📄 JSON Girdi Formatı

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
- Koordinatlar Kartezyen uzayda (Y ekseni yukarı doğru)
- Renkler ARGB formatında: Alfa; Kırmızı; Yeşil; Mavi
- Eğer `filled` true ise, şekil dolgu ve çerçeve ile çizilir

---

## 🖱️ Kullanıcı Etkileşimi

| İşlem               | Davranış             |
| ------------------- | -------------------- |
| Fare tekerleği      | Yakınlaştırma/uzaklaştırma  |
| Şekle sol tıklama  | Şekli seçme          |
| Ok tuşları         | Seçili şekli hareket ettirme |

---

## ⚙️ Genişletilebilirlik

| Uzantı                    | Nasıl uygulanır                                            |
| ------------------------- | ---------------------------------------------------------- |
| Yeni şekil (örn. Dikdörtgen) | `IShape` arayüzünü uygulayın ve parser'ı güncelleyin       |
| Yeni format (örn. XML)     | Yeni `IShapeParser` oluşturup `ShapeFileReader`'a enjekte edin |
| Yeni seçim davranışı       | `ShapeView` ve ViewModel mantığını genişletin               |

---

## 🧠 Kullanılan Tasarım Kalıpları

- **MVVM** – UI ve mantık arasında sorumluluk ayrımı
- **Komut Deseni** – özel `RelayCommand` aracılığıyla
- **Bağımlılık Enjeksiyonu** – `ShapeService`, `IShapeParser` soyutlamaları

---

## 📁 Proje Yapısı

- `/Domain` – iş mantığı & şekil sözleşmeleri
- `/Application` – servisler & arayüzler
- `/Infrastructure` – dosya girişi & ayrıştırma
- `/Presentation` – WPF Görünümleri, ViewModel’ler ve Kontroller

---

## 👨‍💻 Yazar

Bahadırhan Keleş
