using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PROIECT_BAZE.View;
using static clasa_Eveniment;

public class clasa_Eveniment
{
    public string Nume { get; set; }
    public string Descriere { get; set; }
    public string Locatie { get; set; }
    public string Data_Inceput { get; set; }


    public class clasa_contact_hotel
    {
        public int IdContact { get; set; }
        public string NumeHotel { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public clasa_contact_hotel(int idContact, string numeHotel, string adresa, string telefon, string email)
        {
            IdContact = idContact;
            NumeHotel = numeHotel;
            Adresa = adresa;
            Telefon = telefon;
            Email = email;
        }
    }

}


namespace PROIECT_BAZE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string porecla;

        public ObservableCollection<clasa_Eveniment> GetAllEvenimente()
        {
            ObservableCollection<clasa_Eveniment> evenimenteList = new ObservableCollection<clasa_Eveniment>();

            using (Hotel_ManagementDataContext dbContext = new Hotel_ManagementDataContext())
            {
                var allEvenimente = dbContext.Evenimentes
                    .Select(e => new clasa_Eveniment
                    {
                        Nume = e.Nume,
                        Descriere = e.Descriere,
                        Locatie = e.Locatie,
                        Data_Inceput = e.Data_Inceput.ToString() // Converteste DateTime la string, modifica daca este necesar
                    })
                    .ToList();

                foreach (var eveniment in allEvenimente)
                {
                    evenimenteList.Add(new clasa_Eveniment
                    {
                        Nume = eveniment.Nume,
                        Descriere = eveniment.Descriere,
                        Locatie = eveniment.Locatie,
                        Data_Inceput = eveniment.Data_Inceput
                    });
                }
            }

            return evenimenteList;
        }

        public void LoadRestaurantData()
        {
            using (Hotel_ManagementDataContext dbContext = new Hotel_ManagementDataContext())
            {
                var restaurantData = dbContext.Restaurants.Select(r => new
                {
                    Nume = r.Nume,
                    Descriere = r.Descriere,
                    Pret = r.Pret
                }).ToList();

                RestaurantText.Document.Blocks.Clear();

                foreach (var item in restaurantData)
                {
                    RestaurantText.Document.Blocks.Add(new Paragraph(new Run($"Nume: {item.Nume}\nDescriere: {item.Descriere}\nPret: {item.Pret}\n\n")));
                }
            }
        }

        public void LoadCamereData()
        {
            using (Hotel_ManagementDataContext dbContext = new Hotel_ManagementDataContext())
            {
                var camereData = dbContext.Cameres.Select(c => new
                {
                    NrCamera = c.Nr_Camera,
                    TipCamera = c.Tip_Camera
                }).ToList();

                AccomodationText.Document.Blocks.Clear();

                foreach (var item in camereData)
                {
                    AccomodationText.Document.Blocks.Add(new Paragraph(new Run($"Nr Camera: {item.NrCamera}\nTip Camera: {item.TipCamera}\n\n")));
                }
            }
        }

        public void AddSpaSchedule()
        {
            SpaText.Document.Blocks.Clear();

            // Stil pentru titlu
            var titleStyle = new Style(typeof(Paragraph));
            titleStyle.Setters.Add(new Setter(Paragraph.FontWeightProperty, FontWeights.Bold));
            titleStyle.Setters.Add(new Setter(Paragraph.FontSizeProperty, 16));

            // Stil pentru element de listă
            var listItemStyle = new Style(typeof(Paragraph));
            listItemStyle.Setters.Add(new Setter(Paragraph.MarginProperty, new Thickness(20, 0, 0, 0)));
            listItemStyle.Setters.Add(new Setter(Paragraph.FontStyleProperty, FontStyles.Italic));

            string orarSpa = @"
    **Orarul SPA la Hotelul Nostru**

    ---

    **Zilele de Lucru: Luni - Vineri**
    - *07:00 - 08:00:* Yoga Matinală
    - *09:00 - 12:00:* Terapie cu Arome
    - *13:00 - 15:00:* Masaje Relaxante
    - *16:00 - 18:00:* Sauna și Jacuzzi

    **Sâmbătă și Duminică**
    - *08:00 - 09:00:* Pilates în Aer Liber
    - *10:00 - 13:00:* Îngrijirea Tenului
    - *14:00 - 16:00:* Terapie cu Pietre Calde
    - *17:00 - 19:00:* Sesiuni de Relaxare în Piscină

    **Notă:** Programarea este recomandată pentru toate serviciile SPA. Vă rugăm să contactați recepția pentru a stabili un program personalizat.
    ";

            string[] lines = orarSpa.Split('\n');
            foreach (string line in lines)
            {
                Paragraph paragraph = new Paragraph(new Run(line.Trim()));

                if (line.StartsWith("**"))
                {
                    // Aplică stilul pentru titlu
                    paragraph.Style = titleStyle;
                }
                else if (line.StartsWith("-"))
                {
                    // Aplică stilul pentru element de listă
                    paragraph.Style = listItemStyle;
                }

                SpaText.Document.Blocks.Add(paragraph);
            }
        }


        public MainWindow(string nickname)
        {
            porecla=nickname;
            InitializeComponent();
            LoadRestaurantData();
            LoadCamereData();
            AddSpaSchedule();
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScrollViewer.Visibility = Visibility.Collapsed;
            Restaurant.Visibility = Visibility.Collapsed;
            Accomodation.Visibility = Visibility.Collapsed;
            Spa.Visibility = Visibility.Collapsed;
            Events.Visibility = Visibility.Collapsed;
            Review.Visibility = Visibility.Collapsed;
            Contact.Visibility = Visibility.Collapsed;
            galery.Visibility = Visibility.Collapsed;


            if (listView.SelectedItem != null)
            {
                switch (listView.SelectedIndex)
                {
                    case 0:
                        // Open Window1
                        ProfileView window1 = new ProfileView(porecla);
                        window1.Show();
                        break;
                    case 1:
                        Restaurant.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        Accomodation.Visibility = Visibility.Visible;

                        break;
                    case 3:
                       Spa.Visibility = Visibility.Visible;
                        break;
                    case 4:
                        galery.Visibility = Visibility.Visible;
                        break;
                    case 5:
                        // Update RichTextBox content for Item 2
                        Events.Visibility = Visibility.Visible;
                        EventsText.Document.Blocks.Clear();

                        ObservableCollection<clasa_Eveniment> evenimenteList = GetAllEvenimente();

                        foreach (var eveniment in evenimenteList)
                        {
                            EventsText.Document.Blocks.Add(new Paragraph(new Run($"Nume: {eveniment.Nume}\nDescriere: {eveniment.Descriere}\nLocatie: {eveniment.Locatie}\nData Inceput: {eveniment.Data_Inceput}\n\n")));
                        }
                        break;
                    
                    case 6:
                        Review.Visibility = Visibility.Visible;
                        reviewText.Document.Blocks.Clear();

                        using (var context = new Hotel_ManagementDataContext())
                        {
                            var recenzii = context.Recenziis.ToList();
                            foreach (var recenzie in recenzii)
                            {
                                string content = $"Nota: {recenzie.Nota}\nDescriere: {recenzie.Descriere_Text}\n\n";
                                reviewText.AppendText(content);
                            }
                        }
                        break;

                    case 7:
                        // Update RichTextBox content for Item 2
                        Contact.Visibility = Visibility.Visible;
                        ContactText.Document.Blocks.Clear();

                        clasa_contact_hotel contactHotel = new clasa_contact_hotel(1, "M&M", "Strada Cristalelor nr. 10, Orasul Viena", "+123456789", "contactM&M@business.com");

                        ContactText.Document.Blocks.Add(new Paragraph(new Run($"Nume Hotel: {contactHotel.NumeHotel}\nAdresa: {contactHotel.Adresa}\nTelefon: {contactHotel.Telefon}\nEmail: {contactHotel.Email}")));
                        break;
                }
            }
        }
    }
}
