using System;
using System.Collections.Generic;

/// <summary>
/// Clase principal del programa.
/// Controla el flujo general del sistema de pedidos mediante un menú interactivo.
/// </summary>
public class Program
{
    /// <summary>
    /// Punto de entrada del programa.
    /// Inicializa las listas de datos y ejecuta el ciclo principal del menú.
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
    /// Muestra en consola las opciones disponibles del menú principal.
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
    /// Solicita al usuario una opción del menú y valida que esté entre 1 y 4.
    /// </summary>
    /// <returns>Opción válida seleccionada por el usuario.</returns>
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
    /// Ejecuta la acción correspondiente a la opción seleccionada.
    /// </summary>
    /// <param name="opcion">Opción elegida por el usuario.</param>
    /// <param name="montos">Lista de montos de pedidos.</param>
    /// <param name="ciudades">Lista de ciudades.</param>
    /// <param name="tiposCliente">Lista de tipos de cliente.</param>
    /// <param name="cantidadesItems">Lista de cantidades de ítems.</param>
    /// <param name="categorias">Lista de categorías.</param>
    /// <param name="costosEnvio">Lista de costos de envío.</param>
    /// <param name="costosAdicionales">Lista de costos adicionales.</param>
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
                MostrarReportes(montos, true);
                break;

            case 4:
                Console.WriteLine("Saliendo...");
                break;
        }
    }

    /// <summary>
    /// Registra un nuevo pedido solicitando datos al usuario,
    /// calculando sus costos y almacenándolo en las listas.
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
    /// Solicita un texto al usuario.
    /// </summary>
    /// <param name="mensaje">Mensaje mostrado en consola.</param>
    /// <returns>Texto ingresado por el usuario.</returns>
    public static string LeerTexto(string mensaje)
    {
        Console.WriteLine(mensaje);
        return Console.ReadLine() ?? "";
    }

    /// <summary>
    /// Solicita un número decimal validado.
    /// </summary>
    /// <param name="mensaje">Mensaje mostrado en consola.</param>
    /// <returns>Valor decimal válido.</returns>
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
    /// Solicita un número entero validado.
    /// </summary>
    /// <param name="mensaje">Mensaje mostrado en consola.</param>
    /// <returns>Valor entero válido.</returns>
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
    /// Solicita y valida el tipo de cliente ("nuevo" o "recurrente").
    /// </summary>
    /// <returns>Tipo de cliente válido.</returns>
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
    /// Determina la categoría del pedido según reglas de negocio.
    /// </summary>
    public static string CalcularCategoria(decimal monto, int cantidadItems, string tipoCliente)
    {
        if (monto >= 150000 && tipoCliente == "recurrente")
            return "Gratis";

        if (cantidadItems >= 5 || monto >= 300000)
            return "Express";

        return "Estándar";
    }

    /// <summary>
    /// Calcula el costo de envío según la categoría.
    /// </summary>
    public static decimal CalcularCostoEnvio(string categoria)
    {
        if (categoria == "Gratis" || categoria == "Express")
            return 0;

        return 5000;
    }

    /// <summary>
    /// Calcula el costo adicional basado en la ciudad.
    /// </summary>
    public static decimal CalcularCostoAdicional(string ciudad)
    {
        return ciudad == "exterior" ? 10000 : 0;
    }

    /// <summary>
    /// Calcula el costo adicional considerando ciudad y monto (con descuento).
    /// </summary>
    public static decimal CalcularCostoAdicional(string ciudad, decimal monto)
    {
        decimal costoBase = CalcularCostoAdicional(ciudad);

        if (monto > 500000)
            return costoBase * 0.8m;

        return costoBase;
    }

    /// <summary>
    /// Muestra un resumen detallado del pedido.
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
    /// Muestra todos los pedidos registrados.
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
    /// Muestra la cantidad total de pedidos.
    /// </summary>
    public static void MostrarReportes(List<decimal> montos)
    {
        Console.WriteLine("Total pedidos: " + montos.Count);
    }

    /// <summary>
    /// Muestra la cantidad de pedidos y opcionalmente la suma total.
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
