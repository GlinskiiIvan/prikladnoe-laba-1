using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string staticPath = "Static/";
        public Image img;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.туристыTableAdapter.Fill(this.travelAgenciesDataSet.Туристы);   
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            this.туристыTableAdapter.Update(this.travelAgenciesDataSet.Туристы);
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            DataRow myRow = this.travelAgenciesDataSet.Туристы.NewRow();

            myRow["КодТуриста"] = Convert.ToInt32(textBox1.Text);
            myRow["Фамилия"] = textBox2.Text;
            myRow["Имя"] = textBox3.Text;
            myRow["Отчество"] = textBox4.Text;
            myRow["Изображение"] = label_img.Text;

            string imgPath = label_img.Text;           
            img.Save(staticPath + imgPath, ImageFormat.Jpeg);

            this.travelAgenciesDataSet.Туристы.Rows.Add(myRow);

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            label_img.Text = "";
            pictureBox_imgSet.Image = null;
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            int ind = dataGridView1.CurrentRow.Index;
            DataRow varRow = this.travelAgenciesDataSet.Туристы.Rows[ind];
            varRow.BeginEdit();
            varRow.ItemArray = new object[] {
                Convert.ToInt32(textBox1.Text),
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                label_img.Text
            };
            varRow.EndEdit();

            string imgPath = label_img.Text;
            img.Save(staticPath + imgPath, ImageFormat.Jpeg);
        }        

        private void button_imgSet_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox_imgSet.Image = Image.FromFile(openFileDialog.FileName);

                    img = Image.FromFile(openFileDialog.FileName);
                    string imgName = Convert.ToString(openFileDialog.GetHashCode()) + ".jpg";                   
                    label_img.Text = imgName;
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = dataGridView1.CurrentRow.Index;
            DataRow varRow = this.travelAgenciesDataSet.Туристы.Rows[ind];

            textBox1.Text = varRow["КодТуриста"].ToString();
            textBox2.Text = varRow["Фамилия"].ToString();
            textBox3.Text = varRow["Имя"].ToString();
            textBox4.Text = varRow["Отчество"].ToString();
            label_img.Text = varRow["Изображение"].ToString();

            pictureBox_imgSet.Image = Image.FromFile(staticPath + varRow["Изображение"].ToString());
        }
    }
}
