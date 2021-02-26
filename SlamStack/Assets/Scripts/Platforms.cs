using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> tileMeshRenderers = new List<MeshRenderer>();
    [SerializeField] private List<Rigidbody> tileRigidbodies = new List<Rigidbody>();
   
    public void Initialize(List<int> damageTileIdencies, Material normalMat, Material damageMat)
    {
        for (int i = 0; i < tileMeshRenderers.Count; i++)
        { //IF THIS IS A DAMAGE TILE.
            if (damageTileIdencies.Contains(i))
            {
                tileMeshRenderers[i].material = damageMat;
                tileMeshRenderers[i].tag = "Damage tile";
            }
            //IF THIS IS A NORMAL TILE.
            else
            {
                tileMeshRenderers[i].material = normalMat;
                tileMeshRenderers[i].tag = "Normal tile";
            }
        }
    }

    public void DestroyPlatform()
    {
        for (int i = 0; i < tileRigidbodies.Count; i++)
        {
            tileRigidbodies[i].isKinematic = false;
            tileRigidbodies[i].AddExplosionForce(1000, transform.position - new Vector3(0, 1, 0), 2);
        }
        Destroy(gameObject, 1);
    }
}
