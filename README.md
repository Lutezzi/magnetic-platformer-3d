# 🧲 Magnetic Platformer 3D

Unity ile geliştirme yapmayı öğrenmek için başlamış olduğum bir oyun projesi. Bu proje, geleneksel zıplama mekaniklerini **manyetik çekim ve itme** fiziğiyle birleştirerek benzersiz bir oynanış sunar.

## 🚀 Öne Çıkan Mekanikler

* **Kutup Değiştirme:** Oyuncu `Q` ve `E` tuşlarını kullanarak kendi manyetik kutbunu (Pozitif/Negatif) değiştirebilir.
* **Fizik Tabanlı Etkileşim:** Aynı kutuplar birbirini iterken, zıt kutuplar birbirini çeker. Kuvvet hesabı mesafe ile ters orantılıdır:
  $$F = \frac{\text{Güç}}{\text{Mesafe}}$$
* **Slingshot (Sapan) Etkisi:** Çekim kuvveti altındayken doğru zamanlama ile kutup değiştirerek kazanılan ivmeyi koruma ve ileri fırlama mekaniği.
* **Dinamik Görsel Geri Bildirim:** `Line Renderer` ve materyal değişimleri ile aktif manyetik bağların görselleştirilmesi.

## 🛠️ Teknik Detaylar

* **Motor:** Unity 6.3 LTS
* **Dil:** C#
* **Giriş Sistemi:** Legacy & New Input System (Hybrid)
* **Fizik:** Rigidbody tabanlı kuvvet hesaplamaları

## 🎮 Nasıl Oynanır?

1. **WASD:** Hareket
2. **Left Shift:** Zıplama
3. **Q:** Pozitif Kutup (Kırmızı)
4. **E:** Negatif Kutup (Mavi)
5. **Space:** Nötr Durum / Manyetik Bağı Koparma