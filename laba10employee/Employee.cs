using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace laba10employee
{
    public enum gender { Male, Female }
    [Serializable]
    public class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronim { get; set; }
        public gender Gender { get; set; }
        public short FacultyID { get; set; }
        public short Worker { get; set; }
        public DateTime Birth { get; }
        public string Password { get; set; }
        public string Login { get; }
        public string AuthCode { get; }
        public Employee(string Log, string pass, string name, string lastname, string patronim, short gendr, short facul, DateTime brth, short work)
        {
            Name = name; 
            Surname = lastname;
            Patronim = patronim;
            Login = Log;
            Password = pass;
            if (gendr == 0) Gender = gender.Male;
            if (gendr == 1) Gender = gender.Female;
            FacultyID = facul;
            Worker = work;
            Random rnd = new Random();
            AuthCode = rnd.Next(1000, 9999).ToString() + "-" + rnd.Next(1000, 9999).ToString() + "-" + rnd.Next(1000, 9999).ToString()
                + "-" + rnd.Next(1000, 9999).ToString();
        }
        public Employee() { }
    }
    public class AuthGuard : Employee
    {
        public void Log_In(string Login_, string Password)
        {
            string path = (@"\lab10\users\" + Login_ + ".acc");
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var user = new Employee();
                using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate)) //bin
                {
                    try { user = formatter.Deserialize(fs) as Employee; }
                    catch (Exception ex) { }
                    fs.Close();
                }
                if (Password == user.Password)
                {
                    base_form form = new base_form(user);
                    form.Show();
                }
                else throw new Exception("Неверный логин или пароль!");
            }
            else throw new Exception("Неверный логин или пароль!");
        }
    }
}
