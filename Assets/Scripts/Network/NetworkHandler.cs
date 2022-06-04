using System;
using System.Collections;
using System.Collections.Generic;
using Network;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHandler : MonoBehaviour
{
    // Флаг статуса веб сокет соединения
    public bool status;
    public GameObject networkPlayer;

    private readonly string ip = "localhost"; //"212.109.219.49";
    private readonly float MoveForce = 8f;

    // Типо словарь ключ - значение, <id игрока, сам игрок>
    private readonly Dictionary<string, GameObject>
        players = new Dictionary<string, GameObject>(); // Сразу создается пустой экземпляр 

    private readonly string port = "5000"; //88

    // Основное веб сокет соединение
    private WebSocket _webSocketConnection;

    // Счетчик подключений
    private int counter;

    // Объект http запроса
    private UnityWebRequest webRequest;

    // Айдишник веб сокет сервера, который получаем ответом на http запрос
    private string webSocketId;

    private IEnumerator Start()
    {
        status = false;
        var httpHandler = new HttpHandler("http://" + ip + ":" + port + "/"); // Корневой маршрут сервера
        yield return httpHandler.HttpConnection; //HttpConnection("http://" + ip + ":" + port + "/");
        webSocketId = httpHandler.WebSocketId;

        // Прошел http запрос на получание айдишника 
        Debug.Log(webSocketId);

        _webSocketConnection =
            new WebSocket(new Uri("ws://" + ip + ":" + port + "/" +
                                  webSocketId)); // Корневой маршрут сервера + /айдишник
        yield return StartCoroutine(_webSocketConnection.Connect());
        // Произошел веб сокет коннект

        // В качестве айдишника игрока пока юзаем счетчик подключений, он также закеширован на сервере
        counter++;

        // Пихаем плеера в Dictionary по строковому ключу id
        players.Add(counter.ToString(), Instantiate(networkPlayer, Vector3.zero, Quaternion.identity));

        status = true;
    }

    private void Update()
    {
        if (_webSocketConnection == null) return;

        // Отслеживает сообщение с сервера
        var message = _webSocketConnection.RecvString();
        if (message != null)
        {
            Debug.Log(message);

            // Из сообщения получаем данные
            // Для сущности сообщения сделал класс NetworkEntity
            var entity = GetNetworkPlayerData(message); // Метод получения сущности сообщения

            var player = players[entity.id]; // Достаем игрока из словаря
            player.transform.position += entity.vector; // И ебенем ему вектор
        }
    }


    // Кидаем вектор на сервер, айдишник пока не кидаем, генерируем его на сервере аналогично как здесь
    public void sendMessage(Vector3 vector)
    {
        var jsonObject = new JSONObject(JSONObject.Type.OBJECT);

        // Создаем json объект и добавляем в него 2 поля с коордами по x и z
        jsonObject.AddField("x", vector.x.ToString());
        jsonObject.AddField("z", vector.z.ToString());

        _webSocketConnection.SendString(jsonObject.ToString());
    }

    // Запрос на получение айдишника веб сокет сервера
    private IEnumerator HttpConnection(string uri)
    {
        var form = new WWWForm();
        // Костыль эмитации отправки запроса из лоби
        form.AddField("players", "1");

        using (webRequest = UnityWebRequest.Post(uri, form))
        {
            yield return webRequest.SendWebRequest();
            // получаем айдишник из ответа на запрос
            webSocketId = webRequest.downloadHandler.text;
        }
    }

    // Вектор с сервера приходит в формате строки коорды разделены символом пробела
    // Также прокинул айдишник для нахождения нужного игрока
    // Пример: "1 0.132523523 0.123124124"
    private NetworkVectorEntity GetNetworkPlayerData(string obj)
    {
        var array = obj.Split(' '); // Сплитом разбиваем строку на массив по символу пробела

        // Приводим строковые значения к float
        var x = float.Parse(array[1]);
        var z = float.Parse(array[2]);

        var id = array[0];
        Debug.Log(x);
        Debug.Log(z);
        Debug.Log(id);

        var entity = new NetworkVectorEntity();
        entity.id = id;
        entity.vector = new Vector3(x, 0, z);

        return entity;
    }
}