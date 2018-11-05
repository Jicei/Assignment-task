using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool start=false;//Переменная для проверки заданости размерности
        private void button1_Click(object sender, EventArgs e)
        {
            int n, m, i, j, l, buf, cost, cur;
            int j0;
            //Потенциал строк и потенциал столбцов
            int []u;
            int[] v;
            int[] p;//Хранение паросочетания каждой выбраной строки
            int []way;//Номер предшествующего столбца
            int []minv;//Вспомогательный минимум для каждого j столбца
            int[,] a;//Масив искомых данных
            bool[]used;
            n = (int)(numericUpDown1.Value);
            m = (int)(numericUpDown2.Value);
            if (start == false)
            {
                MessageBox.Show("Не задана размерность.");
                return;
            }
            if (n > m)//Зампена мест строка/столбец
            {
                buf = n;
                n=m;
                m=buf;
            }
            listBox1.Items.Clear();
            //Задание размерности
            a = new int[n+1, m+1];
            u=new int[n+1];
            v=new int[m+1];
            p=new int[m+1];
            way=new int[m+1];
            used=new bool[m+1];
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= m; j++)
                {
                    try//Проверка на коректность ввода
                    {
                        a[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                    }
                    catch
                    {
                        MessageBox.Show("Неправильно введенные данные");
                        return;
                    }
                }
            }

            for (i = 1; i <= n; ++i)
            {
                p[0] = i;
                j0 = 0;
                minv = new int[m + 1];
                for (l = 1; l <= m; l++)
                {
                    minv[l] = 9999;
                    used[l] = false;
                }
                do
                {
                    used[j0] = true;//Помечаем посещенный столбец и строку
                    int i0 = p[j0], delta = 9999, j1 = 0;
                    for (j = 1; j <= m; ++j)
                    {
                        if (!used[j])//Проход по элементам
                        {
                            cur = a[i0, j] - u[i0] - v[j];//Расчет вспомогательного минимума
                            if (cur < minv[j]) 
                            {
                                minv[j] = cur;//запись минимума
                                way[j] = j0;//Запись строки
                            }
                            if (minv[j] < delta)//Поиск минимума и запоминание строки
                            {
                                delta = minv[j];
                                j1 = j;
                            }
                        }
                    }
                    for (j = 0; j <= m; ++j)//Пересчет потенциала
                    {
                        if (used[j])
                        {
                            u[p[j]] += delta;
                            v[j] -= delta;
                        }
                        else
                            minv[j] -= delta;
                    }
                    j0 = j1;//Строка минимума
                } while (p[j0] != 0);//Нашли свободный столбец-выход из цикла
                do//Нахождение увеличивающейся цепочки
                {
                    int j1 = way[j0];
                    p[j0] = p[j1];
                    j0 = j1;
                } while (j0 != 0);
            }
            //Вывод нужных индексов
            for (j=1; j<=m; ++j)
            {
                if (p[j] != 0)
                {
                    listBox1.Items.Add("Ячейки:"+p[j] + "-" + j);
                }
            }
            //Стоимомть папросочетания. 
            cost = -v[0];//0 элемент общий для мнимой строки и минимого столбца
            label1.Text = "Минимальные затраты:"+Convert.ToString(cost);
         }

        private void button2_Click(object sender, EventArgs e)
        {
            //Задание розмерности
            int n, m,buf;
            n = (int)(numericUpDown1.Value);
            m = (int)(numericUpDown2.Value);
            start = true;
            dataGridView1.Rows.Clear();
            if (n > m)//Замена мест столбец/строка
            {
                buf = n;
                n = m;
                m = buf;
                dataGridView1.RowCount = n + 1;
                dataGridView1.ColumnCount = m + 1;
                dataGridView1.Rows[0].Cells[0].Value = "№";
                dataGridView1.Columns[0].Width = 100;
                for (int i = 0; i < n; i++)
                {
                    dataGridView1.Rows[i + 1].Cells[0].Value = ("Работа:" + Convert.ToString(i+1));
                }
                for (int j = 0; j < m; j++)
                {
                    dataGridView1.Rows[0].Cells[j + 1].Value = ("Работник:"+Convert.ToString(j+1));
                    dataGridView1.Columns[j + 1].Width = 130;
                }
            }
            else
            {
                dataGridView1.RowCount = n + 1;
                dataGridView1.ColumnCount = m + 1;
                dataGridView1.Rows[0].Cells[0].Value = "№";
                dataGridView1.Columns[0].Width = 130;
                for (int i = 0; i < n; i++)
                {
                    dataGridView1.Rows[i + 1].Cells[0].Value = ("Работник:"+Convert.ToString(i+1));
                }
                for (int j = 0; j < m; j++)
                {
                    dataGridView1.Rows[0].Cells[j + 1].Value = ("Работа:" + Convert.ToString(j+1));
                    dataGridView1.Columns[j + 1].Width = 100;
                }
            }
        }
     }
  }

