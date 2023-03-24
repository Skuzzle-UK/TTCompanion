using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace TTCompanion.GUI.FantasyFootball.MAUI.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private string _authToken;
        public LoginPage()
        {
            _httpClient = MauiProgram.httpClient;
            InitializeComponent();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (IsCredentialCorrect(Username.Text, Password.Text).Result)
            {
                await SecureStorage.SetAsync("hasAuth", "true");
                await SecureStorage.SetAsync("authToken", _authToken);
                await Shell.Current.GoToAsync("///main");
            }
            else
            {
                SecureStorage.RemoveAll();
                await DisplayAlert("Login failed", "Username or password is invalid", "Try again");
            }
        }

        private async Task<bool> IsCredentialCorrect(string username, string password)
        {
            var user = new User() { Username = username, Password = password };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("https://localhost:7295/ttcompanion/api/v1/authentication/authenticate", data).Result;
            var text = response.Content.ReadAsStringAsync();
            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            _authToken = text.Result;
            return true;
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}