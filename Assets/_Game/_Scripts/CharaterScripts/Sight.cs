using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private Charater charater;

    private void Update()
    {
        RemoveTargetWhenHitted();
    }

    private void AddTarget(Collider collider) 
    {
        if (collider.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            Charater charater = Cache.GetCharaterComponentForCollider(collider);

            if (charater == this.charater)
            {
                return;
            }

            this.charater.listCharacterTarget.Add(charater);
        }
    }

    private void RemoveTargetWhenExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            Charater charater = Cache.GetCharaterComponentForCollider(collider);
            this.charater.listCharacterTarget.Remove(charater);
        }
    }

    private void RemoveTargetWhenHitted()
    {
        if (charater.listCharacterTarget.Count <= 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < charater.listCharacterTarget.Count; i++)
            {
                if (charater.listCharacterTarget[i].IsHited == true)
                {
                    charater.listCharacterTarget.RemoveAt(i);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AddTarget(other);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveTargetWhenExit(other);
    }
}
