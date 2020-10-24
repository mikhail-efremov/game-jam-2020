using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
   public void StartGame()
   {
      SceneManager.LoadScene("GameplayScene");
   }

   private void Update()
   {
      if (Input.GetKeyDown (KeyCode.Space))
         SceneManager.LoadScene("GameplayScene");
   }
}
