using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private List<TMP_Text> _listPo2Nums;

    private Action<CubeView, Collision> _onTriggerEnter;
    private ChanceEntry _chanceEntry;
    
    public void Setup(ChanceEntry chanceEntry, Action<CubeView, Collision> onTriggerEnter)
    {
        _chanceEntry = chanceEntry;
        _onTriggerEnter = onTriggerEnter;
        _meshRenderer.materials = chanceEntry.configCube.CubeMaterials.ToArray();
        foreach (var po2NumTxt in _listPo2Nums)
        {
            po2NumTxt.text = chanceEntry.number.ToString();
        }
    }

    public void ResetView()
    {
        if(_rb != null)
        {
            _rb.Sleep();
        }
    }
    
    public (int number, ConfigCube configCube) GetPo2Num()
    {
        if (_chanceEntry == null)
        {
            return (0,null);
        }

        return (_chanceEntry.number, _chanceEntry.configCube);
    }

    public void AddImpulse(Vector3 direction, float force, bool isRotation = false)
    {
        if (isRotation)
        {
            _rb.AddForce(direction.normalized * force, ForceMode.Impulse);

            var randomTorque = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            );
            _rb.AddTorque(randomTorque * force, ForceMode.Impulse);
        }
        else
        {
            _rb.AddForce(direction * force, ForceMode.Impulse);
        }

    }
    
    private void OnCollisionEnter(Collision other)
    {
        _onTriggerEnter?.Invoke(this, other);
    }
}
