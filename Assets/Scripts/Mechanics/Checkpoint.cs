using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActivated = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActivated) return;
        if (!other.CompareTag("Player")) return;

        isActivated = true;
        GameManager.Instance.SetCheckpoint(transform.position);
        GetComponent<Animator>().SetTrigger("Active");
        Debug.Log("Checkpoint activado: " + gameObject.name);
    }
}