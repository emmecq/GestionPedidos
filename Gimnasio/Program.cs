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
	


	/// <summary>
	/// Determina el tipo de cliente según la entrada del usuario.
	/// </summary>
	/// <param name="entradaTipo">
	/// Cadena ingresada por el usuario para indicar si es cliente premium ("s") o no.
	/// </param>
	/// <returns>
	/// "premium" si la entrada es "s"; de lo contrario, "regular".
	/// </returns>

	static string DeterminarTipoCliente(string tipo)
	{
		if(entradaTipo.ToLower() == "s")  
		{
			tipo = "premium";
		}
		else
		{
			tipo = "regular";
		}

		return tipo;
	}

	
	
}