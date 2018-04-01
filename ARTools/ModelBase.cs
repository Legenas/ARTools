using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Win32;

namespace ARTools
{
    public partial class ModelBase
    {

        public const string CONFIGFILE = "config.json";

        private void ModelBase_Load(object sender, RibbonUIEventArgs e)
        {

        }

        int libCan = 1;
        private void gallery1_Click(object sender, RibbonControlEventArgs e)
        {
            string libPath = Properties.Settings.Default.LibraryPath;
            if (Directory.Exists(libPath) && libPath != String.Empty)
            {

            }
            else
            {
                MessageBox.Show("请重新加载库,如果加载库后还是有问题，请先给PPT获取管理员权限再打开");
                libCan = 0;
            }
        }

        private RibbonDropDownItem CreateRibbonDropDownItem()
        {
            return this.Factory.CreateRibbonDropDownItem();
        }

        private void modelLib_ItemsLoading(object sender, RibbonControlEventArgs e)
        {
            string rootDirectoryPath = Properties.Settings.Default.DirectoryPath;
            string libPath = Properties.Settings.Default.LibraryPath;

            if (rootDirectoryPath != String.Empty && Directory.Exists(rootDirectoryPath))
            {
                if (libCan == 1 || (libCan == 0 && Properties.Settings.Default.LibraryRefresh == 1))
                {
                    //library需要刷新，先清空里面原本的信息，然后再重新赋值
                    DirectoryInfo libInfo = new DirectoryInfo(libPath);
                    bool libIsOK = false;
                    foreach (FileInfo f in libInfo.GetFiles())
                    {
                        if (f.Name.Contains(CONFIGFILE))
                        {
                            libIsOK = true;
                            break;
                        }
                    }
                    if (libIsOK)
                    {
                        if (gallery1.Items.Count != 0)
                        {
                            for (int i = 0; i < gallery1.Items.Count; i++)
                            {
                                gallery1.Items[i].Label = "";
                            }
                        }
                        gallery1.Items.Clear();
                        try
                        {
                            FileStream fs = new FileStream(libPath + "/config.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            StreamReader sr = new StreamReader(fs);
                            string jsonText = sr.ReadToEnd();
                            sr.Close();
                            JObject config = (JObject)JsonConvert.DeserializeObject(jsonText);
                            JArray models = (JArray)config["models"];
                            fs.Close();

                            int size = 0;
                            foreach (var model in models)
                            {
                                size++;
                                RibbonDropDownItem item = CreateRibbonDropDownItem();
                                item.Label = model.ToString();
                                gallery1.Items.Add(item);
                            }
                            if(size == 0)
                            {
                                MessageBox.Show("模型库为空，请先添加模型。");
                            }

                            libCan = 0;
                            Properties.Settings.Default.LibraryRefresh = 0;
                            Properties.Settings.Default.Save();

                        }
                         catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n读取配置文件出错，请确定拥有读权限。");
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有配置文件，请重新加载库");
                    }
                }
            }
            else
            {
                MessageBox.Show("请新建库或加载库。");
            }
        }

        private void newLib_Click(object sender, RibbonControlEventArgs e)
        {

            /* 如果模型库目录为空，默认将它放在程序运行目录下面的 */
            if (Properties.Settings.Default.DirectoryPath == String.Empty)
            {
                RegistryKey path1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ARTools", false);
                string path = System.AppDomain.CurrentDomain.BaseDirectory;//当前项目debug时的的目录，也就是debug目录
                Properties.Settings.Default.DirectoryPath = path;
                Properties.Settings.Default.Save();
            }

            /* 新建模型库 */
            string rootDirectoryPath = Properties.Settings.Default.DirectoryPath;
            AddLibName addLibName = null;
            if (addLibName == null || addLibName.IsDisposed)
            {
                addLibName = new AddLibName();
                IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
                NativeWindow win = NativeWindow.FromHandle(handle);
                addLibName.Show();
                newLib.Enabled = false;
            }
            /*
            string newLibName = Properties.Settings.Default.NewLibraryName;
            string newLibPath = rootDirectoryPath + '\\' + newLibName;
            if (Directory.Exists(newLibPath))
            {
                MessageBox.Show("已经包含相同名称的模型库");
            }
            else
            {
                Directory.CreateDirectory(newLibPath);
                Properties.Settings.Default.LibraryPath = newLibPath;
                string newConfigFile = newLibPath + '\\' + CONFIGFILE;
                if (File.Exists(newConfigFile))
                    MessageBox.Show("系统出错，新建的文件夹下面不应该有文件");
                else
                {
                    FileStream fs = File.Create(newConfigFile);
                    JObject config = new JObject();
                    config.Add("LibName",newLibName);
                    config.Add("models",new JArray());
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(config.ToString());
                    sw.Flush();
                    sw.Close();
                }
            }*/
        }

        private void packageLib_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void deleteFromLib_Click(object sender, RibbonControlEventArgs e)
        {
            /* 如果模型库目录为空，默认将它放在程序运行目录下面的 */
            ModelDelete modelDelete = null;
            if (Properties.Settings.Default.LibraryPath == String.Empty)
            {
                MessageBox.Show("请先加载或者新建一个模型库！");
            }
            else
            {
                if (modelDelete == null || modelDelete.IsDisposed)
                {
                    modelDelete = new ModelDelete();
                    IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
                    NativeWindow win = NativeWindow.FromHandle(handle);
                    modelDelete.Show();
                }
            }
        }

        private void setUpDir_Click(object sender, RibbonControlEventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.ShowNewFolderButton = true;
            if (fb.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DirectoryPath = fb.SelectedPath;
                Properties.Settings.Default.DirectoryPath = String.Empty;
                Properties.Settings.Default.Save();
                MessageBox.Show("模型库默认目录设置成功");
            }
        }

        private void resetDir_Click(object sender, RibbonControlEventArgs e)
        {
            RegistryKey path1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ARTools", false);
            string path = System.AppDomain.CurrentDomain.BaseDirectory;//当前项目debug时的的目录，也就是debug目录

            Properties.Settings.Default.DirectoryPath = path;
            Properties.Settings.Default.Save();
            MessageBox.Show("模型库恢复为默认目录（ARTools安装文件夹）");
        }

        private void loadLib_Click(object sender, RibbonControlEventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.ShowNewFolderButton = true;
            if (fb.ShowDialog() == DialogResult.OK) {
                if (checkPathIsLib(fb.SelectedPath))
                {
                    if (Properties.Settings.Default.DirectoryPath == String.Empty)
                    {
                        RegistryKey path1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ARTools", false);
                        string path = System.AppDomain.CurrentDomain.BaseDirectory;//当前项目debug时的的目录，也就是debug目录

                        Properties.Settings.Default.DirectoryPath = path;
                        Properties.Settings.Default.Save();
                    }

                    try
                    {
                        string destDir = CopyDirectory(fb.SelectedPath, Properties.Settings.Default.DirectoryPath);
                        Properties.Settings.Default.LibraryPath = destDir;
                        Properties.Settings.Default.LibraryRefresh = 1;
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n拷贝文件时出错。");
                    }
                }
            }
        }

        private string CopyDirectory(string srcdir, string desdir)
        {
            string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);

            string desfolderdir = desdir + "\\" + folderName;

            if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
            {
                desfolderdir = desdir + folderName;
            }
            string[] filenames = Directory.GetFileSystemEntries(srcdir);

            try
            {
                foreach (string file in filenames)// 遍历所有的文件和目录
                {
                    if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    {

                        string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                        if (!Directory.Exists(currentdir))
                        {
                            Directory.CreateDirectory(currentdir);
                        }

                        CopyDirectory(file, desfolderdir);
                    }

                    else // 否则直接copy文件
                    {
                        string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                        srcfileName = desfolderdir + "\\" + srcfileName;


                        if (!Directory.Exists(desfolderdir))
                        {
                            Directory.CreateDirectory(desfolderdir);
                        }


                        File.Copy(file, srcfileName);
                    }
                }//foreach 
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return desfolderdir;
        }//function end

        private bool checkPathIsLib(string libPath)
        {
            DirectoryInfo pathInfo = new DirectoryInfo(libPath);
            bool pathIsLib = false;

            foreach (FileInfo fi in pathInfo.GetFiles())
            {
                string fileName = fi.FullName.Substring(fi.FullName.LastIndexOf("\\") + 1); ;
                if (fileName == CONFIGFILE)
                {
                    pathIsLib = true;
                    break;
                }
            }

            if (pathIsLib)
            {
                return true;
            }

            MessageBox.Show("这不是一个图形库");
            return false;
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            string libPath = Properties.Settings.Default.LibraryPath;
            if (libPath != string.Empty && Directory.Exists(libPath))
            {
                OpenFileDialog openNewDialog= new OpenFileDialog();
                openNewDialog.InitialDirectory = libPath;
                openNewDialog.Filter = "模型文件|*.obj;*.fbx;*.3dx";
                openNewDialog.FilterIndex = 1;
                if (openNewDialog.ShowDialog() == DialogResult.OK)
                {
                    string modelSrcName = openNewDialog.FileName;
                    int start = modelSrcName.LastIndexOf(@"\") + 1;
                    int end = modelSrcName.LastIndexOf('.');
                    string modelName = modelSrcName.Substring(start, end - start);
                    string modelDestPath = libPath + '/' + modelName;
                    try
                    {
                        if (Directory.Exists(modelDestPath))
                        {
                            MessageBox.Show("已包含同样名称的模型");
                        }
                        else
                        {
                       
                            Directory.CreateDirectory(modelDestPath);
                            string modelDestName = modelDestPath + @"\" + modelSrcName.Substring(start);
                            File.Create(modelDestName).Close();
                            File.Copy(modelSrcName, modelDestName, true);
                        }

                        FileStream fs = new FileStream(libPath + "/config.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        StreamReader sr = new StreamReader(fs);
                        string jsonText = sr.ReadToEnd();
                        sr.Close();
                        JObject config = (JObject)JsonConvert.DeserializeObject(jsonText);
                        JArray models = (JArray)config["models"];
                        models.Add(modelName);
                        config["models"] = models;
                        fs.Close();
                        fs = new FileStream(libPath + "/config.json", FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(config.ToString());
                        sw.Flush();
                        sw.Close();

                        Properties.Settings.Default.LibraryRefresh = 1;
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception ex)
                    {
                        if (Directory.Exists(modelDestPath))
                            Directory.Delete(modelDestPath);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先新建或重新加载库");
            }
        }
    }
}
