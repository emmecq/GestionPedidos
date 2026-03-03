using System;

public class Program
{
    public static void Main()
    {
        decimal montoPedido, costoEnvio, costoAdicional;
        int cantidadItems;
        string entradaMontoPedido, ciudadDestino, tipoCliente, entradaCantidadItems, categoriaDespacho;

        Console.WriteLine("Ingrese el monto del pedido:");
        entradaMontoPedido = Console.ReadLine()??"";
        while(!decimal.TryParse(entradaMontoPedido, out montoPedido))
        {
            Console.WriteLine("La entrada ingresada no es un número. \nIngrese el monto del pedido:");
            entradaMontoPedido = Console.ReadLine()??"";
        }
        Console.WriteLine("El monto ingresado fue:" + montoPedido);

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
            costoEnvio = 0;
            if (cantidadItems >= 5 || montoPedido >= 300000)
            {
                categoriaDespacho = "Express";
            }
        }


    }
}