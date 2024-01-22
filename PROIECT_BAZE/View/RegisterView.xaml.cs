using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// 
    public partial class RegisterView : Window
    {
        public RegisterView()
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

        public string GetPassword(PasswordBox passwordBox)
        {
            string password = null;

            if (passwordBox.SecurePassword != null)
            {
                IntPtr valuePtr = IntPtr.Zero;

                try
                {
                    valuePtr = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(passwordBox.SecurePassword);
                    password = System.Runtime.InteropServices.Marshal.PtrToStringUni(valuePtr);
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                }
            }

            return password;
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username=txtUser.Text;
            string parola = GetPassword(txtPass);
            string nume=txtNume.Text;
            string email=txtMail.Text;
            string adresa=txtAdresa.Text;
            string data = datePicker.SelectedDate.HasValue
         ? datePicker.SelectedDate.Value.ToString("yyyy-MM-dd")
            : string.Empty;

            if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(adresa) || string.IsNullOrWhiteSpace(data) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username) ||string.IsNullOrWhiteSpace(parola))
            {
                MessageBox.Show("Toate câmpurile trebuie completate.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (Hotel_ManagementDataContext dbContext = new Hotel_ManagementDataContext())
            {
                if (dbContext.Clientis.Any(c => c.Username == username))
                {
                    MessageBox.Show("Numele de utilizator există deja. Alegeți altul.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            using (Hotel_ManagementDataContext dbContext = new Hotel_ManagementDataContext())
            {
                try
                {
                    // Crează un obiect de tip Clienti și setează proprietățile
                    PROIECT_BAZE.Clienti client = new PROIECT_BAZE.Clienti
                    {
                        Nume = nume,
                        Adresa = adresa,
                        DataNastere = DateTime.ParseExact(data, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Email = email,
                        Username = username,
                        Parola = parola,
                        Varsta = 24,
                        Telefon="0734546532"
                    };

                    // Adaugă obiectul în context și salvează modificările
                    dbContext.Clientis.InsertOnSubmit(client);
                    dbContext.SubmitChanges();

                    MessageBox.Show("Contul a fost creat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"A apărut o eroare: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            LoginView loginView = new LoginView();
            loginView.Show();
            //ASTA DE MAI SUS SA NU MODIFICI
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