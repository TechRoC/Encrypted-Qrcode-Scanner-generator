using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static string Key = "Rohit[Dogs{Dik}]Rahul[Fish{Sap}]";
        private static string IV = "Akshay[Dogs{Ros}";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData data = qr.CreateQrCode(textBox1.Text, QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(data);
            pictureBox1.Image = code.GetGraphic(5);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = Encrypt(textBox2.Text);
        }

      

        private static string Encrypt(string text)
        {
            byte[] plaintextbytes = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Key);
            aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
            return Convert.ToBase64String(encrypted);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveImageCapture(pictureBox1.Image);
        }
        public static void SaveImageCapture(System.Drawing.Image image)
        {

            SaveFileDialog s = new SaveFileDialog();
            s.FileName = "QR_code";
            s.DefaultExt = ".jpg";
            s.Filter = "Image (.jpg)|*.jpg";
            if (s.ShowDialog() == DialogResult.OK)
            {
                // Save Image
                string filename = s.FileName;
                FileStream fstream = new FileStream(filename, FileMode.Create);
                image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                fstream.Close();

            }

        }
    }
}
