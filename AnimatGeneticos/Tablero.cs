using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatGeneticos{
    class Tablero {
        enum valorCasilla { bloque, izquieda, derecha, arriba, abajo, bomba, animat};
        List<valorCasilla>[,] tablero;
        int N, M;
        void Main(String[] args) {
            N = int.Parse(args[0]);
            M = int.Parse(args[1]);
            poblateTablero();
        }
        void poblateTablero() {
            tablero = new List<valorCasilla>[N,M];
            Random generadorRandom = new Random();
            int contadorBombas = 0;
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
                        if (i % 4 == 0 && j<M-1) {
                            tablero[i, j].Add(valorCasilla.derecha);
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
                        if (j % 4 == 0 && i>0) {
                            tablero[i, j].Add(valorCasilla.arriba);

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
                if (!tablero[x, y].Contains(valorCasilla.bloque)) {
                    tablero[x, y].Add(valorCasilla.bomba);
                    contadorBombas++;
                }
            }
        }
    }
}
