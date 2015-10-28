using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Neumaticos_del_Cibao
{
    /// <summary>
    /// Interaction logic for loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        private Database.databaseEntities database = new Database.databaseEntities();
        public loginWindow()
        {
            InitializeComponent();

        }

        // It's consider that the password is saved in the
        // database as a result of this function.

        private String EncriptingPassWord(String password) 
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
 
            return hash;
        }

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            var user = texboxUsuario.Text.ToLower();
            var password = passwordBox;
            if (user.Equals("") || password.Equals(""))
            {
                MessageBox.Show("Usuario o Contraseña vacios. Favor llenarlos");
            }
            else
            {
                var employee = database.Employees.Where(u => u.Username.ToLower().Contains(user.ToLower())).ToList();

                Console.WriteLine(employee); 
                if (employee == null)
                {
                    MessageBox.Show("Usuario no registrado");
                }
                else if(employee.ElementAt(0).Password.CompareTo(EncriptingPassWord(password.ToString())) == 0)
                {
                    MessageBox.Show("Contraseña no es correcta");
                }
                else
                {
                    var w = new MainWindow();
                    w.Show();
                    this.Close();
                }
            }
        }
    }
}
