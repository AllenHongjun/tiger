using System;
using System.IO;

namespace Volo.Abp.Content;

public interface IFormFile : IDisposable
{
    string FileName { get; }

    string ContentType { get; }

    long? ContentLength { get; }

    Stream GetStream();
}