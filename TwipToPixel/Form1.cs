using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwipToPixel
{
    public partial class Form1 : Form
    {
        List<TextBox> textBoxes;
        private bool pixelMode = false;

        public Form1()
        {
            InitializeComponent();

            textBoxes = new List<TextBox>() { textBox1, textBox2 };
        }

        public static int TwipsToPixels(int twips, float dpi)
        {
            // 1 Twip = 1/20 Point
            // 1 Point = 1/72 Inch
            // 1 Inch = 25.4 mm
            // 1 mm = 25.4 / 72 Inch
            // 1 Pixel = 1/96 Inch (umumnya digunakan untuk resolusi layar)

            // Konversi Twips ke Inch
            float inches = twips / 1440.0f;

            // Konversi Inch ke Pixel dengan memperhitungkan DPI
            int pixels = (int)Math.Round(inches * dpi);

            return pixels;
        }

        private void ConvertTwipsToPixels()
        {
            // Ambil nilai Twips dari TextBox1
            if (int.TryParse(textBox1.Text, out int twips))
            {
                // Tentukan resolusi DPI
                float dpi = 96; // Resolusi umum untuk layar komputer

                // Konversi Twips ke Pixel
                int pixels = TwipsToPixels(twips, dpi);

                // Tampilkan hasil konversi
                //MessageBox.Show($"{twips} Twips = {pixels} Pixels", "Konversi Twips ke Pixels");
                textBox2.Text = pixels.ToString();
                label6.Text = $"{twips} Twips = {pixels} Pixels";
            }
            else
            {
                //MessageBox.Show("Masukkan nilai Twips yang valid dalam TextBox1.", "Error");
            }
        }

        private void ConvertPixelsToTwips()
        {
            // Ambil nilai Pixels dari TextBox1
            if (int.TryParse(textBox1.Text, out int pixels))
            {
                // Tentukan resolusi DPI
                float dpi = 96; // Resolusi umum untuk layar komputer

                // Konversi Pixels ke Twips
                int twips = PixelsToTwips(pixels, dpi);

                // Tampilkan hasil konversi
                //MessageBox.Show($"{pixels} Pixels = {twips} Twips", "Konversi Pixels ke Twips");
                textBox2.Text = twips.ToString();
                label6.Text = $"{pixels} Pixels = {twips} Twips";
            }
            else
            {
                //MessageBox.Show("Masukkan nilai Pixels yang valid dalam TextBox1.", "Error");
            }
        }

        private int PixelsToTwips(int pixels, float dpi)
        {
            // Implementasi konversi Pixels ke Twips
            float inches = pixels / dpi;
            int twips = (int)Math.Round(inches * 1440);
            return twips;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pixelMode == false)
            {
                ConvertTwipsToPixels();
            }
            else if (pixelMode == true)
            {
                ConvertPixelsToTwips();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //ConvertTwipsToPixels();
            button1.PerformClick();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxes.ForEach(textBox => textBox.Text = "");
            if (pixelMode == false)
            {
                label6.Text = "0 twip = 0 pixel (X)";
            }
            else if (pixelMode == true)
            {
                label6.Text = "0 pixel (X) = 0 twip";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
            if (pixelMode == false)
            {
                pixelMode = true;
                label4.Location = new Point(430, 70);
                label3.Location = new Point(430, 28);
            } else if (pixelMode == true)
            {
                pixelMode = false;
                label4.Location = new Point(430, 28);
                label3.Location = new Point(430, 70);
            }
        }
    }
}
