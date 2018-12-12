using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    #region PUBLIC_MEMBERS
    public float loadingDelay = 5.0F;
    #endregion //PUBLIC_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        StartCoroutine(LoadNextSceneAfter(loadingDelay));
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    private IEnumerator LoadNextSceneAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene("3-Main");
    }
    #endregion //PRIVATE_METHODS
}
