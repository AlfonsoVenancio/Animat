using System;
using System.Collections.Generic;

namespace AnimatGeneticos
{
    public class Tablero {
        public enum valorCasilla { bloque, izquierda, derecha, arriba, abajo, bomba, animat};
        public List<valorCasilla>[,] tablero;
        public int N, M;
        public long cambiosDeEstado;

        public Tablero(int N,int M, int noAnimats)
        {
            this.N = N;
            this.M = M;
            this.cambiosDeEstado = 0;
            PoblateTablero(noAnimats);
        }

        public bool Resuelto()
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

        public void ActualizaTablero()
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
                                //Busca Derecha
                                if (tablero[i, j + 1].Contains(valorCasilla.bomba) && !tablero[i, j + 1].Contains(valorCasilla.animat)) {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i, j + 1].Add(valorCasilla.animat);
                                    tableroActualizado[i, j + 1].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Abajo
                                if (tablero[i + 1, j].Contains(valorCasilla.bomba) && !tablero[i + 1, j].Contains(valorCasilla.animat)) {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i + 1, j].Add(valorCasilla.animat);
                                    tableroActualizado[i + 1, j].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Derecha
                                if (tablero[i, j + 2].Contains(valorCasilla.bomba) && !tablero[i, j + 1].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i, j + 1].Add(valorCasilla.animat);
                                    tableroActualizado[i, j + 1].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Abajo
                                if (tablero[i + 2, j].Contains(valorCasilla.bomba) && !tablero[i + 1, j].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i + 1, j].Add(valorCasilla.animat);
                                    tableroActualizado[i + 1, j].Remove(valorCasilla.bomba);
                                    continue;
                                }
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
                                //Busca Abajo
                                if (tablero[i + 1, j].Contains(valorCasilla.bomba) && !tablero[i + 1, j].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i + 1, j].Add(valorCasilla.animat);
                                    tableroActualizado[i + 1, j].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Izquierda
                                if (tablero[i, j - 1].Contains(valorCasilla.bomba) && !tablero[i, j - 1].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i, j - 1].Add(valorCasilla.animat);
                                    tableroActualizado[i, j - 1].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Abajo
                                if (tablero[i + 2, j].Contains(valorCasilla.bomba) && !tablero[i + 1, j].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i + 1, j].Add(valorCasilla.animat);
                                    tableroActualizado[i + 1, j].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Izquierda
                                if (tablero[i, j - 2].Contains(valorCasilla.bomba) && !tablero[i, j - 1].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i, j - 1].Add(valorCasilla.animat);
                                    tableroActualizado[i, j - 1].Remove(valorCasilla.bomba);
                                    continue;
                                }
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
                                //Busca Derecha
                                if (tablero[i, j + 1].Contains(valorCasilla.bomba) && !tablero[i, j + 1].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i, j + 1].Add(valorCasilla.animat);
                                    tableroActualizado[i, j + 1].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Arriba
                                if (tablero[i-1,j].Contains(valorCasilla.bomba) && !tablero[i - 1, j].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i - 1, j].Add(valorCasilla.animat);
                                    tableroActualizado[i - 1, j].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Derecha
                                if (tablero[i, j + 2].Contains(valorCasilla.bomba) && !tablero[i, j + 1].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i, j + 1].Add(valorCasilla.animat);
                                    tableroActualizado[i, j + 1].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Arriba
                                if (tablero[i - 2, j].Contains(valorCasilla.bomba) && !tablero[i - 1, j].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i - 1, j].Add(valorCasilla.animat);
                                    tableroActualizado[i - 1, j].Remove(valorCasilla.bomba);
                                    continue;
                                }

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
                                //Busca Arriba
                                if (tablero[i - 1, j].Contains(valorCasilla.bomba) && !tablero[i - 1, j].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i - 1, j].Add(valorCasilla.animat);
                                    tableroActualizado[i - 1, j].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Izquierda
                                if (tablero[i, j - 1].Contains(valorCasilla.bomba) && !tablero[i, j - 1].Contains(valorCasilla.animat)) {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i, j - 1].Add(valorCasilla.animat);
                                    tableroActualizado[i, j - 1].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Arriba
                                if (tablero[i - 2, j].Contains(valorCasilla.bomba) && !tablero[i - 1, j].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i - 1, j].Add(valorCasilla.animat);
                                    tableroActualizado[i - 1, j].Remove(valorCasilla.bomba);
                                    continue;
                                }
                                //Busca Izquierda
                                if (tablero[i, j - 2].Contains(valorCasilla.bomba) && !tablero[i, j - 1].Contains(valorCasilla.animat))
                                {
                                    tableroActualizado[i, j].Remove(valorCasilla.animat);
                                    tableroActualizado[i, j - 1].Add(valorCasilla.animat);
                                    tableroActualizado[i, j - 1].Remove(valorCasilla.bomba);
                                    continue;
                                }
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
            cambiosDeEstado++;
            tablero = tableroActualizado;
        }

        public string ImprimeTablero()
        {
            string ans = "";
            int i, j;
            for (i = 0; i < N; i++)
            {
                for (j = 0; j < M; j++)
                {
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

        public bool Biyeccion(int i, int j)
        {
            return (tablero[i, j].Contains(valorCasilla.abajo) && tablero[i, j].Contains(valorCasilla.izquierda)) ||
                 (tablero[i, j].Contains(valorCasilla.abajo) && tablero[i, j].Contains(valorCasilla.derecha)) ||
                 (tablero[i, j].Contains(valorCasilla.arriba) && tablero[i, j].Contains(valorCasilla.izquierda)) ||
                 (tablero[i, j].Contains(valorCasilla.arriba) && tablero[i, j].Contains(valorCasilla.derecha));
        }

        public void PoblateTablero(int noAnimats)
        {
            tablero = new List<valorCasilla>[N,M];
            Random generadorRandom = new Random();
            int contadorBombas = 0, contadorAnimats = 0;
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
            int x, y;
            //Bombas      
            while (contadorBombas < N * M * 0.3) { 
                x = generadorRandom.Next(0, N);
                y = generadorRandom.Next(0, M);
                if (!tablero[x, y].Contains(valorCasilla.bloque) && !tablero[x,y].Contains(valorCasilla.bomba)) {
                    tablero[x, y].Add(valorCasilla.bomba);
                    contadorBombas++;
                }
            }
            //Animats
            while(contadorAnimats < noAnimats)
            {
                x = generadorRandom.Next(0, N);
                y = generadorRandom.Next(0, M);
                if (!tablero[x, y].Contains(valorCasilla.bloque) && !tablero[x, y].Contains(valorCasilla.bomba)
                    && !tablero[x, y].Contains(valorCasilla.animat))
                {
                    tablero[x, y].Add(valorCasilla.animat);
                    contadorAnimats++;
                }
            }
        }
    }
}

