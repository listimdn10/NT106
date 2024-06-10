using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
//using MailKit.Net.Smtp;
//using MimeKit;
using System.Net.Mail;
using System.Xml.Linq;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Bai3 : Form
    {
        Attachment attach = null;
        public Bai3()
        {
            InitializeComponent();
        }

        void SendMail(string from, string to, string subject, string message, Attachment file=null)
        {
            using (MailMessage mailMessage = new MailMessage(from, to, subject, message))
            {
                if (file != null)
                {
                    mailMessage.Attachments.Add(file);
                }

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(textBox1.Text, textBox3.Text); // textBox1: Email, textBox3: App Password

                    try
                    {
                        smtpClient.Send(mailMessage);
                        MessageBox.Show("Successfully sent mail to " + to);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to send email: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            attach = null;
            if (!string.IsNullOrEmpty(textBox5.Text) && File.Exists(textBox5.Text))
            {
                try
                {
                    attach = new Attachment(textBox5.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error attaching file: " + ex.Message);
                    return;
                }
            }
            SendMail(textBox1.Text, textBox2.Text, textBox4.Text, richTextBox1.Text, attach);
        }

        private void Bai3_Load(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                textBox5.Text = filePath;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
