using UnityEngine.SceneManagement;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    public override void InstallBindings() {
        
        Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SceneManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CameraSystem>().AsSingle().NonLazy();
    }
}
