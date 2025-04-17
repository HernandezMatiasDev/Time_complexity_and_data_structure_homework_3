using System;
using System.Collections.Generic;

namespace Tarea_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> names = new List<string>
            {
                "Juan", "María", "Pedro", "Lucía", "Carlos",
                "Ana", "Javier", "Sofía", "Diego", "Camila"
            };

            HashCuco hash = new HashCuco(31);
            
            try
            {
                foreach (var name in names)
                {
                    hash.Add(name);     
                }
            }
            catch (NoPlaceExcepcion ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return; 
            }
            
            Console.WriteLine("Nombres insertados");
            hash.PrintTables();
            try
            {
                foreach (var name in names)
                {
                    hash.Delete(name);
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine("Error al eliminar: " + ex.Message);
            }
            
            Console.WriteLine("Nombres eliminados");
            hash.PrintTables();
        }
    }
}
