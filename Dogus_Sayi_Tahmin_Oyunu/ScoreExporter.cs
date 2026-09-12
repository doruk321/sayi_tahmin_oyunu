using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SayiTahminOyunu
{
    // Proje Kuralları: Sınıf ve Nesne Kullanımı + Dosya İşlemleri
    // Bu sınıf, örnek bir dosyayı okuyup, yeni bir klasör açarak verileri yeni.sql dosyasına yazdırır.
    public class ScoreExporter
    {
        // Sınıf özellikleri (Properties)
        public string ExportDirectoryName { get; set; }
        public string ExportFileName { get; set; }
        public string TemplateFilePath { get; set; }

        // Yapıcı metot (Constructor)
        public ScoreExporter()
        {
            ExportDirectoryName = "Exporters";
            ExportFileName = "yeni.sql";
            TemplateFilePath = "template.txt";
        }

        // Dosya okuma, klasör oluşturma ve dosya yazma işlemlerini gerçekleştiren metot
        public string ExportScoresToSql(List<ScoreEntry> scores)
        {
            try
            {
                // 1. ADIM: Dosya Okuma Örneği
                // Okunacak şablon/bilgilendirme dosyası yoksa önce örnek bir dosya oluşturuyoruz.
                if (!File.Exists(TemplateFilePath))
                {
                    File.WriteAllText(TemplateFilePath, "-- SAYI TAHMİN OYUNU SQL YEDEKLEME ŞABLONU --\n-- Tarih: " + DateTime.Now.ToString("dd.MM.yyyy") + "\n");
                }

                // Dosya okuma işlemi gerçekleştiriliyor (Gereksinim: Bir dosya okuma örneği yapmalısınız)
                string templateHeader = File.ReadAllText(TemplateFilePath);

                // 2. ADIM: Yeni Klasör Oluşturma (Gereksinim: Yeni bir klasör açarak...)
                string fullDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ExportDirectoryName);
                if (!Directory.Exists(fullDirectoryPath))
                {
                    Directory.CreateDirectory(fullDirectoryPath); // Klasör oluşturma işlemi
                }

                // 3. ADIM: Yeni Dosyaya Yazdırma (Gereksinim: ...bilgileri yeni bir dosyaya (yeni.sql) yazdırmalısınız)
                string fullFilePath = Path.Combine(fullDirectoryPath, ExportFileName);
                StringBuilder sqlBuilder = new StringBuilder();

                // Okunan şablon başlığını SQL dosyasına ekliyoruz (Dosya okumadan alınan veri)
                sqlBuilder.AppendLine(templateHeader);
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("-- VERİTABANI SKOR VERİLERİ AKTARIM SQL KODLARI --");

                // Koleksiyon yapısındaki skorları gezinerek SQL INSERT ifadeleri hazırlıyoruz
                foreach (var score in scores)
                {
                    string isWonBit = score.IsWon ? "1" : "0";
                    string insertStatement = $"INSERT INTO Scores (Username, TargetNumber, Attempts, IsWon, PlayDate) VALUES ('{score.Username.Replace("'", "''")}', {score.TargetNumber}, {score.Attempts}, {isWonBit}, '{score.PlayDate}');";
                    sqlBuilder.AppendLine(insertStatement);
                }

                // Hazırlanan SQL komutlarını yeni.sql dosyasına yazıyoruz
                File.WriteAllText(fullFilePath, sqlBuilder.ToString(), Encoding.UTF8);

                // İşlem başarılı sonucunu döndürüyoruz
                return $"Başarılı! Dosya okundu, '{ExportDirectoryName}' klasörü oluşturuldu ve veriler '{ExportFileName}' dosyasına yazıldı.\nDosya Yolu: {fullFilePath}";
            }
            catch (Exception ex)
            {
                return "HATA: Dosya işlemleri sırasında bir hata oluştu: " + ex.Message;
            }
        }
    }
}
