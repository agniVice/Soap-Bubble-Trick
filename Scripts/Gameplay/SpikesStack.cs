using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpikesStack : MonoBehaviour
{
    [SerializeField] private bool _verticalMove;
    [SerializeField] private bool _horizontalMove;

    [SerializeField] private bool _isMoving;

    private Tween _verticalTween;
    private Tween _horizontalTween;

    private Vector2 _startPosition;

    private bool _isMovingForward;
    private bool _isSpawned;

    private void Start()
    {
        float move = Random.Range(0, 100);
        if (move >= 35)
        {
            _isMoving = true;
            float vertical = Random.Range(0, 100);
            if (vertical >= 50)
                _verticalMove = true;
            else
                _horizontalMove = true;
        }
        else
            _isMoving = false;
        Spawn();
    }
    private void OnDestroy()
    {
        if (_verticalTween != null)
            _verticalTween.Kill();

        if (_horizontalTween != null)
            _horizontalTween.Kill();
    }
    private void Spawn()
    {
        _startPosition = transform.position;

        if(transform.localScale.x < 0)
            transform.position = new Vector2((transform.right).x -4f, _startPosition.y);
        else
            transform.position = new Vector2((transform.right).x + 2f, _startPosition.y);

        transform.DOMove(_startPosition, 0.5f).SetLink(gameObject).SetEase(Ease.OutBack).OnKill(OnSpikesSpawned);
    }
    private void OnSpikesSpawned()
    {
        _isSpawned = false;

        if (!_isMoving)
            return;

        if (_verticalMove)
            VerticalMove();

        if (_horizontalMove)
            HorizontalMove();
    }
    private void VerticalMove()
    {
        Vector3 startPosition = transform.position;

        Vector3 endPosition = _isMovingForward ? startPosition + Vector3.up * 0.3f : startPosition - Vector3.up * 0.3f;

        _verticalTween = transform.DOMove(endPosition, 0.5f)
            .SetLink(gameObject)
            .SetEase(Ease.Linear) 
            .OnKill(() =>
            {
                _isMovingForward = !_isMovingForward;
                VerticalMove();
            });
    }
    private void HorizontalMove()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = _isMovingForward ? startPosition + Vector3.right * 0.1f : startPosition - Vector3.right * 0.1f;

        _horizontalTween = transform.DOMove(endPosition, 0.5f)
            .SetLink(gameObject)
            .SetEase(Ease.Linear)
            .OnKill(() =>
            {
                _isMovingForward = !_isMovingForward;
                HorizontalMove();
            });
    }
}
