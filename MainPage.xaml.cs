using ControleArquivosApp.ViewModels;

namespace ControleArquivosApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent(); 
        BindingContext = new MainViewModel();
    }
}
