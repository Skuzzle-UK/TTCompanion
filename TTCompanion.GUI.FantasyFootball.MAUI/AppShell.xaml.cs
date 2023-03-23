using TTCompanion.GUI.FantasyFootball.MAUI.Views;

namespace TTCompanion.GUI.FantasyFootball.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("loading", typeof(LoginPage));
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("main", typeof(MainPage));
        }
    }
}