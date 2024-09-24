using Autocomp.Communication.Sniffer;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Autocomp.Communication
{
    public partial class Form1 : Form
    {
        private ListViewItem[] originalItems;
        private ListViewItem[] allItems;
        private bool isFilterActive = false;

        private MessageDataHandler messageDataHandler = new MessageDataHandler();
        private List<Sniffer.Message> allMessages = new List<Sniffer.Message>(); // Ca³a historia wiadomoœci
        private List<Sniffer.Message> filteredMessages = new List<Sniffer.Message>(); // Przefiltrowane wiadomoœci

        PlayerMessageProvider playerMessageProvider = new PlayerMessageProvider();

        bool autoscroll_enabled = false;

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true; // Umo¿liwia zaznaczenie ca³ego wiersza
            listView1.MultiSelect = false;  // Umo¿liwia zaznaczenie tylko jednego elementu na raz


            // Event handler dla zaznaczenia wiersza w ListView
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            FakeMessageProvider fakeMessageProvider = new FakeMessageProvider();

            var message = fakeMessageProvider.MessageReceived();

            // Dodaj wiadomoœæ do listy allMessages
            allMessages.Add(message); // <---- Dodaj tê liniê

            // Dodaj wiadomoœæ do ListView
            ListViewItem item = new ListViewItem(message.DateTime.ToString());
            item.SubItems.Add(message.Type.ToString()); // Typ wiadomoœci
            item.SubItems.Add(message.Content.ToString()); // Treœæ wiadomoœci
            listView1.Items.Add(item);




            // Przypisanie elementów listy do originalItems po dodaniu wiadomoœci
            originalItems = new ListViewItem[listView1.Items.Count];
            listView1.Items.CopyTo(originalItems, 0);
            allItems = new ListViewItem[originalItems.Length];
            originalItems.CopyTo(allItems, 0);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Pobierz zaznaczony element
                var selectedItem = listView1.SelectedItems[0];

                // Pobierz treœæ wiadomoœci (zak³adam, ¿e jest to trzeci SubItem)
                string messageContent = selectedItem.SubItems[2].Text;

                // Wyœwietl treœæ w TextBox
                textBox1.Text = messageContent;
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (originalItems == null || originalItems.Length == 0)
            {
                return;
            }

            string filterText = toolStripTextBox1.Text.ToLower();

            // Wyczyœæ listê przed filtrowaniem
            listView1.Items.Clear();

            // Dodawanie elementów, które spe³niaj¹ warunek filtru
            foreach (ListViewItem item in originalItems)
            {
                // SprawdŸ, czy item ma co najmniej 2 subitemy przed uzyskaniem dostêpu do SubItems[1]
                if (item.SubItems.Count > 1 && item.SubItems[1].Text.ToLower().Contains(filterText))
                {
                    listView1.Items.Add(item);
                }
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            string filterText = toolStripTextBox2.Text.ToLower();

            if (string.IsNullOrEmpty(filterText))
            {
                // Jeœli tekst jest pusty, przywróæ wszystkie elementy
                listView1.Items.Clear();
                listView1.Items.AddRange(allItems);

            }
            else
            {
                // Filtruj elementy na podstawie typu wiadomoœci
                listView1.Items.Clear();
                foreach (ListViewItem item in originalItems)
                {
                    // SprawdŸ, czy item ma co najmniej 2 subitemy przed uzyskaniem dostêpu do SubItems[1]
                    if (item.SubItems.Count > 1 && !item.SubItems[1].Text.ToLower().Contains(filterText))
                    {
                        listView1.Items.Add(item);
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Okno dialogowe z pytaniem, co zapisaæ
            DialogResult result = MessageBox.Show("Czy chcesz zapisaæ ca³¹ historiê (Tak) czy tylko przefiltrowane wiersze (Nie)?",
                                                  "Zapisz dane",
                                                  MessageBoxButtons.YesNoCancel);

            List<Sniffer.Message> messagesToSave = null;

            if (result == DialogResult.Yes)
            {
                // Zapisz ca³¹ historiê wiadomoœci
                if (allMessages.Count == 0)
                {
                    MessageBox.Show("Brak wiadomoœci do zapisania.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                messagesToSave = allMessages;
            }
            else if (result == DialogResult.No)
            {
                // SprawdŸ, czy s¹ przefiltrowane wiadomoœci
                if (filteredMessages.Count == 0)
                {
                    MessageBox.Show("Brak przefiltrowanych wiadomoœci do zapisania.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                messagesToSave = filteredMessages;
            }
            else
            {
                // Anulowano operacjê
                return;
            }

            // Wywo³anie okna dialogowego wyboru lokalizacji zapisu pliku
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Wybierz lokalizacjê do zapisu";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        // Wywo³anie metody Save z klasy MessageDataHandler
                        messageDataHandler.Save(filePath, messagesToSave);
                        MessageBox.Show("Wiadomoœci zapisane pomyœlnie!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"B³¹d podczas zapisywania wiadomoœci: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (isFilterActive)
            {
                // Przywróæ domyœlne t³o i logi
                toolStripButton1.BackColor = SystemColors.Window; // Przywraca domyœlne t³o
                listView1.Items.Clear();
                listView1.Items.AddRange(allItems);
                isFilterActive = false;
            }
            else
            {
                // Zmieñ t³o na niebieskie i ukryj istniej¹ce logi
                toolStripButton1.BackColor = Color.LightBlue;
                listView1.Items.Clear();
                isFilterActive = true;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            // Wywo³anie okna dialogowego do wyboru pliku
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Wybierz plik z wiadomoœciami";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // Próba wczytania wiadomoœci
                    if (messageDataHandler.TryLoad(filePath, out List<Sniffer.Message> loadedMessages))
                    {
                        // Wyczyszczenie listy przed dodaniem nowych wiadomoœci
                        listView1.Items.Clear();

                        allMessages = loadedMessages; // Zapisanie wczytanych wiadomoœci

                        foreach (var message in loadedMessages)
                        {
                            ListViewItem item = new ListViewItem(message.DateTime.ToString());
                            item.SubItems.Add(message.Type);
                            item.SubItems.Add(message.Content);
                            listView1.Items.Add(item);
                        }

                        // Zaktualizowanie originalItems i allItems
                        originalItems = new ListViewItem[listView1.Items.Count];
                        listView1.Items.CopyTo(originalItems, 0);
                        allItems = new ListViewItem[originalItems.Length];
                        originalItems.CopyTo(allItems, 0);

                        MessageBox.Show("Wiadomoœci zosta³y wczytane pomyœlnie!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Nie uda³o siê wczytaæ wiadomoœci.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pausetoolStripButton_Click(object sender, EventArgs e)
        {
            playerMessageProvider.Pause();
        }

        private void playtoolStripButton_Click(object sender, EventArgs e)
        {
            playerMessageProvider.Play();
        }

        private void resettoolStripButton_Click(object sender, EventArgs e)
        {
            playerMessageProvider.SetPlayPercentage(0);
        }

        private void stoptoolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            autoscroll_enabled = !autoscroll_enabled;

            if (autoscroll_enabled)
            {
                listView1.Items[listView1.Items.Count - 1].EnsureVisible();
                toolStripButton3.BackColor = Color.LightBlue;
            }

            else
            {
                toolStripButton3.BackColor = SystemColors.Control;
            }


        }
    }
}