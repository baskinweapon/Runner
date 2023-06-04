using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

public class GroundBlock : BaseBlock {
    private List<GameObject> _boosters = new List<GameObject>();
    
    private List<string> keys = new List<string>() {"boost"};
    AsyncOperationHandle<IList<GameObject>> loadHandle;
    
    // load boosters
    // public IEnumerator Start() {
    //     loadHandle = Addressables.LoadAssetsAsync<GameObject>(
    //         keys,
    //         addressable => {
    //             _boosters.Add(addressable);
    //             if (_boosters.Count == 3) {
    //                 Spawn();
    //             }
    //         }, Addressables.MergeMode.Union,
    //         false);
    //     yield return loadHandle;
    // }
    
    private void Spawn() {
        
        if (Random.Range(0, 10) > GameManager.instance.gameSettings.boostSettings.probabilitySpawn) {
            var boost = Instantiate(_boosters[Random.Range(0, _boosters.Count)]);
            boost.transform.position = transform.position + Vector3.up * 1.5f;
            boost.transform.rotation = transform.rotation;
        }
    }

    protected override void Do(Player player) {
        
    }
    
    // private void OnDestroy() {
    //     Addressables.Release(loadHandle);
    // }
}
