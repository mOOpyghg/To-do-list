using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace To_do_list
{
    public partial class Form1 : Form
    {
        private const string FilePath = "tasks.txt";
        private List<string> tasks = new List<string>();
        public Form1()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string task = txtTask.Text.Trim();
            if (!string.IsNullOrEmpty(task))
            {
                DialogResult result = MessageBox.Show(
                    "Вы уверены что хотите добавить?",
                    "Удаление",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    tasks.Add(task);
                    UpdateTaskList();
                    txtTask.Clear();
                    SaveTasks();
                }
                else
                {
                    return;
                }
                
            }
            else
            {
                MessageBox.Show("Для начала выберите задачу");

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItem != null)
            {
                DialogResult result = MessageBox.Show(
                  "Вы уверены что хотите удалить?",
                  "удаление",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    tasks.Remove(lstTasks.SelectedItem.ToString());
                    UpdateTaskList();
                    SaveTasks();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Для начала выберите задачу");

            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItem != null)
            {
                string select = lstTasks.SelectedItem.ToString();

                
                    if (select.Contains("(Выполнено)"))
                    {
                        MessageBox.Show("Задача уже выполнена!");
                        return;
                    }
                    else
                    {
                        int index = lstTasks.SelectedIndex;
                        string completedTask = tasks[index];
                        tasks[index] = completedTask + " (Выполнено)";
                        UpdateTaskList();
                        SaveTasks();
                    }
                
            }
            else
            {
                MessageBox.Show("Выберите задачу");

            }

        }
        private void UpdateTaskList()
        {
            lstTasks.Items.Clear();
            foreach (var task in tasks)
            {
                lstTasks.Items.Add(task);
            }
        }

        private void SaveTasks()
        {
            File.WriteAllLines(FilePath, tasks);
        }

        private void LoadTasks()
        {
            if (File.Exists(FilePath))
            {
                tasks = File.ReadAllLines(FilePath).ToList();
                UpdateTaskList();
            }
        }

        private void txtTask_TextChanged(object sender, EventArgs e)
        {

        }
    }
}