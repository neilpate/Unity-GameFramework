using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Munchy Settings")]
public class MunchySettings : ScriptableObject
{ 
    public Color Color;
    public int PointsValue;
    public float LifeTimeInSeconds;
    public float LiklihoodOfSpawningPercentPerSecond;

}
