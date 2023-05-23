// Cynthia Tristán
// 420, 69

namespace Listas
{
    public class Lista
    {
        class Nodo
        {
            public int dato;
            public Nodo sig; // enlace al siguiente nodo
                             // constructoras
            public Nodo(int e) { dato = e; sig = null; }
            public Nodo(int e, Nodo n) { dato = e; sig = n; }
        }
        // atributos de la lista enlazada: referencia al primero y al último
        Nodo pri; // DONDE??? DONDE ESTÁ LA REFERENCIA AL ÚLTIMO??????

        // constructora de listas
        public Lista() { pri = null; }


        // version recursiva de SacaLista
        public string SacaListaRec() // vale, me he dado cuenta de que no he entendido
                                               // bien el enunciado y hay que hacer yet another método aparte de este?
        {
            return SacaListaUltraRecursiva(pri, "");
        }

        private string SacaListaUltraRecursiva(Nodo n, string s) // este ^^
        {
            if (n == null) return s;
            else return SacaListaUltraRecursiva(n.sig, s += n.dato.ToString() + " ");
        }

        public bool EsVacia() { return pri == null; }

        public void InsertaPri(int x)
        {
            Nodo aux = new Nodo(x);
            aux.sig = pri;
            pri = aux;
        }

        public void EliminaPri()
        {
            if (pri == null) throw new Exception("EliminaPri");
            else pri = pri.sig;
        }

        public int DamePri()
        {
            if (pri == null) throw new Exception("DamePri");
            else return pri.dato;
        }

        public string SacaLista()
        {
            Nodo aux = pri;
            string sal = "";
            while (aux != null)
            {
                sal += aux.dato.ToString() + " ";
                aux = aux.sig;
            }
            return sal;
        }

    }
}

