using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    [SerializeField] private TextManager textManager;
    [SerializeField] private bool _introSceneFinished;

    private void Update()
    {
        _introSceneFinished = textManager.introSceneFinished;
        if (_introSceneFinished == true) { LoadMainScene(); }
    }
    private void LoadMainScene()
    {
        // SceneManager.LoadScene("SceneName"); // By scene name
        Debug.Log("Load Main Scene");
        SceneManager.LoadScene("MainScene"); 
    }
}
