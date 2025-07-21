using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ControleArquivosApp.ViewModels;
using ControleArquivosApp.Services;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

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


    public ObservableCollection<TipoDisciplina> TiposDisciplinas { get; } = new()
    {
        new() { SiglaDisciplina = "ORC", DescricaoDisciplina = "Orçamento" },
        new() { SiglaDisciplina = "CFV", DescricaoDisciplina = "Alarme e Circuito Fechado de Televisão" },
        new() { SiglaDisciplina = "ARQ", DescricaoDisciplina = "Arquitetônico" },
        new() { SiglaDisciplina = "CBM", DescricaoDisciplina = "Cabeamento Estruturado" },
        new() { SiglaDisciplina = "CLM", DescricaoDisciplina = "Climatização" },
        new() { SiglaDisciplina = "CMV", DescricaoDisciplina = "Comunicação Visual" },
        new() { SiglaDisciplina = "DRE", DescricaoDisciplina = "Drenagem Pluvial" },
        new() { SiglaDisciplina = "ELE", DescricaoDisciplina = "Elétrico" },
        new() { SiglaDisciplina = "EFV", DescricaoDisciplina = "Energia Fotovoltaica" },
        new() { SiglaDisciplina = "GAS", DescricaoDisciplina = "Gás" },
        new() { SiglaDisciplina = "GSM", DescricaoDisciplina = "Gases Medicinais" },
        new() { SiglaDisciplina = "GEO", DescricaoDisciplina = "Geométrico" },
        new() { SiglaDisciplina = "HDS", DescricaoDisciplina = "Hidrossanitário" },
        new() { SiglaDisciplina = "PAV", DescricaoDisciplina = "Pavimentação" },
        new() { SiglaDisciplina = "PCI", DescricaoDisciplina = "Prevenção e Combate a Incêndio" },
        new() { SiglaDisciplina = "PSG", DescricaoDisciplina = "Paisagismo" },
        new() { SiglaDisciplina = "R3D", DescricaoDisciplina = "Representação Tridimensional" },
        new() { SiglaDisciplina = "RST", DescricaoDisciplina = "Restauro" },
        new() { SiglaDisciplina = "SAA", DescricaoDisciplina = "Sistema de Abastecimento de Água" },
        new() { SiglaDisciplina = "SES", DescricaoDisciplina = "Sistema de Esgoto Sanitário" },
        new() { SiglaDisciplina = "SPD", DescricaoDisciplina = "Sistema de Proteção a Descargas Atmosféricas" },
        new() { SiglaDisciplina = "SNL", DescricaoDisciplina = "Sinalização Vertical e Horizontal" },
        new() { SiglaDisciplina = "SON", DescricaoDisciplina = "Sonorização" },
        new() { SiglaDisciplina = "EST", DescricaoDisciplina = "Estrutural" },
        new() { SiglaDisciplina = "TOP", DescricaoDisciplina = "Topográfico" },
        new() { SiglaDisciplina = "TRP", DescricaoDisciplina = "Terraplenagem" },
        new() { SiglaDisciplina = "URB", DescricaoDisciplina = "Urbanização" },
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
        get => _numeroArquivo;
        set
        {
            if (!string.IsNullOrEmpty(value) && !value.All(char.IsDigit))
            {
                OnPropertyChanged(nameof(NumeroArquivo)); // Corrigido aqui!
                return;
            }

            _numeroArquivo = value;
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
        var helper = new GoogleSheetsHelper();

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

    public ObservableCollection<string> ArquivosSelecionados { get; } = new();

    public async Task SelecionarArquivosAsync()
    {
        try
        {
            var resultados = await FilePicker.Default.PickMultipleAsync(new PickOptions
            {
                PickerTitle = "Selecione os arquivos a serem renomeados"
            });

            if (resultados != null && resultados.Any())
            {
                ArquivosSelecionados.Clear();

                foreach (var file in resultados)
                    ArquivosSelecionados.Add(file.FullPath);

                // Monta a mensagem com os nomes dos arquivos
                string arquivos = string.Join("\n", resultados.Select(r => Path.GetFileName(r.FullPath)));

                // Exibe o alerta
                await Shell.Current.DisplayAlert("Arquivos Selecionados", arquivos, "OK");

                if (resultados != null && resultados.Any())
                {
                    ArquivosSelecionados.Clear();

                    foreach (var file in resultados)
                        ArquivosSelecionados.Add(file.FullPath);

                    // ✅ Altera a cor do botão
                    CorBotaoSelecionar = (Color)Application.Current.Resources["Primaryseg"]; ;
                }

            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro", $"Erro ao selecionar arquivos: {ex.Message}", "OK");
        }
    }

    private Color _corBotaoSelecionar = (Color)Application.Current.Resources["Gray600"];
    public Color CorBotaoSelecionar
    {
        get => _corBotaoSelecionar;
        set => SetProperty(ref _corBotaoSelecionar, value);
    }

    private string _diretorioDestino;
    public string DiretorioDestino
    {
        get => _diretorioDestino;
        set => SetProperty(ref _diretorioDestino, value);
    }

    private Color _corBotaoSelecionarPasta = (Color)Application.Current.Resources["Gray600"];
    public Color CorBotaoSelecionarPasta
    {
        get => _corBotaoSelecionarPasta;
        set => SetProperty(ref _corBotaoSelecionarPasta, value);
    }

    public async Task SelecionarDiretorioAsync()
    {
#if WINDOWS
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            var hwnd = ((MauiWinUIWindow)App.Current.Windows[0].Handler.PlatformView).WindowHandle;
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

            folderPicker.FileTypeFilter.Add("*");

            var pasta = await folderPicker.PickSingleFolderAsync();

            if (pasta != null)
            {
                DiretorioDestino = pasta.Path;
                CorBotaoSelecionarPasta = (Color)Application.Current.Resources["Primaryseg"]; // muda a cor aqui
            }
#endif
    }

    public async Task RenomearArquivosAsync()
    {
        string primeiroArquivoRenomeado = null; 

        try
        {
            if (ArquivosSelecionados.Count == 0)
                throw new InvalidOperationException("Nenhum arquivo selecionado.");

            if (string.IsNullOrWhiteSpace(OpcaoSelecionadaArquivo))
                throw new InvalidOperationException("Tipo de arquivo não selecionado.");

            if (OpcaoSelecionadaTarefa == null)
                throw new InvalidOperationException("Tarefa não selecionada.");

            if (string.IsNullOrWhiteSpace(OpcaoSelecionadaFase))
                throw new InvalidOperationException("Fase não selecionada.");

            if (OpcaoSelecionadaDisciplina == null)
                throw new InvalidOperationException("Disciplina não selecionada.");

            if (string.IsNullOrWhiteSpace(NumeroArquivo) || !int.TryParse(NumeroArquivo, out int qtdArquivos))
                throw new InvalidOperationException("Número de arquivos inválido.");

            if (ArquivosSelecionados.Count != qtdArquivos)
                throw new InvalidOperationException($"Foram selecionados {ArquivosSelecionados.Count} arquivos, mas o campo indica {qtdArquivos}.");

            if (string.IsNullOrWhiteSpace(NumeroVersao))
                throw new InvalidOperationException("Número da revisão não informado.");

            if (string.IsNullOrWhiteSpace(DiretorioDestino))
                throw new InvalidOperationException("Diretório de destino não selecionado.");

            var siglaArquivo = OpcaoSelecionadaArquivo.Split(" - ")[0];
            var siglaTarefa = OpcaoSelecionadaTarefa.SiglaTarefa;
            var siglaFase = OpcaoSelecionadaFase.Split(" – ")[0];
            var siglaDisc = OpcaoSelecionadaDisciplina.SiglaDisciplina;
            var versao = NumeroVersao.PadLeft(2, '0');
            var total = qtdArquivos;

            for (int i = 0; i < total; i++)
            {
                var original = ArquivosSelecionados[i];
                var ext = Path.GetExtension(original);
                var sequencia = $"{(i + 1).ToString("D2")}{total.ToString("D2")}";
                var novoNome = $"{siglaArquivo}-{siglaTarefa}-{siglaFase}-{siglaDisc}-{sequencia}-REV{versao}{ext}";
                var destino = Path.Combine(DiretorioDestino, novoNome);

                File.Copy(original, destino, overwrite: true);

                if (i == 0)
                    primeiroArquivoRenomeado = novoNome;
            }

            await Shell.Current.DisplayAlert("Sucesso", $"{total} arquivos renomeados com sucesso!", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro ao renomear", ex.Message, "OK");
            return; 
        }

        // Registro no histórico (Google Sheets)
        try
        {
            var helper = new GoogleSheetsHelper();
            helper.AppendRow(
                spreadsheetId: "1Wcv8QkgRHzJwMlmObAtG0TRLdz9aocoA7v_aN_GnQvo",
                range: "Historico!A:E",
                values: new List<object> {
                Responsavel,
                DiretorioDestino,
                primeiroArquivoRenomeado,
                NumeroArquivo,
                DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
                }
            );
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro ao registrar histórico", ex.Message, "OK");
        }
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
