using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI Settings")]
public class AISettings : ScriptableObject
{
    public enum AggressionLevel
    {
        Passive,
        Lazy,
        Medium,
        NeverGivesUp
    }

    public float Speed;
    public AggressionLevel Aggression; 

}
