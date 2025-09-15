using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

public class DisableVFX : MonoBehaviour
{
    [Inject] private VFXController _vfxController;
    
    [Inject]
    public void Construct()
    {
        _vfxController.DisableVFXTransform = transform;
    }
}
