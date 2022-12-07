using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba10employee
{
    public partial class base_form : Form
    {
        public base_form(Employee user)
        {
            InitializeComponent();
            label1.Text = user.AuthCode;
        }

        private void base_form_Load(object sender, EventArgs e)
        {

        }

        private void base_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var path = "";
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить ключ авторизации как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Ключ авторизации  (*.CHKEY)|*.chkey";

            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                path = savedialog.FileName;
            }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate)) //bin
            {
                formatter.Serialize(fs, label1.Text);
                fs.Close();
            }
        }
    }
}
