using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using FacturacionSystem.Models;
using FacturacionSystem.Repositories;
using FacturacionSystem.Views;
using System.Threading.Tasks;

namespace FacturacionSystem.ViewModels
{
    public class ClientesViewModel : ReactiveObject
    {
        private readonly ClienteRepository _clienteRepo = new ClienteRepository();
        private ObservableCollection<Cliente> _clientes;

        public ObservableCollection<Cliente> Clientes
        {
            get => _clientes;
            set => this.RaiseAndSetIfChanged(ref _clientes, value);
        }

        public ReactiveCommand<Unit, Unit> AgregarClienteCommand { get; }
        public ReactiveCommand<Cliente, Unit> EditarClienteCommand { get; }
        public ReactiveCommand<Cliente, Unit> EliminarClienteCommand { get; }

        public ClientesViewModel()
        {
            CargarClientes();
            
            AgregarClienteCommand = ReactiveCommand.CreateFromTask(AgregarClienteAsync);
            EditarClienteCommand = ReactiveCommand.Create<Cliente>(EditarCliente);
            EliminarClienteCommand = ReactiveCommand.Create<Cliente>(EliminarCliente);
        }

        private void CargarClientes()
        {
            Clientes = new ObservableCollection<Cliente>(_clienteRepo.GetAllClientes());
        }

        private async Task AgregarClienteAsync()
        {
            var nuevoCliente = new Cliente();
            var dialog = new NuevoClienteView(nuevoCliente)
            {
                DataContext = new NuevoClienteViewModel(nuevoCliente)
            };

            var result = await dialog.ShowDialog<bool>(App.Current.MainWindow);
            if (result)
            {
                _clienteRepo.AddCliente(nuevoCliente);
                CargarClientes();
            }
        }

        private async void EditarCliente(Cliente cliente)
        {
            if (cliente == null) return;
            
            var clienteEdit = new Cliente(cliente);
            var dialog = new EditarClienteView(clienteEdit)
            {
                DataContext = new EditarClienteViewModel(clienteEdit)
            };

            var result = await dialog.ShowDialog<bool>(App.Current.MainWindow);
            if (result)
            {
                _clienteRepo.UpdateCliente(clienteEdit);
                CargarClientes();
            }
        }

        private void EliminarCliente(Cliente cliente)
        {
            if (cliente == null) return;
            
            _clienteRepo.DeleteCliente(cliente.Id);
            CargarClientes();
        }
    }
}