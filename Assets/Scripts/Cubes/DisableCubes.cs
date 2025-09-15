using UnityEngine;
using VContainer;

public class DisableCubes : MonoBehaviour
{
    [Inject] private CubeController _cubeController;
    
    [Inject]
    public void Construct()
    {
        _cubeController.DisableCubesTransform = transform;
    }
}
