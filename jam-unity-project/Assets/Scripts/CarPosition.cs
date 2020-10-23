using UnityEngine;

namespace DefaultNamespace
{
  public struct CarPosition
  {
    public readonly Vector3 position;
    public readonly Quaternion rotation;

    public CarPosition(Vector3 position, Quaternion rotation)
    {
      this.position = position;
      this.rotation = rotation;
    }
  }
}