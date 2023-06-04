using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    private int _lives = 3;
    
    public int Lives {
        get => _lives;
        set {
            if (_isShieldActive) return;
            _lives = value;
            if (_lives == 0) {
                GameManager.instance.OnGameOver?.Invoke();
                return;
            }
            GameManager.instance.OnChangeLives?.Invoke(_lives);
        }
    }
    
    private Movement _movement;
    private float _speed;
    private void Start() {
        _movement = GetComponent<Movement>();
        _speed = _movement.speed;
        GameManager.instance.OnContinueAfterWatchVideoAction += ContinueAfterWatchVideo;
        Lives = 3;
    }

    private void ContinueAfterWatchVideo() {
        Lives = 3;
        _movement.rb.velocity = Vector3.zero;
        transform.position += transform.forward * 4f;
        _movement.speed /= 8;
        StartCoroutine(nameof(StartCoroutine));
    }

    IEnumerator StartCoroutine() {
        while (_movement.speed <= _speed) {
            _movement.speed += Time.deltaTime;
            yield return null;
        }
        _movement.speed = _speed;
    }
    
    public void Normal() {
        _movement.speed = _speed;
        _isShieldActive = false;
    }
    
    public void Fast(float time) {
        _movement.speed = 10;
        Invoke(nameof(Normal), time);
    }

    private bool _isShieldActive;
    public void Shield(float time) {
        _isShieldActive = true;
        Invoke(nameof(Normal), time);
    }
    
    private void OnDisable() {
        GameManager.instance.OnContinueAfterWatchVideoAction -= ContinueAfterWatchVideo;
    }
}
