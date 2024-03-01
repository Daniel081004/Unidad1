using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Unidad1PanaderiaWPF
{
    public class Panaderia : INotifyPropertyChanged
    {
        public Pan Concha = new() { Nombre = "Concha", Precio = 11.45m, Stock = 27};
        public Pan Dona = new() { Nombre = "Dona", Precio = 62.99m, Stock = 13 };
        public Pan PanBlanco = new() { Nombre = "Pan Blanco", Precio = 51.50m, Stock = 20 };
        public Pan Baguette = new() { Nombre = "Baguette", Precio = 23.90m, Stock = 52 };
        public Pan Empanada = new() { Nombre = "Empanada", Precio = 12.00m, Stock = 120 };
        public Pan Campechana = new() { Nombre = "Campechana", Precio = 59.40m, Stock = 35 };

        public decimal TotalPagar { get; set; }

        public ObservableCollection<Pan> Panes { get; set; } = new ObservableCollection<Pan>();

        public Pan Panesito { get; set; } = new();

        public Panaderia()
        {
            AgregarCommand = new RelayCommand<Pan>(AgregarProducto);
            EliminarCommand = new RelayCommand<Pan>(EliminarProducto);
        }

        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }

        public void AgregarProducto(Pan? p)
        {
            if (p != null && p.Stock != 0)
            {
                var otro = new Pan
                {
                    Nombre = p.Nombre,
                    Precio = p.Precio
                };
                Panes.Add(otro);
                p.Stock--;
                TotalPagar += p.Precio;
            }
        }
        public void EliminarProducto(Pan? p)
        {
            if (p != null)
            {
                Panes.Remove(p);
                switch (p.Nombre)
                {
                    case "Concha": Concha.Stock++;
                        break;
                    case "Dona": Dona.Stock++;
                        break;
                    case "Pan Blanco": PanBlanco.Stock++;
                        break;
                    case "Baguette": Baguette.Stock++;
                        break;
                    case "Empanada": Empanada.Stock++;
                        break;
                    case "Campechana": Campechana.Stock++;
                        break;
                    default: break;
                }
            }
        }
        public void Pagar(decimal Pago)
        {
            if(Pago > TotalPagar)
            {
                Concha = new() { Nombre = "Concha", Precio = 11.45m, Stock = 27 };
                Dona = new() { Nombre = "Dona", Precio = 62.99m, Stock = 13 };
                PanBlanco = new() { Nombre = "Pan Blanco", Precio = 51.50m, Stock = 20 };
                Baguette = new() { Nombre = "Baguette", Precio = 23.90m, Stock = 52 };
                Empanada = new() { Nombre = "Empanada", Precio = 12.00m, Stock = 120 };
                Campechana = new() { Nombre = "Campechana", Precio = 59.40m, Stock = 35 };

                Panes = new();

                decimal feria = Pago - TotalPagar;
                TotalPagar = 0;

                DateTime fechaDeHoy = DateTime.Now;
                string formatoPersonalizado = fechaDeHoy.ToString("dd/MM/yyyy HH:mm:ss");

                string mensaje = $"Ticket {fechaDeHoy}\n\nProdcutos:\n\n";

                foreach (var pan in Panes)
                {
                    mensaje += pan.ToString() + "\n";
                }

                mensaje += $"\nTotal a Pagar: ${TotalPagar}\n\nPago: ${Pago}\n\nFeria: ${feria}";

                MessageBox.Show(mensaje, "Ticket", MessageBoxButton.OK, MessageBoxImage.Information);              
            }
            else if(TotalPagar == 0)
            {
                MessageBox.Show("Compra un pan primero");
            }
            else
            {
                MessageBox.Show("Pague completo señora");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
    public class Pan
    {
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }
        public byte Stock { get; set; }
    }
}