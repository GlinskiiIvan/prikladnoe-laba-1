using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppConnectionFactory
{
    public partial class Form1 : Form
    {
        public DataSet ds = new DataSet();
        public DataGrid dataGrid1 = new DataGrid();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds = MyProviderFactory.GetMyConn();
            DataViewManager dsview = ds.DefaultViewManager;
            dataGrid1.DataSource = dsview;
            dataGrid1.DataMember = "Туристы";

            dataGridView1.DataSource = ds.Tables["Туры"].DefaultView;
        }
    }
}
