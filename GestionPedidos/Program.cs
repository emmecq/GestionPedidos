using System;
using System.Collections.Generic;

/// <summary>
/// Representa un pedido con todos sus datos y costos asociados.
/// Responsabilidad única: ser el modelo de datos de un pedido.
/// </summary>
public class Pedido
{
    /// <summary>Monto base del pedido ingresado por el usuario.</summary>
    public decimal Monto { get; set; }

    /// <summary>Ciudad de destino del pedido.</summary>
    public string Ciudad { get; set; }

    /// <summary>Tipo de cliente: "nuevo" o "recurrente".</summary>
    public string TipoCliente { get; set; }

    /// <summary>Cantidad de ítems incluidos en el pedido.</summary>
    public int CantidadItems { get; set; }

    /// <summary>Categoría de envío calculada: "Gratis", "Express" o "Estándar".</summary>
    public string Categoria { get; set; }

    /// <summary>Costo de envío derivado de la categoría.</summary>
    public decimal CostoEnvio { get; set; }

    /// <summary>Costo adicional derivado de la ciudad destino y el monto.</summary>
    public decimal CostoAdicional { get; set; }
}

/// <summary>
/// Clase principal del programa. Contiene el menú, el flujo de registro
/// y toda la lógica de negocio del sistema de pedidos.
/// </summary>
public class Program
{
    /// <summary>
    /// Punto de entrada del programa.
    /// Inicializa la lista de pedidos y ejecuta el bucle principal del menú.
    /// </summary>
    public static void Main()
    {
        // Una sola lista en lugar de 7 listas paralelas.
        List<Pedido> pedidos = new List<Pedido>();
        int opcion;

        Console.WriteLine("Bienvenido.");

        do
        {
            MostrarMenu();
            opcion = LeerOpcionMenu();
            EjecutarOpcion(opcion, pedidos);

        } while (opcion != 4);
    }

    /// <summary>
    /// Muestra las opciones del menú principal en consola.
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
    /// Repite la solicitud hasta recibir un entero entre 1 y 4.
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
    /// Ejecuta la acción correspondiente a la opción seleccionada en el menú.
    /// </summary>
    /// <param name="opcion">Número de opción elegida (1-4).</param>
    /// <param name="pedidos">Lista de pedidos registrados en la sesión.</param>
    public static void EjecutarOpcion(int opcion, List<Pedido> pedidos)
    {
        switch (opcion)
        {
            case 1:
                RegistrarPedido(pedidos);
                break;

            case 2:
                MostrarPedidos(pedidos);
                break;

            case 3:
                MostrarReportes(pedidos, true);
                break;

            case 4:
                Console.WriteLine("Saliendo...");
                break;
        }
    }

    // =========================================================
    // REGISTRO DE PEDIDO — separado en tres responsabilidades:
    //   1. LeerDatosPedido   → captura input del usuario
    //   2. CalcularPedido    → aplica la lógica de negocio
    //   3. GuardarPedido     → persiste en la lista
    // =========================================================

    /// <summary>
    /// Orquesta el flujo completo de registro de un pedido:
    /// lee los datos del usuario, aplica los cálculos, muestra el resumen y guarda el pedido.
    /// </summary>
    /// <param name="pedidos">Lista donde se almacenará el nuevo pedido.</param>
    public static void RegistrarPedido(List<Pedido> pedidos)
    {
        Pedido pedido = LeerDatosPedido();
        CalcularPedido(pedido);
        MostrarResumen(pedido);
        GuardarPedido(pedidos, pedido);
    }

    /// <summary>
    /// Solicita al usuario los datos básicos del pedido y los devuelve
    /// como un objeto Pedido sin calcular aún.
    /// </summary>
    /// <returns>
    /// Un Pedido con Monto, Ciudad, TipoCliente y CantidadItems completados.
    /// </returns>
    public static Pedido LeerDatosPedido()
    {
        return new Pedido
        {
            Monto         = LeerDecimal("Ingrese el monto del pedido:"),
            Ciudad        = LeerTexto("Ingrese la ciudad destino:"),
            TipoCliente   = LeerTipoCliente(),
            CantidadItems = LeerEntero("Ingrese la cantidad de items:")
        };
    }

    /// <summary>
    /// Aplica la lógica de negocio sobre el pedido: calcula su categoría,
    /// costo de envío y costo adicional, asignando los resultados al mismo objeto.
    /// </summary>
    /// <param name="pedido">Pedido con datos básicos ya cargados.</param>
    public static void CalcularPedido(Pedido pedido)
    {
        pedido.Categoria      = CalcularCategoria(pedido.Monto, pedido.CantidadItems, pedido.TipoCliente);
        pedido.CostoEnvio     = CalcularCostoEnvio(pedido.Categoria);
        pedido.CostoAdicional = CalcularCostoAdicional(pedido.Ciudad, pedido.Monto);
    }

    /// <summary>
    /// Agrega el pedido a la lista de pedidos registrados en la sesión.
    /// </summary>
    /// <param name="pedidos">Lista destino.</param>
    /// <param name="pedido">Pedido ya calculado que se desea persistir.</param>
    public static void GuardarPedido(List<Pedido> pedidos, Pedido pedido)
    {
        pedidos.Add(pedido);
    }

    // =========================================================
    // LECTURA DE DATOS
    // =========================================================

    /// <summary>
    /// Muestra un mensaje en consola y devuelve el texto ingresado por el usuario.
    /// </summary>
    /// <param name="mensaje">Texto que se mostrará como prompt.</param>
    /// <returns>Cadena ingresada por el usuario. Nunca es <c>null</c>.</returns>
    public static string LeerTexto(string mensaje)
    {
        Console.WriteLine(mensaje);
        return Console.ReadLine() ?? "";
    }

    /// <summary>
    /// Muestra un mensaje y lee un número decimal validado.
    /// Repite la solicitud si la entrada no es convertible a decimal.
    /// </summary>
    /// <param name="mensaje">Texto que se mostrará como prompt.</param>
    /// <returns>Valor decimal ingresado por el usuario.</returns>
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
    /// Muestra un mensaje y lee un número entero validado.
    /// Repite la solicitud si la entrada no es convertible a entero.
    /// </summary>
    /// <param name="mensaje">Texto que se mostrará como prompt.</param>
    /// <returns>Valor entero ingresado por el usuario.</returns>
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
    /// Solicita el tipo de cliente y valida que sea "nuevo" o "recurrente".
    /// Repite la solicitud si el valor ingresado no corresponde a ninguna opción válida.
    /// </summary>
    /// <returns>"nuevo" o "recurrente".</returns>
    public static string LeerTipoCliente()
    {
        string tipo;

        Console.WriteLine("Ingrese el tipo de cliente (nuevo / recurrente):");
        tipo = Console.ReadLine() ?? "";

        while (tipo != "nuevo" && tipo != "recurrente")
        {
            Console.WriteLine("Tipo inválido. Ingrese 'nuevo' o 'recurrente':");
            tipo = Console.ReadLine() ?? "";
        }

        return tipo;
    }

    // =========================================================
    // LÓGICA DE NEGOCIO
    // =========================================================

    /// <summary>
    /// Determina la categoría de envío según el monto, cantidad de ítems y tipo de cliente.
    /// </summary>
    /// <param name="monto">Monto del pedido.</param>
    /// <param name="cantidadItems">Número de ítems en el pedido.</param>
    /// <param name="tipoCliente">"nuevo" o "recurrente".</param>
    /// <returns>
    /// "Gratis" si el monto es ≥ 150.000 y el cliente es recurrente;
    /// "Express" si hay 5 o más ítems o el monto es ≥ 300.000;
    /// "Estándar" en cualquier otro caso.
    /// </returns>
    public static string CalcularCategoria(decimal monto, int cantidadItems, string tipoCliente)
    {
        if (monto >= 150000 && tipoCliente == "recurrente")
            return "Gratis";

        if (cantidadItems >= 5 || monto >= 300000)
            return "Express";

        return "Estándar";
    }

    /// <summary>
    /// Calcula el costo de envío según la categoría del pedido.
    /// Las categorías "Gratis" y "Express" no generan costo; "Estándar" cuesta $5.000.
    /// </summary>
    /// <param name="categoria">Categoría calculada por CalcularCategoria.</param>
    /// <returns>0 para "Gratis" o "Express"; 5000 para "Estándar".</returns>
    public static decimal CalcularCostoEnvio(string categoria)
    {
        if (categoria == "Gratis" || categoria == "Express")
            return 0;

        return 5000;
    }

    /// <summary>
    /// Calcula el costo adicional basado únicamente en la ciudad de destino.
    /// Los pedidos al "exterior" tienen un costo adicional de $10.000.
    /// </summary>
    /// <param name="ciudad">Ciudad de destino del pedido.</param>
    /// <returns>10000 si la ciudad es "exterior"; 0 en cualquier otro caso.</returns>
    public static decimal CalcularCostoAdicional(string ciudad)
    {
        return ciudad == "exterior" ? 10000 : 0;
    }

    /// <summary>
    /// Sobrecarga: calcula el costo adicional considerando ciudad y monto.
    /// Aplica un descuento del 20 % sobre el costo base cuando el monto supera $500.000.
    /// DRY: delega la lógica de ciudad a CalcularCostoAdicional(string).
    /// </summary>
    /// <param name="ciudad">Ciudad de destino del pedido.</param>
    /// <param name="monto">Monto del pedido para evaluar el descuento.</param>
    /// <returns>Costo adicional con descuento aplicado si corresponde.</returns>
    public static decimal CalcularCostoAdicional(string ciudad, decimal monto)
    {
        decimal costoBase = CalcularCostoAdicional(ciudad);

        if (monto > 500000)
            return costoBase * 0.8m;

        return costoBase;
    }

    // =========================================================
    // PRESENTACIÓN
    // =========================================================

    /// <summary>
    /// Muestra en consola el resumen completo de un pedido,
    /// incluyendo el total de costos de envío y adicionales.
    /// </summary>
    /// <param name="pedido">Pedido ya calculado que se desea mostrar.</param>
    public static void MostrarResumen(Pedido pedido)
    {
        Console.WriteLine("\n===== RESUMEN =====");
        Console.WriteLine("Monto: "           + pedido.Monto);
        Console.WriteLine("Ciudad: "          + pedido.Ciudad);
        Console.WriteLine("Tipo cliente: "    + pedido.TipoCliente);
        Console.WriteLine("Cantidad items: "  + pedido.CantidadItems);
        Console.WriteLine("Categoría: "       + pedido.Categoria);
        Console.WriteLine("Costo envío: "     + pedido.CostoEnvio);
        Console.WriteLine("Costo adicional: " + pedido.CostoAdicional);
        Console.WriteLine("Total: "           + (pedido.CostoEnvio + pedido.CostoAdicional));
    }

    /// <summary>
    /// Lista todos los pedidos registrados mostrando su número, monto y categoría.
    /// Si no hay pedidos, informa al usuario.
    /// </summary>
    /// <param name="pedidos">Lista de pedidos a mostrar.</param>
    public static void MostrarPedidos(List<Pedido> pedidos)
    {
        if (pedidos.Count == 0)
        {
            Console.WriteLine("No hay pedidos.");
            return;
        }

        for (int i = 0; i < pedidos.Count; i++)
        {
            Console.WriteLine((i + 1) + ") " + pedidos[i].Monto + " - " + pedidos[i].Categoria);
        }
    }

    /// <summary>
    /// Muestra únicamente la cantidad total de pedidos registrados.
    /// DRY: delega a MostrarReportes(pedidos, bool) con mostrarTotal = false.
    /// </summary>
    /// <param name="pedidos">Lista de pedidos registrados.</param>
    public static void MostrarReportes(List<Pedido> pedidos)
    {
        MostrarReportes(pedidos, false);
    }

    /// <summary>
    /// Sobrecarga: muestra la cantidad de pedidos y, opcionalmente, la suma de todos los montos.
    /// </summary>
    /// <param name="pedidos">Lista de pedidos registrados.</param>
    /// <param name="mostrarTotal">
    /// Si es <c>true</c>, calcula e imprime la suma acumulada de los montos.
    /// </param>
    public static void MostrarReportes(List<Pedido> pedidos, bool mostrarTotal)
    {
        Console.WriteLine("Total pedidos: " + pedidos.Count);

        if (mostrarTotal)
        {
            decimal suma = 0;

            foreach (Pedido p in pedidos)
                suma += p.Monto;

            Console.WriteLine("Suma total: " + suma);
        }
    }
}
