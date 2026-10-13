# 🏢 Kurumsal Envanter ve Zimmet Yönetim Portalı (ZimmetApp)

![.NET 9.0](https://img.shields.io/badge/.NET-9.0-purple?style=flat-square&logo=dotnet)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-blue?style=flat-square)
![Database](https://img.shields.io/badge/Database-SQLite%20%2F%20EF%20Core-green?style=flat-square)
![Security](https://img.shields.io/badge/Security-JWT%20%2B%20SHA--256-red?style=flat-square)

Kurumsal firmaların ve kamu kurumlarının bilişim donanımlarını, yazılım lisanslarını ve personel zimmet süreçlerini uçtan uca yöneten modern ve güvenli varlık yönetim platformu.

---

## 🚀 Öne Çıkan Kurumsal Yetenekler

- **🏛️ Rol Tabanlı Yetkilendirme (RBAC):** 
  - **Sistem Yöneticisi (Admin):** Tüm kurum envanterini izleme, yeni cihaz ekleme, personel tanımlama, zimmetleme, iade alma ve ilişik kesme.
  - **Personel (Standart Kullanıcı):** Yalnızca üzerine zimmetli donanım/yazılımları görüntüleme ve tutanak dökümü alma.
- **📄 Resmi Zimmet Teslim-Tesellüm Tutanağı (A4 PDF):** Kurumsal standartlarda yasal taahhüt metni, teslim eden / alan ıslak imza alanları ve **dinamik doğrulama QR kodu** içeren resmi belge çıktısı.
- **🚪 Off-boarding (İlişik Kesme Modülü):** Kurumdan ayrılan personelin üzerindeki tüm aktif zimmetleri tek tıkla depoya iade alma ve ilişiğini kapatma.
- **⏱️ Cihaz Yaşam Döngüsü ve Denetim İzi (Audit Trail Timeline):** Cihazın kuruma girişinden itibaren kimlere teslim edildiğini ve ne zaman iade edildiğini gösteren kronolojik hareket tarihçesi.
- **📊 Excel / CSV Raporlama:** Envanter listesinin UTF-8 BOM destekli resmi tablo dökümü olarak dışa aktarılması.
- **🔐 Kurumsal Güvenlik:** Parolaların SHA-256 + Salt ile kriptolanarak saklanması ve JWT Bearer Token ile korunan RESTful API uçları.

---

## 🏗️ Mimari Yapı (Clean Architecture)