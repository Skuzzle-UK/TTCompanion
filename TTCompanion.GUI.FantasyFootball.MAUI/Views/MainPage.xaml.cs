using TTCompanion.GUI.FantasyFootball.MAUI.Views.Base;

namespace TTCompanion.GUI.FantasyFootball.MAUI.Views
{
    public partial class MainPage : MenuPageBase
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public override void AddButtons()
        {
            base.AddButtons();
            Button button = new Button();
            button.Text = "Teams";
            button.Clicked += MenuItem_ClickedAsync;
            _menu.Add(button);

            button = new Button();
            button.Text = "Skills quick reference";
            button.Clicked += MenuItem_ClickedAsync;
            _menu.Add(button);

            button = new Button();
            button.Text = "Leagues";
            button.Clicked += MenuItem_ClickedAsync;
            _menu.Add(button);

            button = new Button();
            button.Text = "Tournaments";
            button.Clicked += MenuItem_ClickedAsync;
            _menu.Add(button);

            button = new Button();
            button.Text = "Settings";
            button.Clicked += MenuItem_ClickedAsync;
            _menu.Add(button);
        }

        public override async void MenuItem_ClickedAsync(object sender, EventArgs e)
        {
            var button = sender as Button;
            switch (button.Text)
            {
                case "Teams":
                    //await Shell.Current.GoToAsync("///teams");
                    await Navigation.PushAsync(new TeamsPage());
                    break;
                case "Skills quick reference":
                    break;
                case "Leagues":
                    break;
                case "Tournament":
                    break;
                case "Settings":
                    await Navigation.PushAsync(new SettingsPage());
                    break;
            }
        }
    }
}