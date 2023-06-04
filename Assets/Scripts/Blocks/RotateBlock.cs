using UnityEngine;

public class RotateBlock : BaseBlock {
    public Vector3 direction;

    protected override void Do(Player player) {
        player.GetComponent<Movement>().Rotate(direction, transform.position);
    }
}
