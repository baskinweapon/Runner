using UnityEngine;

public class RotateBlock : BaseBlock {
    public Vector3 direction;

    protected override void Triggered(Player player) {
        player.GetComponent<Movement>().Rotate(direction, transform.position);
    }
}
