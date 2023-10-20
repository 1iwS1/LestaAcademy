using UnityEngine;

namespace lesta_academy
{
  public class Player : MonoBehaviour
  {
    public HealthTextC healthC;

    private Rigidbody rigidBody_;
    private int healthPoints_ = 100;

    public static bool isFinished;
    private bool isStarted_;
    private int onlyOneStart_;

    private MovementState moveState_;
    public static LifeState healthState;
    public static bool isFell;

    [SerializeField] private Transform cam_;
    private float moveSpeed_;
    private Vector3 movement_;
    [SerializeField] private float walkSpeed_ = 0.6f;
    [SerializeField] private float sprintSpeed_ = 0.9f;
    [SerializeField] private float inAirSpeed_ = 0.4f;
    [SerializeField] private float smoothAngleTime_ = 0.1f;
    private float smoothAngleVelocity_;

    [SerializeField] private float jumpForce_ = 15f;

    private Vector3 velocity_;

    [SerializeField] private Transform groundChecker_;
    [SerializeField] private float groundDistance_ = 0.4f;
    [SerializeField] private LayerMask groundMask_;
    private bool isGrounded_;

    private ConstantForce windEffect_;
    private bool isOnWindtrap_;

    private bool isOnDamagetrap_;

    private bool isOnLavagetrap_;
    private bool doSlowdown_;
    private float slow_ = 0.55f;

    void Start()
    {
      rigidBody_ = GetComponent<Rigidbody>();
      windEffect_ = GetComponent<ConstantForce>();

      rigidBody_.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

      SetMovementState(lesta_academy.MovementState.STILL);
      SetLifeState(LifeState.ALIVE);

      isFell = false;
      isFinished = false;

      isStarted_ = false;
      onlyOneStart_ = 1;
    }

    void FixedUpdate()
    {
      MoveStateDetermination();
      LifeStateDetermination();

      WindEffectHandler();
      DamageTrapHandler();
      LavaTrapHandler();

      healthC.SetHealth(healthPoints_.ToString());

      Move();
    }

    private void SetMovementState(MovementState new_state)
    {
      moveState_ = new_state;
    }

    private MovementState GetMovementState()
    {
      return moveState_;
    }

    private void MoveStateDetermination()
    {
      if (isGrounded_ && Input.GetButton("LShift"))
      {
        SetMovementState(lesta_academy.MovementState.SPRINT);
      }

      else if (isGrounded_ && movement_.magnitude != 0)
      {
        SetMovementState(lesta_academy.MovementState.WALK);
      }

      else if (!isGrounded_)
      {
        SetMovementState(lesta_academy.MovementState.IN_AIR);
      }

      else if (isGrounded_ && movement_.magnitude == 0)
      {
        SetMovementState(lesta_academy.MovementState.STILL);
      }
    }

    private void SetLifeState(LifeState new_state)
    {
      healthState = new_state;
    }

    private LifeState GetLifeState()
    {
      return healthState;
    }

    private void LifeStateDetermination()
    {
      if (healthPoints_ > 0)
      {
        SetLifeState(LifeState.ALIVE);
      }

      else
      {
        SetLifeState(LifeState.DEAD);
      }
    }

    private void WindEffectHandler()
    {
      if (isOnWindtrap_)
      {
        Vector3 temp = WindTrapControl.windDirection;

        windEffect_.force = temp;
      }

      else
      {
        windEffect_.force = Vector3.zero;
      }
    }

    private void DamageTrapHandler()
    {
      if (isOnDamagetrap_)
      {
        DamageTrapControl.StartLogic();
        
        if (DamageTrapControl.doDamage)
        {
          DamageTrapControl.doDamage = false;
          healthPoints_ -= DamageTrapControl.damage;
        }
      }

      else
      {
        DamageTrapControl.toReset = true;
      }
    }

    private void LavaTrapHandler()
    {
      if (isOnLavagetrap_)
      {
        if (LavaTrapControl.doEffects)
        {
          LavaTrapControl.doEffects = false;

          healthPoints_ -= LavaTrapControl.damage;

          doSlowdown_ = true;
        }
      }

      else
      {
        doSlowdown_ = false;
      }
    }

    private float SlowdownEffect(float temp)
    {
      if (doSlowdown_)
      {
        return slow_;
      }

      else
      {
        return 1f;
      }
    }

    private void Move()
    {
      GroundCheck();

      float vertical, horizontal;
      float rotate_angle, angle;
      Vector3 move_direction;
      float slowdown = 1f;

      vertical = Input.GetAxisRaw("Vertical");
      horizontal = Input.GetAxisRaw("Horizontal");

      movement_ = new Vector3(horizontal, 0f, vertical).normalized;
      
      if (movement_.magnitude != 0)
      {
        rotate_angle = Mathf.Atan2(movement_.x, movement_.z) * Mathf.Rad2Deg + cam_.eulerAngles.y;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotate_angle, ref smoothAngleVelocity_, smoothAngleTime_);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        move_direction = (Quaternion.Euler(0f, rotate_angle, 0f) * Vector3.forward).normalized;
        moveSpeed_ = MovementState();

        slowdown = SlowdownEffect(slowdown);

        rigidBody_.AddForce(move_direction * slowdown * moveSpeed_, ForceMode.Impulse);
      }

      Jump(slowdown);
    }

    private void GroundCheck()
    {
      isGrounded_ = CollisionControl.IsGroundedCheck(groundChecker_, groundDistance_, groundMask_);
    }

    private void Jump(float slowdown)
    {
      if (isGrounded_ && Input.GetButtonDown("Jump"))
      {
        velocity_.y = jumpForce_ * slowdown;
        rigidBody_.AddForce(velocity_, ForceMode.Impulse);
      }
    }

    private float MovementState()
    {
      if (GetMovementState() == lesta_academy.MovementState.SPRINT)
      {
        return sprintSpeed_;
      }

      else if (GetMovementState() == lesta_academy.MovementState.WALK)
      {
        return walkSpeed_;
      }

      else
      {
        return inAirSpeed_;
      }
    }

    // определение коллизий игрока
    private void OnTriggerEnter(Collider other)
    {
      isOnWindtrap_ = CollisionControl.IsOnWindtrapCheck(other, isOnWindtrap_, true);
    }

    private void OnTriggerStay(Collider other)
    {
      isFinished = CollisionControl.IsFinishedCheck(other, isFinished, true);
    }

    private void OnTriggerExit(Collider other)
    {
      isOnWindtrap_ = CollisionControl.IsOnWindtrapCheck(other, isOnWindtrap_, false);
      isFell = CollisionControl.IsFellCheck(other, isFell, true);

      IsStartedCheck(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
      isOnDamagetrap_ = CollisionControl.IsOnDamagetrapCheck(collision, isOnDamagetrap_, true);
      isOnLavagetrap_ = CollisionControl.IsOnLavatrapCheck(collision, isOnLavagetrap_, true);
    }

    private void OnCollisionExit(Collision collision)
    {
      isOnDamagetrap_ = CollisionControl.IsOnDamagetrapCheck(collision, isOnDamagetrap_, false);
      isOnLavagetrap_ = CollisionControl.IsOnLavatrapCheck(collision, isOnLavagetrap_, false);
    }

    private void IsStartedCheck(Collider other)
    {
      if (onlyOneStart_ == 1)
      {
        --onlyOneStart_;
        isStarted_ = CollisionControl.IsStartedCheck(other, isStarted_, true);
        GameController.toStart = true;
      }
    }
  }
}