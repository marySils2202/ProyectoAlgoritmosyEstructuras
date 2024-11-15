using ProyectoED.ProyectoDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoED
{
    public class NodoArbol
    {
        public Employee Dato { get; set; }
        public NodoArbol derecho { get; set; }
        public NodoArbol izquierdo { get; set; }
        public NodoArbol padre { get; set; }

        public NodoArbol(Employee empleado)
        {
            Dato = empleado;
            derecho = izquierdo = null;
        }
    }

    internal class ArbolBinario
    {
        public NodoArbol raiz;

        public ArbolBinario()
        {
            raiz = null;
        }

        public void Insertar(Employee empleado)
        {
            if (raiz == null)
            {
                raiz = new NodoArbol(empleado);
            }
            else
            {
                Insertar(empleado, raiz);
            }
        }

        public void Insertar(Employee empleado, NodoArbol nodo)
        {
            if (string.Compare(empleado.nombreE, nodo.Dato.nombreE) < 0)
            {
                if (nodo.izquierdo == null)
                {
                    nodo.izquierdo = new NodoArbol(empleado) { padre = nodo };
                }
                else
                {
                    Insertar(empleado, nodo.izquierdo);
                }
            }
            else
            {
                if (nodo.derecho == null)
                {
                    nodo.derecho = new NodoArbol(empleado) { padre = nodo };
                }
                else
                {
                    Insertar(empleado, nodo.derecho);
                }
            }
        }

        public Employee? Buscar(string nombre)
        {
            return Buscar(nombre, raiz);
        }

        public Employee? Buscar(string nombre, NodoArbol nodo)
        {
            if (nodo == null) return null;
            if (nombre == nodo.Dato.nombreE) return nodo.Dato;

            if (string.Compare(nombre, nodo.Dato.nombreE) < 0)
                return Buscar(nombre, nodo.izquierdo);
            else
                return Buscar(nombre, nodo.derecho);
        }


        public void Eliminar(string nombre)
        {
            raiz = Eliminar(nombre, raiz);
        }

        public NodoArbol Eliminar(string nombre, NodoArbol nodo)
        {
            if (nodo == null) return nodo;

            if (string.Compare(nombre, nodo.Dato.nombreE) < 0)
            {
                nodo.izquierdo = Eliminar(nombre, nodo.izquierdo);
            }
            else if (string.Compare(nombre, nodo.Dato.nombreE) > 0)
            {
                nodo.derecho = Eliminar(nombre, nodo.derecho);
            }
            else
            {
                if (nodo.izquierdo == null && nodo.derecho == null)
                {
                    return null;
                }
                else if (nodo.izquierdo == null)
                {
                    return nodo.derecho;
                }
                else if (nodo.derecho == null)
                {
                    return nodo.izquierdo;
                }
                else
                {
                    NodoArbol sucesor = MinimoValor(nodo.derecho);
                    nodo.Dato = sucesor.Dato;
                    nodo.derecho = Eliminar(sucesor.Dato.nombreE, nodo.derecho);
                }
            }

            return nodo;
        }

        public NodoArbol MinimoValor(NodoArbol nodo)
        {
            NodoArbol actual = nodo;
            while (actual.izquierdo != null)
            {
                actual = actual.izquierdo;
            }
            return actual;
        }

        public void RecorrerInOrden(NodoArbol nodo, Action<Employee> accion)
        {
            if (nodo != null)
            {
                RecorrerInOrden(nodo.izquierdo, accion);
                accion(nodo.Dato);
                RecorrerInOrden(nodo.derecho, accion);
            }
        }
    }

}
