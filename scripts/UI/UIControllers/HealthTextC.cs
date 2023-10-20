using UnityEngine;

namespace lesta_academy
{
  public class HealthTextC : MonoBehaviour
  {
    public GameObject healthText;
    public HealthText health;

    public void SetHealth(string newHealth)
    {
      health.healthPointsText.text = newHealth;
    }

    public void EnableHealthText()
    {
      healthText.SetActive(true);
    }

    public void DisableHealthText()
    {
      healthText.SetActive(false);
    }
  }
}
