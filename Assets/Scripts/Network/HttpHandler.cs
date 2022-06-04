using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public class HttpHandler
    {
        // Адрес сервера
        private readonly string _uri;

        // Объект http запроса
        private UnityWebRequest webRequest;

        // Конструктор хендлера
        public HttpHandler(string uri)
        {
            _uri = uri;
        }

        // Геттер айдишника
        public string WebSocketId { get; private set; }

        // Геттер метода коннекта
        public IEnumerator HttpConnection => _HttpConnection();

        // Запрос на получение айдишника веб сокет сервера
        private IEnumerator _HttpConnection()
        {
            var form = new WWWForm();
            // Костыль эмитации отправки запроса из лоби
            form.AddField("players", "1");

            using (webRequest = UnityWebRequest.Post(_uri, form))
            {
                yield return webRequest.SendWebRequest();
                // получаем айдишник из ответа на запрос
                WebSocketId = webRequest.downloadHandler.text;
            }
        }
    }
}