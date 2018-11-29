using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnimatGeneticos{
    class Tablero {
        enum valorCasilla { bloque, izquierda, derecha, arriba, abajo, bomba, animat};
        static List<valorCasilla>[,] tablero;
        static int N, M;

        static void Main(String[] args) {
            //N = int.Parse(args[0]);
            //M = int.Parse(args[1]);
            N = 7;
            M = 7;
            PoblateTablero();
            //for (int i = 0; i < N; i++) {
            //    Console.WriteLine("--------------CAMBIO DE FILA: " + i + "-----------------");
            //    for (int j = 0; j < M; j++) {
            //        Console.WriteLine("--------------CAMBIO DE COLUMNA: " + j + "-----------------");
            //        foreach (valorCasilla valor in tablero[i, j]) {
            //            Console.WriteLine(valor);
            //        }
            //    }
            //}

            tablero[3, 6].Add(valorCasilla.animat);
            tablero[1, 2].Add(valorCasilla.animat);
            tablero[6, 2].Add(valorCasilla.animat);
            Console.WriteLine(ImprimeTablero());

            while (true)
            {
                ActualizaTablero();
                Console.WriteLine(ImprimeTablero());
                Thread.Sleep(500);
                if (Resuelto())
                {
                    Console.WriteLine("Resuelto");
                    break;
                }

            }
            Console.Read();
        }

        static bool Resuelto()
        {
            int i, j;
            for (i = 0; i < N; i++)
                for (j = 0; j < M; j++)
                    if (tablero[i, j].Contains(valorCasilla.bomba))
                    {
                        return false;
                    }

            return true;
        }

        static void ActualizaTablero()
        {
            var tableroActualizado = new List<valorCasilla>[N,M];
            int i, j;

            //Copia profunda
            for (i = 0; i < N; i++)
                for (j = 0; j < M; j++)
                    tableroActualizado[i, j] = new List<valorCasilla>(tablero[i, j]);

            for (i = 0; i < N; i++)
                for(j=0; j<M; j++)
                {
                    var actual = tablero[i, j];
                    //En caso de que sea un animat se actualizara la posición
                    if (actual.Contains(valorCasilla.animat))
                    {
                        //Hay un solo movimiento disponible, se hara a menos que haya un animat en la posicion inmediata
                        if (!Biyeccion(i, j)){
                            if(actual.Contains(valorCasilla.izquierda)  && !tablero[i, j - 1].Contains(valorCasilla.animat))
                            {
                                tableroActualizado[i, j].Remove(valorCasilla.animat);
                                tableroActualizado[i , j - 1].Add(valorCasilla.animat);
                                tableroActualizado[i, j - 1].Remove(valorCasilla.bomba);
                            }

                            if (actual.Contains(valorCasilla.abajo)  && !tablero[i + 1, j].Contains(valorCasilla.animat))
                            {
                                tableroActualizado[i, j].Remove(valorCasilla.animat);
                                tableroActualizado[i + 1, j].Add(valorCasilla.animat);
                                tableroActualizado[i + 1, j].Remove(valorCasilla.bomba);
                            }
                            if (actual.Contains(valorCasilla.derecha) && !tablero[i, j + 1].Contains(valorCasilla.animat))
                            {
                                tableroActualizado[i, j].Remove(valorCasilla.animat);
                                tableroActualizado[i, j + 1].Add(valorCasilla.animat);
                                tableroActualizado[i, j + 1].Remove(valorCasilla.bomba);
                            }

                            if (actual.Contains(valorCasilla.arriba) && !tablero[i - 1, j].Contains(valorCasilla.animat))
                            {
                                tableroActualizado[i, j].Remove(valorCasilla.animat);
                                tableroActualizado[i - 1, j].Add(valorCasilla.animat);
                                tableroActualizado[i - 1, j].Remove(valorCasilla.bomba);
                            }
                        }
                        else
                        {
                            Random r = new Random();
                            double p = r.NextDouble();
                            if(actual.Contains(valorCasilla.abajo) && actual.Contains(valorCasilla.derecha))
                            {
                                if (p < .5)
                                {
                                    //abajo
                                    if (!tablero[i + 1, j].Contains(valorCasilla.animat))
                                    {
                                        tableroActualizado[i, j].Remove(valorCasilla.animat);
                                        tableroActualizado[i + 1, j].Add(valorCasilla.animat);
                                        tableroActualizado[i + 1, j].Remove(valorCasilla.bomba);
                                    }
                                }
                                else {
                                    //derecha
                                    if (!tablero[i, j + 1].Contains(valorCasilla.animat))
                                    {
                                        tableroActualizado[i, j].Remove(valorCasilla.animat);
                                        tableroActualizado[i, j + 1].Add(valorCasilla.animat);
                                        tableroActualizado[i, j + 1].Remove(valorCasilla.bomba);
                                    }
                                }
                            }
                            if (actual.Contains(valorCasilla.abajo) && actual.Contains(valorCasilla.izquierda))
                            {
                                if (p < .5)
                                {
                                    //abajo
                                    if (!tablero[i + 1, j].Contains(valorCasilla.animat))
                                    {
                                        tableroActualizado[i, j].Remove(valorCasilla.animat);
                                        tableroActualizado[i + 1, j].Add(valorCasilla.animat);
                                        tableroActualizado[i + 1, j].Remove(valorCasilla.bomba);
                                    }
                                }
                                else
                                {
                                    //izquierda
                                    if (!tablero[i, j - 1].Contains(valorCasilla.animat))
                                    {
                                        tableroActualizado[i, j].Remove(valorCasilla.animat);
                                        tableroActualizado[i, j - 1].Add(valorCasilla.animat);
                                        tableroActualizado[i, j - 1].Remove(valorCasilla.bomba);
                                    }
                                }
                            }
                            if (actual.Contains(valorCasilla.arriba) && actual.Contains(valorCasilla.derecha))
                            {
                                if (p < .5)
                                {
                                    //arriba
                                    if(!tablero[i - 1, j].Contains(valorCasilla.animat))
                                    {
                                        tableroActualizado[i, j].Remove(valorCasilla.animat);
                                        tableroActualizado[i - 1, j].Add(valorCasilla.animat);
                                        tableroActualizado[i - 1, j].Remove(valorCasilla.bomba);
                                    }
                                }
                                else
                                {
                                    //derecha
                                    if (!tablero[i, j + 1].Contains(valorCasilla.animat))
                                    {
                                        tableroActualizado[i, j].Remove(valorCasilla.animat);
                                        tableroActualizado[i, j + 1].Add(valorCasilla.animat);
                                        tableroActualizado[i, j + 1].Remove(valorCasilla.bomba);
                                    }
                                }
                            }
                            if (actual.Contains(valorCasilla.arriba) && actual.Contains(valorCasilla.izquierda))
                            {
                                if (p < .5)
                                {
                                    //arriba
                                    if (!tablero[i - 1, j].Contains(valorCasilla.animat))
                                    {
                                        tableroActualizado[i, j].Remove(valorCasilla.animat);
                                        tableroActualizado[i - 1, j].Add(valorCasilla.animat);
                                        tableroActualizado[i - 1, j].Remove(valorCasilla.bomba);
                                    }
                                }
                                else
                                {
                                    //izquierda
                                    if (!tablero[i, j - 1].Contains(valorCasilla.animat))
                                    {
                                        tableroActualizado[i, j].Remove(valorCasilla.animat);
                                        tableroActualizado[i, j - 1].Add(valorCasilla.animat);
                                        tableroActualizado[i, j - 1].Remove(valorCasilla.bomba);
                                    }
                                }
                            }
                        }
                    }
                }
            tablero = tableroActualizado;
        }

        static string ImprimeTablero()
        {
            string ans = "";
            int i, j;
            for (i = 0; i < N; i++)
            {
                for (j = 0; j < M; j++)
                {
                    //En caso de que sea un animat se actualizara la posición
                    if (tablero[i, j].Contains(valorCasilla.animat))
                    {
                        ans+='A';
                        continue;
                    }

                    if (tablero[i, j].Contains(valorCasilla.bomba))
                    {
                        ans += 'B';
                        continue;
                    }

                    if (tablero[i, j].Contains(valorCasilla.bloque))
                    {
                        ans += "X";
                        continue;
                    }

                    if(Biyeccion(i,j))
                    {
                        ans += " ";
                        continue;
                    }

                    if (tablero[i, j].Contains(valorCasilla.abajo))
                    {
                        ans += 'D';
                    }
                    if (tablero[i, j].Contains(valorCasilla.arriba))
                    {
                        ans += 'U';
                    }
                    if (tablero[i, j].Contains(valorCasilla.derecha))
                    {
                        ans += 'R';
                    }
                    if (tablero[i, j].Contains(valorCasilla.izquierda))
                    {
                        ans += 'L';
                    }
                }
                ans += '\n';
            }

            return ans;

        }

        static bool Biyeccion(int i, int j)
        {
            return (tablero[i, j].Contains(valorCasilla.abajo) && tablero[i, j].Contains(valorCasilla.izquierda)) ||
                 (tablero[i, j].Contains(valorCasilla.abajo) && tablero[i, j].Contains(valorCasilla.derecha)) ||
                 (tablero[i, j].Contains(valorCasilla.arriba) && tablero[i, j].Contains(valorCasilla.izquierda)) ||
                 (tablero[i, j].Contains(valorCasilla.arriba) && tablero[i, j].Contains(valorCasilla.derecha));
        }

        static void PoblateTablero()
        {
            tablero = new List<valorCasilla>[N,M];
            Random generadorRandom = new Random();
            int contadorBombas = 0;
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < M; j++) {
                    tablero[i, j] = new List<valorCasilla>();
                }
            }
            //Bloques
            for (int i = 1; i < N; i+=2) {
                for(int j = 1; j < M; j += 2) {
                    tablero[i, j].Add(valorCasilla.bloque);                
                }
            }
            //Horizontales
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < M; j++) {
                    if (i % 2 == 0) {
                        if (i % 4 == 0) {
                            if (j < M - 1) {
                                tablero[i, j].Add(valorCasilla.derecha);
                            }
                        }
                        else if(j>0){
                            tablero[i, j].Add(valorCasilla.izquierda);
                        }
                    }
                }
            }
            //Verticales
            for (int i = 0; i < N; i++) {
                for(int j = 0; j < M; j++) {
                    if (j % 2 == 0) {
                        if (j % 4 == 0) {
                            if (i > 0) {
                                tablero[i, j].Add(valorCasilla.arriba);
                            }
                        }else if (i < N - 1) {
                            tablero[i, j].Add(valorCasilla.abajo);
                        }
                    }
                }
            }
            //Bombas      
            while (contadorBombas < N * M * 0.3) { 
                int x = generadorRandom.Next(0, N);
                int y = generadorRandom.Next(0, M);
                if (!tablero[x, y].Contains(valorCasilla.bloque) && !tablero[x,y].Contains(valorCasilla.bomba)) {
                    tablero[x, y].Add(valorCasilla.bomba);
                    contadorBombas++;
                }
            }
        }
    }
}
