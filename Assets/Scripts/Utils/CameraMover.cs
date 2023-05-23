using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector2 range = new Vector2(10, 10);
    private Vector3 _startPos;
    private Vector2 _currentOffset = Vector2.zero;
    private void Start()
    {
        _startPos = transform.position;
    }
    public void Move(Vector2 vector)
    {
        _currentOffset += vector;
        _currentOffset.x = Mathf.Clamp(_currentOffset.x, -range.x, range.x);
        _currentOffset.y = Mathf.Clamp(_currentOffset.y, -range.y, range.y);
        transform.position = _startPos + transform.right * _currentOffset.x + transform.forward * _currentOffset.y;
    }
}
