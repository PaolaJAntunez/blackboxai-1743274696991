using ReactiveUI;
using FacturacionSystem.Models;
using FacturacionSystem.Repositories;
using System.Reactive;

namespace FacturacionSystem.ViewModels
{
    public class NuevoClienteViewModel : ReactiveObject
    {
        private readonly ClienteRepository _clienteRepo = new ClienteRepository();
        public Cliente Cliente { get; }
        
        public ReactiveCommand<Unit, bool> GuardarCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelarCommand { get; }

        public NuevoClienteViewModel(Cliente cliente)
        {
            Cliente = cliente;
            
            GuardarCommand = ReactiveCommand.Create(() => {
                _clienteRepo.AddCliente(Cliente);
                return true;
            });
            
            CancelarCommand = ReactiveCommand.Create(() => {});
        }
    }
}