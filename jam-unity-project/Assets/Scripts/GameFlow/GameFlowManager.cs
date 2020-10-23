using DG.Tweening;
using LevelLogic;
using TMPro;
using UnityEngine;

namespace GameFlow
{
  public class GameFlowManager : MonoBehaviour
  {
    public Car CurrentCar;
    [SerializeField] private TextMeshProUGUI _tabToStartLabel;
    [SerializeField] private FinishTrigger[] _finishes;

    public static GameFlowStateId CurrentStateId;
    public static int CurrentPlayerIndex;

    public static GameFlowManager Instance = null;

    private void Start()
    {
      if (Instance == null)
        Instance = this;

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

      if (CurrentCar != null)
        CurrentCar.enabled = false;
      _tabToStartLabel.DOFade(0.0f, .5f).SetEase(Ease.InExpo).SetLoops(-1, LoopType.Yoyo);

      Debug.LogWarning("set to StartScreen");
    }

    private void SetToGameplay()
    {
      CurrentStateId = GameFlowStateId.Gameplay;
      
      if (CurrentCar != null)
        CurrentCar.enabled = true;
      _tabToStartLabel.gameObject.SetActive(false);

      GetComponent<GameController>().StartWork();

      Debug.LogWarning("set to Gameplay");
    }
  }
}