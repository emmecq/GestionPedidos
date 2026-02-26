using System;

public class Program
{
    public static void Main()
    {
        decimal montoPedido, costoEnvio;
        int cantidadItems;
        string entradaMontoPedido, tipoCliente, entradaCantidadItems;// ciudadDestino, categoriaDespacho;

        Console.WriteLine("Ingrese el monto del pedido:");
        entradaMontoPedido = Console.ReadLine();
        while(!decimal.TryParse(entradaMontoPedido, out montoPedido))
        {
            Console.WriteLine("La entrada ingresada no es un número. \nIngrese el monto del pedido:");
            entradaMontoPedido = Console.ReadLine();
        }
        Console.WriteLine("El monto ingresado fue:" + montoPedido);


        Console.WriteLine("Ingrese el tipo de cliente: ");
        tipoCliente = Console.ReadLine()??"";
        while(tipoCliente != "nuevo" && tipoCliente != "recurrente")
        {
            Console.WriteLine("El tipo de cliente no es válido. \nIngrese el tipo de cliente:");
            tipoCliente = Console.ReadLine()??"";
        }


        Console.WriteLine("Ingrese la cantidad de items:");
        entradaCantidadItems = Console.ReadLine();
        while(!int.TryParse(entradaCantidadItems, out cantidadItems))
        {
            Console.WriteLine("La entrada ingresada no es un número. \nIngrese la cantidad de items:");
            entradaCantidadItems= Console.ReadLine();
        }


    }
}