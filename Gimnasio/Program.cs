using System;

class Program{

	// <summary>
	// Solicita al usuario la cantidad de clientes a ingresar, asegurando que sea un número entero positivo.
	// </summary>
	// <returns>
	// Un número entero mayor o igual a 1 que representa la cantidad de clientes.
	// </returns> 
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
	
}