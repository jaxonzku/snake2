using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public void Start1Player()
    {

        SceneManager.LoadScene(1);

    }

    public void Start2Player()
    {

        SceneManager.LoadScene(2);

    }
}
