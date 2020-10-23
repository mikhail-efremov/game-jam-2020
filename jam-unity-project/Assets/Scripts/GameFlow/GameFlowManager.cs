using System.Collections;
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

    [SerializeField] private TextMeshProUGUI _firstPlayerFinished;
    [SerializeField] private TextMeshProUGUI _secondPlayerFinished;

    [SerializeField] private TextMeshProUGUI _firstPlayerLose;
    [SerializeField] private TextMeshProUGUI _secondPlayerLose;

    [SerializeField] private FinishTrigger[] _finishes;

    public static GameFlowStateId CurrentStateId;
    public static PlayerTypeId CurrentPlayer;

    public static GameFlowManager Instance = null;

    private float CurrentTurnTime;

    public void OnFinishEnter()
    {
      SetToPlayerReachedFinish();
    }

    private void Start()
    {
      _firstPlayerFinished.gameObject.SetActive(false);
      _secondPlayerFinished.gameObject.SetActive(false);
      _firstPlayerLose.gameObject.SetActive(false);
      _secondPlayerLose.gameObject.SetActive(false);

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

      CurrentTurnTime += Time.deltaTime;
      if (CurrentTurnTime > GameController.turnDuration)
      {
        if (CurrentPlayer == PlayerTypeId.First)
          _firstPlayerLose.gameObject.SetActive(true);
        if (CurrentPlayer == PlayerTypeId.Second)
          _secondPlayerLose.gameObject.SetActive(true);
      }
    }

    private void SetToStartScreen()
    {
      CurrentStateId = GameFlowStateId.StartScreen;

      if (CurrentCar != null)
        CurrentCar.enabled = false;
      _tabToStartLabel.DOFade(0.0f, .5f).SetEase(Ease.InExpo).SetLoops(-1, LoopType.Yoyo);
    }

    private void SetToGameplay()
    {
      CurrentStateId = GameFlowStateId.Gameplay;

      if (CurrentCar != null)
        CurrentCar.enabled = true;
      _tabToStartLabel.gameObject.SetActive(false);

      GetComponent<GameController>().GetNextCar();
    }

    private void SetToPlayerReachedFinish()
    {
      CurrentStateId = GameFlowStateId.PlayerReachedFinish;

      StartCoroutine(PlayerReachedFinish());
    }

    private IEnumerator PlayerReachedFinish()
    {
      if (CurrentPlayer == PlayerTypeId.First)
        _firstPlayerFinished.gameObject.SetActive(true);
      if (CurrentPlayer == PlayerTypeId.Second)
        _secondPlayerFinished.gameObject.SetActive(true);

      CurrentCar.EndTrails();
      Destroy(CurrentCar);
      CurrentCar.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
      GetComponent<GameController>().EnableTimeLaps();
      yield return new WaitForSeconds(2);
      GetComponent<GameController>().DisableTimeLaps();

      _firstPlayerFinished.gameObject.SetActive(false);
      _secondPlayerFinished.gameObject.SetActive(false);

      if (CurrentPlayer == PlayerTypeId.First)
        CurrentPlayer = PlayerTypeId.Second;
      else if (CurrentPlayer == PlayerTypeId.Second)
        CurrentPlayer = PlayerTypeId.First;

      SetToGameplay();
    }
  }
}