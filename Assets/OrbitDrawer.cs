using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Attractor))]
public class OrbitDrawer : MonoBehaviour
{
    [SerializeField] bool drawTrail = false;
    [SerializeField] bool drawOrbit = true;
    // Start is called before the first frame update
    GameObject drawerContainer;
    public Attractor parentAttractor;

    Attractor thisAttractor;
    public Material lineMaterial;
    public GameObject dashLinePrefab;


    LineRenderer lineRenderer;
    Transform selfTransform;

    Vector3 currentPos;
    Vector3 oldPos;
    [SerializeField] float deltaTime = 0.1f;
    [SerializeField] int maxPoints = 1200;
    public void Start()
    {
        
        thisAttractor = GetComponent<Attractor>();
        selfTransform = transform;
        drawerContainer = new GameObject("orbit draw");
        drawerContainer.transform.parent = transform;
        drawerContainer.transform.localPosition = Vector3.zero;
        lineRenderer = drawerContainer.AddComponent<LineRenderer>();

        lineRenderer.material = lineMaterial;
        
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;


        currentPos = selfTransform.position;
        oldPos = selfTransform.position;
    }
    
    public List<Vector2> orbitPoints;
    void CalculateOrbitPoints()
    {
        orbitPoints.Clear();
        float time = 0;
        Vector2 velocity = thisAttractor.rb.velocity;
        Vector2 position = selfTransform.position;
        Vector2 start_position = position;
        Vector2 objPos = parentAttractor.transform.position;
        Vector2 acceleration = CalculateAcceleration(position, objPos);

        Debug.DrawLine(position, position+acceleration, Color.red);

        orbitPoints.Add(position);
        for (int i = 0; i < maxPoints; i++)
        {

            time += deltaTime;
            Vector2 new_velocity = velocity + acceleration * deltaTime;
            position += (velocity+ new_velocity )/2* deltaTime + acceleration * deltaTime * deltaTime / 2;            
            
            
            acceleration = CalculateAcceleration(position, objPos);
            orbitPoints.Add(position);
            velocity = new_velocity;
            if (i > 10)
            {
                if ((position - start_position).sqrMagnitude < 0.01f)
                    break;
            }



        }

    }
    Vector2 CalculateAcceleration(Vector2 selfPos,Vector2 objPos)
    {
        return -thisAttractor.CalculateForce(parentAttractor,selfPos,objPos)/thisAttractor.rb.mass;
    }
    void DrawOrbit()
    {
        CalculateOrbitPoints();
        Vector3[] points = new Vector3[orbitPoints.Count];
        lineRenderer.positionCount = orbitPoints.Count;
        for (int i = 0; i < points.Length; i++)
        {
            
            points[i] = new Vector3(orbitPoints[i].x, orbitPoints[i].y, 0);
        
        }
       
        lineRenderer.SetPositions(points);
        
    }

    int framesMax = 0;
    int counter = 0;


    float step = 0.1f;
    float timer = 0f;
    bool spawned;
    void Update()
    {
        HandleOrbitDraw();
        HandleTrailDraw();

    }

    private void HandleTrailDraw()
    {
        if (drawTrail)
        {
            timer += Time.deltaTime;
            if (timer > step / 2 && !spawned)
            {

                currentPos = selfTransform.position;
                SpawnLine(oldPos, currentPos);
                //oldPos = currentPos;
                timer = 0;
                spawned = true;
            }
            if (timer > step)
            {
                timer = 0;
                spawned = false;
                oldPos = selfTransform.position;
            }
        }
    }

    private void HandleOrbitDraw()
    {
        if (drawOrbit)
        {
            lineRenderer.enabled = true;
            counter++;
            if (counter > framesMax)
            {
                DrawOrbit();
                counter = 0;

            }
        }
        else
            lineRenderer.enabled = false;
    }

    void SpawnLine(Vector3 pos1, Vector3 pos2)
    {

        LineRenderer l = SimplePool.Spawn(dashLinePrefab).GetComponent<LineRenderer>();
        l.SetPosition(0, pos1);
        l.SetPosition(1, pos2);
        

    }
}
