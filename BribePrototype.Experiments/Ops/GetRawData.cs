namespace BribePrototype.Experiments.Ops;

[OperationDescription("Get raw data", 1)]
internal class GetRawData : IOperation
{
    public async Task RunAsync()
    {
        List<Task> tasks = new List<Task>()
        {
            DownloadFlashbotsBlocksAsync(),

        };

        Console.WriteLine("Downloading raw data...");
        await Task.WhenAll(tasks);
        Console.WriteLine("Done.");
    }

    private static async Task DownloadFlashbotsBlocksAsync()
    {
        string path = PathConstants.Raw.FlashbotsBlocks;
        if (File.Exists(path))
        {
            Console.WriteLine("Flashbots blocks already downloaded. Skipping");
            return;
        }
        string url = "https://blocks.flashbots.net/v1/all_blocks";
        await Downloader.DownloadAsync(url, path);
    }
}
