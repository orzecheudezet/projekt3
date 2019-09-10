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
        private ObservableCollection<string> sizes = new ObservableCollection<string>();

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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
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
            foreach (var item in this.context.ProductionProducts.Select(x => x.Size).Distinct())
            {
                this.sizes.Add(item);
            }

            lbProductSubcategories.ItemsSource = this.productSubcategories;
            lbProducts.ItemsSource = this.products;
            lbColors.ItemsSource = this.colors;
            lbSizes.ItemsSource = this.sizes;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel viewModel = this.DataContext as MainWindowViewModel;
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var productionProductsList = lbProducts.ItemsSource as IEnumerable<ProductionProduct>;

            try
            {
                productionProductsList = productionProductsList.Where(x => x.Color == (string)lbColors.SelectedItem);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            lbProducts.ItemsSource = productionProductsList;
        }

        private void SizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var productionProductsList = lbProducts.ItemsSource as IEnumerable<ProductionProduct>;

            try
            {
                productionProductsList = productionProductsList.Where(x => x.Size == (string)lbSizes.SelectedItem);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            lbProducts.ItemsSource = productionProductsList;
        }

        private void LbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbProducts.SelectedItem != null)
            {
                var productID = (lbProducts.SelectedItem as ProductionProduct).ProductID;
                var product = this.context.ProductionProducts
                    .Where(x => x.ProductID == productID)
                    .First();

                this.ProductionProduct = product;
            }
   
        }

        private void LbProductSubcategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbProducts.ItemsSource = this.products
                .Where(x => x.ProductSubcategoryID
                == (lbProductSubcategories.SelectedItem as ProductionProductSubcategory).ProductSubcategoryID);

            lbColors.SelectedItem = null;
            lbSizes.SelectedItem = null;
        }
    }
    class MainWindowViewModel
    {
        public ProductionProduct ProductItem { get; set; }
    }
}
