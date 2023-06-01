using UnityEngine;

public class CircleSpawner : BaseSpawner
{
    [SerializeField] private float radius = 15;
    [SerializeField, Range(0, 360)] private float minAngle;
    [SerializeField, Range(0, 360)] private float maxAngle;
    public override Vector3 GetPoint()
    {
        return GetRelativePoint(Random.value);
    }
    /// <param name="relativeValue"> from 0 to 1</param>
    public Vector3 GetRelativePoint(float relativeValue)
    {
        float angle = minAngle + relativeValue * (maxAngle - minAngle);
        return transform.position + GetPosInCircle(radius, angle);
    }
    private Vector3 GetPosInCircle(float radius, float angle)
    {
        return new Vector3(
            Mathf.Cos(angle / 180f * Mathf.PI) * radius,
            0,
            Mathf.Sin(angle / 180f * Mathf.PI) * radius
            );
    }
#if UNITY_EDITOR
    const float OneLineAngle = 5f;
    private void OnDrawGizmosSelected()
    {
        if (minAngle > maxAngle)
        {
            maxAngle = minAngle;
        }
        int spotsCount = Mathf.FloorToInt((maxAngle - minAngle) / OneLineAngle);
        //Gizmos.DrawWireSphere(transform.position, radius);
        if (maxAngle - minAngle < 2f) return;
        float angleStep = (maxAngle - minAngle) / spotsCount;
        Vector3 oldPos = Vector3.zero;
        for (float angle = minAngle; angle <= maxAngle; angle += angleStep)
        {
            Vector3 spotPos = transform.position + GetPosInCircle(radius, angle);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, spotPos);
            if(angle!=minAngle) Gizmos.DrawLine(oldPos, spotPos);
            oldPos = spotPos;
            //Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.5f);
            //Gizmos.DrawSphere(spotPos, 0.25f);
        }
    }
#endif
}
