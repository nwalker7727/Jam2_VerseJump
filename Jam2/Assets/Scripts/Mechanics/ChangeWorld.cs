using UnityEngine;

public class ChangeWorld : MonoBehaviour
{
    public GameObject objectToCollideWith;
    public GameObject prefabToMakeInvisible;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objectToCollideWith)
        {
            // Set the prefab to be invisible
            prefabToMakeInvisible.SetActive(false);
            Debug.Log("works");
        }
    }
}
