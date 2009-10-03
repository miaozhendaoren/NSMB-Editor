﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class Map16Picker : UserControl
    {
        Image map16Image;
        int selx = -1, sely = -1;
        int hovx = -1, hovy = -1;

        public Map16Picker()
        {
            InitializeComponent();
        }

        public delegate void TileSelectedd(int tile);
        public event TileSelectedd TileSelected;

        NSMBTileset t;

        public void SetTileset(NSMBTileset t)
        {
            this.t = t;
            map16Image = GraphicsViewer.CutImage(t.Map16Buffer, 256, 1);
            pictureBox1.Invalidate(true);
        }

        public void selectTile(int tile)
        {
            selx = tile % 16;
            sely = tile / 16;
            pictureBox1.Invalidate(true);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int tx = e.X / 16;
            int ty = e.Y / 16;
            int t = ty * 16 + tx;
            if (t >= 0 && t < 768)
            {
                selx = tx;
                sely = ty;
                if (TileSelected != null)
                    TileSelected(t);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (map16Image == null)
                return;

            e.Graphics.DrawImage(map16Image, 0, 0);
            e.Graphics.DrawRectangle(Pens.White, selx * 16, sely * 16, 16, 16);
            e.Graphics.DrawRectangle(Pens.White, hovx * 16, hovy * 16, 16, 16);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            hovx = e.X / 16;
            hovy = e.Y / 16;
            pictureBox1.Invalidate(true);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            hovx = -1;
            hovy = -1;
            pictureBox1.Invalidate(true);
        }
    }
}
