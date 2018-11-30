﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimatGeneticos
{
    public partial class Form1 : Form
    {
        PictureBox[,] pictures;
        Tablero tablero;
        int N;
        int M;
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
       

        public Form1(int N, int M, int A)
        {
            this.N = N;
            this.M = M;
            //Max 15*27
            tablero = new Tablero(N, M, A);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InitializeComponent();
            InitializePictures();
            UpdatePictures();
            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 500;
            myTimer.Start();
            //Play();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            tablero.ActualizaTablero();
            UpdatePictures();
            if (tablero.Resuelto())
            {
                myTimer.Stop();
                MessageBox.Show("Los animats desactivaron todas las bombas en " +tablero.cambiosDeEstado+ " movimientos");
                this.Dispose();
            }
        }

        private void Play()
        {
            while (!tablero.Resuelto())
            {
                tablero.ActualizaTablero();
                UpdatePictures();
                //Thread.Sleep(500);
            }
        }

        private void UpdatePictures()
        {
            for(int i = 0; i < N; i++)
            {
                for(int j = 0; j < M; j++)
                {
                    
                    if (tablero.tablero[i, j].Contains(Tablero.valorCasilla.animat))
                    {
                        pictures[i, j].Image = Image.FromFile("animat.png");
                        continue;
                    }
                    if (tablero.tablero[i, j].Contains(Tablero.valorCasilla.bomba))
                    {
                        pictures[i, j].Image = Image.FromFile("bomba.png");
                        continue;
                    }
                    if (tablero.tablero[i, j].Contains(Tablero.valorCasilla.bloque))
                    {
                        pictures[i, j].Image = Image.FromFile("block.png");
                        continue;
                    }
                    pictures[i, j].Image = null;
                }
            }
        }

        private void InitializePictures()
        {
            pictures = new PictureBox[N, M];
            int startingCoordinateX = 0;
            int startingCoordinateY = 0;
            int size = 50;

            for(int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    pictures[i, j] = new PictureBox
                    {
                        Name = "pictureBox" + i + "," + j,
                        Size = new Size(size, size),
                        Location = new Point(startingCoordinateX + j * size, startingCoordinateY + i * size),
                        //Image = Image.FromFile("bomba.png"),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                    };
                    Controls.Add(pictures[i, j]);
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
    }
}
