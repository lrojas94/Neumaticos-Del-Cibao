using Neumaticos_del_Cibao.Apps.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
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
        
        private void login(object sender, RoutedEventArgs e) 
        {
            btnLogin.Content = "Entrando...";
            var thread = new Thread(loginThread);
            thread.Start();
        }

        private void loginThread()
        {
            Dispatcher.Invoke(() => 
            {
                var user = usernameTextbox.Text.ToLower();
                var password = passwordBox.Password;

                if (user.Equals("") || password.Equals(""))
                {
                    MessageBox.Show("Ha dejado el campo de Usuario o Contraseña vacio.\nFavor completar los campos.", "Error!");
                    btnLogin.Content = "Entrar";
                }
                else
                {
                    var employee = database.Employees.FirstOrDefault(u => u.Username.ToLower() == user);

                    if (employee == null || !employee.Login(password))
                    {
                        MessageBox.Show("Usuario o Contraseña incorrecta", "Error!");
                        btnLogin.Content = "Entrar";
                    }
                    else
                    {
                        var home = new MainWindow(employee);
                        home.Show();
                        Close();
                    }

                }
            });
            
        }

        private void enterPressed(object sender, KeyEventArgs e)
        {
            if (passwordBox.Password != "" && usernameTextbox.Text != "" && e.Key == Key.Enter)
            {
                btnLogin.Content = "Entrando...";
                VisualStateManager.GoToState(btnLogin, "Pressed", true);
                btnLogin.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            }
        }
    }
}
