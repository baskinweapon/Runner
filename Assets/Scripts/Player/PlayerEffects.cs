using TMPro;
using UnityEngine;

public class PlayerEffects : MonoBehaviour {
    public TextMeshProUGUI text;
    
    public void OnEnable() {
        GameManager.instance.OnGameOver += Death;
        GameManager.instance.OnChangeLives += ChangeLives;
    }

    private void Death() {
        // just imagine that we have some animation here
    }

    private int _prevLives = 3;
    private void ChangeLives(int lives) {
        var dif = lives - _prevLives; 
        text.text = dif < 0 ? $"<color=red>{dif}</color>" : $"<color=green>+{dif}</color>";
        Invoke(nameof(HideText), 0.4f);
        if (_prevLives > lives) {
            // shake camera
            GameManager.instance.serviceLocator.GetCameraSystem().Shake(0.2f, 5);
        }

        _prevLives = lives;
    }

    private void HideText() {
        text.text = "";
    }
    
    private void OnDisable() {
        GameManager.instance.OnGameOver -= Death;
        GameManager.instance.OnChangeLives -= ChangeLives;
    }
}
