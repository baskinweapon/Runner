public class ObstacleBlock : BaseBlock {
    protected override void Triggered(Player player) {
        player.Lives -= 1;
    }
}
