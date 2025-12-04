using DG.Tweening;
using UnityEngine;

public class TileEffectPool
{
	private const float MIN_LIFETIME = 0.05f;

	private readonly ObjectPool<ParticleSystem> _pool;

	private readonly ParticleSystem _prefab;

	private readonly Transform _rootParent;

	private readonly Vector3 _tileBaseScale;

	private readonly float _effectLifetime;

	public TileEffectPool(ParticleSystem prefab, Transform rootParent, int prewarmCount, Vector3 tileBaseScale)
	{
		_prefab = prefab;
		_rootParent = rootParent;
		_tileBaseScale = tileBaseScale;
		_effectLifetime = CalculateLifetime(prefab);
		_pool = new ObjectPool<ParticleSystem>(CreateInstance, OnGet, OnRelease);
		if (_prefab != null && prewarmCount > 0)
		{
			_pool.Prewarm(prewarmCount);
		}
	}

	public void PlayEffectAt(Vector3 worldPosition)
	{
		if (_prefab == null)
		{
			return;
		}
		ParticleSystem instance = _pool.Get();
		if (!(instance == null))
		{
			instance.transform.position = worldPosition;
			DOVirtual.DelayedCall(_effectLifetime, delegate
			{
				_pool.Release(instance);
			});
		}
	}

	private ParticleSystem CreateInstance()
	{
		if (_prefab == null)
		{
			return null;
		}
		ParticleSystem instance = Object.Instantiate(_prefab, _rootParent);
		Transform instanceTransform = instance.transform;
		instance.gameObject.SetActive(false);
		instance.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
		instanceTransform.localScale = _tileBaseScale;
		return instance;
	}

	private void OnGet(ParticleSystem effect)
	{
		effect.gameObject.SetActive(true);
		effect.Clear();
		effect.Play();
	}

	private void OnRelease(ParticleSystem particleSystem)
	{
		particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
		particleSystem.gameObject.SetActive(false);
	}

	private float CalculateLifetime(ParticleSystem prefab)
	{
		if (prefab == null)
		{
			return 0.05f;
		}
		ParticleSystem.MainModule main = prefab.main;
		return Mathf.Max(0.05f, main.duration + main.startLifetime.constantMax);
	}
}
