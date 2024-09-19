using Autocomp.Communication.Sniffer;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Autocomp.Communication
{
    public partial class Form1 : Form
    {
        private ListViewItem[] originalItems;
        private ListViewItem[] allItems;
        
        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true; // Umo�liwia zaznaczenie ca�ego wiersza
            listView1.MultiSelect = false;  // Umo�liwia zaznaczenie tylko jednego elementu na raz

            // Inicjalizacja TextBox
           
            // Inicjalizacja ListView
            // ... (tw�j kod inicjalizuj�cy ListView)

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

    
    }
}
