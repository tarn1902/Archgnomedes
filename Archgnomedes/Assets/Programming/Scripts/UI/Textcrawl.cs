using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Designers making scripts in lowercase
/// It hurts the soul - PJ
/// </summary>
public class Textcrawl : MonoBehaviour
{
    // Start is called before the first frame update
    public float scrollspeed = 20;
    public float waitTime = 10;
    public bool activate = false;
    private float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            Vector3 pos = transform.position;
            Vector3 localvectorup = transform.TransformDirection(0, 1, 0);

            pos += localvectorup * scrollspeed * Time.deltaTime;
            transform.position = pos;

            if (currentTime >= waitTime)
            {
                SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
            }

            currentTime += Time.deltaTime;
        }
    }

}