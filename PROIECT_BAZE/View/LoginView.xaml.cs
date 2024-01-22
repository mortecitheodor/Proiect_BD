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
using System.Windows.Shapes;

namespace PROIECT_BAZE.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPass.Password;

            using (var context = new Hotel_ManagementDataContext())
            {
                // daca exista utilizatorul
                var user = (from c in context.Clientis
                            where c.Username == username && c.Parola == password
                            select c).FirstOrDefault();

                if (user != null)
                {
                    // daca credentialele sunt corecte, se intra in interfata principala a programului
                    MainWindow mainWindow = new MainWindow(username);
                    mainWindow.Left = Left;
                    mainWindow.Top = Top;
                    mainWindow.Width = Width;
                    mainWindow.Height = Height;
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    // daca credentialele nu sunt bune, se afiseaza mesaj de eroare
                    MessageBox.Show("Credențiale invalide. Te rog să încerci din nou.");
                }
            }
        }


        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CreateAccount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegisterView register = new RegisterView();
            register.Show();
        }

      
    }
}