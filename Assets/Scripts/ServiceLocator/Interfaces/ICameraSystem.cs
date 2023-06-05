using Cinemachine;

public interface ICameraSystem {
    public void Shake(float duration, float magnitude);
    public CinemachineVirtualCamera GetCamera();
}
