
public class HoleBlock : BaseBlock {
    protected override void Triggered(Player player) {
        player.Lives--;
    }
}
