using DefaultNamespace.Blocks;
using UnityEngine;

public abstract class BaseBlock : MonoBehaviour {
      public BlockType type;
      
      private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                  var player = other.GetComponent<Player>();
                  if (player) {
                        Do(player);
                  }
            }    
      }

      protected abstract void Do(Player player);
}
