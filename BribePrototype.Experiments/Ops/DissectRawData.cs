namespace BribePrototype.Experiments.Ops;

[OperationDescription("Dissect raw data", 2)]
internal class DissectRawData : IOperation
{
    public Task RunAsync()
    {
        throw new NotImplementedException();
    }

    public static async Task ProcessFlashbotsBlocksAsync()
    {
        FlashbotsRawBlockProcessor processor = new FlashbotsRawBlockProcessor();

        // Progress bar
        int totalTicks = 100; // Just an example value, you can calculate the actual total ticks based on your data
        using var progressBar = new ProgressBar(totalTicks, "Processing blocks", new ProgressBarOptions
        {
            ProgressCharacter = '─',
            ProgressBarOnBottom = true
        });
        processor.ProgressReported += (sender, e) =>
        {
            // Calculate the progress percentage
            int progressPercentage = (int)(((double)e.BytesProcessed / e.TotalBytes) * totalTicks);
            progressBar.Tick(progressPercentage);
        };

        // Process the blocks
        await processor.ProcessBlocksAsync(
            PathConstants.Raw.FlashbotsBlocks, 
            PathConstants.Dissected.FlashbotsBlocks, 
            FlashbotsBlockInterpreter.ConvertFlashbotsBlockTDissectedBundle);
    }
}
