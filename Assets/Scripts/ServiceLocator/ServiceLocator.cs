public class ServiceLocator {
    private IInputSystem inputSystem;
    private ICameraSystem cameraSystem;
    
    public IInputSystem GetInputSystem() {
        return inputSystem ??= new InputSystem();
    }
    
    public ICameraSystem GetCameraSystem() {
        return new CameraSystem();
    }
}
