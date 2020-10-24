using System.Collections;
using EZCameraShake;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Explosion : MonoBehaviour
{
  public float Magnitude = 2f;
  public float Roughness = 10f;
  public float FadeOutTime = 1f;
  public float FxCooldownSecond = .2f;

  private float _lastBoomTime;

  [SerializeField] private GameObject _lowSmokeFx;
  [SerializeField] private GameObject _hightSmokeFx;
  
  [SerializeField] private GameObject _fx;
  [SerializeField] private float _boomPower;
  [SerializeField] private float _boomPowerBarrel;

  private int _hitsCount;
  public int HitsCount
  {
    get => _hitsCount;
    set
    {
      _hitsCount = value;
      UpdateFx();
    }
  }

  private IEnumerator _routine;

  private Rigidbody2D _rigidbody;
  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody2D>();

    _lowSmokeFx.SetActive(false);
    _hightSmokeFx.SetActive(false);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.collider.CompareTag("Player"))
    {
      StartCoroutine(BlockInput());
      Boom(other);
    }
    if (other.collider.CompareTag("Barrel"))
    {
      StartCoroutine(BlockInput());
      BoomBarrel(other);
    }
  }

  private void UpdateFx()
  {
    if (HitsCount == 0)
    {
      _lowSmokeFx.SetActive(false);
      _hightSmokeFx.SetActive(false);
    }

    if (HitsCount == 1)
      _lowSmokeFx.SetActive(true);

    if (HitsCount > 1)
    {
      _lowSmokeFx.SetActive(false);
      _hightSmokeFx.SetActive(true);
    }
  }

  private void Boom(Collision2D collision)
  {
    if (Time.time - _lastBoomTime < FxCooldownSecond)
      return;

    if (FindObjectOfType<GameController>().TimeLapsIsActive)
      HitsCount--;
    else
      HitsCount++;
    
    UpdateFx();
    
    _lastBoomTime = Time.time;
    
    Instantiate(_fx, transform.position, Quaternion.identity);
    if (!_rigidbody.isKinematic)
      _rigidbody.AddForce(collision.contacts[0].normal * _boomPower, ForceMode2D.Impulse);
    CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, 0, FadeOutTime);
  }
  
  private void BoomBarrel(Collision2D collision)
  {
    if (Time.time - _lastBoomTime < FxCooldownSecond)
      return;
    
    _lastBoomTime = Time.time;
    
    Instantiate(_fx, transform.position, Quaternion.identity);
    if (!_rigidbody.isKinematic)
      _rigidbody.AddForce(collision.contacts[0].normal * _boomPowerBarrel, ForceMode2D.Impulse);
    CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, 0, FadeOutTime);
  }

  private IEnumerator BlockInput()
  {
    var car = GetComponent<Car>();
    if (car == null)
      yield break;

    var time = 1.5f;

    car.IsPlayerControlled = false;
    car.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    if (_routine != null)
    {
      Time.timeScale = 1f;
      StopCoroutine(_routine);
      _routine = null;
    }

    Time.timeScale = .6f;
    _routine = TweekHromAbberations(time);
    StartCoroutine(_routine);
    
    yield return new WaitForSeconds(time/3);
    Time.timeScale = .7f;
    yield return new WaitForSeconds(time/3);
    Time.timeScale = .85f;
    yield return new WaitForSeconds(time/3);
    
    car.IsPlayerControlled = true;
    
    Time.timeScale = 1f;
  }

  private IEnumerator TweekHromAbberations(float time)
  {
    var p = FindObjectOfType<PostProcessVolume>();
    p.profile.TryGetSettings(out ChromaticAberration myChromaticAberration);
    myChromaticAberration.intensity.Override(1f);

    var startTime = Time.time;
    while (Time.time < startTime + time)
    { 
      myChromaticAberration.intensity.Override(1f - (Time.time - startTime) / time);
      yield return null;
    }
  }
}
