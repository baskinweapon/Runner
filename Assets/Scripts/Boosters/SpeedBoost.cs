
using DefaultNamespace;

public class SpeedBoost : BaseBoost {
    protected override void Do(Player player) {
        player.Fast(GameManager.instance.gameSettings.boostSettings.speedUpTime);
    }
}
