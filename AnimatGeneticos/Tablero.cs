using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatGeneticos{
    class Tablero {
        enum valorCasilla { bloque, izquieda, derecha, arriba, abajo, bomba, animat};
        static List<valorCasilla>[,] tablero;
        static int N, M;

        static void Main(String[] args) {
            //N = int.Parse(args[0]);
            //M = int.Parse(args[1]);
            N = 7;
            M = 7;
            poblateTablero();
            for (int i = 0; i < N; i++) {
                Console.WriteLine("--------------CAMBIO DE FILA: " + i + "-----------------");
                for (int j = 0; j < M; j++) {
                    Console.WriteLine("--------------CAMBIO DE COLUMNA: " + j + "-----------------");
                    foreach (valorCasilla valor in tablero[i, j]) {
                        Console.WriteLine(valor);
                    }
                }
            }
        }
        static void poblateTablero() {
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
                            tablero[i, j].Add(valorCasilla.izquieda);
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
