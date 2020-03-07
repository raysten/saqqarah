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
	private ParticleSystem _winVfx;
	[SerializeField]
	private Animator _victoryText;

	public override void InstallBindings()
	{
		InstallCamera();
		InstallVictory();
		InstallMisc();
	}

	private void InstallCamera()
	{
		Container.BindInterfacesAndSelfTo<Camera>().FromInstance(_camera).AsSingle();
	}

	private void InstallVictory()
	{
		Container.BindInstance(_winVfx).AsCached();
		Container.BindInstance(_victoryText).AsCached();
		Container.BindInterfacesAndSelfTo<VictoryHandler>().AsSingle();
	}

	private void InstallMisc()
	{
		Container.BindInstance(_aim).AsCached();
		Container.BindInterfacesAndSelfTo<EventBus>().AsSingle();
	}
}
