using System;

namespace SayiTahminOyunu
{
    // Proje Kuralları: Sınıf ve Nesne Kullanımı
    // Bu sınıf, veritabanındaki her bir skor kaydını nesne yönelimli olarak temsil eder.
    public class ScoreEntry
    {
        // Kapsülleme (Encapsulation) kurallarına uygun özellik tanımları (Properties)
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public int TargetNumber { get; set; }
        public int Attempts { get; set; }
        public bool IsWon { get; set; }
        public string PlayDate { get; set; } = string.Empty;

        // Varsayılan Yapıcı Metot (Default Constructor)
        public ScoreEntry()
        {
            Username = "Misafir";
            PlayDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // Parametreli Yapıcı Metot (Parameterized Constructor)
        public ScoreEntry(int id, string username, int targetNumber, int attempts, bool isWon, string playDate)
        {
            Id = id;
            Username = username;
            TargetNumber = targetNumber;
            Attempts = attempts;
            IsWon = isWon;
            PlayDate = playDate;
        }
    }
}
