using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private AdventureWorksContext context = new AdventureWorksContext();
        private ObservableCollection<ProductionProductSubcategory> productSubcategories = new ObservableCollection<ProductionProductSubcategory>();
        private ObservableCollection<ProductionProduct> products = new ObservableCollection<ProductionProduct>();
        private ObservableCollection<string> colors = new ObservableCollection<string>();

        private ProductionProduct productionProduct = new ProductionProduct();
        private ProductionProduct ProductionProduct
        {
            get { return productionProduct; }
            set
            {
                productionProduct = value;
                OnPropertyChanged("ProductionProduct");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }



        public MainWindow()
        {
            InitializeComponent();

            foreach (var item in this.context.ProductionProductSubcategories)
            {
                this.productSubcategories.Add(item);
            }
            foreach (var item in this.context.ProductionProducts)
            {
                this.products.Add(item);
            }
            foreach (var item in this.context.ProductionProducts.Select(x => x.Color).Distinct())
            {
                this.colors.Add(item);
            }

            lbProductSubcategories.ItemsSource = this.productSubcategories;
            lbProducts.ItemsSource = this.products;
            lbColors.ItemsSource = this.colors;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TestViewModel tvm = this.DataContext as TestViewModel;
            //tvm.SubCatViewSource.Source = context.ProductSubcategories.ToList();

            //tvm.ProductViewSource.Source = context.ProductionProducts.ToList();
            tvm.Colors.Source = context.ProductionProducts.Select(x => x.Color ?? "None" ).Distinct().ToList();
            

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {      
            context.SaveChanges();
        }

        

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbProducts.ItemsSource = this.products
                .Where(x => x.Color == (string)lbColors.SelectedItem);
        }

        private void LbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ProductionProduct = (ProductionProduct)lbProducts.SelectedItem;
        }

        private void LbProductSubcategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbProducts.ItemsSource = this.products
                .Where(x => x.ProductSubcategoryID
                == (lbProductSubcategories.SelectedItem as ProductionProductSubcategory).ProductSubcategoryID);
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
        public CollectionViewSource SubCatViewSource { get; set; }
        public CollectionViewSource ProductViewSource { get; set; }
        public CollectionViewSource Colors { get; set; }


    }
}
