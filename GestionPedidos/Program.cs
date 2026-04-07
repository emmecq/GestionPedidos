using System;
using System.Collections.Generic;

public class Pedido
{
    public decimal monto;
    public string ciudadDestino;
    public string tipoCliente;
    public int cantidadItems;
    public string categoria;
    public decimal costoEnvio;
    public decimal costoAdicional;
}
public class Program
{
    public static void Main()
    {
        List<Pedido> pedidos = new List<Pedido>();
        decimal montoPedido, costoEnvio, costoAdicional;
        int cantidadItems, opcion;
        string entradaOpcion, entradaMontoPedido, ciudadDestino, tipoCliente, entradaCantidadItems, categoriaDespacho;

        Console.WriteLine("Bienvenido.");

        do {
            Console.WriteLine("\n===== MENÚ PRINCIPAL =====");
            Console.WriteLine("1) Registrar pedido");
            Console.WriteLine("2) Mostrar pedidos");
            Console.WriteLine("3) Generar reportes");
            Console.WriteLine("4) Salir");

            Console.Write("Ingrese una opción (1-4): ");
            entradaOpcion = Console.ReadLine()??"";

            while(!int.TryParse(entradaOpcion, out opcion) || opcion < 1 || opcion > 4)
            {
                Console.WriteLine("Entrada inválida. Ingrese una opción del 1 al 4");
                entradaOpcion = Console.ReadLine()??"";
            }

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Ingrese el monto del pedido:");
                    entradaMontoPedido = Console.ReadLine()??"";
                    while(!decimal.TryParse(entradaMontoPedido, out montoPedido))
                    {
                        Console.WriteLine("La entrada ingresada no es un número. \nIngrese el monto del pedido:");
                        entradaMontoPedido = Console.ReadLine()??"";
                    }
                    Console.WriteLine("El monto ingresado fue: " + montoPedido);

                    Console.WriteLine("Ingrese la ciudad destino:");
                    ciudadDestino = Console.ReadLine()??"";



                    Console.WriteLine("Ingrese el tipo de cliente: ");
                    tipoCliente = Console.ReadLine()??"";
                    while(tipoCliente != "nuevo" && tipoCliente != "recurrente")
                    {
                        Console.WriteLine("El tipo de cliente no es válido. \nIngrese el tipo de cliente:");
                        tipoCliente = Console.ReadLine()??"";
                    }


                    Console.WriteLine("Ingrese la cantidad de items:");
                    entradaCantidadItems = Console.ReadLine()??"";
                    while(!int.TryParse(entradaCantidadItems, out cantidadItems))
                    {
                        Console.WriteLine("La entrada ingresada no es un número. \nIngrese la cantidad de items:");
                        entradaCantidadItems= Console.ReadLine()??"";
                    }

                    if (montoPedido >= 150000 && tipoCliente == "recurrente")
                    {
                        categoriaDespacho = "Gratis";
                        costoEnvio = 0;
                    }
                    else if (cantidadItems >= 5 || montoPedido >= 300000)
                    {
                        categoriaDespacho = "Express";
                        costoEnvio = 0;
                    } else
                    {
                        categoriaDespacho = "Estándar";
                        costoEnvio = 5000; // El valor no fue asignado en la propuesta. Se establece este como base.
                    }

                    if (ciudadDestino == "exterior")
                    {
                        costoAdicional = 10000; // Este valor tampoco fue asignado en la propuesta. Se establece este como base.
                    }
                    else
                    {
                        costoAdicional = 0;
                    }
                    
                    Console.WriteLine("\n===== RESUMEN DEL PEDIDO =====");
                    Console.WriteLine("Monto del pedido: " + montoPedido);
                    Console.WriteLine("Ciudad destino: " + ciudadDestino);
                    Console.WriteLine("Tipo de cliente: " + tipoCliente);
                    Console.WriteLine("Cantidad de items: " + cantidadItems);
                    Console.WriteLine("Categoría de despacho: " + categoriaDespacho);
                    Console.WriteLine("Costo de envío: " + costoEnvio);
                    Console.WriteLine("Costo adicional: " + costoAdicional);
                    Console.WriteLine("Costo total de despacho: " + (costoEnvio + costoAdicional));
                    Pedido nuevoPedido = new Pedido();

                    nuevoPedido.monto = montoPedido;
                    nuevoPedido.ciudadDestino = ciudadDestino;
                    nuevoPedido.tipoCliente = tipoCliente;
                    nuevoPedido.cantidadItems = cantidadItems;
                    nuevoPedido.categoria = categoriaDespacho;
                    nuevoPedido.costoEnvio = costoEnvio;
                    nuevoPedido.costoAdicional = costoAdicional;

                    pedidos.Add(nuevoPedido);

                break;

                case 2:
                if (pedidos.Count == 0)
                    {
                        Console.WriteLine("No hay pedidos registrados.")
                        Environment.Exit(0);
                    }
                    else
                    {
                        foreach (var p in pedidos)
                        {
                            Console.WriteLine(p.monto + " - " + p.categoria);
                        }
                    }
                
                break;
                case 3:
                if (pedidos.Count == 0)
                    {
                        Console.WriteLine("No hay pedidos registrados.")
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Total de pedidos: " + pedidos.Count);
                    }
                
                break;
                case 4:
                Environment.Exit(0);
                break;   
            }
            
        } while (opcion !=4);

        

    }
}