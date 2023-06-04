using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Blocks {
    public enum BlockType {
        Rotate,
        Ground,
        Hole,
        HalfHole,
        Obstacle,
        Law,
        Finish
    }
    
    public class BlockSpawner : MonoBehaviour {
        public int countBlocks = 5;
        private List<string> keys = new List<string>() {"block"};
        
        AsyncOperationHandle<IList<GameObject>> loadHandle;
        // load all blocks from addressables
        public IEnumerator Start() {
            countBlocks = GameManager.instance.gameSettings.blockSettings.spawnCount;
            loadHandle = Addressables.LoadAssetsAsync<GameObject>(
                keys,
                addressable => {
                    _blocksPrefabs.Add(addressable.GetComponent<BaseBlock>());
                    // if we load all blocks, spawn them
                    if (_blocksPrefabs.Count == 7) {
                        Spawn();
                    }
                }, Addressables.MergeMode.Union,
                false);

            yield return loadHandle;
        }
        
        private List<BaseBlock> _blocksPrefabs = new List<BaseBlock>();
        //spawn blocks and place them in the scene
        private void Spawn() {
            // spawn first block
            var firstBlock = Instantiate(GetBlockByType(BlockType.Ground), transform);
            firstBlock.transform.position = Vector3.zero;
            
            var lastBlock = firstBlock;
            var lastDirection = Vector3.forward;
            var lastBlockType = BlockType.Ground;
            
            for (int i = 0; i < countBlocks; i++) {
                lastBlockType = CalculateNextBlockType(lastBlockType);
                var block = Instantiate(GetBlockByType(lastBlockType), transform);
                var dir = lastDirection;
                if (lastBlockType == BlockType.Rotate) {
                    var rotateBlock = block.GetComponent<RotateBlock>();
                    if (dir != Vector3.forward) {
                        rotateBlock.direction = Vector3.forward;
                    } else 
                        rotateBlock.direction = Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;
                    dir = rotateBlock.direction;
                }

                if (dir != Vector3.forward)
                   block.transform.rotation = Quaternion.LookRotation(dir);
                block.transform.position = lastBlock.transform.position + lastDirection * 5;
                lastDirection = dir;
                lastBlock = block;
            }
            
            // spawn finish block
            var finishBlock = Instantiate(GetBlockByType(BlockType.Finish), transform);
            finishBlock.transform.position = lastBlock.transform.position + lastDirection * 5;
        }

        // calculate next block type
        private BlockType CalculateNextBlockType(BlockType lastBlockType) {
            if (lastBlockType == BlockType.Ground) {
                return (BlockType) Random.Range(0, 6);
            } if (lastBlockType == BlockType.Hole || lastBlockType == BlockType.HalfHole) {
                return BlockType.Ground;
            } if (lastBlockType == BlockType.Law) {
                return BlockType.Ground;
            } if (lastBlockType == BlockType.Obstacle) {
                return BlockType.Ground;
            } if (lastBlockType == BlockType.Rotate) {
                return BlockType.Ground;
            } 
            return BlockType.Ground;
        }
        
        
        // give me block by type
        private BaseBlock GetBlockByType(BlockType type) {
            switch (type) {
                case BlockType.Ground: {
                    foreach (var block in _blocksPrefabs.Where(block => block.GetComponent<GroundBlock>())) {
                        block.type = BlockType.Ground;
                        return block;
                    }
                    break;
                }
                case BlockType.Rotate: {
                    foreach (var block in _blocksPrefabs.Where(block => block.GetComponent<RotateBlock>())) {
                        block.type = BlockType.Rotate;
                        return block;
                    }
                    break;
                }
                case BlockType.Hole: {
                    foreach (var block in _blocksPrefabs.Where(block => block.GetComponent<HoleBlock>())) {
                        block.type = BlockType.Hole;
                        return block;
                    }
                    break;
                }
                case BlockType.HalfHole: {
                    foreach (var block in _blocksPrefabs.Where(block => block.GetComponent<HalfHoleBlock>())) {
                        block.type = BlockType.HalfHole;
                        return block;
                    }
                    break;
                }
                case BlockType.Obstacle: {
                    foreach (var block in _blocksPrefabs.Where(block => block.GetComponent<ObstacleBlock>())) {
                        block.type = BlockType.Obstacle;
                        return block;
                    }
                    break;
                }
                case BlockType.Law: {
                    foreach (var block in _blocksPrefabs.Where(block => block.GetComponent<LawBlock>())) {
                        block.type = BlockType.Law;
                        return block;
                    }
                    break;
                }
                case BlockType.Finish: {
                    foreach (var block in _blocksPrefabs.Where(block => block.GetComponent<FinishBlock>())) {
                        block.type = BlockType.Finish;
                        return block;
                    }
                    break;
                }
            }

            return null;
        }

        private void OnDestroy() {
            Addressables.Release(loadHandle);
        }
    }
}