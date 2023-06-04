using UnityEngine;

public class LawBlock : BaseBlock {
    public Transform law;
    
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
           law.gameObject.SetActive(true); 
        }    
    }

    protected override void Do(Player player) {
        player.Lives--;
    }
}
