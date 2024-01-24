using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;

namespace ders10
{
    public partial class Form1 : Form
    {
        int height;
        int width;
        float alfa = 0, beta = 0;
        Bitmap curImage;

        public Form1()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();
            //opengl ilk islemler
            curImage = new Bitmap("disler.bmp");
            height = curImage.Height;
            width = curImage.Width;
            simpleOpenGlControl1.Size = new System.Drawing.Size(width, height);
            Gl.glViewport(0, 0, width, height);
            Gl.glOrtho(0,width,0,height,-1000,1000);
            Gl.glShadeModel(Gl.GL_SMOOTH);
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            int step = 10;
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glPushMatrix();
            Gl.glTranslatef(width / 2, height / 2, 0);
            Gl.glRotatef(alfa, 0, 1, 0);
            Gl.glRotatef(beta, 1, 0, 0);
            Gl.glTranslatef(-width / 2, -height / 2, 0);
            for (int x = 0; x < width - step; x += step)
            {
                for (int y = 0; y < height - step; y += step)
                {
                    Gl.glBegin(Gl.GL_QUADS);
                    SetVertex(x+step,y+step);
                    SetVertex(x, y + step);
                    SetVertex(x, y);
                    SetVertex(x + step, y);
                    Gl.glEnd();
                }
                
            }
            Gl.glPopMatrix();
        }

        private void simpleOpenGlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                alfa += 5;
            }
            if (e.KeyCode == Keys.A)
            {
                alfa -= 5;
            }
            if (e.KeyCode == Keys.S)
            {
                beta += 5;
            }
            if (e.KeyCode == Keys.W)
            {
                beta -= 5;
            }
            simpleOpenGlControl1.Refresh();
        }

        void SetVertex(int x, int y)
        {
            Color clr = curImage.GetPixel(x, height - y - 1);
            float brt = clr.GetBrightness();
            Gl.glColor3d(brt, brt, brt);
            Gl.glVertex3d(x,y,brt*50f);

        }

    }
}
