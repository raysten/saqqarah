using System;
using UnityEngine;
using Zenject;

public class EdgeView : MonoBehaviour, IPoolable<Vector3, Vector3, ScarabView, IMemoryPool>, IDisposable
{
	[SerializeField]
	private LineRenderer _lineRend;

	[Inject]
	private Settings _settings;

	private IMemoryPool _pool;
	private Vector3 _startPosition;
	private Vector3 _endPosition;
	private ScarabView _view;
	private Vector3 _direction;
	private float _minVertexDistanceSqaured = 0.1f;
	private float _nodeDistanceSquared;

	public void Dispose()
	{
		_pool?.Despawn(this);
	}

	public void OnDespawned()
	{
		_pool = null;
		_lineRend.positionCount = 0;
	}

	public void OnSpawned(Vector3 startPosition, Vector3 endPosition, ScarabView view, IMemoryPool pool)
	{
		_pool = pool;
		enabled = true;
		_lineRend.positionCount = 2;
		_lineRend.SetPosition(0, startPosition);
		_lineRend.SetPosition(1, startPosition);
		_view = view;
		_direction = (endPosition - startPosition).normalized;
		_nodeDistanceSquared = (endPosition - startPosition).sqrMagnitude;
		_startPosition = startPosition;
		_endPosition = endPosition;
	}

	private void Update()
	{
		Vector3 currentEndPosition = _lineRend.GetPosition(1);

		if ((currentEndPosition - _endPosition).sqrMagnitude > _minVertexDistanceSqaured)
		{
			_lineRend.SetPosition(1,
				currentEndPosition + _direction * Time.deltaTime * _settings.animationSpeed *
				_nodeDistanceSquared / (_settings.standardNodeDistance * _settings.standardNodeDistance));
		}
		else
		{
			_lineRend.SetPosition(1, _endPosition);
			_view.Mark(true);
			enabled = false;
		}
	}

	public class Factory : PlaceholderFactory<Vector3, Vector3, ScarabView, EdgeView>
	{
	}

	[Serializable]
	public class Settings
	{
		public float animationSpeed = 2f;
		public float standardNodeDistance = 1.5f;
	}
}
