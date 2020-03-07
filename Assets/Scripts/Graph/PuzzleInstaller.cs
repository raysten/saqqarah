using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PuzzleInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		InstallPuzzle();
		InstallNodes();
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
}
