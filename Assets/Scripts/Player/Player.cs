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
        // move player forward
        transform.position += transform.forward * 4f;
        // slow player
        _movement.speed /= 8;
        StartCoroutine(nameof(ReturnToNormalSpeedProcess));
    }
    
    #region BoostBehavior
    
    private void Normal() {
        _movement.speed = _speed;
        _isShieldActive = false;
    }

    // up speed
    public void Fast(float time) {
        _movement.speed = 10;
        if (fastCoroutine != null) StopCoroutine(fastCoroutine);
        fastCoroutine = StartCoroutine(FastProcess(time));
    }
    
    // set shield
    private bool _isShieldActive;
    public void Shield(float time) {
        _isShieldActive = true;
        if (shieldCoroutine != null) StopCoroutine(shieldCoroutine);
        shieldCoroutine = StartCoroutine(ShieldProcess(time));
    }
    
    private Coroutine fastCoroutine;
    IEnumerator FastProcess(float time) {
        yield return new WaitForSeconds(time);
        Normal();
    }
    
    private Coroutine shieldCoroutine;
    IEnumerator ShieldProcess(float time) {
        yield return new WaitForSeconds(time);
        Normal();
    }
    
    IEnumerator ReturnToNormalSpeedProcess() {
        while (_movement.speed <= _speed) {
            _movement.speed += Time.deltaTime;
            yield return null;
        }
        _movement.speed = _speed;
    }
    
    #endregion
    
    private void OnDisable() {
        GameManager.instance.OnContinueAfterWatchVideoAction -= ContinueAfterWatchVideo;
    }
}
