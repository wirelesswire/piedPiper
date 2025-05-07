namespace DrawingSamples
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBoxDiagram = new PictureBox();
            comboBoxMembershipFunctionFirst = new ComboBox();
            comboBoxMembershipFunctionSecond = new ComboBox();
            comboBoxOperator = new ComboBox();
            buttonDrawDiagram = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDiagram).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxDiagram
            // 
            pictureBoxDiagram.Location = new Point(12, 41);
            pictureBoxDiagram.Name = "pictureBoxDiagram";
            pictureBoxDiagram.Size = new Size(375, 273);
            pictureBoxDiagram.TabIndex = 0;
            pictureBoxDiagram.TabStop = false;
            // 
            // comboBoxMembershipFunctionFirst
            // 
            comboBoxMembershipFunctionFirst.FormattingEnabled = true;
            comboBoxMembershipFunctionFirst.Items.AddRange(new object[] { "Bell", "Binary", "Class L", "Class S", "Gamma", "Gauss", "Left-Right", "Sigmoid", "Step", "Trapeze", "Triangle", "Zadeh" });
            comboBoxMembershipFunctionFirst.Location = new Point(12, 12);
            comboBoxMembershipFunctionFirst.Name = "comboBoxMembershipFunctionFirst";
            comboBoxMembershipFunctionFirst.Size = new Size(121, 23);
            comboBoxMembershipFunctionFirst.TabIndex = 1;
            // 
            // comboBoxMembershipFunctionSecond
            // 
            comboBoxMembershipFunctionSecond.FormattingEnabled = true;
            comboBoxMembershipFunctionSecond.Items.AddRange(new object[] { "Bell", "Binary", "Class L", "Class S", "Gamma", "Gauss", "Left-Right", "Sigmoid", "Step", "Trapeze", "Triangle", "Zadeh" });
            comboBoxMembershipFunctionSecond.Location = new Point(266, 12);
            comboBoxMembershipFunctionSecond.Name = "comboBoxMembershipFunctionSecond";
            comboBoxMembershipFunctionSecond.Size = new Size(121, 23);
            comboBoxMembershipFunctionSecond.TabIndex = 2;
            // 
            // comboBoxOperator
            // 
            comboBoxOperator.FormattingEnabled = true;
            comboBoxOperator.Items.AddRange(new object[] { "Algebraic Product", "Bounded Difference", "Drastic Product", "Einstein Product", "Hamacher Product", "Min", "Yager T-Norm", "Algebraic Sum", "Bounded Sum", "Drastic Sum", "Einstein Sum", "Hamacher Sum", "Max", "Yager S-Norm" });
            comboBoxOperator.Location = new Point(139, 12);
            comboBoxOperator.Name = "comboBoxOperator";
            comboBoxOperator.Size = new Size(121, 23);
            comboBoxOperator.TabIndex = 3;
            // 
            // buttonDrawDiagram
            // 
            buttonDrawDiagram.Location = new Point(12, 320);
            buttonDrawDiagram.Name = "buttonDrawDiagram";
            buttonDrawDiagram.Size = new Size(375, 23);
            buttonDrawDiagram.TabIndex = 4;
            buttonDrawDiagram.Text = "Draw";
            buttonDrawDiagram.UseVisualStyleBackColor = true;
            buttonDrawDiagram.Click += buttonDrawDiagram_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(405, 356);
            Controls.Add(buttonDrawDiagram);
            Controls.Add(comboBoxOperator);
            Controls.Add(comboBoxMembershipFunctionSecond);
            Controls.Add(comboBoxMembershipFunctionFirst);
            Controls.Add(pictureBoxDiagram);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBoxDiagram).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxDiagram;
        private ComboBox comboBoxMembershipFunctionFirst;
        private ComboBox comboBoxMembershipFunctionSecond;
        private ComboBox comboBoxOperator;
        private Button buttonDrawDiagram;
    }
}
