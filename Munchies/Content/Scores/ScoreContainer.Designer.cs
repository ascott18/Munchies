namespace Munchies
{
    partial class ScoreContainer
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
            this.scorePanel = new System.Windows.Forms.Panel();
            this.title = new System.Windows.Forms.Label();
            this.scoreEntryTitleRow = new Munchies.ScoreEntry();
            this.line2 = new System.Windows.Forms.PictureBox();
            this.line1 = new System.Windows.Forms.PictureBox();
            this.scorePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.line2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.line1)).BeginInit();
            this.SuspendLayout();
            // 
            // scorePanel
            // 
            this.scorePanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.scorePanel.BackgroundImage = global::Munchies.Properties.Resources.ScoreBackground;
            this.scorePanel.Controls.Add(this.line1);
            this.scorePanel.Controls.Add(this.line2);
            this.scorePanel.Controls.Add(this.title);
            this.scorePanel.Controls.Add(this.scoreEntryTitleRow);
            this.scorePanel.Location = new System.Drawing.Point(157, 105);
            this.scorePanel.Name = "scorePanel";
            this.scorePanel.Size = new System.Drawing.Size(330, 250);
            this.scorePanel.TabIndex = 0;
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Dock = System.Windows.Forms.DockStyle.Top;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.title.ForeColor = System.Drawing.Color.Red;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(330, 30);
            this.title.TabIndex = 2;
            this.title.Text = "High Scores";
            this.title.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // scoreEntryTitleRow
            // 
            this.scoreEntryTitleRow.BackColor = System.Drawing.Color.Transparent;
            this.scoreEntryTitleRow.Highlighted = false;
            this.scoreEntryTitleRow.Location = new System.Drawing.Point(23, 36);
            this.scoreEntryTitleRow.Name = "scoreEntryTitleRow";
            this.scoreEntryTitleRow.Size = new System.Drawing.Size(284, 19);
            this.scoreEntryTitleRow.TabIndex = 1;
            // 
            // line2
            // 
            this.line2.BackColor = System.Drawing.Color.Black;
            this.line2.Location = new System.Drawing.Point(24, 54);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(282, 1);
            this.line2.TabIndex = 3;
            this.line2.TabStop = false;
            // 
            // line1
            // 
            this.line1.BackColor = System.Drawing.Color.Black;
            this.line1.Location = new System.Drawing.Point(24, 36);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(282, 1);
            this.line1.TabIndex = 3;
            this.line1.TabStop = false;
            // 
            // ScoreContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Munchies.Properties.Resources.BG1;
            this.Controls.Add(this.scorePanel);
            this.Name = "ScoreContainer";
            this.scorePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.line2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.line1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel scorePanel;
        private ScoreEntry scoreEntryTitleRow;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.PictureBox line1;
        private System.Windows.Forms.PictureBox line2;
    }
}
