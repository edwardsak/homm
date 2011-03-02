using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConvertBitmap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "bmp|*.bmp";

            if (txtFile.Text.Length < 1)
                dlg.InitialDirectory = Application.StartupPath;
            else
            {
                if (System.IO.File.Exists(txtFile.Text))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(txtFile.Text);
                    dlg.InitialDirectory = fi.DirectoryName;
                }
                else
                {
                    dlg.InitialDirectory = Application.StartupPath;
                }
            }

            if (dlg.ShowDialog() != DialogResult.OK) return;
            this.txtFile.Text = dlg.FileName;



        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(txtFile.Text))
            {
                MessageBox.Show("File is not exist.");
                return;
            }

            Bitmap bmp = ConvertBitmap(txtFile.Text, chkFlip.Checked, chkSel.Checked);

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "png|*.png";

            if (txtFile.Text.Length < 1)
                dlg.InitialDirectory = Application.StartupPath;
            else
            {
                if (System.IO.File.Exists(txtFile.Text))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(txtFile.Text);
                    dlg.FileName = string.Format(@"{0}\{1}.png", fi.DirectoryName, fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length));
                }
                else
                {
                    dlg.InitialDirectory = Application.StartupPath;
                }
            }

            if (dlg.ShowDialog() != DialogResult.OK) return;

            bmp.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
        }

        private Bitmap ConvertBitmap(string fileName, bool flip, bool select)
        {
            Image img = Image.FromFile(fileName);

            if (flip) img.RotateFlip(RotateFlipType.RotateNoneFlipX);

            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.DrawImage(img, 0, 0, img.Width, img.Height);
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        int c = bmp.GetPixel(x, y).ToArgb();
                        if (c == Color.Cyan.ToArgb())
                        {
                            bmp.SetPixel(x, y, Color.Transparent);
                        }
                        else if (c == Color.Magenta.ToArgb())
                        {
                            // deep shadow
                            bmp.SetPixel(x, y, Color.FromArgb(153, 0, 0, 0));
                        }
                        else if (c == Color.FromArgb(255, 150, 255).ToArgb())
                        {
                            // faint shadow
                            bmp.SetPixel(x, y, Color.FromArgb(102, 0, 0, 0));
                        }
                        else if (c == Color.Yellow.ToArgb())
                        {
                            if (!select)
                                bmp.SetPixel(x, y, Color.Transparent);
                        }
                        else if (c == Color.FromArgb(180, 0, 255).ToArgb())
                        {
                            if (select)
                                // selection + deep shadow
                                //bmp.SetPixel(x, y, Color.FromArgb(169, 169, 0));
                                bmp.SetPixel(x, y, Color.Yellow);
                            else
                                bmp.SetPixel(x, y, Color.FromArgb(153, 0, 0, 0));
                        }
                        else if (c == Color.FromArgb(0, 255, 0).ToArgb())
                        {
                            if (select)
                                // selection + faint shadow
                                //bmp.SetPixel(x, y, Color.FromArgb(209, 209, 0));
                                bmp.SetPixel(x, y, Color.Yellow);
                            else
                                bmp.SetPixel(x, y, Color.FromArgb(102, 0, 0, 0));
                        }
                    }
                }
            }

            return bmp;
        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(txtFile.Text))
            {
                MessageBox.Show("File is not exist.");
                return;
            }

            System.Collections.ArrayList suffixs = new System.Collections.ArrayList();
            suffixs.Add("");

            if (chkFlip.Checked) suffixs.Add("f");
            if (this.chkSel.Checked) suffixs.Add("s");
            if (chkFlip.Checked && chkSel.Checked) suffixs.Add("sf");

            System.IO.FileInfo fi = new System.IO.FileInfo(txtFile.Text);
            System.IO.FileInfo[] fis = fi.Directory.GetFiles("*.bmp", System.IO.SearchOption.TopDirectoryOnly);

            string ext = "";
            System.Drawing.Imaging.ImageFormat imgFormat = null;
            if (optGif.Checked)
            {
                ext = "gif";
                imgFormat = System.Drawing.Imaging.ImageFormat.Gif;
            }
            else if (optPng.Checked)
            {
                ext = "png";
                imgFormat = System.Drawing.Imaging.ImageFormat.Png;
            }

            foreach (System.IO.FileInfo fi2 in fis)
            {
                foreach (string suffix in suffixs)
                {
                    Bitmap bmp = null;
                    if (suffix == "f")
                        bmp = ConvertBitmap(fi2.FullName, true, false);
                    else if (suffix == "s")
                        bmp = ConvertBitmap(fi2.FullName, false, true);
                    else if (suffix == "sf")
                        bmp = ConvertBitmap(fi2.FullName, true, true);
                    else
                        bmp = ConvertBitmap(fi2.FullName, false, false);

                    string fileName = string.Format(@"{0}\{1}{2}.{3}", fi2.DirectoryName,
                        fi2.Name.Substring(0, fi2.Name.Length - fi2.Extension.Length),
                        suffix, ext);

                    if (System.IO.File.Exists(fileName)) System.IO.File.Delete(fileName);

                    
                    
                    bmp.Save(fileName, imgFormat);
                }
            }

            MessageBox.Show("Done");
        }

    }
}
