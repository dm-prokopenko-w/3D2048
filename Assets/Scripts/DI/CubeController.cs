using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

public class CubeController : IStartable
{
    [Inject] private TouchController _touchController;
    [Inject] private ChanceTable _chanceTable;
    
    public UnityEvent<uint> OnUpdateCounter = new();
    
    public Transform SpawnTransform 
    {
        set
        {
            _spawnTransform = value;
        }
    }
    
    public Transform DisableCubesTransform 
    {
        set
        {
            _disableCubesTransform = value;
        }
    }
    
    private Transform 
        _spawnTransform,
        _disableCubesTransform;
    
    private ObjectPool<CubeView> 
        _pool;
    
    private CubeView 
        _cubePrefab,
        _currentCube;
    
    private CubePo2Generator 
        _cubePo2Generator;
    
    private uint _countScore = 0;
    
    public CubeController(ChanceTable chanceTable)
    {
        _cubePo2Generator = new CubePo2Generator(chanceTable);
    }
    
    public void Start()
    {
        _cubePrefab = Resources.Load<CubeView>(Constants.CubePath);
        _pool = new ObjectPool<CubeView>(_cubePrefab, _disableCubesTransform);
        _touchController.TouchStart.AddListener(OnTouchStart); 
        _touchController.TouchEnd.AddListener(OnTouchEnd); 
        _touchController.TouchMoved.AddListener(OnTouchMoved); 
        OnUpdateCounter?.Invoke(0);
    }
    
    private void OnTouchStart(PointerEventData eventData)
    {
        var hitPoint = Utils.GetRaycastPointOnPlane(eventData, _spawnTransform.position);
        var spawnPosition = _spawnTransform.position;
        
        if (hitPoint.HasValue)
        {
            spawnPosition.x = hitPoint.Value.x;
        }

        _currentCube = SpawnCube(_cubePo2Generator.GetRandomNumber(), spawnPosition);
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

            if (currentCube.GetPo2Num().number != cube.number) return;
            
            var po2Num = currentCube.GetPo2Num();
            if (po2Num.configCube != null)
            {
                var next = _cubePo2Generator.GetNextChance(po2Num.number);

                if (next != null && next.configCube != null)
                {
                    var spawnCube = SpawnCube(next, cubeView.transform.position);
                    spawnCube.AddImpulse(Vector3.up, 10f, true);
                }

                UpdateCounter(po2Num.configCube);
            }
            
            ResetView(currentCube);
            ResetView(cubeView);
        }
    }

    private void UpdateCounter(ConfigCube configCube)
    {
        _countScore += configCube.ScoreCount;
        OnUpdateCounter?.Invoke(_countScore);
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
        var hitPoint = Utils.GetRaycastPointOnPlane(eventData, _spawnTransform.position);
        if (!hitPoint.HasValue) return;
        if (eventData.delta.x > 0)
        {
            if (_currentCube.transform.position.x < 3.5f)
            {
                _currentCube.transform.position = new Vector3(hitPoint.Value.x, 0, 0);
            }
        }
        else if (eventData.delta.x < 0)
        {
            if (_currentCube.transform.position.x > -3.5f)
            {
                _currentCube.transform.position = new Vector3(hitPoint.Value.x, 0, 0);
            }
        }

    }
}
