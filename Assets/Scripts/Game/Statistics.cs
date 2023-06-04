using System;
using System.Collections.Generic;
using DefaultNamespace.Blocks;
using UnityEngine;


public class Statistics : MonoBehaviour {
    private Dictionary<BlockType, int> _blockStatistics = new Dictionary<BlockType, int>();

    private void OnEnable() {
        GameManager.instance.OnGameOver += GetStatistics;
        GameManager.instance.OnFinishGame += GetStatistics;
    }

    private GameObject lastBlock;
    private void Update() {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out var hit, 100f)) {
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
            if (lastBlock != hit.transform.gameObject) {
                lastBlock = hit.transform.gameObject;
                var block = lastBlock.GetComponent<BaseBlock>();
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
    
    public void GetStatistics() {
        GameManager.instance.OnStatistics?.Invoke(_blockStatistics);
    }

    private void OnDisable() {
        GameManager.instance.OnGameOver -= GetStatistics;
        GameManager.instance.OnFinishGame -= GetStatistics;
    }
}