namespace BribePrototype.Experiments.Constants;

internal static class PathConstants
{    
    internal static class Raw
    {
        public static string Dir { get => SetupPath(true, "RawData"); }
        public static string FlashbotsBlocks { get => SetupPath(false, Dir, "flashbots-blocks.json"); }
        public static string EtherscanCongestion { get => SetupPath(false, Dir, "etherscan-congestion.csv"); }
    }

    private static string SetupPath(bool isDir, params string[] relativePath) 
    {
        string fullPath = OperationUtils.IO.GetWorkingPath(relativePath);
        OperationUtils.IO.EnsureValidPath(fullPath, isDir);
        return fullPath;
    }
}
