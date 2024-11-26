using UnityEngine;

public class ButterflyGlow : MonoBehaviour
{
    public Material[] wingMaterials; // Array of materials to assign randomly

    public Transform leftWingLow;
    public Transform leftWingHigh;
    public Transform rightWingLow;
    public Transform rightWingHigh;

    public ParticleSystem trailParticleSystem; // Reference to the trail's particle system

    void Start()
    {
        // Apply random material to all wing parts
        Material chosenMaterial = ApplyRandomWingMaterialToAll();

        // Match the trail particle system color to the glow color
        if (chosenMaterial != null)
        {
            SetTrailStartColor(chosenMaterial);
        }
    }

    public Material ApplyRandomWingMaterialToAll()
    {
        if (wingMaterials.Length == 0) return null;

        // Choose a single random material from the list
        Material chosenMaterial = wingMaterials[Random.Range(0, wingMaterials.Length)];

        // Apply the chosen material to all wing parts
        AssignMaterialToWingPart(leftWingLow, chosenMaterial);
        AssignMaterialToWingPart(leftWingHigh, chosenMaterial);
        AssignMaterialToWingPart(rightWingLow, chosenMaterial);
        AssignMaterialToWingPart(rightWingHigh, chosenMaterial);

        return chosenMaterial;
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

    private void SetTrailStartColor(Material glowMaterial)
    {
        if (trailParticleSystem == null || glowMaterial == null) return;

        // Get the color from the glow material
        if (glowMaterial.HasProperty("_Color"))
        {
            Color glowColor = glowMaterial.color;

            // Modify the start color of the particle system
            var mainModule = trailParticleSystem.main;
            mainModule.startColor = glowColor;
        }
    }
}
