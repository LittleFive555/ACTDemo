using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Transform _modelRoot;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private AttackCollider _attackCollider;

    private bool _isJumping;

    private void Awake()
    {
        _attackCollider.RegisterDamageEvent(InflictDamage);
    }

    private void Start()
    {
        _character = new Character(100, 5, 5);
    }


    private void Update()
    {
        // Move
        float speed = 0;
        if (Input.GetKey(OperationBinder.Instance.LeftMove.TriggerKey))
            speed -= _character.MoveSpeed;
        if (Input.GetKey(OperationBinder.Instance.RightMove.TriggerKey))
            speed += _character.MoveSpeed;
        float step = Time.deltaTime * speed;

        // Turn
        if (speed > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (speed < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        transform.position += Vector3.right * step;

        // Jump
        if (Input.GetKey(OperationBinder.Instance.Jump.TriggerKey) && !_isJumping)
        {
            _isJumping = true;
            _rigidbody2D.velocity = Vector2.up * _character.JumpVelocity;
        }

        // Attack
        if (Input.GetKeyDown(OperationBinder.Instance.Attack.TriggerKey))
        {
            _animator.SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(TagStrings.Ground))
        {
            _isJumping = false;
        }
    }
}
