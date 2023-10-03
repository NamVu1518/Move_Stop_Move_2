using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, Charater> cacheCharaterCollision = new Dictionary<Collider, Charater>();

    public static Charater GetCharaterComponentForCollider(Collider collider)
    {
        if (!cacheCharaterCollision.ContainsKey(collider))
        {
            Charater collisCharater = collider.gameObject.GetComponent<Charater>();

            cacheCharaterCollision[collider] = collisCharater;
        }
        return cacheCharaterCollision[collider];
    }

    public static Dictionary<Collider, Renderer> cacheMaterialCollision = new Dictionary<Collider, Renderer>();

    public static Renderer GetRendererComponentForCollider(Collider collider)
    {
        if (!cacheMaterialCollision.ContainsKey(collider))
        {
            Renderer collisMaterial = collider.gameObject.GetComponent<Renderer>();

            cacheMaterialCollision[collider] = collisMaterial;
        }

        return cacheMaterialCollision[collider];
    }

    


    private static Dictionary<GameObject, Renderer> cacheGameObjectRenderer = new Dictionary<GameObject, Renderer>();

    public static Renderer GetRendererComponentGameObject(GameObject gameObject)
    {
        if (!cacheGameObjectRenderer.ContainsKey(gameObject))
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();

            cacheGameObjectRenderer[gameObject] = renderer;
        }

        return cacheGameObjectRenderer[gameObject];
    } 
}
