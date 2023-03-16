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

    [SerializeField]
    private BoxCollider2D _collider2D;

    [Tooltip("The Layers which represent gameobjects that the Character Controller can be grounded on.")]
    [SerializeField]
    private LayerMask _groundedLayerMask;
    private ContactFilter2D _contactFilter;

    [Tooltip("The distance down to check for ground.")]
    [SerializeField]
    private float _groundedRaycastDistance = 0.1f;
    private Vector2[] _raycastPositions = new Vector2[3];
    private RaycastHit2D[] _foundHits = new RaycastHit2D[3];
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[5];
    private Collider2D[] _groundColliders = new Collider2D[3];

    private Vector2 _previousPosition;
    private Vector2 _currentPosition;
    private Vector2 _nextMovement;

    public Vector2 Velocity { get; protected set; }

    private void Awake()
    {
        _attackCollider.RegisterDamageEvent(InflictDamage);


        _currentPosition = _rigidbody2D.position;
        _previousPosition = _rigidbody2D.position;

        _contactFilter.layerMask = _groundedLayerMask;
        _contactFilter.useLayerMask = true;
        _contactFilter.useTriggers = false;
    }

    private void Start()
    {
        _character = new Character(100, 5, 5);
    }

    private void FixedUpdate()
    {
        _previousPosition = _rigidbody2D.position;
        _currentPosition = _previousPosition + _nextMovement;
        Velocity = (_currentPosition - _previousPosition) / Time.deltaTime;

        CheckCapsuleEndCollisions();
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

    /// <summary>
    /// This updates the state of IsGrounded.  It is called automatically in FixedUpdate but can be called more frequently if higher accurracy is required.
    /// </summary>
    public void CheckCapsuleEndCollisions(bool bottom = true)
    {
        Vector2 raycastDirection;
        Vector2 raycastStart;
        float raycastDistance;

        if (_collider2D == null)
        {
            raycastStart = _rigidbody2D.position + Vector2.up;
            raycastDistance = 1f + _groundedRaycastDistance;

            if (bottom)
            {
                raycastDirection = Vector2.down;

                _raycastPositions[0] = raycastStart + Vector2.left * 0.4f;
                _raycastPositions[1] = raycastStart;
                _raycastPositions[2] = raycastStart + Vector2.right * 0.4f;
            }
            else
            {
                raycastDirection = Vector2.up;

                _raycastPositions[0] = raycastStart + Vector2.left * 0.4f;
                _raycastPositions[1] = raycastStart;
                _raycastPositions[2] = raycastStart + Vector2.right * 0.4f;
            }
        }
        else
        {
            raycastStart = _rigidbody2D.position + _collider2D.offset;
            raycastDistance = _collider2D.size.x * 0.5f + _groundedRaycastDistance * 2f;

            if (bottom)
            {
                raycastDirection = Vector2.down;
                Vector2 raycastStartBottomCentre = raycastStart + Vector2.down * (_collider2D.size.y * 0.5f - _collider2D.size.x * 0.5f);

                _raycastPositions[0] = raycastStartBottomCentre + Vector2.left * _collider2D.size.x * 0.5f;
                _raycastPositions[1] = raycastStartBottomCentre;
                _raycastPositions[2] = raycastStartBottomCentre + Vector2.right * _collider2D.size.x * 0.5f;
            }
            else
            {
                raycastDirection = Vector2.up;
                Vector2 raycastStartTopCentre = raycastStart + Vector2.up * (_collider2D.size.y * 0.5f - _collider2D.size.x * 0.5f);

                _raycastPositions[0] = raycastStartTopCentre + Vector2.left * _collider2D.size.x * 0.5f;
                _raycastPositions[1] = raycastStartTopCentre;
                _raycastPositions[2] = raycastStartTopCentre + Vector2.right * _collider2D.size.x * 0.5f;
            }
        }

        for (int i = 0; i < _raycastPositions.Length; i++)
        {
            int count = Physics2D.Raycast(_raycastPositions[i], raycastDirection, _contactFilter, _hitBuffer, raycastDistance);

            if (bottom)
            {
                _foundHits[i] = count > 0 ? _hitBuffer[0] : new RaycastHit2D();
                _groundColliders[i] = _foundHits[i].collider;
            }
            else
            {
                //IsCeilinged = false;

                //for (int j = 0; j < _hitBuffer.Length; j++)
                //{
                //    if (_hitBuffer[j].collider != null)
                //    {
                //        if (!PhysicsHelper.ColliderHasPlatformEffector(_hitBuffer[j].collider))
                //        {
                //            IsCeilinged = true;
                //        }
                //    }
                //}
            }
        }

        if (bottom)
        {
            Vector2 groundNormal = Vector2.zero;
            int hitCount = 0;

            for (int i = 0; i < _foundHits.Length; i++)
            {
                if (_foundHits[i].collider != null)
                {
                    groundNormal += _foundHits[i].normal;
                    hitCount++;
                }
            }

            if (hitCount > 0)
            {
                groundNormal.Normalize();
            }

            Vector2 relativeVelocity = Velocity;
            if (Mathf.Approximately(groundNormal.x, 0f) && Mathf.Approximately(groundNormal.y, 0f))
            {
                _isGrounded = false;
            }
            else
            {
                _isGrounded = relativeVelocity.y <= 0f;

                if (_collider2D != null)
                {
                    if (_groundColliders[1] != null)
                    {
                        float capsuleBottomHeight = _rigidbody2D.position.y + _collider2D.offset.y - _collider2D.size.y * 0.5f;
                        float middleHitHeight = _foundHits[1].point.y;
                        _isGrounded &= middleHitHeight < capsuleBottomHeight + _groundedRaycastDistance;
                    }
                }
            }
        }

        for (int i = 0; i < _hitBuffer.Length; i++)
        {
            _hitBuffer[i] = new RaycastHit2D();
        }
    }
}
