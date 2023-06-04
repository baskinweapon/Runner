using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 0)]
public class GameSettings : ScriptableObject {
    public BoostSettings boostSettings;
    public BlockSettings blockSettings;
}

[Serializable]
public struct BoostSettings {
    [Header("0 = 0%, 10 = 100%")]
    [Range(0, 10)]
    public int probabilitySpawn;
    public float speedUpTime;
    public float shieldTime;
    public int healthCount;
}

[Serializable]
public struct BlockSettings {
    public int spawnCount;
}

