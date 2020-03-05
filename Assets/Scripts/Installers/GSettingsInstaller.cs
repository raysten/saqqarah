using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GSettingsInstaller", menuName = "Installers/GSettingsInstaller")]
public class GSettingsInstaller : ScriptableObjectInstaller<GSettingsInstaller>
{
	[SerializeField]
	private PlayerMovement.Settings _playerMovement;
	[SerializeField]
	private PlayerRotation.Settings _playerRotation;
	[SerializeField]
	private PlayerTargeter.Settings _targeter;

	public override void InstallBindings()
    {
		Container.BindInstance(_playerMovement).AsSingle();
		Container.BindInstance(_playerRotation).AsSingle();
		Container.BindInstance(_targeter).AsSingle();
	}
}