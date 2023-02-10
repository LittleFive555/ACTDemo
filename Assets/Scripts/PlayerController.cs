using System.Collections;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField]
    protected Rigidbody2D _rigidbody2D;

    [SerializeField]
    protected Transform _modelRoot;

    [SerializeField]
    protected Animator _animator;

    [SerializeField]
    private AttackCollider _attackCollider;

    private bool _isGrounded;

    private void Awake()
    {
        _attackCollider.RegisterDamageEvent(InflictDamage);
    }

    private void Start()
    {
        _character = new Character(100, 5, 5);
    }

    public void SetModelFacing(bool faceRight)
    {
        var oldScale = _modelRoot.localScale;
        if (faceRight)
            _modelRoot.localScale = new Vector3(Mathf.Abs(oldScale.x), oldScale.y, oldScale.z);
        else
            _modelRoot.localScale = new Vector3(-Mathf.Abs(oldScale.x), oldScale.y, oldScale.z);
    }

    public bool IsGrounded()
    {
        return _isGrounded;
    }

    public void JumpUp()
    {
        _isGrounded = false;
        _rigidbody2D.velocity = Vector2.up * _character.JumpVelocity;
    }

    public float GetYVelocity()
    {
        return _rigidbody2D.velocity.y;
    }

    public IEnumerator AttackAnim()
    {
        _animator.SetTrigger("Attack");
        
        yield return new WaitForSeconds(0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(TagStrings.Ground))
        {
            _isGrounded = true;
        }
    }
}
