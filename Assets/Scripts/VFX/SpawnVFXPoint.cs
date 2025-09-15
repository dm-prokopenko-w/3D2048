using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class SpawnVFXPoint : MonoBehaviour
{
    [Inject] private VFXController _vfxController;
    
    [Inject]
    public void Construct()
    {
        _vfxController.SpawnTransform = transform;
    }
}
