using System.Numerics;

namespace BribePrototype.MLModel.DataPrep.Preprocessing;

public static class FlashbotsBlockInterpreter
{
    public static FlashbotsDissectedBlock ConvertFlashbotsBlockTDissectedBundle(FlashbotsJsonModel.Block block)
    {
        // Seperate bundles in block
        // BundleIndex->Bundle
        Dictionary<int, List<FlashbotsJsonModel.Transaction>> bundles = block.Transactions
            .GroupBy(x => x.BundleIndex)
            .ToDictionary(x => x.Key, x => x.ToList());

        // Extract ratio of mempool transactions in block
        int mempoolTransactionsCount = block.Transactions.Count(x => x.BundleType == "mempool");
        int totalTxCount = block.Transactions.Count;
        float mempoolTxPercentage = (float)mempoolTransactionsCount / (float)totalTxCount;

        // Extract average bundle bribe
        List<decimal> bribePrices = new();
        List<decimal> gasPrices = new();
        List<int> bundlesWithBribe = new();
        foreach (var bundleKvp in bundles)
        {
            // Calculate bundle bribe price
            var bundleBribe = bundleKvp.Value.Sum(x => long.Parse(x.EthSentToFeeRecipient));
            if (bundleBribe > 0)
            {
                var bundleBribeGasUsed = bundleKvp.Value.Where(x => x.EthSentToFeeRecipient != "0").Sum(x => x.GasUsed);
                bribePrices.Add(bundleBribe / bundleBribeGasUsed);
                bundlesWithBribe.Add(bundleKvp.Key);
            }
            
            // Calculate gas pric
            var gasPrice = bundleKvp.Value.Sum(x => long.Parse(x.GasPrice));
        }


        // Extract average gas price of all transactions in block
        return new FlashbotsDissectedBlock
        {
            BlockNumber = block.BlockNumber,
            NonFlashbotTxRatio = mempoolTxPercentage,
            AvgBribePrice = bribePrices.Average(),
            BundlesWithBribeRatio = (float)bundlesWithBribe.Count / (float)bundles.Count,
            AverageBribePrice = bribePrices.Average(),
            AverageGasPrice = gasPrices.Average(),
        };

    }
}
