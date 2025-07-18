using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.IO;

namespace ControleArquivosApp.Services
{
    public class GoogleSheetsHelper
    {
        private readonly SheetsService _sheetService;

        public GoogleSheetsHelper(string credentialsPath)
        {
            var credential = GoogleCredential
               .FromFile(credentialsPath)
               .CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);

            _sheetService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "ControleArquivosApp"
            });
        }

        public IList<IList<object>> ReadRange(string spreadsheetId, string range)
        {
            var request = _sheetService.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = request.Execute();
            return response.Values;
        }
    }
}
