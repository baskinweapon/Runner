using UnityEngine;

public class PlayerEffects : MonoBehaviour {
    public void OnEnable() {
        GameManager.instance.OnGameOver += Death;
        GameManager.instance.OnChangeLives += ChangeLives;
    }

    private void Death() {
        
    }

    private int _prevLives = 3;
    private void ChangeLives(int lives) {
        if (_prevLives > lives) {
            GameManager.instance.serviceLocator.GetCameraSystem().Shake(0.2f, 5);
        }

        _prevLives = lives;
    }
    
    private void OnDisable() {
        GameManager.instance.OnGameOver -= Death;
        GameManager.instance.OnChangeLives -= ChangeLives;
    }
}
