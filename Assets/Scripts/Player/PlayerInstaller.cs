using Zenject;
using UnityEngine;

public class PlayerInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<PlayerRotation>().AsSingle();
		Container.BindInterfacesAndSelfTo<Transform>().FromComponentOnRoot().AsSingle();
	}
}
