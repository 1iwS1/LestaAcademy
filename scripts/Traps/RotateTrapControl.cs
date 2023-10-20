using UnityEngine;

namespace lesta_academy
{
  public class RotateTrapControl : MonoBehaviour
  {
    private Rigidbody rigBody_;
    [SerializeField] private float rotationKoef_ = 45f;

    private void Start()
    {
      rigBody_ = GetComponent<Rigidbody>();

      rigBody_.constraints = RigidbodyConstraints.FreezePosition |
                        RigidbodyConstraints.FreezeRotationX |
                        RigidbodyConstraints.FreezeRotationZ;

      rotationKoef_ = RandomRotation(rotationKoef_);
    }

    private void FixedUpdate()
    {
      DoLogic();
    }

    private void DoLogic()
    {
      rigBody_.AddTorque(0f, rotationKoef_, 0f, ForceMode.Impulse);
    }

    private float RandomRotation(float rotation_koef)
    {
      int temp = Random.Range(0, 2);

      rotation_koef = temp == 0 ? rotation_koef * -1 : rotation_koef;

      return rotation_koef;
    }
  }
}
