using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FacturacionSystem.Models;
using FacturacionSystem.ViewModels;

namespace FacturacionSystem.Views
{
    public partial class EditarClienteView : Window
    {
        public EditarClienteView(Cliente cliente)
        {
            InitializeComponent();
            DataContext = new EditarClienteViewModel(cliente);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}