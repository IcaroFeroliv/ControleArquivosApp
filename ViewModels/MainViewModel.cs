using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ControleArquivosApp.ViewModels;
using ControleArquivosApp.Services;
using System.Windows.Input;

namespace ControleArquivosApp.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _responsavel;
    private string _projeto;
    private string _opcaoSelecionadaArquivo;
    private string _opcaoSelecionadaFase;


    private Tarefa _opcaoSelecionadaTarefa;
    public Tarefa OpcaoSelecionadaTarefa
    {
        get => _opcaoSelecionadaTarefa;
        set => SetProperty(ref _opcaoSelecionadaTarefa, value);
    }

    private Descricao _opcaoSelecionadaDescricao;
    public Descricao OpcaoSelecionadaDescricao
    {
        get => _opcaoSelecionadaDescricao;
        set => SetProperty(ref _opcaoSelecionadaDescricao, value);
    }



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

    public string OpcaoSelecionadaFase
    {
        get => _opcaoSelecionadaFase;
        set => SetProperty(ref _opcaoSelecionadaFase, value);
    }

    public class TipoDisciplina
    {
        public string SiglaDisciplina { get; set; }
        public string DescricaoDisciplina { get; set; }

        public override string ToString() => DescricaoDisciplina; 
    }


    public ObservableCollection<TipoDisciplina> TiposDisciplinas{ get; } = new()
    {
        new() { SiglaDisciplina = "ORC", DescricaoDisciplina = "Orçamento" },
        new() { SiglaDisciplina = "CFV", DescricaoDisciplina = "Projeto de Alarme e Circuito Fechado de Televisão" },
        new() { SiglaDisciplina = "ARQ", DescricaoDisciplina = "Projeto Arquitetônico" },
        new() { SiglaDisciplina = "CBM", DescricaoDisciplina = "Projeto de Cabeamento Estruturado" },
        new() { SiglaDisciplina = "CLM", DescricaoDisciplina = "Projeto de Climatização" },
        new() { SiglaDisciplina = "CMV", DescricaoDisciplina = "Projeto de Comunicação Visual" },
        new() { SiglaDisciplina = "DRE", DescricaoDisciplina = "Projeto de Drenagem Pluvial" },
        new() { SiglaDisciplina = "ELE", DescricaoDisciplina = "Projeto de Elétrico" },
        new() { SiglaDisciplina = "EFV", DescricaoDisciplina = "Projeto de Energia Fotovoltaica" },
        new() { SiglaDisciplina = "GAS", DescricaoDisciplina = "Projeto de Gás" },
        new() { SiglaDisciplina = "GSM", DescricaoDisciplina = "Projeto de Gases Medicinais" },
        new() { SiglaDisciplina = "GEO", DescricaoDisciplina = "Projeto de Geométrico" },
        new() { SiglaDisciplina = "HDS", DescricaoDisciplina = "Projeto Hidrossanitário" },
        new() { SiglaDisciplina = "PAV", DescricaoDisciplina = "Projeto de Pavimentação" },
        new() { SiglaDisciplina = "PCI", DescricaoDisciplina = "Projeto de Prevenção e Combate a Incêndio" },
        new() { SiglaDisciplina = "PSG", DescricaoDisciplina = "Projeto de Paisagismo" },
        new() { SiglaDisciplina = "R3D", DescricaoDisciplina = "Projeto de Representação Tridimensional" },
        new() { SiglaDisciplina = "RST", DescricaoDisciplina = "Projeto de Restauro" },
        new() { SiglaDisciplina = "SAA", DescricaoDisciplina = "Projeto de Sistema de Abastecimento de Água" },
        new() { SiglaDisciplina = "SES", DescricaoDisciplina = "Projeto de Sistema de Esgoto Sanitário" },
        new() { SiglaDisciplina = "SPD", DescricaoDisciplina = "Projeto de Sistema de Proteção a Descargas Atmosféricas" },
        new() { SiglaDisciplina = "SNL", DescricaoDisciplina = "Projeto de Sinalização Vertical e Horizontal" },
        new() { SiglaDisciplina = "SON", DescricaoDisciplina = "Projeto de Sonorização" },
        new() { SiglaDisciplina = "EST", DescricaoDisciplina = "Projeto Estrutural" },
        new() { SiglaDisciplina = "TOP", DescricaoDisciplina = "Projeto Topográfico" },
        new() { SiglaDisciplina = "TRP", DescricaoDisciplina = "Projeto de Terraplenagem" },
        new() { SiglaDisciplina = "URB", DescricaoDisciplina = "Projeto de Urbanização" },
        new() { SiglaDisciplina = "SND", DescricaoDisciplina = "Sondagem" }
    };
    
    private TipoDisciplina _OpcaoSelecionadaDisciplina;
    public TipoDisciplina OpcaoSelecionadaDisciplina
    {
        get => _OpcaoSelecionadaDisciplina;
        set => SetProperty(ref _OpcaoSelecionadaDisciplina, value);
    }

    private string _numeroVersao;
    public string NumeroVersao
    {
        get => _numeroVersao;
        set
        {
            if (!string.IsNullOrEmpty(value) && !value.All(char.IsDigit))
            {
                OnPropertyChanged(nameof(NumeroVersao)); // Corrigido aqui!
                return;
            }

            _numeroVersao = value;
            OnPropertyChanged(nameof(NumeroVersao)); // Corrigido aqui!
        }
    }

    private string _numeroArquivo;
    public string NumeroArquivo
    {
        get => _numeroVersao;
        set
        {
            if (!string.IsNullOrEmpty(value) && !value.All(char.IsDigit))
            {
                OnPropertyChanged(nameof(NumeroArquivo)); // Corrigido aqui!
                return;
            }

            _numeroVersao = value;
            OnPropertyChanged(nameof(NumeroArquivo)); // Corrigido aqui!
        }
    }

    public class Tarefa
    {
        public string SiglaTarefa { get; set; }
        public string NomeTarefa { get; set; }

        public override string ToString() => NomeTarefa;
    }

    public class Descricao
    {
        public string SiglaDescricao { get; set; }
        public string NomeDescricao { get; set; }

        public override string ToString() => NomeDescricao;
    }


    public ObservableCollection<Tarefa> Tarefas { get; } = new();
    public ObservableCollection<Descricao> Descricoes { get; } = new();


    public void LoadGoogleSheets(string sheetId)
    {
        var helper = new GoogleSheetsHelper("Resources/Raw/credentials.json");

        var tarefasRaw = helper.ReadRange(sheetId, "Tarefas!A2:B");
        foreach (var row in tarefasRaw)
            Tarefas.Add(new Tarefa
            {
                SiglaTarefa = row[1]?.ToString(),
                NomeTarefa = row[0]?.ToString()
            });

        var descRaw = helper.ReadRange(sheetId, "Descricoes!A2:B");
        foreach (var row in descRaw)
            Descricoes.Add(new Descricao
            {
                SiglaDescricao = row[1]?.ToString(),
                NomeDescricao = row[0]?.ToString()
            });
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
