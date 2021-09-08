using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startGameBtn;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName: "SampleScene");
    }

}
