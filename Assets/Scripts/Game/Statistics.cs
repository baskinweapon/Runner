using System.Collections.Generic;
using DefaultNamespace.Blocks;
using UnityEngine;


public class Statistics : MonoBehaviour {
    private Dictionary<BlockType, int> _blockStatistics = new Dictionary<BlockType, int>();

    private void OnEnable() {
        GameManager.instance.OnGameOver += GetStatistics;
        GameManager.instance.OnFinishGame += GetStatistics;
    }

    private GameObject _lastBlock;
    // find block under player. because player can jump, we can not use trigger
    private void Update() {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out var hit, 100f)) {
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
            if (_lastBlock != hit.transform.gameObject) {
                _lastBlock = hit.transform.gameObject;
                var block = _lastBlock.GetComponent<BaseBlock>();
                if (block) {
                    AddBlock(block.type);
                }
            }
        }
    }
    
    private void AddBlock(BlockType type) {
        if (type == BlockType.Ground || type == BlockType.Finish || type == BlockType.Rotate) return;
        if (_blockStatistics.ContainsKey(type)) {
            _blockStatistics[type]++;
        } else {
            _blockStatistics.Add(type, _blockStatistics.ContainsKey(type) ? _blockStatistics[type] + 1 : 1);
        }
    }

    private void GetStatistics() {
        GameManager.instance.OnStatistics?.Invoke(_blockStatistics);
    }

    private void OnDisable() {
        GameManager.instance.OnGameOver -= GetStatistics;
        GameManager.instance.OnFinishGame -= GetStatistics;
    }
}
