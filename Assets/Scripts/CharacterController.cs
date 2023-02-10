using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected Character _character;
    public Character Character => _character;

    public void SetCharacter(Character character)
    {
        _character = character;
    }
    
    public virtual void GetHurt()
    {
        Debug.LogFormat("{0} get hurt.", gameObject.name);
    }

    public void InflictDamage(List<GameObject> targets)
    {
        foreach (var target in targets)
            target.GetComponent<CharacterController>().GetHurt();
    }
}

public class Character
{
    public int Health { get; private set; }

    public float MoveSpeed { get; private set; }

    public float JumpVelocity { get; private set; }

    public Character(int health, float maxMoveSpeed, float jumpVelocity)
    {
        Health = health;
        MoveSpeed = maxMoveSpeed;
        JumpVelocity = jumpVelocity;
    }
}