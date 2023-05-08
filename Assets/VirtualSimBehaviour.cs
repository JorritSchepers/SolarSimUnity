using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VirtualSimBehaviour : MonoBehaviour
{
    public int steps;
    public float timeStep = 0.02f;

    private PlanetBehaviour[] bodies;
    private VirtualBody[] virtualBodies;

    // Start is called before the first frame update
    void Start()
    {
        // bodies = FindObjectsOfType<PlanetBehaviour>();
        
        // virtualBodies = new VirtualBody[bodies.Length];
        // for (int i = 0; i < virtualBodies.Length; i++) 
        // {
        //     virtualBodies[i] = new VirtualBody(bodies[i], timeStep, steps);
        // }
        // Debug.Log("Hello World");
        // for (int i = 0; i < steps; i++) 
        // {
        //     for (int j = 0; j < virtualBodies.Length; j++) 
        //     {
        //         virtualBodies[j].CalculateVelocity(virtualBodies);
        //     }

        //     for (int j = 0; j < virtualBodies.Length; j++) 
        //     {
        //         virtualBodies[j].MovePlanet();
        //     }
        // }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    class VirtualBody {
        public Vector3 position;
        public Vector3 velocity;
        public float mass;
        public LineRenderer lineRenderer;
        public Vector3[] drawPoints;
        private int i = 0;
        private int j = 0;
        private float timeStep;

        public VirtualBody (PlanetBehaviour body, float timeStep, int steps) {
            position = body.transform.position;
            velocity = body.initVelocity;
            mass = body.mass;
            drawPoints = new Vector3[steps];
        }

        public void CalculateVelocity(VirtualBody[] allBodies)
        {
            foreach (var otherBody in allBodies) 
            {
                if (otherBody == this) 
                {
                    continue;
                }

                float g = 0.0001f;
                var m1 = mass;
                var m2 = otherBody.mass;
                float r = (otherBody.position - position).sqrMagnitude;

                Vector3 forceDir = (otherBody.position - position).normalized;
                Vector3 acceleration = forceDir * g * otherBody.mass / r;

                velocity += acceleration * timeStep;
            }
        }

        public void MovePlanet() 
        {
            position = position + velocity * timeStep;    
            DrawFlightLine();
        }

        void DrawFlightLine()
        {
            if (i%10 == 0) 
            {
                lineRenderer.SetPosition(j, position);
                j++;
            }
            i++;
        }
    }
}
