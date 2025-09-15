using UnityEngine;
using VContainer;

public class SpawCubePoint : MonoBehaviour
{
    [Inject] private CubeController _cubeController;
    
    [Inject]
    public void Construct()
    {
        _cubeController.SpawnTransform = transform;
    }
}
