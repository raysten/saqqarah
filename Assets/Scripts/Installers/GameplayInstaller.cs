using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
	[SerializeField]
	private Camera _camera;
	[SerializeField]
	private Image _aim;

	public override void InstallBindings()
	{
		InstallCamera();
		Container.BindInstance(_aim).AsSingle();
	}

	private void InstallCamera()
	{
		Container.BindInterfacesAndSelfTo<Camera>().FromInstance(_camera).AsSingle();
	}
}
