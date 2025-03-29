using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FacturacionSystem.ViewModels;

namespace FacturacionSystem.Views
{
    public partial class ClientesView : Window
    {
        public ClientesView()
        {
            InitializeComponent();
            DataContext = new ClientesViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}