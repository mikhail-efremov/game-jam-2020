using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using GameFlow;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
  public const float turnDuration = 4f;
  public const float timelapseDuration = 2f;
  public GameObject car;
  [FormerlySerializedAs("positions")] public List<Vector3> spawnPositions;

  private List<GameObject> _cars = new List<GameObject>();
  private Dictionary<int, List<CarPosition>> _carToPositions = new Dictionary<int, List<CarPosition>>();
  private int _currentPos = -1;
  private int _gameTick;
  private bool _isTimeLapse;

  public void StartWork()
  {
    StartCoroutine(NextCarChooser());
  }
  private void Start() // todo: DeLETE IT
  {
    StartCoroutine(NextCarChooser());
  }

  private IEnumerator NextCarChooser()
  {
    while (true)
    {
      NextCar();
      yield return new WaitForSeconds(turnDuration);
      _isTimeLapse = true;
      Destroy(_cars[_currentPos].GetComponent<Car>());
      yield return new WaitForSeconds(timelapseDuration);
      _isTimeLapse = false;
    }
  }

  private void FixedUpdate()
  {
    // if(ISACTION) should track only if we play and not showing some numbers 
    if (_isTimeLapse)
    {
      Timelapse();
    }
    else
    {
      MoveStraightforward();
    }
  }

  private void Timelapse()
  {
    var currentCar = _cars[_currentPos];
    _carToPositions[_currentPos].Add(new CarPosition(currentCar.transform.position, currentCar.transform.rotation));

    for (var i = 0; i < _currentPos + 1; i++)
    {
      _cars[i].transform.position = _carToPositions[i][_gameTick].position;
      _cars[i].transform.rotation = _carToPositions[i][_gameTick].rotation;
    }

    _gameTick-=(int)(turnDuration/timelapseDuration);
  }

  private void MoveStraightforward()
  {
    var currentCar = _cars[_currentPos];
    _carToPositions[_currentPos].Add(new CarPosition(currentCar.transform.position, currentCar.transform.rotation));

    for (var i = 0; i < _currentPos; i++)
    {
      _cars[i].transform.position = _carToPositions[i][_gameTick].position;
      _cars[i].transform.rotation = _carToPositions[i][_gameTick].rotation;
    }

    _gameTick++;
  }

  public void NextCar()
  {
    _currentPos++;

    var go = Instantiate(car, spawnPositions[_currentPos], Quaternion.identity);
    var carComponent = go.GetComponent<Car>();
    carComponent.IsPlayerControlled = true;

    // GameFlowManager.Instance.CurrentCar = carComponent;
    _cars.Add(go);
    _carToPositions.Add(_currentPos, new List<CarPosition>());
    _gameTick = 0;
  }
}