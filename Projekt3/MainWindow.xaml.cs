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

namespace Projekt3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model1 context = new Model1(); 


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TestViewModel tvm = this.DataContext as TestViewModel;
            tvm.SubCatViewSource.Source = context.ProductSubcategories.ToList();

            //tvm.ProductViewSource.Source = context.ProductionProducts.ToList();
            tvm.Colors.Source = context.ProductionProducts.Select(x => x.Color ?? "None" ).Distinct().ToList();
            

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {      
            context.SaveChanges();
        }

        

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            TestViewModel tvm = this.DataContext as TestViewModel;
            tvm.ProductViewSource.Source = new List<string>();
         
            tvm.ProductViewSource.Source = 
            context.ProductionProducts.Select(x => x).Where(x => x.Color == tvm.ColorChoice).ToList();
            tvm.ProductViewSource.Source = new List<string>();
        }

        
    }
    class TestViewModel
    {
        public TestViewModel()
        {
            this.SubCatViewSource = new CollectionViewSource();
            this.ProductViewSource = new CollectionViewSource();
            this.Colors = new CollectionViewSource();
        }
        
        
        public String ColorChoice { get; set; }
        public ProductionProduct ProductItem { get; set; }
        public ProductSubcategory CurrentItem { get; set; }
        public CollectionViewSource SubCatViewSource { get; set; }
        public CollectionViewSource ProductViewSource { get; set; }
        public CollectionViewSource Colors { get; set; }


    }
}
