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
        foreach (var bundleKvp in bundles)
        {
            // Only include flashbots uses
            if (bundleKvp.Value.First().BundleType == "mempool")
                continue;
            
            // Calculate bundle bribe price
            var bundleBribe = bundleKvp.Value.Sum(x => decimal.Parse(x.EthSentToFeeRecipient));
            var bundleGasUsed = bundleKvp.Value.Sum(x => x.GasUsed);
            bribePrices.Add(bundleBribe / bundleGasUsed);

            // todo calc actual gas price
        }


        // Extract average gas price of all transactions in block
        return new FlashbotsDissectedBlock
        {
            BlockNumber = block.BlockNumber,
            NonFlashbotTxRatio = mempoolTxPercentage,
            AvgBribePrice = bribePrices.Average()
        };

    }
}
