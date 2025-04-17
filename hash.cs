using System;


namespace Tarea_3
{
    public class HashCuco
    {
        (string?, bool)[] list_1;
        (string?, bool)[] list_2;
        int count;
        int cycles;

        public HashCuco(int count)
        {
            this.count = count;
            cycles = count * 2;
            list_1 = new (string?, bool)[count];
            list_2 = new (string?, bool)[count];
        }


        public (int, int) hash(string value)
        {
            int sum_character = 0;
            int negativ_vocals = 0;
            string vocals = "aeiouAEIOU";

            foreach (char character in value)
            {
                sum_character = (int)character + sum_character;
                if (vocals.Contains(character)) 
                {
                    negativ_vocals = negativ_vocals - (int)character;
                }
                else
                {
                    negativ_vocals = negativ_vocals + (int)character;
                }
            }

            sum_character = sum_character * (count / 20);
            negativ_vocals = Math.Abs(negativ_vocals) * (count / 20);

            sum_character = sum_character % count;
            negativ_vocals = negativ_vocals % count;
            
            return (sum_character, negativ_vocals);
        }


        public void Add(string value, bool current_list = true, int auxiliar_count = 0)
        {
            
            if (auxiliar_count >= cycles)
            {
                throw new NoPlaceExcepcion();
            }

            int index_1;
            int index_2;
            
            var tupla = hash(value);
            index_1 = tupla.Item1;
            index_2 = tupla.Item2;

            if (current_list)
            {            
                if ((list_1[index_1].Item2))
                {
                    Add(list_1[index_1].Item1, !current_list, auxiliar_count + 1);
                }
            }
            else
            {
                if ((list_2[index_2].Item2))
                {
                    Add(list_2[index_2].Item1, !current_list, auxiliar_count + 1);
                }
            }

            if (current_list)
            {
                list_1[index_1] = (value, true);

            }
            else
            {
                list_2[index_2] = (value, true);
            }
        }

        public string Serch(string value)
        {
            var tupla = Serching(value);
            if( tupla.Item1 == -1)
            {
                throw new NotFoundException();
            }
            if(tupla.Item2)
            {
                return list_1[tupla.Item1].Item1;
            }
            else
            {
                return list_2[tupla.Item1].Item1;
            }
        }
        public void Delete(string value)
        {
            var tupla = Serching(value);
            if( tupla.Item1 == -1)
            {
                throw new NotFoundException();
            }
            if(tupla.Item2)
            {
                list_1[tupla.Item1].Item2 = false;
            }
            else
            {
                list_2[tupla.Item1].Item2 = false;
            }
        }




        public (int, bool) Serching(string value, bool current_list = true)
        {  


            int index;

            var tupla = hash(value);
            index = tupla.Item1;
            List<(string?, bool)> list = new List<(string?, bool)>(list_1);  


            if (!current_list)
            {
                index = tupla.Item2;
                list = new List<(string?, bool)>(list_2);  
            }
        
            if (!(list[index].Item1 == null) && !(list[index].Item1 == value  && !(list[index].Item2)))
            {
                if (list[index].Item1 == value)
                {
                    return (index, current_list);
                }
                else if(current_list)
                {
                    return Serching(value, !current_list);
                }
                
            }
            return (-1, false);

        }

        //Funcion creada con ChatGPT para depuracion
        public void PrintTables()
        {
            Console.WriteLine("Tabla 1:");
            for (int i = 0; i < list_1.Length; i++)
            {
                Console.WriteLine($"[{i}] => {list_1[i].Item1} (Activo: {list_1[i].Item2})");
            }

            Console.WriteLine("Tabla 2:");
            for (int i = 0; i < list_2.Length; i++)
            {
                Console.WriteLine($"[{i}] => {list_2[i].Item1} (Activo: {list_2[i].Item2})");
            }

            Console.WriteLine("-----------------------------------------------------");
        }

    }
}
