
public class HalfHoleBlock : BaseBlock {
    
    protected override void Triggered(Player player) {
        player.Lives--;
    }
}
