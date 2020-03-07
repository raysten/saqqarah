using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleInstaller : MonoInstaller
{
	[SerializeField]
	private GameObject _edgePrefab;

	public override void InstallBindings()
	{
		InstallPuzzle();
		InstallNodes();
		InstallEdges();
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
		Container.BindFactory<Vector3, Vector3, EdgeView, EdgeView.Factory>()
			.FromPoolableMemoryPool<Vector3, Vector3, EdgeView, EdgeMemoryPool>(x => x
				.WithInitialSize(30)
				.ExpandByDoubling()
				.FromComponentInNewPrefab(_edgePrefab)
				.UnderTransform(transform)
			);
	}

	public class EdgeMemoryPool : MonoPoolableMemoryPool<Vector3, Vector3, IMemoryPool, EdgeView>
	{
	}
}
