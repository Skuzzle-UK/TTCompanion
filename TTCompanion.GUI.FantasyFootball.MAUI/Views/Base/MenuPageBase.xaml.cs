namespace TTCompanion.GUI.FantasyFootball.MAUI.Views.Base;

public partial class MenuPageBase : ContentPage
{
    public readonly FlexLayout _menu;
    private readonly List<Button> _menuItems;

    public MenuPageBase()
    {
        InitializeComponent();
        _menu = this.GetTemplateChild("Menu") as FlexLayout;
        AddButtons();
        _menuItems = _menu.Children.OfType<Button>().ToList();
        _menu.SizeChanged += Menu_MainDisplayInfoChanged;
        SetupMenuGrid();
    }

    public virtual void AddButtons()
    {
    }

    private void Menu_MainDisplayInfoChanged(object sender, EventArgs e)
    {
        SetupMenuGrid();
    }

    private void SetupMenuGrid()
    {
        double buttonSize;
        if (DeviceDisplay.Current.MainDisplayInfo.Width < DeviceDisplay.Current.MainDisplayInfo.Height)
        {
            buttonSize = Math.Floor(_menu.Width / 2);
        }
        else
        {
            buttonSize = Math.Floor(_menu.Width / 4);
        }

        foreach (var item in _menuItems)
        {
            item.WidthRequest = buttonSize;
            item.HeightRequest = buttonSize;
        }
    }

    public virtual void MenuItem_ClickedAsync(object sender, EventArgs e)
    {
    }
}