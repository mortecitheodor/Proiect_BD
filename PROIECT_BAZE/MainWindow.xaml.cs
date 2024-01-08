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
using PROIECT_BAZE.View;

namespace PROIECT_BAZE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string porecla;
        public MainWindow(string nickname)
        {
            porecla=nickname;
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
                        // Open Window2
                        //Window2 window2 = new Window2();
                        //window2.Show();
                        break;
                    case 2:
                        // Open Window3
                        //Window3 window3 = new Window3();
                        //window3.Show();
                        break;
                    case 3:
                        // Open Window4
                        //Window4 window4 = new Window4();
                        //window4.Show();
                        break;
                    case 5:
                        // Update RichTextBox content for Item 2
                        richTextBox.Document.Blocks.Clear();
                        richTextBox.Document.Blocks.Add(new Paragraph(new Run("AICI TREBUIE SA INTRODUCI EVENTS DIN BAZA DE DATE")));
                        break;
                    
                    case 6:
                        richTextBox.Document.Blocks.Clear();

                        using (var context = new Hotel_ManagementDataContext())
                        {
                            var recenzii = context.Recenziis.ToList();
                            foreach (var recenzie in recenzii)
                            {
                                string content = $"Nota: {recenzie.Nota}\nDescriere: {recenzie.Descriere_Text}\n\n";
                                richTextBox.AppendText(content);
                            }
                        }
                        break;

                    case 7:
                        // Update RichTextBox content for Item 2
                        richTextBox.Document.Blocks.Clear();
                        richTextBox.Document.Blocks.Add(new Paragraph(new Run("AICI TREBUIE SA INTRODUCI CONTACTUL HOTELULUI")));
                        break;
                }
            }
        }
    }
}
