using Zenject;
using UnityEngine;

public class PlayerInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		InstallComponents();
		InstallModules();
	}

	private void InstallComponents()
	{
		Container.BindInterfacesAndSelfTo<Transform>().FromComponentOnRoot().AsSingle();
		Container.BindInterfacesAndSelfTo<CharacterController>().FromComponentOnRoot().AsSingle();
	}

	private void InstallModules()
	{
		Container.BindInterfacesAndSelfTo<PlayerRotation>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerTargeter>().AsSingle();
	}
}
