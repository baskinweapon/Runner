using UnityEngine;

public abstract class BaseBoost : MonoBehaviour {
    protected abstract void Do(Player player);

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Do(other.GetComponent<Player>());
            Destroy(gameObject);
        }
    }    
}
