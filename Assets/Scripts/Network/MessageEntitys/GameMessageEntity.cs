using UnityEngine;

namespace Network
{
    // Сущность сообщения от сервера
    public class GameMessageEntity
    {
        public Vector3 position;
        public float rotation;
        public string id;
        public string displayName;
        public bool isRedTeam;
    }
}