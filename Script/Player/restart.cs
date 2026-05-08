using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    public static bool resate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RESTART();
        }
    }
    void RESTART()
    {
        Time.timeScale = 1f;
        resate = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
