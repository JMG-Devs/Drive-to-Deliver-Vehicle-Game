using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMoverMultiple : MonoBehaviour
{
    [Header("Arrow Renderers")]
    public Renderer[] arrowRenderers; // Assign 4 arrows here

    [Header("Texture Movement")]
    public float speed = 0.5f;

    private Material[] arrowMaterials;

    void Start()
    {
        arrowMaterials = new Material[arrowRenderers.Length];

        for (int i = 0; i < arrowRenderers.Length; i++)
        {
            if (arrowRenderers[i] == null) continue;

            // Get first material only (index 0)
            arrowMaterials[i] = arrowRenderers[i].materials[0];
        }
    }

    void Update()
    {
        float move = speed * Time.deltaTime;

        for (int i = 0; i < arrowMaterials.Length; i++)
        {
            if (arrowMaterials[i] == null) continue;

            Vector2 offset = arrowMaterials[i].mainTextureOffset;
            offset.x -= move;
            arrowMaterials[i].mainTextureOffset = offset;
        }
    }
}
