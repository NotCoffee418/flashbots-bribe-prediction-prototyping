// Set working dir to RepoRoot/data assuming the experiment runs in the default build dir
OperationUtils.IO.SetWorkingDirBase(Path.Combine(
    Directory.GetParent(
        Directory.GetParent(
            Directory.GetParent(
                Directory.GetParent(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ).FullName).FullName).FullName).FullName, "data"));

// Handle operations
OperationManager.AutoRegisterOperations();
await OperationManager.StartListeningAsync();