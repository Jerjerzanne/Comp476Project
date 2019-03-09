using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfVision : MonoBehaviour
{
    public float visionRadius;
    [Range(0,360)]
    public float visionAngle;

    public LayerMask maskOfTarget;
    public LayerMask maskOfObstacle;

    public List<Transform> listOfTargets = new List<Transform>();
    /// <summary>
    /// Find the targets 
    /// </summary>

    void Update()
    {
        FindTarget();
    }

    void FindTarget()
    {
        listOfTargets.Clear();
        Collider[] visibleTargets = Physics.OverlapSphere(transform.position, visionRadius, maskOfTarget);

        for(int i = 0; i< visibleTargets.Length; i++)
        {
            Transform target = visibleTargets[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < visionAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, maskOfObstacle))
                {
                    listOfTargets.Add(target);
                }
            }
        }
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
}
