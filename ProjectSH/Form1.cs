using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace ProjectSH
{


    

    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        static string sKey;
        private void button1_Click(object sender, EventArgs e)
        {
            sKey = textBox2.Text;
            openFileDialog1.Filter = "txt file |*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;
                saveFileDialog1.Filter = "encrypted files|*.des";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string destination = saveFileDialog1.FileName;
                    EncryptFile(source, destination, sKey);
                }
            }

        }

        private void EncryptFile(string source, string destination, string sKey)
        {
            FileStream fsin = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream fsEnc = new FileStream(destination, FileMode.Create, FileAccess.ReadWrite);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cs = new CryptoStream(fsEnc, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsin.Length - 0];
                fsin.Read(bytearrayinput, 0, bytearrayinput.Length);
                cs.Write(bytearrayinput, 0, bytearrayinput.Length);
                cs.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка! Вероятнее всего вы использовали слишком длинны, или короткий мастер-ключ.");
            }
            fsin.Close();
            fsEnc.Close();
        }

        private void DecryptFile(string source, string destination, string sKey)
        {
            FileStream fsin = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream fsEnc = new FileStream(destination, FileMode.Create, FileAccess.ReadWrite);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            CryptoStream cs = new CryptoStream(fsEnc, desencrypt, CryptoStreamMode.Write);
            byte[] bytearrayinput = new byte[fsin.Length - 0];
            fsin.Read(bytearrayinput, 0, bytearrayinput.Length);
            cs.Write(bytearrayinput, 0, bytearrayinput.Length);
            cs.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка! Вероятнее всего вы использовали слишком длинны, или короткий мастер-ключ.");
            }
            fsin.Close();
            fsEnc.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sKey = textBox2.Text;
            openFileDialog1.Filter = "encrypted files|*.des";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;
                saveFileDialog1.Filter = "txt file |*.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string destination = saveFileDialog1.FileName;
                    DecryptFile(source, destination, sKey);
                }
            }

        }
        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
