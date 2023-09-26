using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public DataSet dsTests;
        public DataTable dtQuestions;
        public DataTable dtVariants;
        public int counter = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Создание объекта класса DataSet с именем dsTests 
            dsTests = new System.Data.DataSet();
            dsTests.DataSetName = "Тесты";

            dtQuestions = new DataTable("Вопросы");
            dsTests.Tables.Add(dtQuestions);
            // Создание полей в объекте dtQuestions
            DataColumn dсQuestID = dtQuestions.Columns.Add("Код вопроса", typeof(Int32));
            dсQuestID.Unique = true;
            // Свойство  Unique  указывает,  
            // что  в  этом  поле  не  должно  быть  повторяющихся  
            // значений,  то есть оно  должно  быть  уникальным
            // поле  "Код вопроса"  является первичным ключом таблицы "Вопросы"
            // Это выражение представляет собой неявный способ 
            // задания ограничения UniqueConstraint
            DataColumn dcQuestion = dtQuestions.Columns.Add("Вопрос");
            DataColumn dcQuestType = dtQuestions.Columns.Add("Код ответа", typeof(Int32));
            DataColumn dcQuestTime = dtQuestions.Columns.Add("Время ответа", typeof(Int32));
            // Создание объекта класса DataTable для таблицы "Варианты ответов"
            dtVariants = new DataTable("Варианты ответов");
            dsTests.Tables.Add(dtVariants);
            // Создание полей в объекте dtVariants
            DataColumn dcID = dtVariants.Columns.Add("Код ответа", typeof(Int32));
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            DataColumn dcVariantQuestID = dtVariants.Columns.Add("Код вопроса", typeof(Int32));
            DataColumn dcVariant = dtVariants.Columns.Add("Вариант ответа");
            DataColumn dcIsRight = dtVariants.Columns.Add("Признак правильности", typeof(Boolean));
            // Создание  связи  между  таблицами
            DataRelation drQuestionsVariants = new DataRelation("ВопросОтвет", dсQuestID, dcVariantQuestID);
            dsTests.Relations.Add(drQuestionsVariants);
            // Варианты подключения DataView
            DataView myDataView = new DataView(dtQuestions);
            dataGridView1.DataSource = myDataView;

            dataGridView2.DataSource = dsTests.Tables["Варианты ответов"].DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Добавить сроку в таблицу Вопросы
            DataRow myRow = dtQuestions.NewRow();

            myRow["Код вопроса"] = Convert.ToInt32(textBox1.Text);
            myRow["Вопрос"] = textBox2.Text;
            myRow["Код ответа"] = Convert.ToInt32(textBox3.Text);
            myRow["Время ответа"] = Convert.ToInt32(textBox4.Text);

            dtQuestions.Rows.Add(myRow);

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Добавить сроку в таблицу Варианты ответов
            DataRow varRow = dtVariants.NewRow();

            varRow["Код ответа"] = Convert.ToInt32(textBox5.Text);
            varRow["Код вопроса"] = Convert.ToInt32(textBox6.Text);
            varRow["Вариант ответа"] = textBox7.Text;
            varRow["Признак правильности"] = checkBox1.Checked;

            dtVariants.Rows.Add(varRow);

            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            checkBox1.Checked = false;
            dataGridView2.ClearSelection();
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            // Изменение некоторых полей записи таблицы Варианты ответов
            // индекс изменяемой записи в коллекции 
            // задаётся индексом выбранной строки в dataGridView2 
            int ind = dataGridView2.CurrentRow.Index;
            DataRow varRow = dtVariants.Rows[ind];
            varRow.BeginEdit();
            varRow.ItemArray = new object[] { Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox6.Text), textBox7.Text, checkBox1.Checked };
            varRow.EndEdit();
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(counter > 0)
            {
                int ind = dataGridView2.CurrentRow.Index;
                DataRow varRow = dtVariants.Rows[ind];

                textBox5.Text = varRow["Код ответа"].ToString();
                textBox6.Text = varRow["Код вопроса"].ToString();
                textBox7.Text = varRow["Вариант ответа"].ToString();
                checkBox1.Checked = Convert.ToBoolean(varRow["Признак правильности"]);

                label8.Text = ind.ToString();
            }
            counter++;
        }
    }
}
