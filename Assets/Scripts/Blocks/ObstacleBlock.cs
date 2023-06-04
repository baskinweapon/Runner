public class ObstacleBlock : BaseBlock {
    protected override void Do(Player player) {
        player.Lives -= 1;
    }
}
