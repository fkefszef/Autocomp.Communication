using Autocomp.Communication.Sniffer;
using System.Timers;


namespace Autocomp.Communication
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }


    interface IMessageProvider
    {
        Sniffer.Message MessageReceived(object source, ElapsedEventArgs e);
    }


    public class FakeMessageProvider : IMessageProvider
    {
        // Obiekt random
        static Random r = new Random();

        // Lista z losowymi typami i trescia
        private List<string> types = new List<string> { "ServerLauncher", "StateMachineLauncher", "XmlConnectionBroker", "OperatorSwitcher" };
        private List<string> contents = new List<string> { "G4XUh35UQGfz", "uwCuQ4taUyZV", "?=KDLGyDBBxfmP", "D8QmDFsbrrR3", "5TyPT5MaLSKK", "?=9jE48B5HJTbZ" };

        private DateTime generated_date { get; set; }
        private string generated_type { get; set; }
        private string generated_content { get; set; }

        // Implementacja interfejsu
        public Sniffer.Message MessageReceived(object source, ElapsedEventArgs e)
        {
            RandomMessageGenerator();
            return new Sniffer.Message(generated_date, generated_type, generated_content);
        }
        // Metoda losowo wybierajaca tresc i typ, która potem ustawia wygenerowane zmienne
        public void RandomMessageGenerator()
        {
            int r_types = r.Next(types.Count);
            int r_contents = r.Next(contents.Count);

            generated_date = DateTime.Now;
            generated_type = types[r_types];
            generated_content = contents[r_contents];

        }
        // Konstruktor z timerem
        public FakeMessageProvider()
        {
            // Losowa liczba od 0 do 5 sekund
            int r_int = r.Next(0,5000);

            System.Timers.Timer r_timer = new System.Timers.Timer(r_int);
            //Nie da sie zrobic tego na timerze. Timery dzialaja tylko z metodami typu void
            //r_timer.Elapsed += MessageReceived;
            r_timer.AutoReset = true;
            r_timer.Enabled = true;

        }

    }

}