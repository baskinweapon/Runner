
public class ShieldBoost : BaseBoost {
    protected override void Do(Player player) {
        player.Shield(GameManager.instance.gameSettings.boostSettings.shieldTime);
    }
}
