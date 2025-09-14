using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Cubes", menuName = "Configs/Cubes")]
public class ConfigCube : ScriptableObject
{
    public List<Material> CubeMaterials;
    public uint ScoreCount;
}