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

        /// <summary>
        /// Конструктор формы. Настраивает таблицу, загружает категории
        /// и показывает все мемы при открытии программы.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            txtCategory.DataSource = logic.GetCategoryNames();

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = logic.GetMemes(
                onlyActual: showingOnlyActual ? true : (bool?)null,
                category: currentSelectedCategory
            );
            dataGridView1.Refresh();
        }

        /// <summary>
        ///  Обработчик кнопки «Добавить». Берёт данные из полей и добавляет новый мем.
        /// Если всё хорошо — обновляет таблицу, если нет — показывает ошибку.
        /// </summary>
        /// <param name="sender">Кнопка, которая была нажата</param>
        /// <param name="e">Параметры события</param>
        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtTitle.Text;
            string category = txtCategory.Text;
            bool isActual = chkActual.Checked;

            if (logic.AddMeme(name, category, isActual, out string error))
            {
                RefreshGrid();
                txtTitle.Clear();
                chkActual.Checked = false;
            }
            else
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Обработчик кнопки «Удалить». Удаляет мем, выбранный в таблице.
        /// Если ничего не выбрано — просит выбрать.
        /// </summary>
        /// <param name="sender">Кнопка, которая была нажата</param>
        /// <param name="e">Параметры события</param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedMeme = (MemeDto)dataGridView1.SelectedRows[0].DataBoundItem;

                if (logic.DeleteMeme(selectedMeme.Id, out string error))
                {
                    RefreshGrid();
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

        /// <summary>
        /// Обработчик кнопки «Изменить». Меняет данные выбранного мема на то,
        /// что написано в полях. Если название поменялось — переименовывает картинку.
        /// </summary>
        /// <param name="sender">Кнопка, которая была нажата.</param>
        /// <param name="e">Параметры события.</param>
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

                    RefreshGrid();
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

        /// <summary>
        /// Переименовывает файл картинки мема в папке Images,
        /// когда меняется название мема.
        /// </summary>
        /// <param name="oldName">Старое название мема.</param>
        /// <param name="newName">Новое название мема.</param>
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

        /// <summary>
        /// Обработчик кнопки фильтра по актуальности.
        /// Переключает показ: только актуальные мемы или все подряд.
        /// </summary>
        /// <param name="sender">Кнопка, которая была нажата.</param>
        /// <param name="e">Параметры события.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            showingOnlyActual = !showingOnlyActual;
            RefreshGrid();
        }

        /// <summary>
        /// Обработчик кнопки фильтра по категории.
        /// Показывает мемы выбранной категории, а при повторном нажатии — сбрасывает фильтр.
        /// </summary>
        /// <param name="sender">Кнопка, которая была нажата.</param>
        /// <param name="e">Параметры события.</param>
        private void button4_Click(object sender, EventArgs e)
        {
            string selectedCategory = txtCategory.Text;

            if (string.IsNullOrWhiteSpace(selectedCategory))
            {
                currentSelectedCategory = null;
            }
            else if (currentSelectedCategory == selectedCategory)
            {
                currentSelectedCategory = null;
                txtCategory.SelectedIndex = -1;
            }
            else
            {
                currentSelectedCategory = selectedCategory;
            }

            RefreshGrid();

        }

        /// <summary>
        /// Срабатывает при выборе строки в таблице. Заполняет поля данными мема
        /// и загружает его картинку.
        /// </summary>
        /// <param name="sender">Таблица, в которой выбрали строку.</param>
        /// <param name="e">Параметры события.</param>
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

        /// <summary>
        /// Загружает картинку из файла в pictureBox.
        /// </summary>
        /// <param name="path">Путь к файлу картинки.</param>
        private void LoadMemeImage(string path)
        {
            ClearPictureBox();

            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                pictureBox1.Image = Image.FromStream(stream);
            }
        }

        /// <summary>
        /// Очищает pictureBox и убирает старую картинку из памяти.
        /// </summary>
        private void ClearPictureBox()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
        }

        /// <summary>
        /// Срабатывает при клике по картинке. Открывает выбор файла
        /// и сохраняет выбранную картинку для текущего мема.
        /// </summary>
        /// <param name="sender">Картинка, по которой кликнули.</param>
        /// <param name="e">Параметры события.</param>
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

