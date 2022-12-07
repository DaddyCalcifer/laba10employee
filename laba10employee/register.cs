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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
            if(System.IO.File.Exists(@"\lab10\facul.inf")==false)
                System.IO.File.WriteAllLines(@"\lab10\facul.inf",new string[]{"ФИСИС","ФЭМИТ","ФИТКБ","ФМАТ","ФГЕО" });
            if (System.IO.File.Exists(@"\lab10\jobs.inf") == false)
                System.IO.File.WriteAllLines(@"\lab10\jobs.inf", new string[] { "Full master", "fucking slave", "boy next door", "Driver", "General" });
            facultyBox.Items.AddRange(System.IO.File.ReadAllLines(@"\lab10\facul.inf"));
            worker_box.Items.AddRange(System.IO.File.ReadAllLines(@"\lab10\jobs.inf"));
        }

        private void splitter2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void login_box_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.Text = txt.Text.Replace(" ", "");
            txt.Text = txt.Text.Replace(".", "");
            txt.Text = txt.Text.Replace(",", "");
            txt.Text = txt.Text.Replace("-", "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(login_box.Text.Trim() != "" && System.IO.File.Exists(@"\lab10\users\" + login_box.Text.Trim() + ".acc")==false)
            {
                if(pass_box.Text.Trim() != "" && re_pass_box.Text.Trim() != "" && name_box.Text.Trim() != ""
                    && surname_box.Text.Trim() != "" && patronim_box.Text.Trim() != "" && worker_box.Text.Trim() != ""
                    && facultyBox.SelectedIndex >= 0 && GenderBox.SelectedIndex >= 0 && birth_box.Value < System.DateTime.Now)
                {
                    if(pass_box.Text.Trim() == re_pass_box.Text.Trim() && pass_box.Text.Trim().Length >= 8)
                    {
                        var NewUser = new Employee(login_box.Text.Trim(),pass_box.Text.Trim(),name_box.Text.Trim(),surname_box.Text.Trim(),
                            patronim_box.Text.ToString(),(short)GenderBox.SelectedIndex,(short)facultyBox.SelectedIndex,birth_box.Value,(short)worker_box.SelectedIndex);
                        var path = @"\lab10\users\" + login_box.Text.Trim() + ".acc";
                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate)) //bin
                        {
                            formatter.Serialize(fs, NewUser);
                            fs.Close();
                        }
                        MessageBox.Show("Пользователь успешно создан!", "Успех!");
                        this.Close();
                    }
                    else { MessageBox.Show("Ошибка при вводе пароля: должен содержать не менее 8 символов, либо пароли не совпадают."); }
                }else { MessageBox.Show("Все поля должны быть заполены корректно!", "Ошибка!"); }
            }else { MessageBox.Show("Такой пользователь уже существует!", "Ошибка!"); }
        }
    }
}
