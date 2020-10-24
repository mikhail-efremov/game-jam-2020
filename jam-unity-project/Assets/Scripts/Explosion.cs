using EZCameraShake;
using UnityEngine;

public class Explosion : MonoBehaviour
{
  public float Magnitude = 2f;
  public float Roughness = 10f;
  public float FadeOutTime = 1f;
  public float FxCooldownSecond = .2f;

  private float _lastBoomTime;
  
  [SerializeField] private GameObject _fx;
  [SerializeField] private float _boomPower;

  private Rigidbody2D _rigidbody;
  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.collider.CompareTag("Player"))
    {
      Boom(other);
    }
  }

  private void Boom(Collision2D collision)
  {
    if (Time.time - _lastBoomTime < FxCooldownSecond)
      return;
    
    _lastBoomTime = Time.time;
    
    Instantiate(_fx, transform.position, Quaternion.identity);
    _rigidbody.AddForce(collision.contacts[0].normal * _boomPower, ForceMode2D.Impulse);
    CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, 0, FadeOutTime);
  }
}
