using Microsoft.Maui.Controls;
using TTCompanion.GUI.FantasyFootball.MAUI.Views.Base;

namespace TTCompanion.GUI.FantasyFootball.MAUI.Views;
public partial class TeamsPage : MenuPageBase
{
    public TeamsPage()
	{
		InitializeComponent();
    }

    public override void AddButtons()
    {
        base.AddButtons();
        Button button = new Button();
        button.Text = "Create new team";
        button.Clicked += MenuItem_ClickedAsync;
        _menu.Add(button);

        button = new Button();
        button.Text = "Existing team";
        button.CommandParameter = "1";
        button.Clicked += MenuItem_ClickedAsync;
        _menu.Add(button);

        button = new Button();
        button.Text = "Existing team";
        button.CommandParameter = "2";
        button.Clicked += MenuItem_ClickedAsync;
        _menu.Add(button);

        button = new Button();
        button.Text = "Existing team";
        button.CommandParameter = "3";
        button.Clicked += MenuItem_ClickedAsync;
        _menu.Add(button);
    }

    public override async void MenuItem_ClickedAsync(object sender, EventArgs e)
    {
        var button = sender as Button;
        switch (button.Text)
        {
            case "Create new team":
                await Navigation.PushAsync(new TeamsPage());
                break;
        }
    }
}