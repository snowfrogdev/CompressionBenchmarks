using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

using BenchmarkDotNet.Attributes;

namespace Compression.Benchmarks;

[MemoryDiagnoser]
public class DecompressBenchmarks
{
  public Dictionary<string, byte[]> optimalInputs = new();
  public Dictionary<string, byte[]> smallestInputs = new();

  [Params("Optimal", "SmallestSize")]
  public string Compression { get; set; }

  [GlobalSetup]
  public void Setup()
  {
    var compressors = new CompressBenchmarks();
    compressors.Setup();
    compressors.Compression = "Optimal";
    optimalInputs.Add("Brotli", compressors.CompressBrotli());
    optimalInputs.Add("GZip", compressors.CompressGZip());
    optimalInputs.Add("Deflate", compressors.CompressDeflate());
    optimalInputs.Add("ZLib", compressors.CompressZLib());

    compressors.Compression = "SmallestSize";
    smallestInputs.Add("Brotli", compressors.CompressBrotli());
    smallestInputs.Add("GZip", compressors.CompressGZip());
    smallestInputs.Add("Deflate", compressors.CompressDeflate());
    smallestInputs.Add("ZLib", compressors.CompressZLib());
  }

  [Benchmark]
  public byte[] DecompressBrotli()
  {
    var compression = Compression == "Optimal" ? CompressionLevel.Optimal : CompressionLevel.SmallestSize;
    using var input = new MemoryStream(optimalInputs["Brotli"]);
    using var output = new MemoryStream();
    using var decompressor = new BrotliStream(input, CompressionMode.Decompress);
    decompressor.CopyTo(output);
    return output.ToArray();
  }

  [Benchmark]
  public byte[] DecompressGZip()
  {
    var compression = Compression == "Optimal" ? CompressionLevel.Optimal : CompressionLevel.SmallestSize;
    using var input = new MemoryStream(optimalInputs["GZip"]);
    using var output = new MemoryStream();
    using var decompressor = new GZipStream(input, CompressionMode.Decompress);
    decompressor.CopyTo(output);
    return output.ToArray();
  }

  [Benchmark]
  public byte[] DecompressDeflate()
  {
    var compression = Compression == "Optimal" ? CompressionLevel.Optimal : CompressionLevel.SmallestSize;
    using var input = new MemoryStream(optimalInputs["Deflate"]);
    using var output = new MemoryStream();
    using var decompressor = new DeflateStream(input, CompressionMode.Decompress);
    decompressor.CopyTo(output);
    return output.ToArray();
  }

  [Benchmark]
  public byte[] DecompressZLib()
  {
    var compression = Compression == "Optimal" ? CompressionLevel.Optimal : CompressionLevel.SmallestSize;
    using var input = new MemoryStream(optimalInputs["ZLib"]);
    using var output = new MemoryStream();
    using var decompressor = new ZLibStream(input, CompressionMode.Decompress);
    decompressor.CopyTo(output);
    return output.ToArray();
  }
}

