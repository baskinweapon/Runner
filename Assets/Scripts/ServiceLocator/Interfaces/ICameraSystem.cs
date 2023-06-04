using UnityEngine;

public interface ICameraSystem {
    public void Shake(float duration, float magnitude);
    public Camera GetCamera();
}
