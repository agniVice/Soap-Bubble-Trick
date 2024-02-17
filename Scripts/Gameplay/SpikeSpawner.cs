using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour, ILevelSpawner
{
    [SerializeField] private List<GameObject> _spikePreafbs;
    [SerializeField] private Transform _leftPosition;
    [SerializeField] private Transform _rightPosition;

    private GameObject _spike;

    private bool _isLeft = false;

    private void OnEnable()
    {
        GameState.Instance.ScoreAdded += Rebuild;
    }
    private void OnDisable()
    {
        GameState.Instance.ScoreAdded -= Rebuild;
    }
    private void Start()
    {
        //Build();
    }
    public void Build()
    {
        _spike = Instantiate(_spikePreafbs[Random.Range(0, _spikePreafbs.Count-1)]);

        if (_isLeft)
        {
            _spike.transform.position = _leftPosition.position;
            _spike.transform.localScale = new Vector2(-1, 1);
        }
        else
            _spike.transform.position = _rightPosition.position;
    }
    public void Clear()
    {
        Destroy(_spike);
    }
    public void Rebuild()
    {
        _isLeft = !_isLeft;

        Clear();
        Build();
    }
}