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
using System.Runtime.Serialization.Formatters.Binary;

namespace laba10employee
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (System.IO.Directory.Exists(@"\lab10\") == false)
                System.IO.Directory.CreateDirectory(@"\lab10\");
            if (System.IO.Directory.Exists(@"\lab10\users\") == false)
                System.IO.Directory.CreateDirectory(@"\lab10\users\");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthGuard auth = new AuthGuard();
            try
            {
                auth.Log_In(login_box.Text.Trim(), pass_box.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка входа!");
                pass_box.Clear();
            }
        }

        private void showPass_CheckedChanged(object sender, EventArgs e)
        {
            if(showPass.Checked == true)
            {
                pass_box.PasswordChar = '\0';
            }
            else
            {
                pass_box.PasswordChar = '*';
            }
        }

        private void register_butt_Click(object sender, EventArgs e)
        {
            register reg = new register();
            this.Hide();
            reg.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            var logkey = "";
            var path = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выбрать ключ авторизации...";
            ofd.CheckPathExists = true;
            ofd.Filter = "Ключ авторизации  (*.CHKEY)|*.chkey";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
            }
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate)) //bin
            {
                try { logkey = formatter.Deserialize(fs) as string; }
                catch { }
                fs.Close();
            }
            var accs = Directory.GetFiles(@"\lab10\users\");
            foreach (var accsFile in accs)
            {
                var user = new Employee();
                using (System.IO.FileStream fs = new System.IO.FileStream(accsFile, System.IO.FileMode.OpenOrCreate)) //bin
                {
                    try { user = formatter.Deserialize(fs) as Employee; }
                    catch { }
                    fs.Close();
                }
                if (logkey == user.AuthCode)
                {
                    base_form form = new base_form(user);
                    form.Show();
                    break;
                }
            }
        }
    }
}
