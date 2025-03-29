using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FacturacionSystem.Views
{
    public partial class ProductosView : Window
    {
        public ProductosView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}