using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GSettingsInstaller", menuName = "Installers/GSettingsInstaller")]
public class GSettingsInstaller : ScriptableObjectInstaller<GSettingsInstaller>
{
	[SerializeField]
	private PlayerMovement.Settings _playerMovement;
	[SerializeField]
	private PlayerLookCamera.Settings _playerCamera;
	[SerializeField]
	private PlayerTargeter.Settings _targeter;

	public override void InstallBindings()
    {
		Container.BindInstance(_playerMovement).AsSingle();
		Container.BindInstance(_playerCamera).AsSingle();
		Container.BindInstance(_targeter).AsSingle();
	}
}