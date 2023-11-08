using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collide");
            SceneTransition sceneTransition = FindObjectOfType<SceneTransition>();
            if (sceneTransition != null)
            {
                sceneTransition.TransitionToScene(sceneName);
            }
        }
    }
}
