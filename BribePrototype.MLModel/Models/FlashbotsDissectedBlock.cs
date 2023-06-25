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

    /// <summary>
    /// Define how many bundles in this block contain a bribe
    /// bundlesWithBribeCount / totalBundleCount
    /// </summary>
    public float BundlesWithBribeRatio { get; internal set; }

    /// <summary>
    /// This is the bribe equivalent of AverageGasPrice
    /// </summary>
    public decimal AverageBribePrice { get; internal set; }

    /// <summary>
    /// Average gas price for the block
    /// </summary>
    public decimal AverageGasPrice { get; internal set; }
}
