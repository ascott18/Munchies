namespace Munchies
{
    partial class GameContainer : ContentContainer
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus_PointsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Points = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_LevelLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Level = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_LivesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Lives = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_PeasLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Peas = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Spacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_TopScoreLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_TopScore = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_TopMunchieLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_TopMunchie = new System.Windows.Forms.ToolStripStatusLabel();
            this.gameOverPicture = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameOverPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus_PointsLabel,
            this.toolStripStatus_Points,
            this.toolStripStatus_LevelLabel,
            this.toolStripStatus_Level,
            this.toolStripStatus_LivesLabel,
            this.toolStripStatus_Lives,
            this.toolStripStatus_PeasLabel,
            this.toolStripStatus_Peas,
            this.toolStripStatus_Spacer,
            this.toolStripStatus_TopScoreLabel,
            this.toolStripStatus_TopScore,
            this.toolStripStatus_TopMunchieLabel,
            this.toolStripStatus_TopMunchie});
            this.statusStrip1.Location = new System.Drawing.Point(0, 456);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(640, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatus_PointsLabel
            // 
            this.toolStripStatus_PointsLabel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_PointsLabel.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatus_PointsLabel.Margin = new System.Windows.Forms.Padding(5, 2, -3, 2);
            this.toolStripStatus_PointsLabel.Name = "toolStripStatus_PointsLabel";
            this.toolStripStatus_PointsLabel.Size = new System.Drawing.Size(38, 20);
            this.toolStripStatus_PointsLabel.Text = "Score:";
            this.toolStripStatus_PointsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_Points
            // 
            this.toolStripStatus_Points.AutoSize = false;
            this.toolStripStatus_Points.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_Points.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(231)))));
            this.toolStripStatus_Points.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatus_Points.Name = "toolStripStatus_Points";
            this.toolStripStatus_Points.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.toolStripStatus_Points.Size = new System.Drawing.Size(41, 20);
            this.toolStripStatus_Points.Text = "999999";
            this.toolStripStatus_Points.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_LevelLabel
            // 
            this.toolStripStatus_LevelLabel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_LevelLabel.Margin = new System.Windows.Forms.Padding(0, 2, -3, 2);
            this.toolStripStatus_LevelLabel.Name = "toolStripStatus_LevelLabel";
            this.toolStripStatus_LevelLabel.Size = new System.Drawing.Size(35, 20);
            this.toolStripStatus_LevelLabel.Text = "Level:";
            this.toolStripStatus_LevelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_Level
            // 
            this.toolStripStatus_Level.AutoSize = false;
            this.toolStripStatus_Level.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_Level.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(179)))));
            this.toolStripStatus_Level.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatus_Level.Name = "toolStripStatus_Level";
            this.toolStripStatus_Level.Size = new System.Drawing.Size(22, 20);
            this.toolStripStatus_Level.Text = "100";
            this.toolStripStatus_Level.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_LivesLabel
            // 
            this.toolStripStatus_LivesLabel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_LivesLabel.Margin = new System.Windows.Forms.Padding(0, 2, -3, 2);
            this.toolStripStatus_LivesLabel.Name = "toolStripStatus_LivesLabel";
            this.toolStripStatus_LivesLabel.Size = new System.Drawing.Size(34, 20);
            this.toolStripStatus_LivesLabel.Text = "Lives:";
            this.toolStripStatus_LivesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_Lives
            // 
            this.toolStripStatus_Lives.AutoSize = false;
            this.toolStripStatus_Lives.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_Lives.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripStatus_Lives.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatus_Lives.Name = "toolStripStatus_Lives";
            this.toolStripStatus_Lives.Size = new System.Drawing.Size(22, 20);
            this.toolStripStatus_Lives.Text = "100";
            this.toolStripStatus_Lives.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_PeasLabel
            // 
            this.toolStripStatus_PeasLabel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_PeasLabel.Margin = new System.Windows.Forms.Padding(0, 2, -3, 2);
            this.toolStripStatus_PeasLabel.Name = "toolStripStatus_PeasLabel";
            this.toolStripStatus_PeasLabel.Size = new System.Drawing.Size(33, 20);
            this.toolStripStatus_PeasLabel.Text = "Peas:";
            this.toolStripStatus_PeasLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_Peas
            // 
            this.toolStripStatus_Peas.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_Peas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.toolStripStatus_Peas.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatus_Peas.Name = "toolStripStatus_Peas";
            this.toolStripStatus_Peas.Size = new System.Drawing.Size(40, 20);
            this.toolStripStatus_Peas.Text = "100 SP";
            this.toolStripStatus_Peas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_Spacer
            // 
            this.toolStripStatus_Spacer.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_Spacer.Name = "toolStripStatus_Spacer";
            this.toolStripStatus_Spacer.Size = new System.Drawing.Size(1, 19);
            this.toolStripStatus_Spacer.Spring = true;
            // 
            // toolStripStatus_TopScoreLabel
            // 
            this.toolStripStatus_TopScoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_TopScoreLabel.Margin = new System.Windows.Forms.Padding(0, 2, -3, 2);
            this.toolStripStatus_TopScoreLabel.Name = "toolStripStatus_TopScoreLabel";
            this.toolStripStatus_TopScoreLabel.Size = new System.Drawing.Size(60, 20);
            this.toolStripStatus_TopScoreLabel.Text = "Top Score:";
            this.toolStripStatus_TopScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_TopScore
            // 
            this.toolStripStatus_TopScore.AutoSize = false;
            this.toolStripStatus_TopScore.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_TopScore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(231)))));
            this.toolStripStatus_TopScore.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatus_TopScore.Name = "toolStripStatus_TopScore";
            this.toolStripStatus_TopScore.Size = new System.Drawing.Size(41, 20);
            this.toolStripStatus_TopScore.Text = "999999";
            this.toolStripStatus_TopScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_TopMunchieLabel
            // 
            this.toolStripStatus_TopMunchieLabel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_TopMunchieLabel.Margin = new System.Windows.Forms.Padding(0, 2, -3, 2);
            this.toolStripStatus_TopMunchieLabel.Name = "toolStripStatus_TopMunchieLabel";
            this.toolStripStatus_TopMunchieLabel.Size = new System.Drawing.Size(77, 20);
            this.toolStripStatus_TopMunchieLabel.Text = "Top Munchie:";
            this.toolStripStatus_TopMunchieLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_TopMunchie
            // 
            this.toolStripStatus_TopMunchie.AutoSize = false;
            this.toolStripStatus_TopMunchie.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatus_TopMunchie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripStatus_TopMunchie.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatus_TopMunchie.Name = "toolStripStatus_TopMunchie";
            this.toolStripStatus_TopMunchie.Size = new System.Drawing.Size(70, 20);
            this.toolStripStatus_TopMunchie.Text = "Melvin";
            this.toolStripStatus_TopMunchie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gameOverPicture
            // 
            this.gameOverPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gameOverPicture.BackgroundImage = global::Munchies.Properties.Resources.GameOver;
            this.gameOverPicture.Location = new System.Drawing.Point(194, 148);
            this.gameOverPicture.Name = "gameOverPicture";
            this.gameOverPicture.Size = new System.Drawing.Size(252, 76);
            this.gameOverPicture.TabIndex = 3;
            this.gameOverPicture.TabStop = false;
            this.gameOverPicture.Visible = false;
            // 
            // GameContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gameOverPicture);
            this.Controls.Add(this.statusStrip1);
            this.Name = "GameContainer";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.GameContainer_Layout);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameOverPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_PointsLabel;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Points;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_LevelLabel;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Level;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_LivesLabel;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Lives;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_PeasLabel;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Peas;
        private System.Windows.Forms.PictureBox gameOverPicture;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Spacer;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_TopScoreLabel;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_TopScore;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_TopMunchieLabel;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatus_TopMunchie;
    }
}
