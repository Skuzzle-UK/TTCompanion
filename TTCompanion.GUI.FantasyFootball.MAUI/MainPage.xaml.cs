namespace TTCompanion.GUI.FantasyFootball.MAUI
{
    public partial class MainPage : ContentPage
    {
        private List<Button> menuItems;

        public MainPage()
        {
            InitializeComponent();
            Menu.SizeChanged += Menu_MainDisplayInfoChanged;
            GetMenuItems();
            SetupMenuGrid();
        }

        private void Menu_MainDisplayInfoChanged(object sender, EventArgs e)
        {
            SetupMenuGrid();
        }

        private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            SetupMenuGrid();
        }

        private void GetMenuItems()
        {
            menuItems = Menu.Children.OfType<Button>().ToList();
        }

        private void SetupMenuGrid()
        {
            double buttonSize;
            if (DeviceDisplay.Current.MainDisplayInfo.Width < DeviceDisplay.Current.MainDisplayInfo.Height)
            {
                buttonSize = Math.Floor(Menu.Width / 2);
            }
            else
            {
                buttonSize = Math.Floor(Menu.Width / 4);
            }

            foreach (var item in menuItems)
            {
                item.WidthRequest = buttonSize;
                item.HeightRequest = buttonSize;
            }
        }

        private async void MenuItem_ClickedAsync(object sender, EventArgs e)
        {
            var button = sender as Button;
            switch (button.Text)
            {
                case "Teams":
                    await Navigation.PushAsync(new TeamsPage());
                    break;
                case "Skills quick reference":
                    break;
                case "Leagues":
                    break;
                case "Tournament":
                    break;
                case "Settings":
                    break;
            }
        }
    }
}