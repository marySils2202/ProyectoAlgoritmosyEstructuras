using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoED.ProyectoDS
{
    public class ArbolAVL
    {
        public NodoAVL raiz;

        public class NodoAVL
        {
            public Employee empleado;
            public NodoAVL izquierdo;
            public NodoAVL derecho;
            public int altura;

            public NodoAVL(Employee empleado)
            {
                this.empleado = empleado;
                this.altura = 1; 
            }
        }

        private int ObtenerAltura(NodoAVL nodo)
        {
            return nodo == null ? 0 : nodo.altura;
        }

        private int ObtenerBalance(NodoAVL nodo)
        {
            return nodo == null ? 0 : ObtenerAltura(nodo.izquierdo) - ObtenerAltura(nodo.derecho);
        }

        private NodoAVL RotarDerecha(NodoAVL y)
        {
            NodoAVL x = y.izquierdo;
            NodoAVL T2 = x.derecho;

          
            x.derecho = y;
            y.izquierdo = T2;

            
            y.altura = Math.Max(ObtenerAltura(y.izquierdo), ObtenerAltura(y.derecho)) + 1;
            x.altura = Math.Max(ObtenerAltura(x.izquierdo), ObtenerAltura(x.derecho)) + 1;

            return x; 
        }

        private NodoAVL RotarIzquierda(NodoAVL x)
        {
            NodoAVL y = x.derecho;
            NodoAVL T2 = y.izquierdo;

         
            y.izquierdo = x;
            x.derecho = T2;

           
            x.altura = Math.Max(ObtenerAltura(x.izquierdo), ObtenerAltura(x.derecho)) + 1;
            y.altura = Math.Max(ObtenerAltura(y.izquierdo), ObtenerAltura(y.derecho)) + 1;

            return y; 
        }

        public void Insertar(Employee empleado)
        {
            raiz = InsertarRecursivo(raiz, empleado);
        }

        private NodoAVL InsertarRecursivo(NodoAVL nodo, Employee empleado)
        {
            if (nodo == null)
                return new NodoAVL(empleado);

            if (string.Compare(empleado.nombreE, nodo.empleado.nombreE) < 0)
                nodo.izquierdo = InsertarRecursivo(nodo.izquierdo, empleado);
            else if (string.Compare(empleado.nombreE, nodo.empleado.nombreE) > 0)
                nodo.derecho = InsertarRecursivo(nodo.derecho, empleado);
            else
                throw new Exception("No se permiten duplicados en el árbol AVL.");

          
            nodo.altura = 1 + Math.Max(ObtenerAltura(nodo.izquierdo), ObtenerAltura(nodo.derecho));

        
            int balance = ObtenerBalance(nodo);

          
            if (balance > 1 && string.Compare(empleado.nombreE, nodo.izquierdo.empleado.nombreE) < 0)
                return RotarDerecha(nodo);

            if (balance < -1 && string.Compare(empleado.nombreE, nodo.derecho.empleado.nombreE) > 0)
                return RotarIzquierda(nodo);

            if (balance > 1 && string.Compare(empleado.nombreE, nodo.izquierdo.empleado.nombreE) > 0)
            {
                nodo.izquierdo = RotarIzquierda(nodo.izquierdo);
                return RotarDerecha(nodo);
            }

            if (balance < -1 && string.Compare(empleado.nombreE, nodo.derecho.empleado.nombreE) < 0)
            {
                nodo.derecho = RotarDerecha(nodo.derecho);
                return RotarIzquierda(nodo);
            }

            return nodo;
        }

        public void Eliminar(string nombre)
        {
            raiz = EliminarRecursivo(raiz, nombre);
        }

        private NodoAVL EliminarRecursivo(NodoAVL nodo, string nombre)
        {
            if (nodo == null)
                return nodo;

           
            if (string.Compare(nombre, nodo.empleado.nombreE) < 0)
                nodo.izquierdo = EliminarRecursivo(nodo.izquierdo, nombre);
            else if (string.Compare(nombre, nodo.empleado.nombreE) > 0)
                nodo.derecho = EliminarRecursivo(nodo.derecho, nombre);
            else
            {
             
                if ((nodo.izquierdo == null) || (nodo.derecho == null))
                {
                    NodoAVL temp = nodo.izquierdo ?? nodo.derecho;

                 
                    if (temp == null)
                    {
                        temp = nodo;
                        nodo = null;
                    }
                    else 
                    {
                        nodo = temp;
                    }
                }
                else
                {
                    NodoAVL temp = ObtenerMinimoValor(nodo.derecho);

                  
                    nodo.empleado = temp.empleado;

                  
                    nodo.derecho = EliminarRecursivo(nodo.derecho, temp.empleado.nombreE);
                }
            }

           
            if (nodo == null)
                return nodo;

           
            nodo.altura = Math.Max(ObtenerAltura(nodo.izquierdo), ObtenerAltura(nodo.derecho)) + 1;

          
            int balance = ObtenerBalance(nodo);

           
            if (balance > 1 && ObtenerBalance(nodo.izquierdo) >= 0)
                return RotarDerecha(nodo);

            if (balance > 1 && ObtenerBalance(nodo.izquierdo) < 0)
            {
                nodo.izquierdo = RotarIzquierda(nodo.izquierdo);
                return RotarDerecha(nodo);
            }

           
            if (balance < -1 && ObtenerBalance(nodo.derecho) <= 0)
                return RotarIzquierda(nodo);

           
            if (balance < -1 && ObtenerBalance(nodo.derecho) > 0)
            {
                nodo.derecho = RotarDerecha(nodo.derecho);
                return RotarIzquierda(nodo);
            }

            return nodo;
        }

        private NodoAVL ObtenerMinimoValor(NodoAVL nodo)
        {
            NodoAVL actual = nodo;
            while (actual.izquierdo != null)
                actual = actual.izquierdo;

            return actual;
        }
    public void RecorrerInOrden(NodoAVL nodo, Action<Employee> accion)
        {
            if (nodo == null) return;

            RecorrerInOrden(nodo.izquierdo, accion);
            accion(nodo.empleado);
            RecorrerInOrden(nodo.derecho, accion);
        }

        public Employee? Buscar(string nombre)
        {
            return BuscarRecursivo(raiz, nombre);
        }

        private Employee? BuscarRecursivo(NodoAVL nodo, string nombre)
        {
            if (nodo == null)
                return null;

            if (nombre == nodo.empleado.nombreE)
                return nodo.empleado;

            if (string.Compare(nombre, nodo.empleado.nombreE) < 0)
                return BuscarRecursivo(nodo.izquierdo, nombre);

            return BuscarRecursivo(nodo.derecho, nombre);
        }
    }
}
