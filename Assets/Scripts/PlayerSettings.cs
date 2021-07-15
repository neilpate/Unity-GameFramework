using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player Settings")]
public class PlayerSettings : ScriptableObject
{
    public float RotationSpeedDegreesPerSecond = 360;
    public float ForwardSpeedMetersPerSecond = 5;
}
