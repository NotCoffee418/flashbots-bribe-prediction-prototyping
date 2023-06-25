using static BribePrototype.MLModel.Models.FlashbotsJsonModel;

namespace BribePrototype.Experiments.Helpers;

// Modified to handle flashbots bulk data specifically, ignoring the root object and only processing the blocks
public class FlashbotsRawBlockProcessor
{
    public event EventHandler<ProgressEventArgs> ProgressReported;

    /// <summary>
    /// Converts flashbots json data into T, using Block data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="inputFilePath"></param>
    /// <param name="outputFilePath"></param>
    /// <param name="convertFunc"></param>
    /// <returns></returns>
    public async Task ProcessBlocksAsync<T>(string inputFilePath, string outputFilePath, Func<Block, T> convertFunc)
    {
        // Initialize input and output FileStreams
        using FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
        using FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
        using StreamWriter streamWriter = new StreamWriter(outputFileStream);

        // Write the opening bracket for the JSON array
        await streamWriter.WriteAsync("[");

        // Initialize Utf8JsonReader
        byte[] buffer = new byte[4096];
        bool isFirstTx = true;
        long totalBytes = inputFileStream.Length;
        long bytesRead = 0;

        int bytesReadThisIteration;
        while ((bytesReadThisIteration = await inputFileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            bytesRead += bytesReadThisIteration;
            ProcessBuffer(buffer, streamWriter, convertFunc, ref isFirstTx);
            OnProgressReported(new ProgressEventArgs { BytesProcessed = bytesRead, TotalBytes = totalBytes });
        }

        // Write the closing bracket for the JSON array
        await streamWriter.WriteAsync("]");
    }

    private void ProcessBuffer<T>(byte[] buffer, StreamWriter streamWriter, Func<Block, T> convertFunc, ref bool isFirstTx)
    {
        JsonReaderOptions options = new JsonReaderOptions { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip };
        Utf8JsonReader reader = new Utf8JsonReader(buffer, options);

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.StartObject && reader.CurrentDepth == 2)
            {
                // Deserialize the current block
                Block block = JsonSerializer.Deserialize<Block>(ref reader);

                // Apply the conversion function
                T convertedBlock = convertFunc(block);
                string serializedBlock = JsonSerializer.Serialize(convertedBlock);

                // Write the converted transaction to the output file
                if (!isFirstTx) streamWriter.Write(",");
                else isFirstTx = false;
                streamWriter.Write(serializedBlock);
            }
        }
    }

    private void OnProgressReported(ProgressEventArgs e)
    {
        ProgressReported?.Invoke(null, e);
    }

    public class ProgressEventArgs : EventArgs
    {
        public long BytesProcessed { get; set; }
        public long TotalBytes { get; set; }
    }
}

