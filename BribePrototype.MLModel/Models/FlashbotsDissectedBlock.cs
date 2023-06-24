namespace BribePrototype.MLModel.Models;

public class FlashbotsDissectedBlock
{
    public int BlockNumber { get; internal set; }
    
    /// <summary>
    /// nonFlashbotTxCount / totalTxCount
    /// </summary>
    public float NonFlashbotTxRatio { get; internal set; }

    /// <summary>
    /// Average bribe price paid by flashbots users in block
    /// This factors in gas used and should be treated similar to AvgGasPrice
    /// </summary>
    public decimal AvgBribePrice { get; internal set; }
}
