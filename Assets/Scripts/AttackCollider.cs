using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private List<GameObject> _attackTargets = new List<GameObject>();

    private Action<List<GameObject>> _onInflictDamage;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        CleanUp();
    }

    public void RegisterDamageEvent(Action<List<GameObject>> onInflictDamage)
    {
        _onInflictDamage = onInflictDamage;
    }

    public void InflictDamage()
    {
        _onInflictDamage?.Invoke(_attackTargets);
    }

    public void CleanUp()
    {
        _attackTargets.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(TagStrings.Enermy))
        {
            _attackTargets.Add(collision.gameObject);
        }
    }
}
