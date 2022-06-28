using System;
using System.Collections;
using System.Collections.Generic;
using Network;
using UnityEngine;

public class NetworkHandler : MonoBehaviour
{
    // Флаг статуса веб сокет соединения
    public bool status;
    public GameObject networkPlayer;

    private readonly string ip = "212.109.219.49"; //"212.109.219.49";

    // Типо словарь ключ - значение, <id игрока, сам игрок>
    private readonly Dictionary<string, GameObject>
        players = new Dictionary<string, GameObject>(); // Сразу создается пустой экземпляр 

    private readonly string port = "88"; //88

    private string _currentPlayerId;

    // Айдишник веб сокет сервера, который получаем ответом на http запрос
    private string webSocketId;

    public WebSocketHandler getWsHandler { get; private set; }

    private IEnumerator Start()
    {
        status = false;
        var httpHandler = new HttpHandler("http://" + ip + ":" + port + "/"); // Корневой маршрут сервера
        yield return httpHandler.HttpConnection;

        var connectionData = httpHandler.ConnectionDataResult.Split(' ');

        _currentPlayerId = connectionData[0];
        webSocketId = connectionData[1];

        // Прошел http запрос на получание айдишника 
        Debug.Log(webSocketId + "      " + _currentPlayerId);

        getWsHandler = WebSocketHandler.CreateInstance(new Uri("ws://" + ip + ":" + port + "/" + webSocketId));
        yield return StartCoroutine(getWsHandler.WebSocketConnection); // Произошел веб сокет коннект

        status = true;
    }

    private void Update()
    {
        if (getWsHandler == null) return;

        var wsConnection = getWsHandler.getWsConnection;
        if (wsConnection == null) return;

        // Отслеживает сообщение с сервера
        var message = wsConnection.RecvString();
        if (message != null) ApplyGameMessage(message);
    }

    public string currentPlayerId()
    {
        return _currentPlayerId;
    }

    private void ApplyGameMessage(string messageData)
    {
        // Сообщение типа ",displayName:id:0 10:0:true"

        var playersMessage = messageData.Split(';');

        for (var i = 1; i < playersMessage.Length; i++)
        {
            var data = playersMessage[i].Split(':');
            //var displayName = data[0];
            var id = data[1];

            if (id == _currentPlayerId) continue;

            if (!players.ContainsKey(id))
                players.Add(id, Instantiate(networkPlayer, Vector3.zero, Quaternion.identity));

            var vcStr = data[2];
            //var rtStr = data[3];

            var vcArr = vcStr.Split(' ');

            // Приводим строковые значения к float
            var x = float.Parse(vcArr[0]);
            var z = float.Parse(vcArr[1]);

            var player = players[id]; // Достаем игрока из словаря
            player.transform.position = new Vector3(x, 0, z); // И ебенем ему вектор
        }
    }
}