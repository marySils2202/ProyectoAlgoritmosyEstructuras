using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoED
{
    public class Grafo
    {
       private int[,] matrizAdyacencia;
            private int numeroNodos;

            public Grafo(int numNodos)
            {
                numeroNodos = numNodos;
                matrizAdyacencia = new int[numNodos, numNodos];

               
                for (int i = 0; i < numNodos; i++)
                {
                    for (int j = 0; j < numNodos; j++)
                    {
                        matrizAdyacencia[i, j] = i == j ? 0 : int.MaxValue / 2; 
                    }
                }
            }

            public void AgregarArista(int origen, int destino, int peso)
            {
                matrizAdyacencia[origen, destino] = peso;
                matrizAdyacencia[destino, origen] = peso; 
            }

            public int[,] ObtenerMatrizAdyacencia()
            {
                return matrizAdyacencia;
            }

            public int NumeroNodos()
            {
                return numeroNodos;
            }
        public int[] Dijkstra(int origen)
        {
            int[] distancias = new int[numeroNodos];
            bool[] visitado = new bool[numeroNodos];

           
            for (int i = 0; i < numeroNodos; i++)
            {
                distancias[i] = int.MaxValue / 2;
                visitado[i] = false;
            }
            distancias[origen] = 0;

            for (int i = 0; i < numeroNodos - 1; i++)
            {
               
                int min = int.MaxValue, indiceMin = -1;
                for (int j = 0; j < numeroNodos; j++)
                {
                    if (!visitado[j] && distancias[j] < min)
                    {
                        min = distancias[j];
                        indiceMin = j;
                    }
                }

              
                visitado[indiceMin] = true;

             
                for (int k = 0; k < numeroNodos; k++)
                {
                    if (!visitado[k] && matrizAdyacencia[indiceMin, k] != int.MaxValue / 2 &&
                        distancias[indiceMin] + matrizAdyacencia[indiceMin, k] < distancias[k])
                    {
                        distancias[k] = distancias[indiceMin] + matrizAdyacencia[indiceMin, k];
                    }
                }
            }

            return distancias;
        }

        public int[,] FloydWarshall()
        {
            int[,] distancias = (int[,])matrizAdyacencia.Clone();

            for (int k = 0; k < numeroNodos; k++)
            {
                for (int i = 0; i < numeroNodos; i++)
                {
                    for (int j = 0; j < numeroNodos; j++)
                    {
                        if (distancias[i, k] + distancias[k, j] < distancias[i, j])
                        {
                            distancias[i, j] = distancias[i, k] + distancias[k, j];
                        }
                    }
                }
            }

            return distancias;
        }
        public bool[,] Warshall()
        {
            bool[,] conectividad = new bool[numeroNodos, numeroNodos];
            for (int i = 0; i < numeroNodos; i++)
            {
                for (int j = 0; j < numeroNodos; j++)
                {
                    conectividad[i, j] = matrizAdyacencia[i, j] != int.MaxValue / 2;
                }
            }
            for (int k = 0; k < numeroNodos; k++)
            {
                for (int i = 0; i < numeroNodos; i++)
                {
                    for (int j = 0; j < numeroNodos; j++)
                    {
                        conectividad[i, j] = conectividad[i, j] || (conectividad[i, k] && conectividad[k, j]);
                    }
                }
            }
            return conectividad;
        }




    }


}

