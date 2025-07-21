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


    private void Button_Clicked(object sender, EventArgs e)
    {

    }

}
