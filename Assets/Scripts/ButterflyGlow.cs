using UnityEngine;

public class ButterflyGlow : MonoBehaviour
{
    public Material[] wingMaterials; // Array of materials to assign randomly

    public Transform leftWingLow;
    public Transform leftWingHigh;
    public Transform rightWingLow;
    public Transform rightWingHigh;

    void Start()
    {
        // Apply random material to all wing parts
        ApplyRandomWingMaterialToAll();
    }

    public void ApplyRandomWingMaterialToAll()
    {
        if (wingMaterials.Length == 0) return;

        // Choose a single random material from the list
        Material chosenMaterial = wingMaterials[Random.Range(0, wingMaterials.Length)];

        // Apply the chosen material to all wing parts
        AssignMaterialToWingPart(leftWingLow, chosenMaterial);
        AssignMaterialToWingPart(leftWingHigh, chosenMaterial);
        AssignMaterialToWingPart(rightWingLow, chosenMaterial);
        AssignMaterialToWingPart(rightWingHigh, chosenMaterial);
    }

    private void AssignMaterialToWingPart(Transform wingPart, Material material)
    {
        if (wingPart == null) return;

        Renderer renderer = wingPart.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material[] newMaterials = renderer.materials;

            for (int i = 0; i < newMaterials.Length; i++)
            {
                // Assign the chosen material to all slots
                newMaterials[i] = material;
            }

            renderer.materials = newMaterials;
        }
    }
}
