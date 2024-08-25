using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToGameScene : MonoBehaviour
{
    private float timer;

    private void Update()
    {
        timer++;
        //Debug.Log(timer);
        if (timer > 3500f)
        {
            SceneManager.LoadScene(2);
        }
    }
}
