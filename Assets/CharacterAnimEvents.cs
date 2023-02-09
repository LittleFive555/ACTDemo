using UnityEngine;

public class CharacterAnimEvents : MonoBehaviour
{
    [SerializeField]
    private AttackCollider _attackCollider;

    /// <summary>
    /// Used by animation
    /// </summary>
    public void InflictDamage()
    {
        _attackCollider.InflictDamage();
    }
}
