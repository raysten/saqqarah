using System;
using UnityEngine;
using Zenject;

public class EdgeView : MonoBehaviour, IPoolable<Vector3, Vector3, IMemoryPool>, IDisposable
{
	[SerializeField]
	private LineRenderer _lineRend;

	[Inject]
	private Settings _settings;

	private IMemoryPool _pool;
	private Vector3 _startPosition;
	private Vector3 _endPosition;
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

	public void OnSpawned(Vector3 startPosition, Vector3 endPosition, IMemoryPool pool)
	{
		_pool = pool;
		enabled = true;
		_lineRend.positionCount = 2;
		_lineRend.SetPosition(0, startPosition);
		_lineRend.SetPosition(1, startPosition);
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
				currentEndPosition + _direction * Time.deltaTime *
				_settings.animationSpeed * _nodeDistanceSquared / _settings.standardNodeDistanceSquared);
		}
		else
		{
			_lineRend.SetPosition(1, _endPosition);
			enabled = false;
		}
	}

	public class Factory : PlaceholderFactory<Vector3, Vector3, EdgeView>
	{
	}

	[Serializable]
	public class Settings
	{
		public float animationSpeed = 2f;
		public float standardNodeDistanceSquared = 3f;
	}
}