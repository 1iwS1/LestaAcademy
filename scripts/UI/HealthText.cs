using UnityEngine;
using UnityEngine.UI;

namespace lesta_academy
{
  public class HealthText : UIComponent
  {
    [SerializeField] private HealthTextSO healthTextData_;

    public Text healthPointsText;

    public override void Setup() {}
    public override void Configure() {}
  }
}
