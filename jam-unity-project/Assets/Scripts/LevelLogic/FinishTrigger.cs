using GameFlow;
using UnityEngine;

namespace LevelLogic
{
  public class FinishTrigger : MonoBehaviour
  {
    public int Index => index;
    [SerializeField] private int index;

    private void OnTriggerStay2D(Collider2D other)
    {
      if (other.transform.GetComponent<Car>() != null)
        GameFlowManager.Instance.OnFinishEnter();
    }
  }
}