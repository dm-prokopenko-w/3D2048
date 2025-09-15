using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class VFXController  
{
    public Transform DisableVFXTransform 
    {
        set
        {
            _disableVFXTransform = value;
        }
    }

    public Transform SpawnTransform 
    {
        set
        {
            _spawnTransform = value;
        }
    }
    private Transform 
        _spawnTransform, 
        _disableVFXTransform;
    
    private Dictionary<string, ObjectPool<Behaviour>> _pools = new();
    private VFXConfig _vfxConfig;
    
    public VFXController(VFXConfig vfxConfig)
    {
        _vfxConfig = vfxConfig;
    }
    
    public void Spawn(string nameVfx, Vector3 positionVFX)
    {
        var vfx = _vfxConfig.VFXs.Find(x => x.NameVfx == nameVfx);
        if(vfx == null) return;

        if (!_pools.ContainsKey(nameVfx))
        {
            var pool = new ObjectPool<Behaviour>(vfx.VFXPrefab, _disableVFXTransform);
            _pools.Add(nameVfx, pool);
            pool.Spawn(vfx.VFXPrefab, positionVFX, Quaternion.identity, _spawnTransform);
        }
        else
        {
            _pools[nameVfx].Spawn(vfx.VFXPrefab, positionVFX, Quaternion.identity, _spawnTransform);
        }
    }
}
