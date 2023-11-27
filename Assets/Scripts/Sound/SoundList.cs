using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoundList", menuName = "Custom/SoundList")]
public class SoundList: ScriptableObject
{
    public List<Sound> Sounds;
}
