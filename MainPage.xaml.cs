using ControleArquivosApp.ViewModels;

namespace ControleArquivosApp;

public partial class MainPage : ContentPage
{
    private MainViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();

        _viewModel = new MainViewModel();
        BindingContext = _viewModel;

        _viewModel.LoadGoogleSheets("1Wcv8QkgRHzJwMlmObAtG0TRLdz9aocoA7v_aN_GnQvo");

        // Atualiza a logo ao abrir o app conforme o tema atual
        UpdateLogo();
    }

    private async void OnSelecionarArquivosClicked(object sender, EventArgs e)
    {
        if (BindingContext is MainViewModel vm)
            await vm.SelecionarArquivosAsync();
    }

    private async void OnSelecionarPastaClicked(object sender, EventArgs e)
    {
        if (BindingContext is MainViewModel vm)
            await vm.SelecionarDiretorioAsync();
    }

    private async void OnAvancarClicked(object sender, EventArgs e)
    {
        try
        {
            if (BindingContext is MainViewModel vm)
                await vm.RenomearArquivosAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void OnThemeToggleClicked(object sender, EventArgs e)
    {
        if (App.Current.UserAppTheme == AppTheme.Dark)
            App.Current.UserAppTheme = AppTheme.Light;
        else
            App.Current.UserAppTheme = AppTheme.Dark;

        // Troca as cores do gradiente conforme o tema selecionado
        UpdateGradientColors();
        UpdateLogo();
    }

    private void UpdateGradientColors()
    {
        if (App.Current.UserAppTheme == AppTheme.Dark)
        {
            // Cores claras
            MeuGradientStop1.Color = Colors.White;
            MeuGradientStop2.Color = Color.FromArgb("#C8C8C8");
            MeuGradientStop3.Color = Color.FromArgb("#ACACAC");
            Titulo.TextColor = Color.FromArgb("#141414");
        }
        else
        {
            // Cores escuras
            MeuGradientStop1.Color = Color.FromArgb("#141414");
            MeuGradientStop2.Color = Color.FromArgb("#212121");
            MeuGradientStop3.Color = Color.FromArgb("#828282");
            Titulo.TextColor = Colors.White;
        }
    }



    private void UpdateLogo()
    {

        var theme = App.Current.UserAppTheme;

        if (LogoImage != null)
        {
            LogoImage.Source = theme == AppTheme.Dark ? "logowithe.png" : "logodark.png";
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        // Você pode remover esse método se não for usar.
    }
}
