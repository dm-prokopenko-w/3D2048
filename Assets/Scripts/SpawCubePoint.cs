using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VContainer;

public class SpawCubePoint : MonoBehaviour
{
    [Inject] private CubeSpawner _cubeSpawner;
    
    [Inject]
    public void Construct()
    {
        _cubeSpawner.SpawnTransform = transform;
    }
}
