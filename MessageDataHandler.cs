using System;
using System.Collections.Generic;
using System.IO;

namespace Autocomp.Communication
{
    public class MessageDataHandler
    {
        // Metoda zapisująca listę wiadomości do pliku w formacie tekstowym
        public void Save(string filePath, List<Sniffer.Message> messages)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var message in messages)
                    {
                        // Zapisujemy datę, typ i treść na osobnych liniach
                        writer.WriteLine(message.DateTime.ToString("yyyy-MM-dd HH:mm:ss")); // Format daty bez problemów
                        writer.WriteLine(message.Type);
                        writer.WriteLine(message.Content);
                    }
                }
                Console.WriteLine("Messages saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving messages: {ex.Message}");
            }
        }

        // Metoda wczytująca listę wiadomości z pliku
        public bool TryLoad(string filePath, out List<Sniffer.Message> messages)
        {
            messages = new List<Sniffer.Message>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return false;
            }

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        // Odczytujemy trzy linie na każdą wiadomość: datę, typ i treść
                        string dateLine = reader.ReadLine();
                        string typeLine = reader.ReadLine();
                        string contentLine = reader.ReadLine();

                        // Konwertujemy string na datę
                        DateTime messageDate = DateTime.ParseExact(dateLine, "yyyy-MM-dd HH:mm:ss", null);

                        // Tworzymy wiadomość i dodajemy ją do listy
                        Sniffer.Message message = new Sniffer.Message(messageDate, typeLine, contentLine);
                        messages.Add(message);
                    }
                }
                Console.WriteLine("Messages loaded successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading messages: {ex.Message}");
                return false;
            }
        }
    }
}
