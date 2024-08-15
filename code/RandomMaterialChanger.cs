using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialChanger : MonoBehaviour
{
    public List<Material> materialsToApply = new List<Material>(); // List of materials to apply
    private Renderer renderer;
    private int materialIndex;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        if (renderer != null && materialsToApply.Count > 0)
        {
            // Randomly select an initial material index
            materialIndex = Random.Range(0, materialsToApply.Count);

            // Get a copy of the materials array
            Material[] materials = renderer.materials;

            // Apply the initial material to the first submesh (element 1)
            materials[1] = materialsToApply[materialIndex];

            // Assign the modified materials array back to the renderer
            renderer.materials = materials;

            // Update material initially
            UpdateMaterial();
        }
        else
        {
            Debug.LogError("Renderer component or materials list is missing or empty!");
        }
    }

    void UpdateMaterial()
    {
        // You can implement any additional logic for updating materials here if needed
    }
}
