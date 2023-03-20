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
