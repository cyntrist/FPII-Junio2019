// Cynthia Tristán
// 420, 69

using Listas;

namespace takuzu
{
    class Program
    {
        const string FICHERO = @"takuzu6x6";
        static void Main(string[] args)
        {
            Tablero tab;
            char ch = ' ';

            if (File.Exists(FICHERO))
            {
                Lee(FICHERO, out int tam, out string[] lineas);
                tab = new(tam, lineas);
            }
            else
            { 
                Ejemplo(out int tam, out string[] lineas);
                tab = new(tam, lineas);
            }

            tab.Escribe(); // render inicial
            while (!tab.EstaLleno() && ch != 'q') // bucle principal
            {
                ch = LeeInput(); // input
                tab.ProcesaInput(ch); // lógica
                tab.Escribe(); // re render
            }

            Console.Clear(); // flush
            tab.BuscaIncorrectas(out Lista fils, out Lista cols); // a ver a ver
            MuestraErrores(fils, cols); // si existen receipts los saca
        }


        // método pedido
        public static void Lee(string file, out int tam, out string[] lineas)
        { // no se que coño es esto dont @ me gracias / he puesto las excepciones aquí por dónde las nombran en el enunciado pero idk
            StreamReader sr = null!;
            try
            {
                sr = new(file);

                lineas = new string[File.ReadAllLines(file).Length];
                int i = 0;
                while (!sr.EndOfStream)
                {
                    lineas[i] = sr.ReadLine()!;
                    i++;
                }

                // tam = lineas.Length; no pongo esto porque es posible que haya líneas vacías al final
                if (lineas.Length > 0) 
                    tam = lineas[0].Length;
                else  // por si no consigue leer lineas, asignarlo igualmente? idk bro
                    tam = -1;
            }
            catch (FileNotFoundException) { Console.WriteLine("ERROR: No existe archivo."); tam = -1; lineas = null; }
            catch (TypeLoadException) { Console.WriteLine("ERROR: El formato es incorrecto."); tam = -1; lineas = null; } // me he inventado esta excepcion yo que se
            catch (Exception e) { Console.WriteLine($"ERROR: " + e.Message); tam = -1; lineas = null; } // genérica
            finally { sr?.Close(); }
        }

        // método que me he sacado de la chistera para legibilildad de main
        static private void MuestraErrores(Lista filas, Lista columnas)
        {
            if (!filas.EsVacia()) Console.WriteLine("Errores en las filas " + filas.SacaListaRec());
            if (!columnas.EsVacia()) Console.WriteLine("Errores en las columnas " + columnas.SacaListaRec());
        }

        // otro que me he sacado pero esta vez de la caja donde he serrado a una señora en dos
        static private void Ejemplo(out int tam, out string[] lineas)
        {
            lineas = new string[] {".1.0",
                            "..0.",
                            ".0..",
                            "11.0",
                            };
            tam = 4;
        }

        static char LeeInput()
        {
            char d = ' ';
            while (d == ' ')
            {
                if (Console.KeyAvailable)
                {
                    string tecla = Console.ReadKey().Key.ToString();
                    switch (tecla)
                    {
                        case "LeftArrow": d = 'l'; break;  // izquierda
                        case "UpArrow": d = 'u'; break;  // arriba
                        case "RightArrow": d = 'r'; break;  // derecha
                        case "DownArrow": d = 'd'; break;  // abajo
                        case "D0": d = '0'; break;  // dígito 0
                        case "D1": d = '1'; break;  // dígito 1
                        case "NumPad0": d = '0'; break;  // dígito 0
                        case "NumPad1": d = '1'; break;  // dígito 1
                        case "Spacebar": d = '.'; break;  // casilla vacia                    
                        case "OemPeriod": d = '.'; break;  // casilla vacia                    
                        case "Escape": d = 'q'; break;  // terminar
                        case "Y": d = 'y'; break;
                        default: d = ' '; break;
                    }
                }
            }
            return d;
        }
    }
}
