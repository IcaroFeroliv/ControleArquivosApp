using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Diagnostics;
using System.IO;

namespace ControleArquivosApp.Services
{
    public class GoogleSheetsHelper
    {
        private readonly SheetsService _sheetService;

        public GoogleSheetsHelper()
        {
            // Caminho absoluto para o JSON de credenciais
            var credentialsPath = Path.Combine(AppContext.BaseDirectory, "GoogleServiceAccounts", "credentials.json");

            // Cria as credenciais com escopo de leitura
            var credential = GoogleCredential
                .FromFile(credentialsPath)
                .CreateScoped(SheetsService.Scope.Spreadsheets);

            // Inicializa o servi�o do Google Sheets
            _sheetService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "ControleArquivosApp"
            });
        }

        public IList<IList<object>> ReadRange(string spreadsheetId, string range)
        {
            try
            {
                var request = _sheetService.Spreadsheets.Values.Get(spreadsheetId, range);
                var response = request.Execute();
                return response.Values;
            }
            catch (Google.GoogleApiException ex)
            {
                Debug.WriteLine($"[Sheets API] Erro: {ex.HttpStatusCode} - {ex.Message}");

                if (ex.Error != null && ex.Error.Errors != null)
                {
                    foreach (var err in ex.Error.Errors)
                    {
                        Debug.WriteLine($" - {err.Message}");
                    }
                }

                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Sheets API] Erro inesperado: {ex.Message}");
                throw;
            }
        }

        public void AppendRow(string spreadsheetId, string range, IList<object> values)
        {
            var valueRange = new ValueRange { Values = new List<IList<object>> { values } };
            var request = _sheetService.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            request.Execute();
        }

    }
}
