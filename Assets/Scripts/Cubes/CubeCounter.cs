using TMPro;
using UnityEngine;
using VContainer;

public class CubeCounter : MonoBehaviour
{
    [Inject] private CubeController _cubeController;
    
    [SerializeField] private TMP_Text _txtCounter;
    [Inject]
    public void Construct()
    {
        _cubeController.OnUpdateCounter.AddListener(counter =>
        {
            _txtCounter.text = "Score: " + counter; 
        });
    }
}
