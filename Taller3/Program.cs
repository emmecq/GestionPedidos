using System;

public class AplicacionNomina
{
    static void Main()
    {
        for (int i = 1; i <= 3; i++)
        {
            // Se cambian los siguientes nombres de variables para mayor claridad:
            // h por horas
            // t por tarifa
            // b por salarioBruto (horas * tarifa)
            // p por salarioNeto

            Console.Write($"Empleado {i} horas: ");
            double horas = double.Parse(Console.ReadLine()); 
            Console.Write($"Empleado {i} tarifa: ");
            double tarifa = double.Parse(Console.ReadLine());
            double salarioBruto = horas * tarifa;
            double salarioNeto = b * 0.92;
            Console.WriteLine($"Bruto {salarioBruto:C} Neto {salarioNeto:C}");
        }
    }

}