using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
	[SerializeField]
	private Camera _camera;
	[SerializeField]
	private GameObject _player;

	public override void InstallBindings()
	{
		InstallCamera();
		InstallPlayer();
	}

	private void InstallCamera()
	{
		Container.BindInterfacesAndSelfTo<Camera>().FromInstance(_camera).AsSingle();
	}

	// TODO: Use separate context for player.
	private void InstallPlayer()
	{
		Container.BindInterfacesAndSelfTo<Transform>().FromComponentOn(_player).AsSingle();
		Container.BindInterfacesAndSelfTo<CharacterController>().FromComponentOn(_player).AsSingle();

		Container.BindInterfacesAndSelfTo<PlayerLookCamera>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerTargeter>().AsSingle();
	}
}
