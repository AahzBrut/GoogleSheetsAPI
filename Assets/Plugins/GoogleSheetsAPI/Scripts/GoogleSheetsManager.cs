#if UNITY_EDITOR
using System;
using System.Collections;
using System.IO;
using System.Text;
using GoogleSheetsAPI.Domain;
using GoogleSheetsAPI.Utils;
using Newtonsoft.Json;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

namespace GoogleSheetsAPI
{
    public delegate void OnSheetLoadComplete(GoogleSpreadSheet sheet);

    
    public static class GoogleSheetsManager
    {
        private const string APIKeyPath = @"%USERPROFILE%\GoogleAPIKey.txt";

        private static string APIKey => File.ReadAllText(Environment.ExpandEnvironmentVariables(APIKeyPath));

        public static void ReadPublicSpreadSheet(GoogleSheetRequest request, OnSheetLoadComplete callback)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append("https://sheets.googleapis.com/v4/spreadsheets");
            urlBuilder.Append($"/{request.SheetId}");
            urlBuilder.Append("/values");
            urlBuilder.Append($"/{request.WorkSheetName}!{request.CellsRange}");
            urlBuilder.Append($"?key={APIKey}");

            StartCoroutine(GetRequest(urlBuilder.ToString(), callback));
        }

        private static void StartCoroutine(IEnumerator getRequest)
        {
            EditorCoroutines.Execute(getRequest);
        }

        private static IEnumerator GetRequest(string uri, OnSheetLoadComplete callback)
        {
            using var webRequest = Get(uri);
            yield return webRequest.SendWebRequest();

            while (webRequest.result == Result.InProgress) yield return webRequest;

            if (webRequest.error != null)
            {
                Debug.LogError($"Error: {webRequest.error}");
                yield break;
            }

            var response = JsonConvert.DeserializeObject<GoogleSheetsResponse>(webRequest.downloadHandler.text);

            callback.Invoke(response.ToSpreadSheet());
        }
    }
}
#endif