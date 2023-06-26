using System.Collections;
using UnityEngine;

public class LawBlock : BaseBlock {
    public Transform law;
    public float speedSaw = 5f;
    
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
           law.gameObject.SetActive(true);
           StartCoroutine(MoveSaw());
        }    
    }

    // saw up when player collision
    IEnumerator MoveSaw() {
        while (law.transform.localPosition.y < 0f) {
            law.transform.localPosition = Vector3.Lerp(law.transform.localPosition, law.transform.localPosition += Vector3.up, Time.deltaTime * speedSaw);
            yield return null;
        }

        law.transform.localPosition = Vector3.zero;
    }

    protected override void Triggered(Player player) {
        player.Lives--;
    }
}
