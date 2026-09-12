namespace SayiTahminOyunu;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Uygulama ayarlarını başlat
        ApplicationConfiguration.Initialize();
        
        // Başlangıç formu olarak LoginForm'u çalıştırıyoruz (Kurallara uygun başlangıç)
        Application.Run(new LoginForm());
    }    
}