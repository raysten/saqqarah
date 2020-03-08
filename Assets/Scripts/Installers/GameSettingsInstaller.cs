using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
	[SerializeField]
	private PlayerMovement.Settings _playerMovement;
	[SerializeField]
	private PlayerLookCamera.Settings _playerCamera;
	[SerializeField]
	private PlayerTargeter.Settings _targeter;
	[SerializeField]
	private EdgeView.Settings _edgeView;
	[SerializeField]
	private ScarabView.Settings _scarabView;
	[SerializeField]
	private PuzzleMessage.Settings _messages;

	public override void InstallBindings()
    {
		Container.BindInstance(_playerMovement).AsSingle();
		Container.BindInstance(_playerCamera).AsSingle();
		Container.BindInstance(_targeter).AsSingle();
		Container.BindInstance(_edgeView).AsSingle();
		Container.BindInstance(_scarabView).AsSingle();
		Container.BindInstance(_messages).AsSingle();
	}
}