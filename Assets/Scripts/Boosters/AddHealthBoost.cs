public class AddHealthBoost : BaseBoost {
    protected override void Do(Player player) {
        player.Lives += GameManager.instance.gameSettings.boostSettings.healthCount;
    }    
}
