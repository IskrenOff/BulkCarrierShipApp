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
    private int sectionCount = 2;
    public MainWindow()
    {
        InitializeComponent();
    }
    //add section to the ship
    private void AddSection (object sender, RoutedEventArgs e)
    {
        sectionCount++;
        Grid newSection = new Grid
        {
            Margin = new Thickness(0),
            Height = 100,
            Width = 100
        };
        newSection.Children.Add(new Image
        {
            Source = new BitmapImage(new Uri("/img/mid.jpg", UriKind.Relative)),
            Height = 100,
            Width = 100
        });
        newSection.Children.Add(new TextBlock
        {
          Text = sectionCount.ToString(),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            RenderTransform = new RotateTransform(90),
            RenderTransformOrigin = new Point(0.5, 0.5),
            FontSize = 16,
            Foreground = Brushes.Black
        });
        ShipPanel.Children.Insert(1, newSection);
    }
    //remove section from the ship
    private void RemoveSection(object sender, RoutedEventArgs e)
    {
        if (ShipPanel.Children.Count > 3)
        {
            ShipPanel.Children.RemoveAt(1);
            sectionCount--;
        }
    }

    private void PrintShipLayout(object sender, RoutedEventArgs e) 
    {
        StackPanel printPanel = new StackPanel { Orientation = Orientation.Horizontal };
        foreach (UIElement element in ShipPanel.Children)
        {
            if (element is Image)
            {
                printPanel.Children.Add(new Image
                {
                    Source = ((Image)element).Source,
                    Margin = ((Image)element).Margin,
                    Height = ((Image)element).Height,
                    Width = ((Image)element).Width

                });
            }
        }
        //print the ship layout
        PrintDialog printDialog = new PrintDialog();
        if (printDialog.ShowDialog()==true)
        {
            printDialog.PrintVisual(printPanel, "Ship Layout");
        }
    }

    private void UserInputTextBox(object sender, TextChangedEventArgs e)
    {
        string newText = (sender as TextBox).Text;
    }

    private void PrintPreviewShipLayout(object sender, RoutedEventArgs e)
    {
        StackPanel printPanel = new StackPanel { Orientation = Orientation.Horizontal };
        foreach (UIElement element in ShipPanel.Children)
        {
            if (element is Image)
            {
                printPanel.Children.Add(new Image
                {
                    Source = ((Image)element).Source,
                    Margin = ((Image)element).Margin,
                    Height = ((Image)element).Height,
                    Width = ((Image)element).Width
                });
            }
        }

        PrintDialog printDialog = new PrintDialog();
        if (printDialog.ShowDialog() == true)
        {
            FlowDocument doc = new FlowDocument(new Paragraph(new InlineUIContainer(printPanel)));
            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;
            printDialog.PrintDocument(idpSource.DocumentPaginator, "Ship Layout Preview");
        }
    }
    
    private void UserInputText_GotFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if (textBox.Text == "Enter the ship name here")
        {
            textBox.Text = "";
            textBox.Foreground = Brushes.Black;
        }
    }
    private void UserInputText_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "Enter the ship name here";
            textBox.Foreground = Brushes.Black;
        }
    }
}