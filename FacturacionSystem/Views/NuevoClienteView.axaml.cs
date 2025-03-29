using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FacturacionSystem.ViewModels;
using FacturacionSystem.Models;

namespace FacturacionSystem.Views
{
    public partial class NuevoClienteView : Window
    {
        public NuevoClienteView(Cliente cliente)
        {
            InitializeComponent();
            DataContext = new NuevoClienteViewModel(cliente);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}