using GameFlow;
using UnityEngine;

namespace LevelLogic
{
  public class FinishTrigger : MonoBehaviour
  {
    [SerializeField] private int index;

    private void OnTriggerStay2D(Collider2D other)
    {
      if (other.transform.GetComponent<Car>() != null)
        GameFlowManager.Instance.OnFinishEnter();
    }
  }
}