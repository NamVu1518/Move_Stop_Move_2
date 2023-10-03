using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransperentcyBuiding : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] private Material buildingMaterial;
    [SerializeField] private Material buildingMaterialTransperency;

    private void TransparentBuilding(Collider collider)
    {
        if (collider.gameObject.CompareTag(Constant.TAG_BUILDING))
        {
            Cache.GetRendererComponentForCollider(collider).material = buildingMaterialTransperency;
        }
    }

    private void UntransparentBuilding(Collider collider)
    {
        if (collider.gameObject.CompareTag(Constant.TAG_BUILDING))
        {
            Cache.GetRendererComponentForCollider(collider).material = buildingMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TransparentBuilding(other);
    }

    private void OnTriggerExit(Collider other)
    {
        UntransparentBuilding(other);
    }
}
