using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cubes", menuName = "Configs/Cubes")]
public class ConfigCube : ScriptableObject
{
    public List<Material> CubeMaterials;
    public uint ScoreCount;
}