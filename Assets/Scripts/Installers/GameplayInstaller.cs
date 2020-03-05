using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
	[SerializeField]
	private Camera _camera;
	[SerializeField]
	private Image _aim;
	[SerializeField]
	private GameObject _player;

	public override void InstallBindings()
	{
		InstallCamera();
		InstallPlayer();
		InstallMisc();
	}

	private void InstallCamera()
	{
		Container.BindInterfacesAndSelfTo<Camera>().FromInstance(_camera).AsSingle();
	}

	private void InstallPlayer()
	{
		Container.BindInterfacesAndSelfTo<Transform>().FromComponentOn(_player).AsSingle();
		Container.BindInterfacesAndSelfTo<CharacterController>().FromComponentOn(_player).AsSingle();

		Container.BindInterfacesAndSelfTo<PlayerRotation>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerTargeter>().AsSingle();
	}

	private void InstallMisc()
	{
		Container.BindInstance(_aim).AsSingle();
	}
}
