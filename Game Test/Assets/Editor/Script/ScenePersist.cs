using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    static int sceneIndexofTheLastScene = 0;
    private int sceneIndexAtStart;


    private void Awake()
    {
        int actualSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndexofTheLastScene == actualSceneIndex)
        {
            int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
            if (numScenePersist > 1)
            {
                Destroy(gameObject);
                Debug.Log("ScenePersist has been destoyed due to Singleton");
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            StartCoroutine(Singleton());
        }

    }

    IEnumerator Singleton()
    {

        float yieldDuration;

        yield return new WaitForSecondsRealtime(Time.deltaTime);

        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersist > 1)
        {
            Destroy(gameObject);
            Debug.Log("ScenePersist has been destoyed due to Singleton");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        sceneIndexAtStart = SceneManager.GetActiveScene().buildIndex;
        sceneIndexofTheLastScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfStillInSameScene();

    }

    private void CheckIfStillInSameScene()
    {
        int actualSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (actualSceneIndex != sceneIndexAtStart)
        {
            Destroy(gameObject);
            Debug.Log("ScenePersist has been destoyed due to SceneChange");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
