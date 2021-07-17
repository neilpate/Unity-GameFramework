using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game Settings")]
public class GameSettings : ScriptableObject
{
    public bool GodMode = false;

    public AudioClip mainMusic;
}
