

namespace BribePrototype.MLModel.Models;

public class FlashbotsJsonModel
{
    public class Transaction
    {
        [JsonPropertyName("transaction_hash")]
        public string TransactionHash { get; set; }

        [JsonPropertyName("tx_index")]
        public int TxIndex { get; set; }

        [JsonPropertyName("bundle_type")]
        public string BundleType { get; set; }

        [JsonPropertyName("bundle_index")]
        public int BundleIndex { get; set; }

        [JsonPropertyName("block_number")]
        public int BlockNumber { get; set; }

        [JsonPropertyName("eoa_address")]
        public string EoaAddress { get; set; }

        [JsonPropertyName("to_address")]
        public string ToAddress { get; set; }

        [JsonPropertyName("gas_used")]
        public long GasUsed { get; set; }

        [JsonPropertyName("gas_price")]
        public string GasPrice { get; set; }

        [JsonPropertyName("eth_sent_to_fee_recipient")]
        public string EthSentToFeeRecipient { get; set; }

        [JsonPropertyName("fee_recipient_eth_diff")]
        public string FeeRecipientEthDiff { get; set; }
    }

    public class Block
    {
        [JsonPropertyName("block_number")]
        public int BlockNumber { get; set; }

        [JsonPropertyName("fee_recipient")]
        public string FeeRecipient { get; set; }

        [JsonPropertyName("fee_recipient_eth_diff")]
        public string FeeRecipientEthDiff { get; set; }

        [JsonPropertyName("eth_sent_to_fee_recipient")]
        public string EthSentToFeeRecipient { get; set; }

        [JsonPropertyName("gas_used")]
        public int GasUsed { get; set; }

        [JsonPropertyName("gas_price")]
        public string GasPrice { get; set; }

        [JsonPropertyName("transactions")]
        public List<Transaction> Transactions { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("latest_block_number")]
        public int LatestBlockNumber { get; set; }

        [JsonPropertyName("blocks")]
        public List<Block> Blocks { get; set; }
    }
}
