using System;
using System.Collections;
using UnityEngine;

namespace Network
{
    public class WebSocketHandler
    {
        private static WebSocketHandler _instance;

        private WebSocketHandler(Uri uri)
        {
            getWsConnection = new WebSocket(uri);
        }

        public IEnumerator WebSocketConnection => getWsConnection.Connect();

        public WebSocket getWsConnection { get; }

        public static WebSocketHandler CreateInstance(Uri uri)
        {
            if (_instance == null) _instance = new WebSocketHandler(uri);

            return _instance;
        }

        // Кидаем вектор на сервер, айдишник пока не кидаем, генерируем его на сервере аналогично как здесь
        public void sendMessage(Vector3 vector, Quaternion rotation, string id)
        {
            /*var jsonObject = new JSONObject(JSONObject.Type.OBJECT);

            // Создаем json объект и добавляем в него 2 поля с коордами по x и z
            jsonObject.AddField("x", vector.x.ToString());
            jsonObject.AddField("z", vector.z.ToString());*/

            var message = vector.x + " " + vector.z + ":" + rotation.y + ":" + id;

            getWsConnection.SendString(message);
        }
    }
}