using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWaveSet", menuName = "ScriptableObjects/EnemyWaveSet", order = 2)]
public class EnemyWaveSet : ScriptableObject
{
    public EnemyWaveData[] waves; // Mảng các wave
}
