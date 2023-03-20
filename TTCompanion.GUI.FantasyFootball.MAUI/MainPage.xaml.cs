using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace TTCompanion.GUI.FantasyFootball.MAUI
{
    public partial class MainPage : ContentPage
    {
        private List<Button> menuItems;

        public MainPage()
        {
            InitializeComponent();
            DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;
            GetMenuItems();
            SetupMenuGrid();
        }

        private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            SetupMenuGrid();
        }

        private void GetMenuItems()
        {
            //menuItems.Clear();
            menuItems = new List<Button>();
            menuItems.Add(
                new Button()
                {
                    Text = "Teams"
                });
            menuItems.Add(
                new Button()
                {
                    Text = "Leagues"
                });
            menuItems.Add(
                new Button()
                {
                    Text = "Tornaments"
                });
            menuItems.Add(
                new Button()
                {
                    Text = "Settings"
                });
            menuItems.Add(
                new Button()
                {
                    Text = "Exit"
                });
            menuItems.Add(
                new Button()
                {
                    Text = "Extra Button"
                });
        }

        private void SetupMenuGrid()
        {
            var numberOfColumns = 5;
            if (DeviceDisplay.Current.MainDisplayInfo.Width < DeviceDisplay.Current.MainDisplayInfo.Height)
            {
                numberOfColumns = 2;
            }

            MenuGrid.Children.Clear();
            MenuGrid.ColumnDefinitions.Clear();
            MenuGrid.RowDefinitions.Clear();

            ColumnDefinition cd = new ColumnDefinition();
            for (int i = 0; i < numberOfColumns; i++)
            {
                MenuGrid.AddColumnDefinition(cd);
            }

            decimal val = (decimal)menuItems.Count / (decimal)numberOfColumns;
            var numberOfRows = Math.Ceiling(val);

            RowDefinition rd = new RowDefinition();
            for (int i = 0; i < numberOfRows; i++)
            {
                MenuGrid.AddRowDefinition(rd);
            }

            var column = 0;
            var row = 0;

            foreach (Button button in menuItems)
            {
                MenuGrid.SetColumn(button, column);
                MenuGrid.SetRow(button, row);
                MenuGrid.Children.Add(button);

                column++;
                if (column >= numberOfColumns)
                {
                    column = 0;
                    row++;
                }
            }
        }
    }
}