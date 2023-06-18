using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public enum ControlsMode
{
    HoldToRun,
    Joystick,
    Shooter,
    Idle
}
public class PlayerController : MonoBehaviour
{
    public ControlsMode controlsType;
    public Joystick joystick;
    public GameObject child, ballsHolder, dragPanel;
    public Transform destination;
    public VariableJoystick variableJoystick;
    public Animator playerAnimator, camAnimator;
    public Rigidbody rb;
    public NavMeshAgent agent;
    public ParticleSystem respawnParticles;
    public Vector3 childRotation, currentVelocity;
    public Quaternion lastRotation;
    public float speed, runSpeed, distance;
    public int ballCount;
    public bool isDown, isUp, timeUp, isFalling;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();
        child = gameObject.transform.GetChild(0).gameObject;
    }
    public void FixedUpdate()
    {
        //if (GameManager.instance.gameStart)
        {
            if (controlsType == ControlsMode.Joystick && !timeUp && !isFalling)
            {
                if (Input.GetMouseButton(0))
                {
                    LookAround();
                    MovePlayer();
                }
                else if (!timeUp)
                {
                    StopPlayer();
                    StopLookAround();
                }
            }
        }

    }


    public void Update()
    {
        //if (GameManager.instance.gameStart)
        {
            if (controlsType == ControlsMode.HoldToRun && !timeUp && !isFalling)
            {
                if (Input.GetMouseButton(0))
                {
                    playerAnimator.SetBool("Idle", false);
                    playerAnimator.SetBool("Run", true);
                    transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
                }
                else if (!timeUp)
                {
                    playerAnimator.SetBool("Run", false);
                    playerAnimator.SetBool("Idle", true);
                }
            }
        }
        if (timeUp)
        {
            transform.LookAt(destination);
            DistanceChecker();
        }
    }

    public void MovePlayer()
    {
        Vector3 desiredVelocity = gameObject.transform.forward * ((Mathf.Abs(joystick.Vertical) + Mathf.Abs(joystick.Horizontal) > 0.1f) ? speed : 0);
        desiredVelocity.y = rb.velocity.y;
        rb.velocity = desiredVelocity;
        currentVelocity = desiredVelocity;
        //playerAnimator.SetTrigger("Run");
        playerAnimator.SetBool("Idle", false);
        playerAnimator.SetBool("Run", true);
    }

    public void StopPlayer()
    {
        //playerAnimator.SetTrigger("Idle");
        playerAnimator.SetBool("Run", false);
        playerAnimator.SetBool("Idle", true);
        rb.velocity = new Vector3(0, 0, 0);
    }
    public void LookAround()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        childRotation = new Vector3(horizontal, rb.transform.rotation.y, vertical);
        Quaternion rotation = Quaternion.LookRotation(childRotation);
        lastRotation = Quaternion.LookRotation(childRotation);
        rb.transform.rotation = rotation;
    }

    public void StopLookAround()
    {
        rb.transform.rotation = lastRotation;
    }

    public void DistanceChecker()
    {
        distance = Vector3.Distance(gameObject.transform.position, destination.position);
        if (distance <= 1)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            playerAnimator.SetBool("Run", false);
            playerAnimator.SetBool("Idle", true);
        }
    }
    public void TimeUp()
    {
        controlsType = ControlsMode.Idle;

        //joystick.background.gameObject.SetActive(false);
        rb.velocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<NavMeshAgent>().SetDestination(destination.position);
        playerAnimator.SetBool("Run", true);
        playerAnimator.SetBool("Idle", false);
        timeUp = true;
        StartCoroutine(TimeUpDelay());
    }

    public void Fall()
    {
        
        isFalling = true;
        
        //child.SetActive(false);


        StartCoroutine(FallDelay());
    }

    public IEnumerator FallDelay()
    {
        yield return new WaitForSeconds(0.1f);
        playerAnimator.SetBool("Fall", true);
        playerAnimator.SetBool("Idle", false);
        playerAnimator.SetBool("Run", false);
        child.transform.DOLocalJump(new Vector3(child.transform.localPosition.x, child.transform.localPosition.y, child.transform.localPosition.z-3), 2, 1, 1f).OnComplete(() =>
        {
            
        });
        
        yield return new WaitForSeconds(2f);

        child.SetActive(false);
        child.transform.DOLocalMoveX(0, 0.1f);
        gameObject.transform.DOLocalMoveZ(transform.position.z - 3, 1f).SetEase(Ease.OutBack);

        child.transform.localPosition = new Vector3(0, -0.43f, 0);
        isFalling = false;
        playerAnimator.SetBool("Fall", false);
        playerAnimator.SetBool("Idle", true);
        child.SetActive(true);
    }

    public IEnumerator TimeUpDelay()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => distance < 10);
        camAnimator.SetBool("Shooter", true);
        /*playerAnimator.SetBool("Run", false);
        playerAnimator.SetBool("Idle", true);*/
        controlsType = ControlsMode.Shooter;
    }
}