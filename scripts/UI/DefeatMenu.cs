using UnityEngine;
using UnityEngine.UI;

namespace lesta_academy
{
  public class DefeatMenu : UIComponent
  {
    [SerializeField] private DefeatMenuSO defeatMenuData_;

    [SerializeField] private Image panel_;
    [SerializeField] private Text text_;
    [SerializeField] private Button restartButton_;
    private Text restartButtonText_;

    public override void Setup()
    {
      restartButtonText_ = restartButton_.GetComponentInChildren<Text>();
      restartButton_.onClick.AddListener(DefeatMenuC.OnClick);
    }

    public override void Configure()
    {
      text_.text = defeatMenuData_.title;
      restartButtonText_.text = defeatMenuData_.buttonText;
    }
  }
}
