using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfVision : MonoBehaviour
{
    #region Variables

    public float visionRadius;
    [Range(0,360)]
    public float visionAngle;

    public LayerMask maskOfTarget;
    public LayerMask maskOfObstacle;
    private float distanceTarget;

    public float meshes;
    public MeshFilter meshFilter;
    private Mesh meshOfVision;

    private Entity myEntity;

    [HideInInspector]
    public List<GameObject> listOfTargets = new List<GameObject>();

    #endregion

    #region Create Cone of Vision
    void Start()
    {
        meshOfVision = new Mesh();
        meshOfVision.name = "Vision Mesh";
        meshFilter.mesh = meshOfVision;
    }
    void Awake()
    {
        myEntity = this.GetComponent<Entity>();
    }
    void Update()
    {
        FindTarget();
        DrawConeOfVision();
    }

    /// <summary>
    /// Find targets within its range
    /// </summary>
    void FindTarget()
    {
        listOfTargets.Clear();
        Collider[] visibleTargets = Physics.OverlapSphere(transform.position, visionRadius, maskOfTarget);

        for(int i = 0; i< visibleTargets.Length; i++)
        {
            GameObject target = visibleTargets[i].gameObject;
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < visionAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, maskOfObstacle))
                {
                    listOfTargets.Add(target);
                }
                distanceTarget = Vector3.Distance(transform.position, listOfTargets[i].transform.position);
            }
            else
            {
                distanceTarget = 0;
            }
        }
        myEntity.reactionEvent.Invoke(listOfTargets.ToArray());
        //Debug.Log(listOfTargets.ToArray());
    }


    /// <summary>
    /// Return a vector direction from the angles
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="globalAngle"></param>
    /// <returns></returns>
    public Vector3 DirectionFromAngle(float angle, bool globalAngle)
    {
        if (!globalAngle)
        {
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    /// <summary>
    /// Get the distance to target
    /// </summary>
    /// <returns></returns>
    public float getDistanceToTarget()
    {
        return distanceTarget;
    }

    #endregion

    #region DrawingCone

    /// <summary>
    /// Draw the meshes for the cone of vision
    /// </summary>
    void DrawConeOfVision()
    {
        int meshCount = Mathf.RoundToInt(visionAngle * meshes); //How many mesh of rays
        float meshCountSize = visionAngle / meshCount; // size of the mesh
        List<Vector3> visiblePoints = new List<Vector3>(); // list of visibles points to create the meshes
        for(int i = 0; i <= meshCount; i++)
        {
            float angle = transform.eulerAngles.y - visionAngle / 2 + meshCountSize * i;
            VisionCast rayCast = VisionRays(angle);
            visiblePoints.Add(rayCast.point);
        }

        int numOfVertices = visiblePoints.Count + 1;
        Vector3[] vertices = new Vector3[numOfVertices];
        int[] triangle = new int[(numOfVertices - 2) * 3];
        vertices[0] = Vector3.zero; //first vertice relative to gameobject

        for(int i = 0; i < numOfVertices - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(visiblePoints[i]);
            if (i < numOfVertices - 2)
            {
                triangle[i * 3] = 0; //first vertex of the triangle
                triangle[i * 3 + 1] = i + 1;
                triangle[i * 3 + 2] = i + 2;
            }
        }

        meshOfVision.Clear();
        meshOfVision.vertices = vertices;
        meshOfVision.triangles = triangle;
        meshOfVision.RecalculateNormals();
    }

    /// <summary>
    /// Create the visible rays
    /// </summary>
    /// <param name="globalAngle"></param>
    /// <returns></returns>
    VisionCast VisionRays(float globalAngle)
    {
        Vector3 direction = DirectionFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, visionRadius, maskOfObstacle))
        {
            return new VisionCast(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new VisionCast(false, transform.position + direction * visionRadius, visionRadius, globalAngle);
        }
    }
    #endregion

    #region struct
    /// <summary>
    /// Information of the rays cast
    /// </summary>
    public struct VisionCast
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public VisionCast(bool tempHit, Vector3 tempPoint, float tempDistance, float tempAngle)
        {
            hit = tempHit;
            point = tempPoint;
            distance = tempDistance;
            angle = tempAngle;
        }
    }
    #endregion
}
