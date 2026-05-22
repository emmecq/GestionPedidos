using System;
using System.Collections.Generic;

public class Program
{
    /// <summary>
    /// Punto de entrada del programa. Controla el flujo principal mediante un menú.
    /// </summary>
    public static void Main()
    {
        List<decimal> montos = new List<decimal>();
        List<string> ciudades = new List<string>();
        List<string> tiposCliente = new List<string>();
        List<int> cantidadesItems = new List<int>();
        List<string> categorias = new List<string>();
        List<decimal> costosEnvio = new List<decimal>();
        List<decimal> costosAdicionales = new List<decimal>();

        int opcion;

        Console.WriteLine("Bienvenido.");

        do
        {
            MostrarMenu();
            opcion = LeerOpcionMenu();

            EjecutarOpcion(
                opcion,
                montos,
                ciudades,
                tiposCliente,
                cantidadesItems,
                categorias,
                costosEnvio,
                costosAdicionales
            );

        } while (opcion != 4);
    }

    /// <summary>
    /// Muestra el menú principal del sistema.
    /// </summary>
    public static void MostrarMenu()
    {
        Console.WriteLine("\n===== MENÚ PRINCIPAL =====");
        Console.WriteLine("1) Registrar pedido");
        Console.WriteLine("2) Mostrar pedidos");
        Console.WriteLine("3) Generar reportes");
        Console.WriteLine("4) Salir");
    }

    /// <summary>
    /// Lee y valida la opción del menú ingresada por el usuario.
    /// </summary>
    /// <returns>Opción válida entre 1 y 4.</returns>
    public static int LeerOpcionMenu()
    {
        int opcion;
        string entrada;

        Console.Write("Ingrese una opción (1-4): ");
        entrada = Console.ReadLine() ?? "";

        while (!int.TryParse(entrada, out opcion) || opcion < 1 || opcion > 4)
        {
            Console.WriteLine("Entrada inválida.");
            entrada = Console.ReadLine() ?? "";
        }

        return opcion;
    }

    /// <summary>
    /// Ejecuta la opción seleccionada por el usuario.
    /// </summary>
    public static void EjecutarOpcion(
        int opcion,
        List<decimal> montos,
        List<string> ciudades,
        List<string> tiposCliente,
        List<int> cantidadesItems,
        List<string> categorias,
        List<decimal> costosEnvio,
        List<decimal> costosAdicionales
    )
    {
        switch (opcion)
        {
            case 1:
                RegistrarPedido(
                    montos,
                    ciudades,
                    tiposCliente,
                    cantidadesItems,
                    categorias,
                    costosEnvio,
                    costosAdicionales
                );
                break;

            case 2:
                MostrarPedidos(montos, categorias);
                break;

            case 3:
                MostrarReportes(montos, true); // usando sobrecarga
                break;

            case 4:
                Console.WriteLine("Saliendo...");
                break;
        }
    }

    /// <summary>
    /// Registra un nuevo pedido solicitando datos al usuario.
    /// </summary>
    public static void RegistrarPedido(
        List<decimal> montos,
        List<string> ciudades,
        List<string> tiposCliente,
        List<int> cantidadesItems,
        List<string> categorias,
        List<decimal> costosEnvio,
        List<decimal> costosAdicionales
    )
    {
        decimal montoPedido = LeerDecimal("Ingrese el monto del pedido:");
        string ciudadDestino = LeerTexto("Ingrese la ciudad destino:");
        string tipoCliente = LeerTipoCliente();
        int cantidadItems = LeerEntero("Ingrese la cantidad de items:");

        string categoria = CalcularCategoria(montoPedido, cantidadItems, tipoCliente);
        decimal costoEnvio = CalcularCostoEnvio(categoria);

        // Uso de sobrecarga
        decimal costoAdicional = CalcularCostoAdicional(ciudadDestino, montoPedido);

        MostrarResumen(
            montoPedido,
            ciudadDestino,
            tipoCliente,
            cantidadItems,
            categoria,
            costoEnvio,
            costoAdicional
        );

        montos.Add(montoPedido);
        ciudades.Add(ciudadDestino);
        tiposCliente.Add(tipoCliente);
        cantidadesItems.Add(cantidadItems);
        categorias.Add(categoria);
        costosEnvio.Add(costoEnvio);
        costosAdicionales.Add(costoAdicional);
    }

    /// <summary>
    /// Lee un texto desde consola.
    /// </summary>
    public static string LeerTexto(string mensaje)
    {
        Console.WriteLine(mensaje);
        return Console.ReadLine() ?? "";
    }

    /// <summary>
    /// Lee un número decimal validado.
    /// </summary>
    public static decimal LeerDecimal(string mensaje)
    {
        decimal numero;
        string entrada;

        Console.WriteLine(mensaje);
        entrada = Console.ReadLine() ?? "";

        while (!decimal.TryParse(entrada, out numero))
        {
            Console.WriteLine("Entrada inválida.");
            entrada = Console.ReadLine() ?? "";
        }

        return numero;
    }

    /// <summary>
    /// Lee un número entero validado.
    /// </summary>
    public static int LeerEntero(string mensaje)
    {
        int numero;
        string entrada;

        Console.WriteLine(mensaje);
        entrada = Console.ReadLine() ?? "";

        while (!int.TryParse(entrada, out numero))
        {
            Console.WriteLine("Entrada inválida.");
            entrada = Console.ReadLine() ?? "";
        }

        return numero;
    }

    /// <summary>
    /// Lee el tipo de cliente (nuevo o recurrente).
    /// </summary>
    public static string LeerTipoCliente()
    {
        string tipo;

        Console.WriteLine("Ingrese el tipo de cliente:");
        tipo = Console.ReadLine() ?? "";

        while (tipo != "nuevo" && tipo != "recurrente")
        {
            Console.WriteLine("Tipo inválido.");
            tipo = Console.ReadLine() ?? "";
        }

        return tipo;
    }

    /// <summary>
    /// Calcula la categoría de envío.
    /// </summary>
    public static string CalcularCategoria(decimal monto, int cantidadItems, string tipoCliente)
    {
        if (monto >= 150000 && tipoCliente == "recurrente")
        {
            return "Gratis";
        }
        else if (cantidadItems >= 5 || monto >= 300000)
        {
            return "Express";
        }

        return "Estándar";
    }

    /// <summary>
    /// Calcula el costo de envío según la categoría.
    /// </summary>
    public static decimal CalcularCostoEnvio(string categoria)
    {
        if (categoria == "Gratis" || categoria == "Express")
        {
            return 0;
        }

        return 5000;
    }

    /// <summary>
    /// Calcula costo adicional basado en la ciudad.
    /// </summary>
    public static decimal CalcularCostoAdicional(string ciudad)
    {
        if (ciudad == "exterior")
        {
            return 10000;
        }

        return 0;
    }

    /// <summary>
    /// Sobrecarga: calcula costo adicional considerando ciudad y monto.
    /// </summary>
    public static decimal CalcularCostoAdicional(string ciudad, decimal monto)
    {
        decimal costoBase = CalcularCostoAdicional(ciudad);

        if (monto > 500000)
        {
            return costoBase * 0.8m;
        }

        return costoBase;
    }

    /// <summary>
    /// Muestra el resumen del pedido.
    /// </summary>
    public static void MostrarResumen(
        decimal monto,
        string ciudad,
        string tipoCliente,
        int cantidadItems,
        string categoria,
        decimal costoEnvio,
        decimal costoAdicional
    )
    {
        Console.WriteLine("\n===== RESUMEN =====");
        Console.WriteLine("Monto: " + monto);
        Console.WriteLine("Ciudad: " + ciudad);
        Console.WriteLine("Tipo cliente: " + tipoCliente);
        Console.WriteLine("Cantidad items: " + cantidadItems);
        Console.WriteLine("Categoría: " + categoria);
        Console.WriteLine("Costo envío: " + costoEnvio);
        Console.WriteLine("Costo adicional: " + costoAdicional);
        Console.WriteLine("Total: " + (costoEnvio + costoAdicional));
    }

    /// <summary>
    /// Muestra los pedidos registrados.
    /// </summary>
    public static void MostrarPedidos(List<decimal> montos, List<string> categorias)
    {
        if (montos.Count == 0)
        {
            Console.WriteLine("No hay pedidos.");
            return;
        }

        for (int i = 0; i < montos.Count; i++)
        {
            Console.WriteLine(montos[i] + " - " + categorias[i]);
        }
    }

    /// <summary>
    /// Muestra solo la cantidad de pedidos.
    /// </summary>
    public static void MostrarReportes(List<decimal> montos)
    {
        Console.WriteLine("Total pedidos: " + montos.Count);
    }

    /// <summary>
    /// Sobrecarga: muestra cantidad y total acumulado.
    /// </summary>
    public static void MostrarReportes(List<decimal> montos, bool mostrarTotal)
    {
        Console.WriteLine("Total pedidos: " + montos.Count);

        if (mostrarTotal)
        {
            decimal suma = 0;

            foreach (decimal m in montos)
            {
                suma += m;
            }

            Console.WriteLine("Suma total: " + suma);
        }
    }
}