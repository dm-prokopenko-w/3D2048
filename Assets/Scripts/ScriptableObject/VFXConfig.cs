using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VFX", menuName = "Configs/VFX")]
public class VFXConfig : ScriptableObject
{
    [SerializeField] private List<VFXData> vfxs;
    
    public List<VFXData> VFXs => vfxs; 
}

[System.Serializable]
public class VFXData
{
    public string NameVfx;
    public Behaviour VFXPrefab;
}