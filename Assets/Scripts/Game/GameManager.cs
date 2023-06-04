using System;
using System.Collections.Generic;
using DefaultNamespace.Blocks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    public ServiceLocator serviceLocator;
    public GameSettings gameSettings;
    
    public Action OnContinueAfterWatchVideoAction;
    public Action OnGameOver;
    public Action OnFinishGame;
    public Action<int> OnChangeLives;
    public Action<Dictionary<BlockType, int>> OnStatistics;

    protected override void Awake() {
        base.Awake();
        
        ServicesInit();
        OnGameOver += GameOver;
    }
    
    #region Services
    
    public IInputSystem inputSystem;
    
    private void ServicesInit() {
        serviceLocator = new ServiceLocator();
        inputSystem = serviceLocator.GetInputSystem();
    }
    #endregion
    
    public AssetReference _reference;
    public void StartGame() {
        Addressables.LoadSceneAsync(_reference);
    }

    // player win
    public void FinishGame() {
        Pause();
        OnFinishGame?.Invoke();
        // add difficulty
        gameSettings.blockSettings.spawnCount += 30;
    }
    
    // player lose
    public void GameOver() {
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
        inputSystem.GetInputSystem().OnDestroy();
        OnGameOver -= GameOver;
    }

    #region PlayPause

    
    public void Pause() {
        Time.timeScale = 0;
    }

    public void Play() {
        Time.timeScale = 1;
    }

    #endregion
}
