using System;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

public class CameraSystem: ICameraSystem {
    private Camera _camera;
    private CinemachineVirtualCamera _virtualCamera;
    
    public CameraSystem() {
        _camera = Camera.main;
        if (_camera)
            _virtualCamera = _camera.GetComponentInChildren<CinemachineVirtualCamera>();
    }
    
    public CinemachineVirtualCamera GetCamera() {
        if (_virtualCamera == null)
            _virtualCamera = _camera.GetComponentInChildren<CinemachineVirtualCamera>();
        return _virtualCamera;
    }
    
    public async void Shake(float duration, float magnitude) {
        if (_virtualCamera == null) return;
        _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = magnitude;
        await Task.Delay(TimeSpan.FromSeconds(duration));
        if (_virtualCamera == null) return;
        _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
