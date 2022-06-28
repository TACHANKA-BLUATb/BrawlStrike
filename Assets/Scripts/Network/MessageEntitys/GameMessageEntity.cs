using UnityEngine;

namespace Network
{
    // Сущность сообщения от сервера
    public class GameMessageEntity
    {
        public string displayName;
        public string id;
        public bool isRedTeam;
        public Vector3 position;
        public float rotation;
    }
}