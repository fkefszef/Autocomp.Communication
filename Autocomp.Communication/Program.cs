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
        Sniffer.Message MessageReceived();
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
        public Sniffer.Message MessageReceived()
        {
            GenerateRandomMessage();
            return new Sniffer.Message(generated_date, generated_type, generated_content);
        }

        // Metoda losowo wybieraj¹ca treœæ i typ, która potem ustawia wygenerowane zmienne
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
            MessageReceived();
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
        private bool isPaused = false;
        private DateTime paused_time;
        private double play_speed = 1.0;

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

        public PlayerMessageProvider()
        {
        }

        public Sniffer.Message MessageReceived()
        {
            Sniffer.Message message = messages[currIndex];
            //Autoinkrementowanie listy, a¿ do jej koñca
            currIndex = (currIndex + 1) % messages.Count;
            return message;
        }

        public TimeSpan RecordingLength()
        {
            return recordingLength;
        }

        //Asynchroniczna Funckja Play ¿eby u¿yæ await Task.Delay, która odpowiada za opóŸnienie
        public async Task Play(Form1 form)
{
    if (isPaused)
    {
        TimeSpan resume_offset = paused_time - startTime;
        startTime = DateTime.Now - resume_offset;
        isPaused = false;
    }
    else
    {
        startTime = DateTime.Now;
    }

    DateTime firstMessageTime = messages[0].DateTime;

    // Skopiuj listê wiadomoœci, aby unikn¹æ modyfikacji podczas iteracji
    var messagesCopy = new List<Sniffer.Message>(messages);

    foreach (var message in messagesCopy)
    {
        if (isPaused)
        {
            break; // Zatrzymaj odtwarzanie podczas pauzy
        }

        TimeSpan delay = message.DateTime - firstMessageTime;
        delay = TimeSpan.FromTicks((long)(delay.Ticks / play_speed));

        await Task.Delay(delay); // Zachowanie odstêpów czasowych

        lastMessageTime = DateTime.Now;

        // Oblicz procent odtwarzania
        double percentage = PlayedPercentageChanged();

        // Wywo³anie metody dodaj¹cej wiadomoœæ na ListView w g³ównym w¹tku UI
        form.AddMessageToListView(message);
    }
}


        public double PlayedPercentageChanged()
        {
            if (recordingLength.TotalSeconds <= 0)
            {
                return 0;
            }
            // Oblicza czas trwania od rozpoczêcia odtwarzania
            TimeSpan elapsedTime = DateTime.Now - startTime;
            // Ustala maksymalny czas, do którego mo¿emy obliczaæ procent odegranego nagrania
            TimeSpan endTime = messages[messages.Count - 1].DateTime - messages[0].DateTime;
            //Jezeli wiadomosc zostala skonczona to wyswietla ukonczenie a jak nie to procent ukonczenia
            TimeSpan effectiveElapsed = elapsedTime > endTime ? endTime : elapsedTime;
            // Oblicza procent na podstawie up³ywu czasu
            double percentage = (effectiveElapsed.TotalSeconds / recordingLength.TotalSeconds) * 100;
            return percentage;
        }

        public DateTime SetPlayPercentage(double percentage)
        {
            // Czas ostatniej wiadomosci
            DateTime endtime = messages[messages.Count].DateTime;

            // Obliczenie dlugosci
            TimeSpan duration = endtime - startTime;

            // Obliczenie czasu uzywajac podanego procentu
            TimeSpan timefromstart = TimeSpan.FromTicks((long)(duration.Ticks * (percentage / 100)));

            //Zwrocenie rzeczywistego czasu
            return startTime.Add(timefromstart);
        }

        public void Pause()
        {
            if (!isPaused)
            {
                isPaused = true;
                paused_time = DateTime.Now; // Zapisz czas kiedy odtwarzanie zostalo zatrzymane
            }
        }

        public void SetSpeed(double speed)
        {
            play_speed = speed;
        }
    }
}
