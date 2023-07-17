
using System;

[Obsolete("Use Zenject instead")]
public class ServiceLocator {
    private IInputSystem inputSystem;
    private ICameraSystem cameraSystem;
    
    public IInputSystem GetInputSystem() {
        return inputSystem ??= new InputSystem();
    }
    
    public ICameraSystem GetCameraSystem() {
        return new CameraSystem(); // because we need to create new instance of camera system immediately
    }
}
