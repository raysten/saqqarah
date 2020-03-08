using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PuzzleInstaller : MonoInstaller
{
	[SerializeField]
	private GameObject _edgePrefab;
	[SerializeField]
	private Text _messageBox;

	public override void InstallBindings()
	{
		InstallPuzzle();
		InstallNodes();
		InstallEdges();
		InstallMessages();
	}

	private void InstallPuzzle()
	{
		Container.BindInterfacesAndSelfTo<PuzzleController>().AsSingle();
	}

	private void InstallNodes()
	{
		ScarabNode[] nodes = GetComponentsInChildren<ScarabNode>();

		foreach (ScarabNode node in nodes)
		{
			Container.BindInterfacesAndSelfTo<ScarabNode>().FromInstance(node).AsCached();
		}
	}

	private void InstallEdges()
	{
		Container.BindInterfacesAndSelfTo<EdgeDrawer>().AsSingle();
		Container.BindFactory<Vector3, Vector3, ScarabView, EdgeView, EdgeView.Factory>()
			.FromPoolableMemoryPool<Vector3, Vector3, ScarabView, EdgeView, EdgeMemoryPool>(x => x
				.WithInitialSize(30)
				.ExpandByDoubling()
				.FromComponentInNewPrefab(_edgePrefab)
				.UnderTransform(transform)
			);
	}

	private void InstallMessages()
	{
		Container.BindInterfacesAndSelfTo<PuzzleMessage>().AsSingle();
		Container.BindInstance(_messageBox).AsCached();
	}

	public class EdgeMemoryPool : MonoPoolableMemoryPool<Vector3, Vector3, ScarabView, IMemoryPool, EdgeView>
	{
	}
}
