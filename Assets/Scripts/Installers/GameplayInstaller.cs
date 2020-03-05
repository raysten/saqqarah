using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
	[SerializeField]
	private Camera _camera;

	public override void InstallBindings()
	{
		InstallCamera();
	}

	private void InstallCamera()
	{
		Container.BindInterfacesAndSelfTo<Camera>().FromInstance(_camera).AsSingle();
	}

	private void InstallPlayer()
	{
		//Container.BindInterfacesAndSelfTo
	}
}
