using Autocomp.Communication.Sniffer;
using System.Windows.Forms;

namespace Autocomp.Communication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FakeMessageProvider fakeMessageProvider = new FakeMessageProvider();
            //generuje 5 losowych wiadomoœci
            for (int i = 0; i < 5; i++){
                var message = fakeMessageProvider.MessageReceived();
          
                ListViewItem item = new ListViewItem(message.DateTime.ToString());
                item.SubItems.Add(message.Type.ToString());
                item.SubItems.Add(message.Content.ToString());
                listView1.Items.Add(item);
                
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}