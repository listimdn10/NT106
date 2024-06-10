using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MailKit.Net.Imap;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using MailKit;

namespace WindowsFormsApp1
{
    public partial class Bai2 : Form
    {
        public Bai2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            listView1.Columns.Clear();
            listView1.Items.Clear();
            listView1.Columns.Add("Email", 200);
            listView1.Columns.Add("From", 100);
            listView1.Columns.Add("Thời gian", 100);
            listView1.View = View.Details;

            using (var client = new ImapClient())
            {
                try
                {
                    // Connect to the IMAP server
                    client.Connect("imap.gmail.com", 993, true);
                    MessageBox.Show("Connected to IMAP server.");

                    // Authenticate with the server
                    client.Authenticate(textBox1.Text, textBox2.Text);
                    MessageBox.Show("Authenticated.");

                    // Access the inbox
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);
                    MessageBox.Show("Inbox opened. Total messages: " + inbox.Count);

                    // Process first 100 messages for testing
                    int messageCount = Math.Min(100, inbox.Count); // Process first 100 messages
                    for (int i = 0; i < messageCount; i++)
                    {
                        var message = inbox.GetMessage(i);
                        string subject = message.Subject ?? "(No Subject)";
                        string from = message.From.ToString() ?? "(No Sender)";
                        string date = message.Date.ToString();
                        ListViewItem item = new ListViewItem(subject);
                        item.SubItems.Add(from);
                        item.SubItems.Add(date);

                        listView1.Items.Add(item);
                    }

                    // Display total and recent message counts
                    textBox3.Text = inbox.Count.ToString();
                    textBox4.Text = inbox.Recent.ToString();

                    MessageBox.Show("Emails loaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Ensure the client disconnects
                    if (client.IsConnected)
                    {
                        client.Disconnect(true);
                    }
                    MessageBox.Show("Disconnected from IMAP server.");
                }
            }
        }


        private void Bai2_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;

        }
    }
}
