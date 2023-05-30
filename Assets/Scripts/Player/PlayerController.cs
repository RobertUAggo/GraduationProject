using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] Joystick joystick;
    [SerializeField] private float baseSpeed = 4;
    public Player Player => player;
    public NavMeshAgent NavAgent;
    public void Init()
    {
        
    }
    private void Start()
    {
        Player.ControllerStart();
    }
    private void Update()
    {
        if (joystick.Direction3D != Vector3.zero)
        {
            NavAgent.Move(joystick.Direction3D * Time.deltaTime * NavAgent.speed);
            transform.rotation = Quaternion.LookRotation(joystick.Direction3D);
            Player.Animator.SetBool(Player.IsMovingParam, true);
        }
        else
        {
            Player.Animator.SetBool(Player.IsMovingParam, false);
        }
    }
    public void Disable()
    {
        NavAgent.isStopped = true;
        joystick.gameObject.SetActive(false);
        enabled = false;
    }
}
