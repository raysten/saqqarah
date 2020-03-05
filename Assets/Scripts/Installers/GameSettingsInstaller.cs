using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/GameSettings")]
public class GameSettings : ScriptableObjectInstaller<GameSettings>
{
	[SerializeField]
	private PlayerMovement.Settings _playerMovement;
	[SerializeField]
	private PlayerRotation.Settings _playerRotation;

    public override void InstallBindings()
    {
		Container.BindInstance(_playerMovement).AsSingle();
		Container.BindInstance(_playerRotation).AsSingle();
    }
}