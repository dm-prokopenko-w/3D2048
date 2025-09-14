using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

public class CubeSpawner : IStartable
{
    [Inject] private TouchController _touchController;
    [Inject] private ChanceTable _chanceTable;
    
    public Transform SpawnTransform 
    {
        set
        {
            _spawnTransform = value;
        }
    }
    
    private Transform _spawnTransform;
    private IObjectResolver _container;
    private ObjectPool<CubeView> _pool;
    private CubeView _cubePrefab;
    private CubeView _currentCube;
    private CubePo2Generator _cubePo2Generator;
    
    public CubeSpawner(IObjectResolver container, ChanceTable chanceTable)
    {
        _container = container;
        _cubePo2Generator = new CubePo2Generator(chanceTable);
    }
    
    public void Start()
    {
        _cubePrefab = Resources.Load<CubeView>("Prefabs/CubePrefab");
        _pool = new ObjectPool<CubeView>(_cubePrefab, _spawnTransform);
        _touchController.TouchStart.AddListener(OnTouchStart); 
        _touchController.TouchEnd.AddListener(OnTouchEnd); 
        _touchController.TouchMoved.AddListener(OnTouchMoved); 
    }
    
    private void OnTouchStart(PointerEventData eventData)
    {
        _currentCube = SpawnCube(_cubePo2Generator.GetRandomNumber(), _spawnTransform.position);
    }

    private CubeView SpawnCube(ChanceEntry chance, Vector3 position)
    {
        var currentCube = _pool.Spawn(_cubePrefab, position, _spawnTransform.rotation, _spawnTransform);
        currentCube.Setup(chance, UpdatePo2);
        return currentCube;
    }
    
    private void UpdatePo2(CubeView currentCube, Collision collision)
    {
        if(currentCube.gameObject.GetInstanceID() > collision.gameObject.GetInstanceID()) return;

        if (collision.gameObject.TryGetComponent<CubeView>(out var cubeView))
        {
            var cube = cubeView.GetPo2Num();
            if (cube.configCube == null)
            {
                ResetView(currentCube);
                ResetView(cubeView);
                return;
            }
            
            if (currentCube.GetPo2Num().number == cube.number)
            {
                var po2Num = currentCube.GetPo2Num();
                if (po2Num.configCube != null)
                {
                    var next = _cubePo2Generator.GetNextChance(po2Num.number);

                    if (next != null && next.configCube != null)
                    {
                        var spawnCube = SpawnCube(next, cubeView.transform.position);
                        spawnCube.AddImpulse(Vector3.up, 10f, true);
                    }
                }
                ResetView(currentCube);
                ResetView(cubeView);
            }
        }
    }

    private void ResetView(CubeView view)
    {
        view.ResetView();
        _pool.Despawn(view);
    }
    
    private void OnTouchEnd(PointerEventData eventData)
    {
        _currentCube.AddImpulse(_currentCube.transform.forward, 30f);
    }
    
    private void OnTouchMoved(PointerEventData eventData)
    {
        if (eventData.delta.x > 0)
        {
            if (_currentCube.transform.position.x < 3.5f)
            {
                _currentCube.transform.position += new Vector3(0.2f, 0, 0);
            }
        }
        else if (eventData.delta.x < 0)
        {
            if (_currentCube.transform.position.x > -3.5f)
            {
                _currentCube.transform.position -= new Vector3(0.2f, 0, 0);
            }
        }
    }
}
