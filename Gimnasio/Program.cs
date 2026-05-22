using System;

class Program{

	/// <summary>
	/// Solicita al usuario la cantidad de clientes a ingresar, asegurando que sea un número entero positivo.
	/// </summary>
	/// <returns>
	/// Un número entero mayor o igual a 1 que representa la cantidad de clientes.
	/// </returns> 
	static int LeerCantidadClientes()
	{
		int cantidadClientes;
		Console.WriteLine("Cantidad de clientes a ingresar:");
		while(!int.TryParse(Console.ReadLine(), out cantidadClientes) || cantidadClientes < 1)
		{
			Console.WriteLine("Entrada inválida. Favor reingresar:");
		}

		return cantidadClientes;
	}
	
	static int LeerCliente(out string nombre, out int visitas, out int tipo)
	{

		Console.Write("Nombre: ");
		nombre = Console.ReadLine();
		
		Console.Write("Visitas: ");
		
		while (!int.TryParse(Console.ReadLine(), out visitas) || visitas < 0)
		{
			Console.WriteLine("Entrada inválida. Favor reingresar:");
			int visitas = Console.ReadLine();
		}
		
		Console.Write("¿Es cliente premium? (S/N): ");
		string entradaTipo = Console.ReadLine().ToLower();
		while (entradaTipo != "s" && entradaTipo != "n")
		{
			Console.WriteLine("Entrada inválida. Favor reingresar (S/N):");
			entradaTipo = Console.ReadLine();
		}
		
		return (visitas, tipo); 
	}

	/// <summary>
	/// Determina el tipo de cliente según la entrada del usuario.
	/// Se asume que el valor recibido ya está en minúsculas.
	/// </summary>
	/// <param name="entradaTipo">
	/// Cadena ingresada por el usuario para indicar si es cliente premium ("s") o no.
	/// </param>
	/// <returns>
	/// "premium" si la entrada es "s"; de lo contrario, "regular".
	/// </returns>

	static string DeterminarTipoCliente(string tipo)
	{
		if(entradaTipo() == "s")  
		{
			tipo = "premium";
		}
		else
		{
			tipo = "regular";
		}

		return tipo;
	}
	
	static int ProcesarClientes(int cantidadClientes)
	{
		List<string> nombres = new List<string>();
		List<int> listaVisitas = new List<int>();
		List<int> listaTipos = new List<int>();

		for (int i = 1; i <= cantidadClientes; i++)
		{
			string nombre;
			int visitas;
			int tipo;

			Console.WriteLine($"Cliente {i}:");

			
			LeerCliente(out nombre, out visitas, out tipo);

			nombres.Add(nombre);
			visitasLista.Add(visitas);
			tipos.Add(tipo);
		}

		
		Console.WriteLine("\nClientes registrados:");

		for (int i = 0; i < nombres.Count; i++)
		{
			Console.WriteLine($"{nombres[i]} - Visitas: {visitasLista[i]} - Tipo: {tipos[i]}");
		}
	}
	
	static double CalcularDescuento(int visitas, int tipo)
	{
		double descuento;

		if (tipo == 2)
		{
			descuento = 0.20;
		}
		else if (visitas > 15) 
		{
			descuento = 0.10;
		}
		else
		{
			descuento = 0;
		}

		return descuento;
	}
	
	static void ImprimirTotal(double subtotal, double descuento)
	{
		
	}
	
	static void Main()
	{
		LeerCantidadClientes();
		LeerCliente();
		DeterminarTipoCliente();
		ProcesarClientes();
		CalcularDescuento();

	}
	
	
}