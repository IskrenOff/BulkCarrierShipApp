using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BulkCarrierShipApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void AddSection (object sender, RoutedEventArgs e)
    {
        Image newSection = new Image
        {
            Source = new BitmapImage(new Uri("img/mid.jpg", UriKind.Relative)),
            Margin = new Thickness(0),
            Height = 100,
            Width = 100
        };
        ShipPanel.Children.Insert(ShipPanel.Children.Count - 1, newSection);
    }

    private void RemoveSection(object sender, RoutedEventArgs e)
    {
        if (ShipPanel.Children.Count > 3)
        {
            ShipPanel.Children.RemoveAt(ShipPanel.Children.Count - 2);
        }
    }
}