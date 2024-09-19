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
        private List<Sniffer.Message> allMessages = new List<Sniffer.Message>(); // Ca�a historia wiadomo�ci
        private List<Sniffer.Message> filteredMessages = new List<Sniffer.Message>(); // Przefiltrowane wiadomo�ci


        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true; // Umo�liwia zaznaczenie ca�ego wiersza
            listView1.MultiSelect = false;  // Umo�liwia zaznaczenie tylko jednego elementu na raz


            // Event handler dla zaznaczenia wiersza w ListView
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            FakeMessageProvider fakeMessageProvider = new FakeMessageProvider();

            // Generuje 5 losowych wiadomo�ci
            for (int i = 0; i < 5; i++)
            {
                var message = fakeMessageProvider.MessageReceived();

                ListViewItem item = new ListViewItem(message.DateTime.ToString());
                item.SubItems.Add(message.Type.ToString()); // Typ wiadomo�ci
                item.SubItems.Add(message.Content.ToString()); // Tre�� wiadomo�ci
                listView1.Items.Add(item);
            }

            // Przypisanie element�w listy do originalItems po dodaniu wiadomo�ci
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

                // Pobierz tre�� wiadomo�ci (zak�adam, �e jest to trzeci SubItem)
                string messageContent = selectedItem.SubItems[2].Text;

                // Wy�wietl tre�� w TextBox
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

            // Wyczy�� list� przed filtrowaniem
            listView1.Items.Clear();

            // Dodawanie element�w, kt�re spe�niaj� warunek filtru
            foreach (ListViewItem item in originalItems)
            {
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
                // Je�li tekst jest pusty, przywr�� wszystkie elementy
                listView1.Items.Clear();
                listView1.Items.AddRange(allItems);
            }
            else
            {
                // Filtruj elementy na podstawie typu wiadomo�ci
                listView1.Items.Clear();
                foreach (ListViewItem item in originalItems)
                {
                    if (item.SubItems.Count > 1 && !item.SubItems[1].Text.ToLower().Contains(filterText))
                    {
                        listView1.Items.Add(item);
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Okno dialogowe z pytaniem, co zapisa�
            DialogResult result = MessageBox.Show("Czy chcesz zapisa� ca�� histori� (Tak) czy tylko przefiltrowane wiersze (Nie)?",
                                                  "Zapisz dane",
                                                  MessageBoxButtons.YesNoCancel);

            List<Sniffer.Message> messagesToSave = null;

            if (result == DialogResult.Yes)
            {
                messagesToSave = allMessages; // Zapisz ca�� histori�
            }
            else if (result == DialogResult.No)
            {
                messagesToSave = filteredMessages; // Zapisz tylko przefiltrowane wiersze
            }
            else
            {
                return; // Anulowano operacj�
            }

            // Wywo�anie okna dialogowego wyboru lokalizacji zapisu pliku
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Wybierz lokalizacj� do zapisu";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Wywo�anie metody Save z klasy MessageDataHandler
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        messageDataHandler.Save(filePath, messagesToSave);
                        MessageBox.Show("Wiadomo�ci zapisane pomy�lnie!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"B��d podczas zapisywania wiadomo�ci: {ex.Message}", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (isFilterActive)
            {
                // Przywr�� domy�lne t�o i logi
                listView1.BackColor = SystemColors.Window; // Przywraca domy�lne t�o
                listView1.Items.Clear();
                listView1.Items.AddRange(allItems);
                isFilterActive = false;
            }
            else
            {
                // Zmie� t�o na niebieskie i ukryj istniej�ce logi
                listView1.BackColor = Color.LightBlue;
                listView1.Items.Clear();
                isFilterActive = true;
            }
        }
    }
}