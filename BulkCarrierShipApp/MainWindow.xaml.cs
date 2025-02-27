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
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
        if (ShipPanel.Children.Count == 0)
        {
            MessageBox.Show("There is nothing to print.", "Print Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        

        StackPanel printPanel = new StackPanel { Orientation = Orientation.Horizontal};
        foreach (UIElement element in ShipPanel.Children)
        {
            if (element is Grid grid)
            {
                Grid newGrid = new Grid();

                foreach (UIElement child in grid.Children)
                {
                    if (child is Image image)
                    {
                        newGrid.Children.Add(new Image {
                            Source = image.Source,
                            Margin = image.Margin,
                            Height = image.Height,
                            Width = image.Width
                        });
                    }
                    else if (child is TextBlock textBlock)
                    {
                        newGrid.Children.Add(new TextBlock
                        {
                            Text = textBlock.Text,
                            HorizontalAlignment = textBlock.HorizontalAlignment,
                            VerticalAlignment = textBlock.VerticalAlignment,
                            FontSize = textBlock.FontSize,
                            Foreground = textBlock.Foreground,
                            RenderTransform = textBlock.RenderTransform,
                            RenderTransformOrigin = textBlock.RenderTransformOrigin,
                            Margin = textBlock.Margin
                        });
                    }
                }
                printPanel.Children.Add(newGrid);
            }
            else if (element is Image image)
            {
                printPanel.Children.Add(new Image
                {
                    Source = image.Source,
                    Margin = image.Margin,
                    Height = image.Height,
                    Width = image.Width
                });
            }
        }
        //add static image to the print panel
        printPanel.Children.Add(new Image
        {
            Source = new BitmapImage(new Uri("/img/Odessos.jpg", UriKind.Relative)),
            Width = 100,
            Height = 100,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(10)
        });
        //print the ship layout
        PrintDialog printDialog = new PrintDialog();      

        if (printDialog.ShowDialog()==true)
        {
            
            printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
         

            FlowDocument doc = new FlowDocument(new BlockUIContainer(printPanel))
            {
                PageWidth = printDialog.PrintableAreaWidth,
                PageHeight = printDialog.PrintableAreaHeight,
                PagePadding = new Thickness(50),
                ColumnWidth = printDialog.MaxPage

            };

            printPanel.Measure(new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));
            printPanel.Arrange(new Rect(new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight)));
            double scale = Math.Min(printDialog.PrintableAreaWidth / printPanel.DesiredSize.Width, printDialog.PrintableAreaHeight / printPanel.DesiredSize.Height);
            printPanel.LayoutTransform = new ScaleTransform(scale, scale);


            double offsetX = (printDialog.PrintableAreaWidth - printPanel.DesiredSize.Width * scale) / 2;
            double offsetY = (printDialog.PrintableAreaHeight - printPanel.DesiredSize.Height * scale) / 2;
            printPanel.RenderTransform = new TranslateTransform(offsetX, offsetY);

            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;
            printDialog.PrintDocument(idpSource.DocumentPaginator, "Ship Layout Preview");
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