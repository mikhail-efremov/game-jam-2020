using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameFlow
{
  public class MenuSceneController : MonoBehaviour
  {
    [SerializeField] private AudioSource _music;
    [SerializeField] private Image _fadeOutUIImage;
    [SerializeField] private float _fadeSpeed = 0.8f;
    
    [SerializeField] private GameObject _controlsOverlay;
    [SerializeField] private GameObject _spaceClick;

    [SerializeField] private Text _startInTimer;

    private void Start()
    {
      _controlsOverlay.SetActive(false);
      _music.Play();
    }

    private void Update()
    {
      var spacePressed = Input.GetKeyDown(KeyCode.Space);
      if (!spacePressed)
        return;

      StartCoroutine(ShowControls());
    }

    private IEnumerator ShowControls()
    {
      _controlsOverlay.SetActive(true);

      StartCoroutine(ShowTimer());
      yield return new WaitForSeconds(3);
      
      //START IN 3.00 SECONDS
      StartCoroutine(FadeAndLoadScene(FadeDirection.In));
    }

    private IEnumerator ShowTimer()
    {
      _startInTimer.gameObject.SetActive(true);
      _spaceClick.gameObject.SetActive(false);

      var startTime = Time.time;
      while (Time.time < startTime + 3)
      {
        yield return null;
        var time = (3f - (Time.time - startTime)).ToString("N2").Replace(",", ".");
        _startInTimer.text = $"START IN {time} SECONDS"; 
      }
      _controlsOverlay.gameObject.SetActive(false);
      _startInTimer.gameObject.SetActive(false);
    }

    public enum FadeDirection
    {
      In, //Alpha = 1
      Out // Alpha = 0
    }
         
    private IEnumerator Fade(FadeDirection fadeDirection) 
    {
      float alpha = (fadeDirection == FadeDirection.Out)? 1 : 0;
      float fadeEndValue = (fadeDirection == FadeDirection.Out)? 0 : 1;
      if (fadeDirection == FadeDirection.Out) {
        while (alpha >= fadeEndValue)
        {
          SetColorImage (ref alpha, fadeDirection);
          yield return null;
        }
        _fadeOutUIImage.enabled = false; 
      } else {
        _fadeOutUIImage.enabled = true; 
        while (alpha <= fadeEndValue)
        {
          SetColorImage (ref alpha, fadeDirection);
          yield return null;
        }
      }
    }
    
    public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection) 
    {
      yield return Fade(fadeDirection);
      SceneManager.LoadScene(1);
    }
 
    private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    {
      _fadeOutUIImage.color = new Color (_fadeOutUIImage.color.r,_fadeOutUIImage.color.g, _fadeOutUIImage.color.b, alpha);
      alpha += Time.deltaTime * (1.0f / _fadeSpeed) * ((fadeDirection == FadeDirection.Out)? -1 : 1) ;
    }
  }
}
