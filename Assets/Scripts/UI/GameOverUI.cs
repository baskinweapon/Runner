using System.Collections.Generic;
using DefaultNamespace.Blocks;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class GameOverUI : MonoBehaviour {
        [SerializeField]
        private BlockUI blockUIPrefab;
        [SerializeField]
        private GameObject gameOverPanel;

        [SerializeField] Button restartButton;
        [SerializeField] Button continueButton;
        
        [SerializeField] private Transform content;
        
        private void Start() {
            GameManager.instance.OnGameOver += Death;
            GameManager.instance.OnStatistics += CreateStatistics;
            
            //buttons
            restartButton.onClick.AddListener(Restart);
            continueButton.onClick.AddListener(Continue);
        }
        
        private void Restart() {
            gameOverPanel.SetActive(false);
            GameManager.instance.Restart();
        }
        
        private void Continue() {
            gameOverPanel.SetActive(false);
            GameManager.instance.ContinueAfterWatchVideo();
        }

        private void Death() {
            gameOverPanel.SetActive(true);
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
        
        private void DestroyContent() {
            foreach (Transform child in content) {
                Destroy(child.gameObject);
            }
        }
        
        private void OnDisable() {
            GameManager.instance.OnGameOver -= Death;
            GameManager.instance.OnStatistics -= CreateStatistics;
            
            restartButton.onClick.RemoveListener(Restart);
            continueButton.onClick.RemoveListener(Continue);
        }
    }
}