using Neumaticos_del_Cibao.Apps.Common;
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

    public partial class loginWindow :  Window
    {
        private Database.databaseEntities database = new Database.databaseEntities();
        public loginWindow()
        {
            InitializeComponent();
            
        }

        // It's consider that the password is saved in the
        // database as a result of this function.
        
        private void Login(object sender, RoutedEventArgs e) 
        {
            var user = usernameTextbox.Text.ToLower();
            var password = passwordBox.Password;
            var encrypt = new Encryption();
            
            if (user.Equals("") || password.Equals(""))
            {
                MessageBox.Show("Usuario o Contraseña vacios. Favor llenarlos");
            }
            else
            {
                var employee = database.Employees.Where(u => u.Username.ToLower() == (user.ToLower())).ToList();
                
                if (!employee.Any() || !encrypt.EncryptSHA256(password).Contains(employee[0].Password))
                {
                    MessageBox.Show("Usuario o Contraseña incorrecta");
                }
                else
                {
                    var home = new MainWindow();
                    home.Show();
                    this.Close();
                }
                
            }
        }
    }
}
