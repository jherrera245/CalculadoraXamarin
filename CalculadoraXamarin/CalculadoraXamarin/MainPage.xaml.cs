using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculadoraXamarin
{
    public partial class MainPage : ContentPage
    {

        //Configuracion inicial
        private string EntradaActual = "0";
        private double Resultado = 0;
        private string OperadorActual = "";
        private bool EsNuevaEntrada = true;

        public MainPage()
        {
            InitializeComponent();
            ConfiguracionBotones();
        }

        //en esta seccion procedemos a configurar los botones
        private void ConfiguracionBotones()
        {
            //recorremos los elementos en nuestra grid (Botones)
            //para asignar eventos a botones numericos y operadores
            foreach (var child in grid.Children)
            { 
                if (child is Button button)
                {
                    button.Clicked += OnButtonClicked;
                }
            }

            btnRemover.Clicked += OnRemoverClicked;
            btnIgual.Clicked += OnIgualClicked;
            btnLimpiar.Clicked += OnLimpiarClicked;
            btnDecimal.Clicked += OnDecimalClicked;
            btnPorcentaje.Clicked += OnPorcentajeClicked;

        }

        //capturar entrada de valores numericos y agregarlos a la pantalla
        private void OnButtonClicked(object seeder, EventArgs e)
        {
            if (seeder is Button button)
            { 
                string textoBoton = button.Text;

                if (EsNuevaEntrada)
                {
                    EntradaActual = textoBoton;
                    EsNuevaEntrada = false;
                } 
                else 
                {
                    EntradaActual += textoBoton;
                }   
                entradaCalculadora.Text = EntradaActual;
            }
        }

        //remover elmentos de la pantalla
        private void OnRemoverClicked(object seeder, EventArgs e)
        {
            if (EntradaActual.Length > 1)
            {
                EntradaActual = EntradaActual.Substring(0, EntradaActual.Length - 1);
            }
            else
            {
                EntradaActual = "0";
                EsNuevaEntrada = true;
            }

            entradaCalculadora.Text += EntradaActual;
        }


        //presionar el boton igual
        private void OnIgualClicked(object seeder, EventArgs e)
        {
            try
            {
                Resultado = EvaluarExpresion();
                EntradaActual = Resultado.ToString();
                EsNuevaEntrada = true;
                entradaCalculadora.Text = EntradaActual;
            }
            catch (Exception)
            {
                EntradaActual = "Error";
                entradaCalculadora.Text = EntradaActual;
            }
        }

        //Limpiar la calculadora
        private void OnLimpiarClicked(object seeder, EventArgs e)
        {
            EntradaActual = "0";
            Resultado = 0;
            OperadorActual = "";
            entradaCalculadora.Text = EntradaActual;
        }

        //Boton punto decimal
        private void OnDecimalClicked(object seeder, EventArgs e)
        {
            if (!EntradaActual.Contains("."))
            {
                EntradaActual += ".";
                EsNuevaEntrada = false;
                entradaCalculadora.Text = EntradaActual;
            }
        }

        //Presionar boton porcentaje
        private void OnPorcentajeClicked(object seeder, EventArgs e)
        {
            try
            {
                double entrada = double.Parse(EntradaActual);
                Resultado = entrada / 100;
                EntradaActual = entrada.ToString();
                entradaCalculadora.Text = EntradaActual;
            }
            catch (Exception)
            {
                EntradaActual = "Error";
                entradaCalculadora.Text = EntradaActual;
            }
        }

        //evaluar la expresion a presionar.
        private double EvaluarExpresion()
        {
            if (double.TryParse(EntradaActual, out double ValorEntrada))
            {
                switch (OperadorActual)
                {
                    case "+":
                        Resultado += ValorEntrada;
                        break;
                    case "-":
                        Resultado -= ValorEntrada;
                        break;
                    case "*":
                        Resultado *= ValorEntrada;
                        break;
                    case "÷":
                        if (ValorEntrada != 0)
                        {
                            Resultado /= ValorEntrada;
                        }
                        else
                        {
                            throw new DivideByZeroException();
                        }
                        break;
                    default:
                        Resultado = ValorEntrada;
                        break ;
                }
            }

            return Resultado;
        }

    }
}
