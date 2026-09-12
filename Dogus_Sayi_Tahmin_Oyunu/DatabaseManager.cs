using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace SayiTahminOyunu
{
    // Proje Kuralları: Sınıf, Nesne ve Metot Kullanımı
    // Bu sınıf, SQLite veritabanı işlemlerini (CRUD) yöneten ve veri koleksiyonları dönen metotları barındırır.
    public class DatabaseManager
    {
        // Bağlantı dizesi (Connection String) - yerel klasörde skorlar.db dosyası
        private const string ConnectionString = "Data Source=skorlar.db";

        // Veritabanını ve gerekli tabloları başlatan metot
        public static void InitializeDatabase()
        {
            // Veritabanı bağlantısı açılıyor
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                // Tablo oluşturma sorgusu (Eğer yoksa oluşturur)
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Scores (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL,
                        TargetNumber INTEGER NOT NULL,
                        Attempts INTEGER NOT NULL,
                        IsWon INTEGER NOT NULL,
                        PlayDate TEXT NOT NULL
                    );";

                // Sorguyu çalıştıracak komut oluşturuluyor
                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery(); // Sorgu veritabanında çalıştırılıyor
                }
            }
        }

        // Tüm skorları getiren ve Koleksiyon (List) dönen metot (Sıralama parametresi destekler)
        public static List<ScoreEntry> GetAllScores(string orderBy = "Id DESC")
        {
            // Skorları tutacağımız C# Koleksiyon (Collection) yapısı olan List tanımlanıyor
            List<ScoreEntry> scores = new List<ScoreEntry>();

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                // Parametrik sıralama ile veritabanından veri seçme sorgusu
                string selectQuery = $"SELECT Id, Username, TargetNumber, Attempts, IsWon, PlayDate FROM Scores ORDER BY {orderBy}";

                using (var command = new SqliteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        // Satır satır verileri oku ve nesneye (Object) dönüştürerek listeye ekle
                        while (reader.Read())
                        {
                            ScoreEntry score = new ScoreEntry
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                TargetNumber = reader.GetInt32(2),
                                Attempts = reader.GetInt32(3),
                                IsWon = reader.GetInt32(4) == 1,
                                PlayDate = reader.GetString(5)
                            };

                            scores.Add(score); // Nesneyi koleksiyona ekleme
                        }
                    }
                }
            }

            return scores; // Koleksiyonu geri döndür
        }

        // Veritabanına yeni skor EKLEME (Insert) metodu
        public static void AddScore(ScoreEntry score)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                // SQL Enjeksiyonuna (SQL Injection) karşı parametrik insert sorgusu
                string insertQuery = @"
                    INSERT INTO Scores (Username, TargetNumber, Attempts, IsWon, PlayDate)
                    VALUES ($username, $targetNumber, $attempts, $isWon, $playDate);";

                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    // Parametrelerin atanması
                    command.Parameters.AddWithValue("$username", score.Username);
                    command.Parameters.AddWithValue("$targetNumber", score.TargetNumber);
                    command.Parameters.AddWithValue("$attempts", score.Attempts);
                    command.Parameters.AddWithValue("$isWon", score.IsWon ? 1 : 0);
                    command.Parameters.AddWithValue("$playDate", score.PlayDate);

                    command.ExecuteNonQuery(); // Sorgunun çalıştırılması
                }
            }
        }

        // Veritabanından skor SİLME (Delete) metodu
        public static void DeleteScore(int id)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                // ID değerine göre silme sorgusu
                string deleteQuery = "DELETE FROM Scores WHERE Id = $id;";

                using (var command = new SqliteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("$id", id);
                    command.ExecuteNonQuery(); // Sorgunun çalıştırılması
                }
            }
        }

        // Veritabanında skor GÜNCELLEME (Update) metodu
        public static void UpdateScore(ScoreEntry score)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                // ID değerine göre veri güncelleme sorgusu
                string updateQuery = @"
                    UPDATE Scores 
                    SET Username = $username, 
                        TargetNumber = $targetNumber, 
                        Attempts = $attempts, 
                        IsWon = $isWon, 
                        PlayDate = $playDate 
                    WHERE Id = $id;";

                using (var command = new SqliteCommand(updateQuery, connection))
                {
                    // Parametrelerin güncellenmesi
                    command.Parameters.AddWithValue("$username", score.Username);
                    command.Parameters.AddWithValue("$targetNumber", score.TargetNumber);
                    command.Parameters.AddWithValue("$attempts", score.Attempts);
                    command.Parameters.AddWithValue("$isWon", score.IsWon ? 1 : 0);
                    command.Parameters.AddWithValue("$playDate", score.PlayDate);
                    command.Parameters.AddWithValue("$id", score.Id);

                    command.ExecuteNonQuery(); // Sorgunun çalıştırılması
                }
            }
        }
    }
}
