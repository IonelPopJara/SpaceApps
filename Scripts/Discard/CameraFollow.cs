using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    /*
    * This script is not used in the final version
    */
    [Header("Target")]
    public Transform target;

    [Header("Offset")]
    public Vector3 offset = Vector3.zero;

    [Header("Limits")]
    public Vector2 limits = new Vector2(5,3);

    [Range(0,1)]
    public float smoothTime;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        if(!Application.isPlaying)
        {
            transform.localPosition = offset;
        }

        FollowTarget(target);
    }

    private void LateUpdate()
    {
        Vector3 localPos = transform.localPosition;

        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limits.x, limits.x), Mathf.Clamp(localPos.y, -limits.y, limits.y), localPos.z);
    }

    public void FollowTarget(Transform _transform)
    {
        Vector3 localPos = transform.localPosition;
        Vector3 targetLocalPos = _transform.localPosition;
        transform.localPosition = Vector3.SmoothDamp(localPos, new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, localPos.z), ref velocity, smoothTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-limits.x, -limits.y, transform.position.z), new Vector3(limits.x, -limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-limits.x, limits.y, transform.position.z), new Vector3(limits.x, limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-limits.x, -limits.y, transform.position.z), new Vector3(-limits.x, limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(limits.x, -limits.y, transform.position.z), new Vector3(limits.x, limits.y, transform.position.z));
    }
}
