using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeColorOnCollision : MonoBehaviour
{
    public List<Material> materialsToApply = new List<Material>(); // List of materials to apply
    public List<GameObject> objectsToChangeMaterial = new List<GameObject>(); // List of objects that can have their materials changed

    private void OnCollisionEnter(Collision collision)
    {
        // Log the names of the colliding objects for debugging
        Debug.Log("Collision detected between: " + gameObject.name + " and " + collision.collider.gameObject.name);

        // Check if the collided object is in the list of objectsToChangeMaterial
        if (objectsToChangeMaterial.Contains(collision.collider.gameObject))
        {
            // Start the coroutine to handle sequential material assignment
            StartCoroutine(ApplyMaterialsSequentially(collision.collider.gameObject));
        }
    }

    private IEnumerator ApplyMaterialsSequentially(GameObject targetObject)
    {
        Renderer renderer = targetObject.GetComponent<Renderer>();

        if (renderer == null)
        {
            Debug.LogError("Renderer component not found on the target object.");
            yield break;
        }

        foreach (Material material in materialsToApply)
        {
            // Check if the object is null before accessing its components
            if (targetObject == null)
            {
                Debug.LogWarning("Target object is null. Coroutine will be stopped.");
                yield break;
            }

            // Assign the current material to the target object's renderer
            renderer.material = material;

            // Remove the object from the list if it has material 3
            if (material.name == "Material3")
            {
                int index = objectsToChangeMaterial.IndexOf(targetObject);
                if (index != -1)
                {
                    objectsToChangeMaterial.RemoveAt(index);
                }

                // Destroy the GameObject (optional)
                Destroy(targetObject);
            }

            // Wait for a short time before applying the next material
            yield return new WaitForSeconds(1.15f);
        }
    }
}
