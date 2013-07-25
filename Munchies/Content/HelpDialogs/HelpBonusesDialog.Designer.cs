namespace Munchies.HelpDialogs
{
    partial class HelpBonusesDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spriteDescription1 = new Munchies.HelpDialogs.SpriteDescription();
            this.button1 = new System.Windows.Forms.Button();
            this.spriteDescription2 = new Munchies.HelpDialogs.SpriteDescription();
            this.spriteDescription3 = new Munchies.HelpDialogs.SpriteDescription();
            this.spriteDescription4 = new Munchies.HelpDialogs.SpriteDescription();
            this.spriteDescription5 = new Munchies.HelpDialogs.SpriteDescription();
            this.SuspendLayout();
            // 
            // spriteDescription1
            // 
            this.spriteDescription1.Description = "Peapods get you peas which you can use to shoot your enemies. Click the mouse but" +
    "ton to shoot. Enemies are worth 50 points each.";
            this.spriteDescription1.Image = global::Munchies.Properties.Resources.Peas;
            this.spriteDescription1.Location = new System.Drawing.Point(12, 12);
            this.spriteDescription1.Name = "spriteDescription1";
            this.spriteDescription1.Size = new System.Drawing.Size(415, 49);
            this.spriteDescription1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(360, 245);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // spriteDescription2
            // 
            this.spriteDescription2.Description = "Salt gets Smart shots. Guide the peas by moving Melvin up and down.";
            this.spriteDescription2.Image = global::Munchies.Properties.Resources.Salt;
            this.spriteDescription2.Location = new System.Drawing.Point(12, 78);
            this.spriteDescription2.Name = "spriteDescription2";
            this.spriteDescription2.Size = new System.Drawing.Size(415, 32);
            this.spriteDescription2.TabIndex = 0;
            // 
            // spriteDescription3
            // 
            this.spriteDescription3.Description = "Pepper gets Power shots which plow through multiple objects.";
            this.spriteDescription3.Image = global::Munchies.Properties.Resources.Pepper;
            this.spriteDescription3.Location = new System.Drawing.Point(12, 127);
            this.spriteDescription3.Name = "spriteDescription3";
            this.spriteDescription3.Size = new System.Drawing.Size(415, 32);
            this.spriteDescription3.TabIndex = 0;
            // 
            // spriteDescription4
            // 
            this.spriteDescription4.Description = "Butter surrounds Melvin with a shield of pure cholesterol, making him temporarily" +
    " invulnerable.";
            this.spriteDescription4.Image = global::Munchies.Properties.Resources.Butter;
            this.spriteDescription4.Location = new System.Drawing.Point(12, 176);
            this.spriteDescription4.Name = "spriteDescription4";
            this.spriteDescription4.Size = new System.Drawing.Size(415, 32);
            this.spriteDescription4.TabIndex = 0;
            // 
            // spriteDescription5
            // 
            this.spriteDescription5.Description = "The cup of coffee gets you an extra life!";
            this.spriteDescription5.Image = global::Munchies.Properties.Resources.Coffee3;
            this.spriteDescription5.Location = new System.Drawing.Point(12, 224);
            this.spriteDescription5.Name = "spriteDescription5";
            this.spriteDescription5.Size = new System.Drawing.Size(415, 32);
            this.spriteDescription5.TabIndex = 0;
            // 
            // HelpBonusesDialog
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(439, 280);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.spriteDescription5);
            this.Controls.Add(this.spriteDescription4);
            this.Controls.Add(this.spriteDescription3);
            this.Controls.Add(this.spriteDescription2);
            this.Controls.Add(this.spriteDescription1);
            this.Name = "HelpBonusesDialog";
            this.Text = "Bonuses";
            this.ResumeLayout(false);

        }

        #endregion

        private SpriteDescription spriteDescription1;
        private System.Windows.Forms.Button button1;
        private SpriteDescription spriteDescription2;
        private SpriteDescription spriteDescription3;
        private SpriteDescription spriteDescription4;
        private SpriteDescription spriteDescription5;


    }
}