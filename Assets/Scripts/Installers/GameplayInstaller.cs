using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
	[SerializeField]
	private Camera _camera;
	[SerializeField]
	private ParticleSystem _winVfx;

	public override void InstallBindings()
	{
		InstallCamera();
		InstallVfx();
	}

	private void InstallCamera()
	{
		Container.BindInterfacesAndSelfTo<Camera>().FromInstance(_camera).AsSingle();
	}

	private void InstallVfx()
	{
		Container.BindInstance(_winVfx).AsCached();
	}
}
