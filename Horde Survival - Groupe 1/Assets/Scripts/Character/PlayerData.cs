using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "Data", menuName = "PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {
        public float characterSpeed = 20f;
    }
}