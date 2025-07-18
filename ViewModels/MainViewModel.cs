using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ControleArquivosApp.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _responsavel;
    private string _projeto;
    private string _categoria;
    private string _codigo;

    public string Responsavel
    {
        get => _responsavel;
        set => SetProperty(ref _responsavel, value);
    }

    public ObservableCollection<string> TipoArquivo { get; } = new()
    {
        "ATA - Ata de Reunião",
        "DGN - Diagnóstico",
        "EST - Estudo",
        "GPNK - GeoPackage",
        "LAU - Laudo",
        "LAYOUT - Layout",
        "MANUAL - Manual",
        "MIN - Minuta",
        "MMD - Memoria Descritivo",
        "OFI - Oficio",
        "PLN - Planilha",
        "PPT - Apresentação",
        "PRJ - Projeto",
        "RLT - Relatório",
        "TRF - Termo de Referência"
    };

    private string _opcaoSelecionadaArquivo;
    public string OpcaoSelecionadaArquivo
    {
        get => _opcaoSelecionadaArquivo;
        set => SetProperty(ref _opcaoSelecionadaArquivo, value);
    }

    public ObservableCollection<string> TipoFase { get; } = new()
    {
        "ATP – Nível de Anteprojeto",
        "BSC – Nível Básico",
        "EXE – Nível Executivo"
    };

    private string _opcaoSelecionadaFase;

    public string OpcaoSelecionadaFase
    {
        get => _opcaoSelecionadaFase;
        set => SetProperty(ref _opcaoSelecionadaFase, value);
    }


    public string Projeto
    {
        get => _projeto;
        set => SetProperty(ref _projeto, value);
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
