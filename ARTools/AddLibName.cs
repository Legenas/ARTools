using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace ARTools
{
    public partial class AddLibName : Form
    {
        public AddLibName()
        {
            InitializeComponent();
        }

        private bool m_isMouseDown = false;
        private Point m_mousePos = new Point();
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_mousePos = Cursor.Position;
            m_isMouseDown = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_isMouseDown = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_isMouseDown)
            {
                Point tempPos = Cursor.Position;
                this.Location = new Point(Location.X + (tempPos.X - m_mousePos.X), Location.Y + (tempPos.Y - m_mousePos.Y));
                m_mousePos = Cursor.Position;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AddLibName.ActiveForm.Close();
            Globals.Ribbons.ModelBase.button1.Enabled = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newName = textBox1.Text.Trim();
            if (newName.IndexOf(' ') >= 0)
            {
                AddLibName.ActiveForm.Enabled = false;
                Globals.Ribbons.ModelBase.button1.Enabled = true;
                MessageBox.Show("模型库名称里不允许有空格");
            }
            Properties.Settings.Default.NewLibraryName = newName;

            string rootDirectoryPath = Properties.Settings.Default.DirectoryPath;
            string newLibName = Properties.Settings.Default.NewLibraryName;
            string newLibPath = rootDirectoryPath + newLibName;
            if (Directory.Exists(newLibPath))
            {
                MessageBox.Show("已经包含相同名称的模型库");
            }
            else
            {
                Directory.CreateDirectory(newLibPath);
                Properties.Settings.Default.LibraryPath = newLibPath;
                Properties.Settings.Default.Save();
                string newConfigFile = newLibPath + "/" + "config.json";
                if (File.Exists(newConfigFile))
                    MessageBox.Show("系统出错，新建的文件夹下面不应该有文件");
                else
                {
                    FileStream fs = File.Create(newConfigFile);
                    JObject config = new JObject();
                    config.Add("LibName", newLibName);
                    config.Add("models", new JArray());
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(config.ToString());
                    sw.Flush();
                    sw.Close();
                }
            }
            AddLibName.ActiveForm.Close();
            MessageBox.Show("设置成功！");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
