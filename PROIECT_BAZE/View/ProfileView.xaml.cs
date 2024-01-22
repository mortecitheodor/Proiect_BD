using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


public class clasa_rezervare
{
    public string BookingDate { get; set; }
    public string CheckIn { get; set; }
    public string CheckOut { get; set; }
    public string all_Inclusive { get; set; }
    public string Transaction { get; set; }
}

public class clasa_plata
{
    public string Date { get; set; }
    public string Total { get; set; }
    public string Type_Transaction { get; set; }
}

public class Recenzie
{
    public string Nota { get; set; }
    public string Descriere_Text { get; set; }
}








namespace PROIECT_BAZE.View
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Window
    {
        string porecla;

        public List<clasa_rezervare> GetRezervariForClient(string porecla)
        {
            List<clasa_rezervare> rezervariList = new List<clasa_rezervare>();

            using (Hotel_ManagementDataContext dbContext = new Hotel_ManagementDataContext())
            {
                int? idClient = dbContext.Clientis
                    .Where(c => c.Username == porecla)
                    .Select(c => c.ID_Client)
                    .FirstOrDefault();

                if (idClient.HasValue)
                {
                    var rezervariClient = dbContext.Rezervaris
                        .Where(r => r.ID_Client == idClient.Value)
                        .ToList();

                    foreach (var rezervare in rezervariClient)
                    {
                        rezervariList.Add(new clasa_rezervare
                        {
                            BookingDate = rezervare.Data_Rezervarii.ToString(), // Converteste DateTime la string, modifica daca este necesar
                            CheckIn = rezervare.Check_In.ToString(),
                            CheckOut = rezervare.Check_Out.ToString(),
                            all_Inclusive = rezervare.All_Inclusive,
                            Transaction = rezervare.Status_Plata
                        });
                    }
                }
            }

            return rezervariList;
        }

        public List<clasa_plata> GetPlatiForClient(string porecla)
        {
            List<clasa_plata> platiList = new List<clasa_plata>();

            using (Hotel_ManagementDataContext dbContext = new Hotel_ManagementDataContext())
            {
                int? idClient = dbContext.Clientis
                    .Where(c => c.Username == porecla)
                    .Select(c => c.ID_Client)
                    .FirstOrDefault();

                if (idClient.HasValue)
                {
                    var platiClient = dbContext.Platis
                        .Where(p => p.ID_Client == idClient.Value)
                        .ToList();

                    foreach (var plata in platiClient)
                    {
                        platiList.Add(new clasa_plata
                        {
                            Date = plata.Data_Tranzactiei.ToString(), // Converteste DateTime la string, modifica daca este necesar
                            Total = plata.Suma_Totala.ToString(), // Converteste la string, modifica daca este necesar
                            Type_Transaction = plata.Tipul_Tranzactiei
                        });
                    }
                }
            }

            return platiList;
        }

        public Recenzie GetRecenzieForClient(string porecla)
        {
            Recenzie recenzie = new Recenzie();

            using (Hotel_ManagementDataContext dbContext = new Hotel_ManagementDataContext())
            {
                int? idClient = dbContext.Clientis
                    .Where(c => c.Username == porecla)
                    .Select(c => c.ID_Client)
                    .FirstOrDefault();

                if (idClient.HasValue)
                {
                    var recenzieClient = dbContext.Recenziis
                        .Where(r => r.ID_Client == idClient.Value)
                        .FirstOrDefault();

                    if (recenzieClient != null)
                    {
                        recenzie.Nota = recenzieClient.Nota.ToString(); // Converteste la string, modifica daca este necesar
                        recenzie.Descriere_Text = recenzieClient.Descriere_Text;
                    }
                }
            }

            return recenzie;
        }







        public ProfileView(string nickname)
        {
            porecla = nickname;
            InitializeComponent();
            gridBookings.ItemsSource=GetRezervariForClient(nickname);
            gridReceipts.ItemsSource=GetPlatiForClient(nickname);

            Recenzie recenzieClient = GetRecenzieForClient(nickname);

textboxreview.Text = $"Nota: {recenzieClient.Nota}, Descriere: {recenzieClient.Descriere_Text}";

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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            string nume = txtNume.Text;
            string prenume=txtPrenume.Text;
            nume = nume + " " + prenume;
            string adresa = txtAdresa.Text;
            string date=dpDataNasterii.ToString();
        

            using (Hotel_ManagementDataContext context = new Hotel_ManagementDataContext())
            {
                
                // Find the client with the given nickname.
                Clienti clientToUpdate = context.Clientis.SingleOrDefault(c => c.Username == porecla);

                if (clientToUpdate != null)
                {
                    // Update the fields.
                    clientToUpdate.Nume = nume;
                    clientToUpdate.Adresa = adresa;

                    // Parse the date string and update the DataNastere field.
                    if (DateTime.TryParse(date, out DateTime newDate))
                    {
                        clientToUpdate.DataNastere = newDate;
                    }
                    else
                    {
                        MessageBox.Show("Toate câmpurile trebuie completate!", "Eroare");
                    }

                    // Save changes to the database.
                    MessageBox.Show("Datele au fost modificate!");
                    context.SubmitChanges();
                }
                
            }


        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            editFieldsPanel.Visibility = Visibility.Collapsed;
        }

        private void offer()
        {
            using (Hotel_ManagementDataContext dbContext = new Hotel_ManagementDataContext())
            {
                Clienti clientCautat = dbContext.Clientis.FirstOrDefault(client => client.Username == porecla);

                // Verificați ziua de naștere
                if (clientCautat != null && clientCautat.DataNastere.HasValue)
                {
                    DateTime dataNastere = clientCautat.DataNastere.Value;

                    // Verificați dacă ziua de naștere este în mai puțin de o lună
                    if (dataNastere.Month == DateTime.Now.Month && dataNastere.Day > DateTime.Now.Day)
                    {
                        // Afisati MessageBox
                        MessageBox.Show("Oferta aniversare! Cu ocazia zilei de nastere, beneficiati de 25% reducere! Cod de reducere: WR5HG");
                    }
                }
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editFieldsPanel.Visibility= Visibility.Collapsed;
            myBookingsFiled.Visibility = Visibility.Collapsed;
            receiptsStack.Visibility = Visibility.Collapsed;
            myReviews.Visibility = Visibility.Collapsed;

            if (listView.SelectedItem != null)
            {
                switch (listView.SelectedIndex)
                {
                    case 0:
                       
                        editFieldsPanel.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        myBookingsFiled.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        receiptsStack.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        offer();
                        break;
                    case 4:
                        myReviews.Visibility = Visibility.Visible;
                        break;
                    case 5:
                        LoginView loginView = new LoginView();
                        loginView.Show();
                        break;
                }
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var context = new Hotel_ManagementDataContext())
            {

                var client = context.Clientis.FirstOrDefault(c => c.Username == porecla);

                if (client != null)
                {
                    Nume_Client.Text = client.Nume;
                }
            }
        }

       private void ListViewItem_Selected(object sender, RoutedEventArgs e)
{
    using (var context = new Hotel_ManagementDataContext())
    {
        var clientId = (from client in context.Clientis
                        where client.Username == porecla
                        select client.ID_Client).FirstOrDefault();

        if (clientId != null)
        {
            var rezervare = (from rez in context.Rezervaris
                             where rez.ID_Client == clientId
                             select rez).FirstOrDefault();

                    if (rezervare != null)
                    {
                        // myBookingsFiled.Children.Clear();
                        //gridBookings.ItemsSource = rezervare;//AICI TRB SA FACI SELECT DIN BAZA DE DATE SI SA BAGI INTR O LISAT
                        //SI DUPA SA DAI COMANDA AIA DE MAI SUS

                        //MessageBox.Show($"Data rezervării: {rezervare.Data_Rezervarii}\n" +
                        //                $"Durata cazării: {rezervare.Durata_Cazarii}\n" +
                        //                $"Check-In: {rezervare.Check_In}\n" +
                        //                $"Check-Out: {rezervare.Check_Out}\n" +
                        //                $"All Inclusive: {rezervare.All_Inclusive}\n" +
                        //                $"Half Board: {rezervare.Half_Board}\n" +
                        //                $"Acces Spa: {rezervare.Acces_Spa}\n" +
                        //                $"Acces Piscină: {rezervare.Acces_Piscina}\n" +
                        //                $"Status Plată: {rezervare.Status_Plata}");

                    }
                   

                }
            }
}

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            using (var context = new Hotel_ManagementDataContext())
            {
                
            }
        }


        private void plata_Selected(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(porecla);
            main.Show();
        }

       
    }


}
