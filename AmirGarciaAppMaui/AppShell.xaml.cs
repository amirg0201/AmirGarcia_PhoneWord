namespace AmirGarciaAppMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.AgNotePage), typeof(Views.AgNotePage));
        }
    }
}
