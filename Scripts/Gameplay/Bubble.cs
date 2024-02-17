using UnityEngine;

public class Bubble : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _particlePrefab;

    [Space]
    [Header("BirdSettings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpHeight = 5f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _direction = Vector2.right;

    private bool _isMoving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _rigidbody.simulated = false;
    }
    private void OnEnable()
    {
        PlayerInput.Instance.PlayerMouseDown += OnPlayerMouseDown;
    }
    private void OnDisable()
    {
        PlayerInput.Instance.PlayerMouseDown -= OnPlayerMouseDown;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (_isMoving)
        {
            _rigidbody.velocity = new Vector2(_direction.normalized.x * _speed, _rigidbody.velocity.y);
        }
    }
    private void SpawnParticle()
    {
        var particle = Instantiate(_particlePrefab).GetComponent<ParticleSystem>();

        if (_direction.x > 0)
            particle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else
            particle.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);

        particle.transform.position = new Vector2(transform.position.x, transform.position.y+0.2f);
        particle.Play();

        Destroy(particle.gameObject, 2f);
    }
    private void ChangeDirection()
    {
        _direction *= new Vector2(-1, 0);
    }
    private void OnPlayerMouseDown()
    {
        if (_isMoving == false)
        {
            _isMoving = true;
            _rigidbody.simulated = true;

            Jump();
        }
        else
            Jump();
    }
    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpHeight);
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.PopUp, 1, 0.6f);
        SpawnParticle();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlayerScore.Instance.AddScore();
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.BubbleSound, Random.Range(0.85f, 1.1f));
            ChangeDirection();
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            GameState.Instance.FinishGame();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
