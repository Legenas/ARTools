namespace ARTools
{
    partial class ModelBase : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ModelBase()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelBase));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.model_base = this.Factory.CreateRibbonGroup();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gallery1 = this.Factory.CreateRibbonGallery();
            this.libOperation = this.Factory.CreateRibbonMenu();
            this.newLib = this.Factory.CreateRibbonButton();
            this.packageLib = this.Factory.CreateRibbonButton();
            this.deleteFromLib = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.setUpDir = this.Factory.CreateRibbonButton();
            this.resetDir = this.Factory.CreateRibbonButton();
            this.loadLib = this.Factory.CreateRibbonButton();
            this.button1 = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.model_base.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Groups.Add(this.model_base);
            this.tab1.Label = "ARTools";
            this.tab1.Name = "tab1";
            // 
            // model_base
            // 
            this.model_base.Items.Add(this.gallery1);
            this.model_base.Items.Add(this.libOperation);
            this.model_base.Items.Add(this.loadLib);
            this.model_base.Items.Add(this.button1);
            this.model_base.Label = "模型库";
            this.model_base.Name = "model_base";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // gallery1
            // 
            this.gallery1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.gallery1.Image = ((System.Drawing.Image)(resources.GetObject("gallery1.Image")));
            this.gallery1.ItemImageSize = new System.Drawing.Size(40, 40);
            this.gallery1.Label = "模型库";
            this.gallery1.Name = "gallery1";
            this.gallery1.ScreenTip = "模型库";
            this.gallery1.ShowImage = true;
            this.gallery1.SuperTip = "模型库功能，点击模型库出现模型名称列表";
            this.gallery1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.gallery1_Click);
            this.gallery1.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.modelLib_ItemsLoading);
            // 
            // libOperation
            // 
            this.libOperation.Items.Add(this.newLib);
            this.libOperation.Items.Add(this.packageLib);
            this.libOperation.Items.Add(this.deleteFromLib);
            this.libOperation.Items.Add(this.separator1);
            this.libOperation.Items.Add(this.setUpDir);
            this.libOperation.Items.Add(this.resetDir);
            this.libOperation.Label = "库操作";
            this.libOperation.Name = "libOperation";
            this.libOperation.ScreenTip = "库操作";
            this.libOperation.SuperTip = "新建库、打包库、从库删除、设置库目录以及恢复库目录";
            // 
            // newLib
            // 
            this.newLib.Image = ((System.Drawing.Image)(resources.GetObject("newLib.Image")));
            this.newLib.Label = "新建库";
            this.newLib.Name = "newLib";
            this.newLib.ScreenTip = "新建库";
            this.newLib.ShowImage = true;
            this.newLib.SuperTip = "在库目录下新建一个模型库";
            this.newLib.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.newLib_Click);
            // 
            // packageLib
            // 
            this.packageLib.Label = "打包库";
            this.packageLib.Name = "packageLib";
            this.packageLib.ScreenTip = "打包库";
            this.packageLib.ShowImage = true;
            this.packageLib.SuperTip = "打包模型库";
            this.packageLib.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.packageLib_Click);
            // 
            // deleteFromLib
            // 
            this.deleteFromLib.Label = "从库删除";
            this.deleteFromLib.Name = "deleteFromLib";
            this.deleteFromLib.ScreenTip = "从库删除";
            this.deleteFromLib.ShowImage = true;
            this.deleteFromLib.SuperTip = "从模型库中删除某模型";
            this.deleteFromLib.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.deleteFromLib_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // setUpDir
            // 
            this.setUpDir.Image = ((System.Drawing.Image)(resources.GetObject("setUpDir.Image")));
            this.setUpDir.Label = "设置库目录";
            this.setUpDir.Name = "setUpDir";
            this.setUpDir.ScreenTip = "设置库目录";
            this.setUpDir.ShowImage = true;
            this.setUpDir.SuperTip = "设置模型库目录";
            this.setUpDir.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.setUpDir_Click);
            // 
            // resetDir
            // 
            this.resetDir.Label = "恢复库目录";
            this.resetDir.Name = "resetDir";
            this.resetDir.ScreenTip = "恢复库目录";
            this.resetDir.ShowImage = true;
            this.resetDir.SuperTip = "将库目录恢复成默认位置";
            this.resetDir.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.resetDir_Click);
            // 
            // loadLib
            // 
            this.loadLib.Label = "加载库";
            this.loadLib.Name = "loadLib";
            this.loadLib.ScreenTip = "加载库";
            this.loadLib.SuperTip = "加载一个模型库（文件夹）";
            this.loadLib.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.loadLib_Click);
            // 
            // button1
            // 
            this.button1.Label = "添加模型";
            this.button1.Name = "button1";
            this.button1.ScreenTip = "添加模型";
            this.button1.SuperTip = "添加模型至模型库";
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // ModelBase
            // 
            this.Name = "ModelBase";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ModelBase_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.model_base.ResumeLayout(false);
            this.model_base.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup model_base;
        internal Microsoft.Office.Tools.Ribbon.RibbonGallery gallery1;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu libOperation;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton newLib;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton deleteFromLib;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton setUpDir;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton resetDir;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton packageLib;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton loadLib;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        private System.Windows.Forms.ImageList imageList1;
    }

    partial class ThisRibbonCollection
    {
        internal ModelBase ModelBase
        {
            get { return this.GetRibbon<ModelBase>(); }
        }
    }
}
