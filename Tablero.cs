// Nombre y apellidos
// Laboratorio, Puesto

using Listas;

namespace takuzu
{
    public class Tablero
    {
        const bool DEBUG = true;

        int N;                                // lado de la cuadrícula
        enum Casilla { Cero, Uno, Vacio };        // contenido de las casillas      
        Casilla[,] mat;                      // cuadrícula de juego
        bool[,] fijos;                       // 0s y 1s fijos (iniciales)      
        struct Coor { public int x, y; }       // tipo para coordenadas 2D 
        Coor pos;                             // posición del cursor     
                                              // métodos pedidos

        public Tablero(int tam, string[] lineas)
        {
            N = tam;
            pos = new()
            {
                x = 0,
                y = 0
            };
            mat = new Casilla[N, N];
            fijos = new bool[N, N];

            for (int j = 0; j < N; j++) // vertical
            {
                for (int i = 0; i < N; i++) // horizontal
                {
                    Casilla c; // casilla a añadir
                    bool b; // booleano a añadir

                    switch (lineas[i][j]) // según el valor de la línea en la posición correspondiente
                    {
                        case '1':
                            c = Casilla.Uno; // es un uno
                            b = true; // si es fijo
                            break;
                        case '0':
                            c = Casilla.Cero; // es un cero
                            b = true; // si es fijo
                            break;
                        default:
                            c = Casilla.Vacio;
                            b = false; // no es fijo
                            break;
                    }

                    mat[j, i] = c; // asignación
                    fijos[j, i] = b;
                }
            }
        }

        public void Escribe()
        {
            Console.Clear();
            for (int j = 0; j < N; j++) // vertical
            {
                for (int i = 0; i < N; i++) // horizontal
                {
                    char ch = ' ';
                    switch (mat[i, j]) // caracteres según casilla
                    {
                        case Casilla.Uno:
                            ch = '1';
                            break;
                        case Casilla.Cero:
                            ch = '0';
                            break;
                        case Casilla.Vacio:
                            ch = '.';
                            break;
                    }

                    if (fijos[i, j] == true) // colores según true o false
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write(" " + ch); // escritura
                }
                Console.Write('\n'); // salto de línea
            }
            if (DEBUG)
            {
                Console.WriteLine("CURSOR: x " + pos.x + ", y " + pos.y);
                Console.WriteLine("ESTALLENO: " + EstaLleno());
            }

            Console.SetCursorPosition(pos.x * 2 + 1, pos.y); // cursor
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void ProcesaInput(char c)
        {
            bool fijo = fijos[pos.x, pos.y];
            switch (c)
            {
                case 'u':
                    if (pos.y > 0) pos.y--;
                    break;
                case 'd':
                    if (pos.y < N - 1) pos.y++;
                    break;
                case 'l':
                    if (pos.x > 0) pos.x--;
                    break;
                case 'r':
                    if (pos.x < N - 1) pos.x++;
                    break;

                case '1':
                    if (!fijo) mat[pos.x, pos.y] = Casilla.Uno;
                    break;
                case '0':
                    if (!fijo) mat[pos.x, pos.y] = Casilla.Cero;
                    break;
                case '.':
                    if (!fijo) mat[pos.x, pos.y] = Casilla.Vacio;
                    break;
            }
        }

        public bool EstaLleno() // mejorable pero puf joder, no se que coño he hecho
        {
            int i,
                j = 0;
            bool encontrado = false;

            while (j < N && !encontrado)
            {
                i = 0;
                while (i < N && !encontrado)
                {
                    if (mat[i, j] == Casilla.Vacio)
                        encontrado = true;
                    else
                        i++;
                }
                j++;
            }
            return !encontrado;
        }

        private void SacaFilCol(int i, out Casilla[] fil, out Casilla[] col) 
        {
            fil = new Casilla[N];
            col = new Casilla[N];

            for (int j = 0; j < N; j++)
            {
                fil[j] = mat[i, j];
                col[j] = mat[j, i];
                //Console.WriteLine(fil[j] + " " + col[j]);
            }
        }

        private bool TresSeguidos(Casilla[] lin) 
        {
            int i = 0;
            while (i + 2 < lin.Length && lin[i] != lin[i + 1] && lin[i] != lin[i + 2])
            {
                i++;
            }
            return i + 2 < N;
        }

        private bool IgCerosUnos(Casilla[] lin)
        {
            int ceros = 0,
                unos = 0;

            for (int i = 0; i < N; i++)
            {
                if (lin[i] == Casilla.Uno) unos++;
                else if (lin[i] == Casilla.Cero) ceros++;
            }

            return ceros == unos;
        }

        public void BuscaIncorrectas(out Lista fils, out Lista cols)
        {
            fils = new Lista();
            cols = new Lista();
            for (int i = 0; i < N; i++)
            {
                SacaFilCol(i, out Casilla[] fil, out Casilla[] col);
                if (TresSeguidos(fil) && !IgCerosUnos(fil)) fils.InsertaPri(i);
                if (TresSeguidos(col) && !IgCerosUnos(col)) cols.InsertaPri(i);
            }
        }
    }
}
