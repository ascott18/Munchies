namespace Munchies.HelpDialogs
{
    partial class HelpEnemiesDialog
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
            this.spriteDescription1.Description = "The fork\'s tines are honed to razor sharpness. Avoid it!";
            this.spriteDescription1.Image = global::Munchies.Properties.Resources.Fork1;
            this.spriteDescription1.Location = new System.Drawing.Point(12, 12);
            this.spriteDescription1.Name = "spriteDescription1";
            this.spriteDescription1.Size = new System.Drawing.Size(358, 40);
            this.spriteDescription1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(324, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // spriteDescription2
            // 
            this.spriteDescription2.Description = "The knife is pretty sharp, too. It means instant death. Avoid it, too.";
            this.spriteDescription2.Image = global::Munchies.Properties.Resources.Knife1;
            this.spriteDescription2.Location = new System.Drawing.Point(12, 58);
            this.spriteDescription2.Name = "spriteDescription2";
            this.spriteDescription2.Size = new System.Drawing.Size(358, 40);
            this.spriteDescription2.TabIndex = 0;
            // 
            // spriteDescription3
            // 
            this.spriteDescription3.Description = "The Spoon is not sharp, but it shoots droplets of deadly poison. Avoid it!";
            this.spriteDescription3.Image = global::Munchies.Properties.Resources.Spoon1;
            this.spriteDescription3.Location = new System.Drawing.Point(12, 104);
            this.spriteDescription3.Name = "spriteDescription3";
            this.spriteDescription3.Size = new System.Drawing.Size(358, 40);
            this.spriteDescription3.TabIndex = 0;
            // 
            // spriteDescription4
            // 
            this.spriteDescription4.Description = "Are you getting the idea? Avoid it, avoid it, avoid it.";
            this.spriteDescription4.Image = global::Munchies.Properties.Resources.Skull1;
            this.spriteDescription4.Location = new System.Drawing.Point(12, 150);
            this.spriteDescription4.Name = "spriteDescription4";
            this.spriteDescription4.Size = new System.Drawing.Size(358, 40);
            this.spriteDescription4.TabIndex = 0;
            // 
            // spriteDescription5
            // 
            this.spriteDescription5.Description = "You can tell the Smart Skull from the normal, stupid skulls by the glasses. Shees" +
    "h! Talk about sterotypes!";
            this.spriteDescription5.Image = global::Munchies.Properties.Resources.SmartSkull1;
            this.spriteDescription5.Location = new System.Drawing.Point(12, 196);
            this.spriteDescription5.Name = "spriteDescription5";
            this.spriteDescription5.Size = new System.Drawing.Size(358, 56);
            this.spriteDescription5.TabIndex = 0;
            // 
            // HelpEnemiesDialog
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(403, 282);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.spriteDescription5);
            this.Controls.Add(this.spriteDescription4);
            this.Controls.Add(this.spriteDescription3);
            this.Controls.Add(this.spriteDescription2);
            this.Controls.Add(this.spriteDescription1);
            this.Name = "HelpEnemiesDialog";
            this.Text = "Enemies";
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