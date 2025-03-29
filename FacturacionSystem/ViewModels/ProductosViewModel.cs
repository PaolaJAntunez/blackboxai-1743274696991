using System.Collections.ObjectModel;
using ReactiveUI;
using FacturacionSystem.Models;
using FacturacionSystem.Repositories;

namespace FacturacionSystem.ViewModels
{
    public class ProductosViewModel : ReactiveObject
    {
        private readonly ProductoRepository _repo = new ProductoRepository();
        private ObservableCollection<Producto> _productos;

        public ObservableCollection<Producto> Productos
        {
            get => _productos;
            set => this.RaiseAndSetIfChanged(ref _productos, value);
        }

        public ProductosViewModel()
        {
            CargarProductos();
        }

        private void CargarProductos()
        {
            Productos = new ObservableCollection<Producto>(_repo.GetAllProductos());
        }
    }
}