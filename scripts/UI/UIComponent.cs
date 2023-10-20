using UnityEngine;

namespace lesta_academy
{
  public abstract class UIComponent : MonoBehaviour
  {
    private void Awake()
    {
      Initialization();
    }

    private void Initialization()
    {
      Setup();
      Configure();
    }

    public abstract void Setup();
    public abstract void Configure();
  }
}
