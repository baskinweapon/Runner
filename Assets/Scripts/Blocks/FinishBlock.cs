
public class FinishBlock : BaseBlock {
    protected override void Triggered(Player player) {
        GameManager.instance.FinishGame();
    }
}
