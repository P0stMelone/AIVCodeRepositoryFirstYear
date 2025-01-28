namespace EsercizioBonus
{
    partial class Form1
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
            this.Testo = new System.Windows.Forms.Label();
            this.BottoneSomma = new System.Windows.Forms.Button();
            this.Numero1 = new System.Windows.Forms.TextBox();
            this.Numero2 = new System.Windows.Forms.TextBox();
            this.BottoneDifferenza = new System.Windows.Forms.Button();
            this.BottoneMoltiplicazione = new System.Windows.Forms.Button();
            this.BottoneDivisione = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Testo
            // 
            this.Testo.AutoSize = true;
            this.Testo.Location = new System.Drawing.Point(473, 160);
            this.Testo.Name = "Testo";
            this.Testo.Size = new System.Drawing.Size(48, 13);
            this.Testo.TabIndex = 0;
            this.Testo.Text = "Risultato";
            this.Testo.Click += new System.EventHandler(this.Testo_Click);
            // 
            // BottoneSomma
            // 
            this.BottoneSomma.Location = new System.Drawing.Point(193, 109);
            this.BottoneSomma.Name = "BottoneSomma";
            this.BottoneSomma.Size = new System.Drawing.Size(75, 23);
            this.BottoneSomma.TabIndex = 1;
            this.BottoneSomma.Text = "Somma";
            this.BottoneSomma.UseVisualStyleBackColor = true;
            this.BottoneSomma.Click += new System.EventHandler(this.button1_Click);
            // 
            // Numero1
            // 
            this.Numero1.Location = new System.Drawing.Point(193, 157);
            this.Numero1.Name = "Numero1";
            this.Numero1.Size = new System.Drawing.Size(100, 20);
            this.Numero1.TabIndex = 2;
            this.Numero1.Text = "10";
            this.Numero1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Numero2
            // 
            this.Numero2.Location = new System.Drawing.Point(340, 157);
            this.Numero2.Name = "Numero2";
            this.Numero2.Size = new System.Drawing.Size(100, 20);
            this.Numero2.TabIndex = 3;
            this.Numero2.Text = "7";
            // 
            // BottoneDifferenza
            // 
            this.BottoneDifferenza.Location = new System.Drawing.Point(274, 109);
            this.BottoneDifferenza.Name = "BottoneDifferenza";
            this.BottoneDifferenza.Size = new System.Drawing.Size(75, 23);
            this.BottoneDifferenza.TabIndex = 4;
            this.BottoneDifferenza.Text = "Differenza";
            this.BottoneDifferenza.UseVisualStyleBackColor = true;
            this.BottoneDifferenza.Click += new System.EventHandler(this.BottoneDifferenza_Click);
            // 
            // BottoneMoltiplicazione
            // 
            this.BottoneMoltiplicazione.Location = new System.Drawing.Point(355, 109);
            this.BottoneMoltiplicazione.Name = "BottoneMoltiplicazione";
            this.BottoneMoltiplicazione.Size = new System.Drawing.Size(85, 23);
            this.BottoneMoltiplicazione.TabIndex = 5;
            this.BottoneMoltiplicazione.Text = "Moltiplicazione";
            this.BottoneMoltiplicazione.UseVisualStyleBackColor = true;
            this.BottoneMoltiplicazione.Click += new System.EventHandler(this.button3_Click);
            // 
            // BottoneDivisione
            // 
            this.BottoneDivisione.Location = new System.Drawing.Point(446, 109);
            this.BottoneDivisione.Name = "BottoneDivisione";
            this.BottoneDivisione.Size = new System.Drawing.Size(75, 23);
            this.BottoneDivisione.TabIndex = 6;
            this.BottoneDivisione.Text = "Divisione";
            this.BottoneDivisione.UseVisualStyleBackColor = true;
            this.BottoneDivisione.Click += new System.EventHandler(this.BottoneDivisione_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BottoneDivisione);
            this.Controls.Add(this.BottoneMoltiplicazione);
            this.Controls.Add(this.BottoneDifferenza);
            this.Controls.Add(this.Numero2);
            this.Controls.Add(this.Numero1);
            this.Controls.Add(this.BottoneSomma);
            this.Controls.Add(this.Testo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Testo;
        private System.Windows.Forms.Button BottoneSomma;
        private System.Windows.Forms.TextBox Numero1;
        private System.Windows.Forms.TextBox Numero2;
        private System.Windows.Forms.Button BottoneDifferenza;
        private System.Windows.Forms.Button BottoneMoltiplicazione;
        private System.Windows.Forms.Button BottoneDivisione;
    }
}

