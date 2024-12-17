namespace BranchAndBound
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelButtons = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonAddEdge = new System.Windows.Forms.Button();
            this.buttonAddVertex = new System.Windows.Forms.Button();
            this.buttonRemoveVertex = new System.Windows.Forms.Button();
            this.buttonShowMatrix = new System.Windows.Forms.Button();
            this.buttonSetMatrix = new System.Windows.Forms.Button();
            this.buttonLoadFile = new System.Windows.Forms.Button();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.progressBar);
            this.panelButtons.Controls.Add(this.buttonSolve);
            this.panelButtons.Controls.Add(this.buttonSaveFile);
            this.panelButtons.Controls.Add(this.buttonLoadFile);
            this.panelButtons.Controls.Add(this.buttonSetMatrix);
            this.panelButtons.Controls.Add(this.buttonShowMatrix);
            this.panelButtons.Controls.Add(this.buttonRemoveVertex);
            this.panelButtons.Controls.Add(this.buttonAddVertex);
            this.panelButtons.Controls.Add(this.buttonAddEdge);
            this.panelButtons.Controls.Add(this.labelTitle);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(220, 600);
            this.panelButtons.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(10, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(200, 20);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Задача Коммивояжёра";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonAddEdge
            // 
            this.buttonAddEdge.Location = new System.Drawing.Point(10, 60);
            this.buttonAddEdge.Name = "buttonAddEdge";
            this.buttonAddEdge.Size = new System.Drawing.Size(200, 40);
            this.buttonAddEdge.TabIndex = 1;
            this.buttonAddEdge.Text = "Добавить ребро";
            this.buttonAddEdge.UseVisualStyleBackColor = true;
            this.buttonAddEdge.Click += new System.EventHandler(this.buttonAddEdge_Click);
            // 
            // buttonAddVertex
            // 
            this.buttonAddVertex.Location = new System.Drawing.Point(10, 110);
            this.buttonAddVertex.Name = "buttonAddVertex";
            this.buttonAddVertex.Size = new System.Drawing.Size(200, 40);
            this.buttonAddVertex.TabIndex = 2;
            this.buttonAddVertex.Text = "Добавить вершину";
            this.buttonAddVertex.UseVisualStyleBackColor = true;
            this.buttonAddVertex.Click += new System.EventHandler(this.buttonAddVertex_Click);
            // 
            // buttonRemoveVertex
            // 
            this.buttonRemoveVertex.Location = new System.Drawing.Point(10, 160);
            this.buttonRemoveVertex.Name = "buttonRemoveVertex";
            this.buttonRemoveVertex.Size = new System.Drawing.Size(200, 40);
            this.buttonRemoveVertex.TabIndex = 3;
            this.buttonRemoveVertex.Text = "Удалить вершину";
            this.buttonRemoveVertex.UseVisualStyleBackColor = true;
            this.buttonRemoveVertex.Click += new System.EventHandler(this.buttonRemoveVertex_Click);
            // 
            // buttonShowMatrix
            // 
            this.buttonShowMatrix.Location = new System.Drawing.Point(10, 210);
            this.buttonShowMatrix.Name = "buttonShowMatrix";
            this.buttonShowMatrix.Size = new System.Drawing.Size(200, 40);
            this.buttonShowMatrix.TabIndex = 4;
            this.buttonShowMatrix.Text = "Показать матрицу";
            this.buttonShowMatrix.UseVisualStyleBackColor = true;
            this.buttonShowMatrix.Click += new System.EventHandler(this.buttonShowMatrix_Click);
            // 
            // buttonSetMatrix
            // 
            this.buttonSetMatrix.Location = new System.Drawing.Point(10, 260);
            this.buttonSetMatrix.Name = "buttonSetMatrix";
            this.buttonSetMatrix.Size = new System.Drawing.Size(200, 40);
            this.buttonSetMatrix.TabIndex = 5;
            this.buttonSetMatrix.Text = "Установить матрицу";
            this.buttonSetMatrix.UseVisualStyleBackColor = true;
            this.buttonSetMatrix.Click += new System.EventHandler(this.buttonSetMatrix_Click);
            // 
            // buttonLoadFile
            // 
            this.buttonLoadFile.Location = new System.Drawing.Point(10, 310);
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.Size = new System.Drawing.Size(200, 40);
            this.buttonLoadFile.TabIndex = 6;
            this.buttonLoadFile.Text = "Загрузить из файла";
            this.buttonLoadFile.UseVisualStyleBackColor = true;
            this.buttonLoadFile.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Location = new System.Drawing.Point(10, 360);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(200, 40);
            this.buttonSaveFile.TabIndex = 7;
            this.buttonSaveFile.Text = "Сохранить результаты";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // buttonSolve
            // 
            this.buttonSolve.Location = new System.Drawing.Point(10, 410);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(200, 40);
            this.buttonSolve.TabIndex = 8;
            this.buttonSolve.Text = "Ответ";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 470);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 9;
            this.progressBar.Visible = false;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelButtons);
            this.Name = "MainForm";
            this.Text = "TSP Solver - Branch and Bound";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonAddEdge;
        private System.Windows.Forms.Button buttonAddVertex;
        private System.Windows.Forms.Button buttonRemoveVertex;
        private System.Windows.Forms.Button buttonShowMatrix;
        private System.Windows.Forms.Button buttonSetMatrix;
        private System.Windows.Forms.Button buttonLoadFile;
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
