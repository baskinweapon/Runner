using System;
using System.Collections.Generic;
using DefaultNamespace.Blocks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;


public class GameManager : Singleton<GameManager> {
    [Header("Game Settings")]
    public GameSettings gameSettings;
    [Header("Game Scene reference")]
    public AssetReference _reference;
    
    public Action OnContinueAfterWatchVideoAction;
    public Action OnGameOver;
    public Action OnFinishGame;
    public Action<int> OnChangeLives;
    public Action<Dictionary<BlockType, int>> OnStatistics;

    protected override void Awake() {
        base.Awake();
        
        OnGameOver += GameOver;
    }
    
    public void StartGame() {
        Addressables.LoadSceneAsync(_reference);
    }

    // player win
    public void FinishGame() {
        Pause();
        OnFinishGame?.Invoke();
        
        // add difficulty after win
        gameSettings.blockSettings.spawnCount += 30;
    }
    
    // player lose
    private void GameOver() {
        Pause();
    }

    public void Restart() {
        Addressables.LoadSceneAsync(_reference);
        Play();
    }

    public void ContinueAfterWatchVideo() {
        OnContinueAfterWatchVideoAction?.Invoke();
        Play();
    }
    
    private void OnDestroy() {
        OnGameOver -= GameOver;
    }

    #region PlayPause

    private void Pause() {
        Time.timeScale = 0;
    }

    private void Play() {
        Time.timeScale = 1;
    }

    #endregion
}
