using Autocomp.Communication.Sniffer;
using System.Drawing.Text;
using System.Timers;
using System.Windows.Forms;

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
        private static Random r = new Random();

        // Lista z losowymi typami i trescia
        private List<string> types = new List<string> { "ServerLauncher", "StateMachineLauncher", "XmlConnectionBroker", "OperatorSwitcher" };
        private List<string> contents = new List<string> { "G4XUh35UQGfz", "uwCuQ4taUyZV", "?=KDLGyDBBxfmP", "D8QmDFsbrrR3", "5TyPT5MaLSKK", "?=9jE48B5HJTbZ" };

        private DateTime generated_date { get; set; }
        private string generated_type { get; set; }
        private string generated_content { get; set; }

        // Implementacja interfejsu
        public Sniffer.Message MessageReceived(object source, ElapsedEventArgs e)
        {
            GenerateRandomMessage();
            return new Sniffer.Message(generated_date, generated_type, generated_content);
        }

        // Metoda losowo wybieraj�ca tre�� i typ, kt�ra potem ustawia wygenerowane zmienne
        private void GenerateRandomMessage()
        {
            int r_types = r.Next(types.Count);
            int r_contents = r.Next(contents.Count);

            generated_date = DateTime.Now;
            generated_type = types[r_types];
            generated_content = contents[r_contents];
        }

        //Generuje losowa wiadomosc
        //(musi byc typu void dlatego inna funkcja odpowiada za zwracanie)
        public void wywolywanie_randommsg(object sender, ElapsedEventArgs e)
        {
            GenerateRandomMessage();
        }

        // Konstruktor z timerem
        public FakeMessageProvider()
        {
            // Losowa liczba od 0 do 5 sekund
            int r_int = r.Next(0, 5000);
            System.Timers.Timer r_timer = new System.Timers.Timer(r_int);

            r_timer.Interval = r.Next(1000, 5000);

            r_timer.Elapsed += wywolywanie_randommsg;
            r_timer.AutoReset = true;
            r_timer.Enabled = true;
            r_timer.Start();
        }
    }


    public class PlayerMessageProvider : IMessageProvider
    {
        private List<Sniffer.Message> messages;
        private int currIndex;
        private TimeSpan recordingLength;
        private DateTime startTime;
        private DateTime lastMessageTime;

        public PlayerMessageProvider(List<Sniffer.Message> msg)
        {
            messages = msg;
            currIndex = 0;

            if (messages.Count > 0)
            {
                DateTime firstmsg = messages[0].DateTime;
                DateTime lastmsg = messages[messages.Count - 1].DateTime;
                recordingLength = lastmsg - firstmsg;
            }
        }

        public Sniffer.Message MessageReceived(object source, ElapsedEventArgs e)
        {
            Sniffer.Message message = messages[currIndex];
            //Autoinkrementowanie listy, a� do jej ko�ca
            currIndex = (currIndex + 1) % messages.Count;
            return message;
        }

        public TimeSpan RecordingLength()
        {
            return recordingLength;
        }

        //Asynchroniczna Funckja Play �eby u�y� await Task.Delay, kt�ra odpowiada za op�nienie
        public async Task Play()
        {
            // Pobierz czas pierwszej wiadomo�ci
            DateTime firstMessageTime = messages[0].DateTime;
            // Odtwarzanie wiadomo�ci
            foreach (var message in messages)
            {
                // Oblicz czas, kiedy nale�y wy�wietli� wiadomo��
                TimeSpan delay = message.DateTime - firstMessageTime;
                // Oczekiwanie przed wy�wietleniem wiadomo�ci
                await Task.Delay(delay);
                lastMessageTime = DateTime.Now;
                double percentage = PlayedPercentageChanged();
                // Wy�wietl wiadomo�� (mo�na tu doda� logik� do faktycznego wy�wietlania lub przetwarzania wiadomo�ci)
                //Kod
            }
        }
        
        public double PlayedPercentageChanged()
        {
            if (recordingLength.TotalSeconds <= 0)
            {
                return 0;
            }
            // Oblicza czas trwania od rozpocz�cia odtwarzania
            TimeSpan elapsedTime = DateTime.Now - startTime;
            // Ustala maksymalny czas, do kt�rego mo�emy oblicza� procent odegranego nagrania
            TimeSpan endTime = messages[messages.Count - 1].DateTime - messages[0].DateTime;
            //Jezeli wiadomosc zostala skonczona to wyswietla ukonczenie a jak nie to procent ukonczenia
            TimeSpan effectiveElapsed = elapsedTime > endTime ? endTime : elapsedTime;
            // Oblicza procent na podstawie up�ywu czasu
            double percentage = (effectiveElapsed.TotalSeconds / recordingLength.TotalSeconds) * 100;
            return percentage;
        }
    }
}
