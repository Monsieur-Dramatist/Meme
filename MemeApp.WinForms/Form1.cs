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
        private string? currentSelectedCategory = null;

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

                string oldName = selectedMeme.Name;
                string newName = txtTitle.Text;
                string newCategory = txtCategory.Text;
                bool newIsActual = chkActual.Checked;

                if (logic.UpdateMeme(selectedMeme.Id, newName, newCategory, newIsActual, out string error))
                {
                    if (!string.Equals(oldName, newName, StringComparison.OrdinalIgnoreCase))
                    {
                        RenameMemeImage(oldName, newName);
                    }

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

        private void RenameMemeImage(string oldName, string newName)
        {
            try
            {
                string imagesFolder = System.IO.Path.Combine(Application.StartupPath, "Images");
                string oldPath = System.IO.Path.Combine(imagesFolder, $"{oldName}.jpg");
                string newPath = System.IO.Path.Combine(imagesFolder, $"{newName}.jpg");

                if (System.IO.File.Exists(oldPath))
                {
                    ClearPictureBox();

                    System.IO.File.Move(oldPath, newPath, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось переименовать файл картинки: {ex.Message}", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (string.IsNullOrWhiteSpace(selectedCategory))
            {
                currentSelectedCategory = null;
                UpdateGrid();
                return;
            }

            if (currentSelectedCategory == selectedCategory)
            {
                currentSelectedCategory = null;
                txtCategory.SelectedIndex = -1;
                UpdateGrid();
            }
            else
            {
                currentSelectedCategory = selectedCategory;
                UpdateGrid(logic.GetMemesByCategory(selectedCategory));
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
                        LoadMemeImage(imagePath);
                    }
                    else
                    {
                        ClearPictureBox();
                    }
                }
            }
        }

        private void LoadMemeImage(string path)
        {
            ClearPictureBox();

            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                pictureBox1.Image = Image.FromStream(stream);
            }
        }

        private void ClearPictureBox()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Сначала выберите мем из таблицы, к которому хотите прикрепить картинку!",
                                "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedMeme = (MemeDto)dataGridView1.SelectedRows[0].DataBoundItem;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "Выберите изображение для мема";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string imagesFolder = System.IO.Path.Combine(Application.StartupPath, "Images");

                        if (!System.IO.Directory.Exists(imagesFolder))
                        {
                            System.IO.Directory.CreateDirectory(imagesFolder);
                        }

                        string destinationPath = System.IO.Path.Combine(imagesFolder, $"{selectedMeme.Name}.jpg");

                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                            pictureBox1.Image = null;
                        }

                        System.IO.File.Copy(openFileDialog.FileName, destinationPath, true);

                        LoadMemeImage(destinationPath);

                        MessageBox.Show("Картинка успешно сохранена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении картинки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
    }

