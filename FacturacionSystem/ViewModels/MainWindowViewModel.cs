using System;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using FacturacionSystem.Views;
using FacturacionSystem.ViewModels;

namespace FacturacionSystem.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> OpenClientesCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenProductosCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenFacturacionCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenReportesCommand { get; }

        public MainWindowViewModel()
        {
            OpenClientesCommand = ReactiveCommand.Create(OpenClientes);
            OpenProductosCommand = ReactiveCommand.Create(OpenProductos);
            OpenFacturacionCommand = ReactiveCommand.Create(OpenFacturacion);
            OpenReportesCommand = ReactiveCommand.Create(OpenReportes);
        }

        private void OpenClientes()
        {
            var clientesView = new ClientesView
            {
                DataContext = new ClientesViewModel()
            };
            clientesView.Show();
        }

        private void OpenProductos()
        {
            var productosView = new ProductosView
            {
                DataContext = new ProductosViewModel()
            };
            productosView.Show();
        }

        private void OpenFacturacion()
        {
            var facturacionView = new FacturacionView
            {
                DataContext = new FacturacionViewModel()
            };
            facturacionView.Show();
        }

        private void OpenReportes()
        {
            var reportesView = new ReportesView
            {
                DataContext = new ReportesViewModel()
            };
            reportesView.Show();
        }
    }
}