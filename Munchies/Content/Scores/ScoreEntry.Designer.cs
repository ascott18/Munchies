namespace Munchies
{
    partial class ScoreEntry
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                NormalFont.Dispose();
                HighlightFont.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rank = new System.Windows.Forms.Label();
            this.points = new System.Windows.Forms.Label();
            this.scoreBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.level = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scoreBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rank
            // 
            this.rank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.rank.Location = new System.Drawing.Point(0, 1);
            this.rank.Margin = new System.Windows.Forms.Padding(0);
            this.rank.Name = "rank";
            this.rank.Size = new System.Drawing.Size(29, 17);
            this.rank.TabIndex = 3;
            this.rank.Text = "1.";
            this.rank.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // points
            // 
            this.points.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.scoreBindingSource, "Points", true));
            this.points.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.points.Location = new System.Drawing.Point(32, 1);
            this.points.Margin = new System.Windows.Forms.Padding(0);
            this.points.Name = "points";
            this.points.Size = new System.Drawing.Size(68, 17);
            this.points.TabIndex = 3;
            this.points.Text = "123";
            // 
            // scoreBindingSource
            // 
            this.scoreBindingSource.DataSource = typeof(Munchies.Score);
            // 
            // level
            // 
            this.level.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.scoreBindingSource, "Level", true));
            this.level.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.level.Location = new System.Drawing.Point(103, 1);
            this.level.Margin = new System.Windows.Forms.Padding(0);
            this.level.Name = "level";
            this.level.Size = new System.Drawing.Size(54, 17);
            this.level.TabIndex = 3;
            this.level.Text = "1";
            // 
            // name
            // 
            this.name.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.scoreBindingSource, "Name", true));
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(159, 1);
            this.name.Margin = new System.Windows.Forms.Padding(0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(123, 17);
            this.name.TabIndex = 3;
            this.name.Text = "Melvin";
            // 
            // ScoreEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.name);
            this.Controls.Add(this.level);
            this.Controls.Add(this.points);
            this.Controls.Add(this.rank);
            this.Name = "ScoreEntry";
            this.Size = new System.Drawing.Size(284, 18);
            ((System.ComponentModel.ISupportInitialize)(this.scoreBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.BindingSource scoreBindingSource;
        internal System.Windows.Forms.Label rank;
        internal System.Windows.Forms.Label points;
        internal System.Windows.Forms.Label level;
        internal System.Windows.Forms.Label name;
    }
}
