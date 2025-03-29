using ReactiveUI;
using FacturacionSystem.Models;
using FacturacionSystem.Repositories;
using System.Reactive;

namespace FacturacionSystem.ViewModels
{
    public class EditarClienteViewModel : ReactiveObject
    {
        public Cliente Cliente { get; }
        
        public ReactiveCommand<Unit, bool> GuardarCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelarCommand { get; }

        public EditarClienteViewModel(Cliente cliente)
        {
            Cliente = cliente;
            
            GuardarCommand = ReactiveCommand.Create(() => {
                var repo = new ClienteRepository();
                repo.UpdateCliente(Cliente);
                return true;
            });
            
            CancelarCommand = ReactiveCommand.Create(() => {});
        }
    }
}