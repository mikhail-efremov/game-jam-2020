using DG.Tweening;
using LevelLogic;
using TMPro;
using UnityEngine;

namespace GameFlow
{
  public class GameFlowManager : MonoBehaviour
  {
    [SerializeField] private Car _currentCar;
    [SerializeField] private TextMeshProUGUI _tabToStartLabel;
    [SerializeField] private FinishTrigger[] _finishes;

    public static GameFlowStateId CurrentStateId;
    public static int CurrentPlayerIndex;

    private void Start()
    {
      SetToStartScreen();
    }

    private void Update()
    {
      var spacePressed = Input.GetKeyDown(KeyCode.Space);

      if (CurrentStateId == GameFlowStateId.StartScreen && spacePressed)
      {
        SetToGameplay();
      }
    }

    private void SetToStartScreen()
    {
      CurrentStateId = GameFlowStateId.StartScreen;
      _currentCar.enabled = false;
      _tabToStartLabel.DOFade(0.0f, .5f).SetEase(Ease.InExpo).SetLoops(-1, LoopType.Yoyo);

      Debug.LogWarning("set to StartScreen");
    }

    private void SetToGameplay()
    {
      CurrentStateId = GameFlowStateId.Gameplay;
      _currentCar.enabled = true;
      _tabToStartLabel.gameObject.SetActive(false);

      Debug.LogWarning("set to Gameplay");
    }
  }
}