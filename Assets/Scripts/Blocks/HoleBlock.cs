
public class HoleBlock : BaseBlock {
    protected override void Do(Player player) {
        player.Lives--;
    }
}
