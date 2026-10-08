using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BuisnessLogic;
using MemeApp.Model;

namespace MemeApp.WinForms
{
    public partial class Form1 : Form
    {
        private Logic logic = new Logic();

        public Form1()
        {
            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            txtCategory.DataSource = logic.GetCategoryNames();

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            UpdateGrid();
        }

        private void UpdateGrid(object data = null)
        {
            dataGridView1.DataSource = null;
            if (data != null)
            {
                dataGridView1.DataSource = data;
            }
            else
            {
                dataGridView1.DataSource = logic.GetAllMemes();
            }

            dataGridView1.Refresh();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtTitle.Text;
            string category = txtCategory.Text;
            bool isActual = chkActual.Checked;

            if (logic.AddMeme(name, category, isActual, out string error))
            {
                UpdateGrid();
                txtTitle.Clear();
                chkActual.Checked = false;
            }
            else
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedMeme = (MemeDto)dataGridView1.SelectedRows[0].DataBoundItem;

                if (logic.DeleteMeme(selectedMeme.Id, out string error))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите мем для удаления из таблицы!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedMeme = (MemeDto)dataGridView1.SelectedRows[0].DataBoundItem;

                string newName = txtTitle.Text;
                string newCategory = txtCategory.Text;
                bool newIsActual = chkActual.Checked;

                if (logic.UpdateMeme(selectedMeme.Id, newName, newCategory, newIsActual, out string error))
                {
                    UpdateGrid();
                    txtTitle.Clear();
                    txtCategory.SelectedIndex = -1;
                    chkActual.Checked = false;
                }
                else
                {
                    MessageBox.Show(error, "Ошибка при изменении", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите мем из таблицы, который хотите изменить!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool showingOnlyActual = false;

        private void button3_Click(object sender, EventArgs e)
        {
            if (!showingOnlyActual)
            {
                UpdateGrid(logic.GetMemesByActualStatus(true));
                showingOnlyActual = true;
            }
            else
            {
                UpdateGrid(logic.GetAllMemes());
                showingOnlyActual = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedCategory = txtCategory.Text;

            if (!string.IsNullOrWhiteSpace(selectedCategory))
            {
                UpdateGrid(logic.GetMemesByCategory(selectedCategory));
            }
            else
            {
                UpdateGrid();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedMeme = (MemeDto)dataGridView1.SelectedRows[0].DataBoundItem;

                if (selectedMeme != null)
                {
                    txtTitle.Text = selectedMeme.Name;
                    txtCategory.Text = selectedMeme.Category;
                    chkActual.Checked = selectedMeme.IsActual;

                    string imagePath = System.IO.Path.Combine(Application.StartupPath, "Images", $"{selectedMeme.Name}.jpg");

                    if (System.IO.File.Exists(imagePath))
                    {
                        pictureBox1.ImageLocation = imagePath;
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }
            }
        }

    }
}
