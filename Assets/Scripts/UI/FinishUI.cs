using System.Collections.Generic;
using DefaultNamespace.Blocks;
using UI;
using UnityEngine;

public class FinishUI : MonoBehaviour
{
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private Transform content;
    [SerializeField]
    private BlockUI blockUIPrefab;
    
    public void Start() {
        GameManager.instance.OnFinishGame += FinishGame;
        GameManager.instance.OnStatistics += CreateStatistics;
    }
    
    private void FinishGame() {
        finishPanel.SetActive(true);
    }
    
    public void Restart() {
        finishPanel.SetActive(false);
        GameManager.instance.Restart();
    }
    
    private void CreateStatistics(Dictionary<BlockType, int> statistics) {
        DestroyContent();
        foreach (var pair in statistics) {
            CreateBlockUI(pair.Key, pair.Value);
        }
    }
        
    private void CreateBlockUI(BlockType type, int count) {
        var blockUI = Instantiate(blockUIPrefab, content);
        blockUI.nameBlock.text = TranslateType(type);
        blockUI.countBlock.text = count.ToString();
    }
    
    private string TranslateType(BlockType type) {
        if (type == BlockType.Hole) {
            return "Пропасть";
        }
        if (type == BlockType.Law) {
            return "Пила";
        }
        if (type == BlockType.Obstacle) {
            return "Препятствие";
        }
        if (type == BlockType.HalfHole) {
            return "Половина пропасти";
        }
        return "";
    }
    
    // remove childrens
    private void DestroyContent() {
        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }
    }

    private void OnDestroy() {
        GameManager.instance.OnFinishGame -= FinishGame;
        GameManager.instance.OnStatistics -= CreateStatistics;
    }
}
