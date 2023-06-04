
public class FinishBlock : BaseBlock {
    protected override void Do(Player player) {
        GameManager.instance.FinishGame();
    }
}
