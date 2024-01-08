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
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Window
    {
        string porecla;
        public ProfileView(string nickname)
        {
            porecla = nickname;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Se salveaza datele in BAZA DE DATE
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            editFieldsPanel.Visibility = Visibility.Collapsed;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                switch (listView.SelectedIndex)
                {
                    case 0:
                        editFieldsPanel.Visibility = Visibility.Visible;
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
                        MessageBox.Show($"Data rezervării: {rezervare.Data_Rezervarii}\n" +
                                        $"Durata cazării: {rezervare.Durata_Cazarii}\n" +
                                        $"Check-In: {rezervare.Check_In}\n" +
                                        $"Check-Out: {rezervare.Check_Out}\n" +
                                        $"All Inclusive: {rezervare.All_Inclusive}\n" +
                                        $"Half Board: {rezervare.Half_Board}\n" +
                                        $"Acces Spa: {rezervare.Acces_Spa}\n" +
                                        $"Acces Piscină: {rezervare.Acces_Piscina}\n" +
                                        $"Status Plată: {rezervare.Status_Plata}");
                    }
                    else
                    {
                        MessageBox.Show("Nicio rezervare găsită!");
                    }

                }
            }
}

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            using (var context = new Hotel_ManagementDataContext())
            {
                var client = context.Clientis.FirstOrDefault(c => c.Username == porecla);
                if (client != null)
                {
                    var recenzieClient = context.Recenziis.FirstOrDefault(r => r.ID_Client == client.ID_Client);

                    if (recenzieClient != null)
                    {
                        // Afișăm detaliile recenziei într-un MessageBox
                        string message = $"Nota: {recenzieClient.Nota}\n" +
                                         $"Descriere: {recenzieClient.Descriere_Text}";
                        MessageBox.Show(message, "Detalii Recenzie", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // Afisăm un mesaj dacă nu există recenzii
                        MessageBox.Show("Nicio recenzie găsită pentru acest client!", "Detalii Recenzie", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }


        private void plata_Selected(object sender, RoutedEventArgs e)
        {
            using (var context = new Hotel_ManagementDataContext())
            {
                var client = context.Clientis.FirstOrDefault(c => c.Username == porecla);
                if (client != null)
                {
                    var plataClient = context.Platis.FirstOrDefault(p => p.ID_Client == client.ID_Client);

                    if (plataClient != null)
                    {
                        string message = $"Suma Totala: {plataClient.Suma_Totala}\n" +
                                         $"Data Tranzactiei: {plataClient.Data_Tranzactiei}\n" +
                                         $"Tipul Tranzactiei: {plataClient.Tipul_Tranzactiei}";
                        MessageBox.Show(message, "Detalii Plata", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Nicio plată găsită pentru acest client!", "Detalii Plata", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

    }


}
