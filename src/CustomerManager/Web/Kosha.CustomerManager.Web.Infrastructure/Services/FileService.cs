using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Store;
using Kosha.CustomerManager.Web.Infrastructure.Models.File;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.Extensions.Options;

namespace Kosha.CustomerManager.Web.Infrastructure.Services;

internal sealed class FileService(
    IPathService pathService,
    IOptions<FileInformation> options
) : IFileService
{
    private FileInformation Information => options.Value;

    public async Task<Result<string>> ReadContentAsync(string fileName, CancellationToken cancellation = default)
    {
        Result<string> result = Result.Failed<string>(Error.None);

        string path = CompletePath(fileName);

        if (string.IsNullOrEmpty(path))
            try
            {
                if (File.Exists(path))
                    result =
                        Result.Success(
                            await File.ReadAllTextAsync(
                                path,
                                cancellation
                            )
                        );
            }
            catch (Exception)
            {
                //
            }

        return result;
    }

    private string CompletePath(string fileName)
    {
        string[] files = fileName.Split(pathService.Separator);

        bool hasExtension = false;

        try
        {
            FileInfo fileInfo = new FileInfo(files[^1]);

            hasExtension = !string.IsNullOrEmpty(fileInfo.Extension);
        }
        catch (Exception)
        {
            //
        }

        if (!hasExtension) 
            files[^1] = string.Concat(files[^1], Information.DefaultFileExtension);

        return
            Path.Combine(
                pathService.DirectoryPath,
                string.Join(
                    pathService.Separator,
                    files
                )
            );
    }
}