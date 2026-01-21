using UnityEngine;

public class RainController : MonoBehaviour
{
    [Header("Rain Settings")]
    public int rainCount = 1000;           // Number of particles emitted per second
    public float rainHeight = 20f;         // Height above scene
    public float areaSizeX = 50f;          // Width of rain area
    public float areaSizeZ = 50f;          // Depth of rain area
    public float dropLifetimeMin = 4f;     // Minimum particle lifetime
    public float dropLifetimeMax = 6f;     // Maximum particle lifetime
    public float fallSpeed = -25f;         // Speed of falling drops
    public Material rainMaterial;          // Assign a simple unlit material with white/blue texture

    void Start()
    {
        // Create a new GameObject for the particle system
        GameObject rain = new GameObject("RainSystem");
        rain.transform.parent = transform;
        rain.transform.localPosition = Vector3.zero;

        ParticleSystem ps = rain.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.duration = 10f;
        main.loop = true;
        main.startLifetime = new ParticleSystem.MinMaxCurve(dropLifetimeMin, dropLifetimeMax);
        main.startSpeed = 0f;  // velocity controlled by VelocityOverLifetime
        main.startSize = 0.1f;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.playOnAwake = true;
        main.prewarm = true;

        // Emission
        var emission = ps.emission;
        emission.rateOverTime = rainCount;

        // Shape
        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(areaSizeX, 1f, areaSizeZ);
        shape.position = new Vector3(0f, rainHeight, 0f);

        // Velocity over lifetime
        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.World;
        velocity.y = fallSpeed;

        // Optional slight wind
        velocity.x = new ParticleSystem.MinMaxCurve(-1f, 1f);
        velocity.z = new ParticleSystem.MinMaxCurve(-1f, 1f);

        // Renderer
        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        renderer.renderMode = ParticleSystemRenderMode.Stretch;
        renderer.lengthScale = 8f;
        renderer.velocityScale = 0.1f;
        if (rainMaterial != null)
            renderer.material = rainMaterial;

        // Play
        ps.Play();
    }
}

