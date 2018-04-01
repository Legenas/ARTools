using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ARTools
{
    public partial class ModelDelete : Form
    {
        public ModelDelete()
        {
            InitializeComponent();
        }
        /*
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
        }*/

        string libPath = Properties.Settings.Default.LibraryPath;
        private void ModelDelete_Load(object sender, EventArgs e)
        {
            if (libPath != String.Empty)
            {
                if (Directory.Exists(libPath))
                {
                    try
                    {
                        string configPath = libPath + @"\config.json";
                        FileStream fs = new FileStream(configPath, FileMode.Open, FileAccess.ReadWrite);
                        StreamReader sr = new StreamReader(fs);
                        string jsonText = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                        JObject config = (JObject)JsonConvert.DeserializeObject(jsonText);
                        JArray models = (JArray)config["models"];
                        foreach (var model in models)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = model.ToString();
                            listView1.Items.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("读取配置文件时出错\n" + ex.Message);
                    }
                }
                else
                {
                    Properties.Settings.Default.LibraryPath = "";
                    Properties.Settings.Default.Save();
                    MessageBox.Show("模型库路径不存在，请新建或者重新加载库。");
                }
            }
            else
            {
                MessageBox.Show("请先新建或者加载库！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModelDelete.ActiveForm.Close();
            Globals.Ribbons.ModelBase.deleteFromLib.Enabled = true;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void ModelDelete_Refresh()
        {
            try
            {
                string configPath = libPath + @"\config.json";
                FileStream fs = new FileStream(configPath, FileMode.Open, FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs);
                string jsonText = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                JObject config = (JObject)JsonConvert.DeserializeObject(jsonText);
                JArray models = (JArray)config["models"];
                listView1.Items.Clear();
                foreach (var model in models)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = model.ToString();
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取配置文件时出错\n" + ex.Message);
            }
        }

        public static void DeleteDirFiles(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.listView1.FocusedItem != null)
                {
                    ListViewItem item = this.listView1.GetItemAt(e.X, e.Y);
                    string selectedModelName = item.Text;

                    if (!String.IsNullOrEmpty(libPath))
                    {
                        try
                        {
                            string configPath = libPath + @"\config.json";
                            FileStream fs = new FileStream(configPath, FileMode.Open, FileAccess.ReadWrite);
                            StreamReader sr = new StreamReader(fs);
                            string jsonText = sr.ReadToEnd();
                            sr.Close();
                            fs.Close();

                            JObject config = (JObject)JsonConvert.DeserializeObject(jsonText);
                            JArray models = (JArray)config["models"];
                            foreach (var model in models)
                            {
                                if (model.ToString().Equals(selectedModelName))
                                {
                                    models.Remove(model);
                                    break;
                                }
                            }
                            config["models"] = models;

                            fs = new FileStream(configPath, FileMode.OpenOrCreate, FileAccess.Write);
                            StreamWriter sw = new StreamWriter(fs);
                            fs.SetLength(0);
                            sw.Write(config.ToString());
                            sw.Flush();
                            sw.Close();
                            fs.Close();
                            Properties.Settings.Default.LibraryRefresh = 1;
                            Properties.Settings.Default.Save();
                            ModelDelete_Refresh();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("读写配置文件时出错\n" + ex.Message);
                        }

                        try
                        {
                            string modelPath = libPath + @"\" + selectedModelName;
                            DeleteDirFiles(modelPath);
                            Directory.Delete(modelPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("删除模型库\n" + ex.Message);
                            return;
                        }
                    }
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {

        }
    }
}
