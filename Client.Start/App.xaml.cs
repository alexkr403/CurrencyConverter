using System;
using System.Windows;
using Client.Start.CompositionRoot;

namespace Client.Start
{
    public partial class App : Application
    {
        public static Workstation Root
        {
            get;
            private set;
        }


        public App()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            try
            {
                using (Root = new Workstation())
                {
                    Root.Init();

                    var app = new App();
                    MainWindow window = new MainWindow();
                    app.Run(window);
                }

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                //should log

                MessageBox.Show(ex.Message);

                Environment.Exit(1);
            }
        }

    }
}
