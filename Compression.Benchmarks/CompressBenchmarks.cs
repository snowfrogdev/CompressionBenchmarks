using System.IO;
using System.IO.Compression;

using BenchmarkDotNet.Attributes;

namespace Compression.Benchmarks;

[MemoryDiagnoser]
public class CompressBenchmarks
{
  public byte[] input;

  [Params("Optimal", "SmallestSize")]
  public string Compression { get; set; } 

  [GlobalSetup]
  public void Setup()
  {
    input = File.ReadAllBytes("bible.txt");
  }

  [Benchmark]
  public byte[] CompressBrotli()
  {
    var compression = Compression == "Optimal" ? CompressionLevel.Optimal : CompressionLevel.SmallestSize;
    using var output = new MemoryStream();
    using var compressor = new BrotliStream(output, compression, true);
    compressor.Write(input, 0, input.Length);
    compressor.Close();
    return output.ToArray();
  }

  [Benchmark]
  public byte[] CompressGZip()
  {
    var compression = Compression == "Optimal" ? CompressionLevel.Optimal : CompressionLevel.SmallestSize;
    using var output = new MemoryStream();
    using var compressor = new GZipStream(output, compression, true);
    compressor.Write(input, 0, input.Length);
    compressor.Close();
    return output.ToArray();
  }

  [Benchmark]
  public byte[] CompressDeflate()
  {
    var compression = Compression == "Optimal" ? CompressionLevel.Optimal : CompressionLevel.SmallestSize;
    using var output = new MemoryStream();
    using var compressor = new DeflateStream(output, compression, true);
    compressor.Write(input, 0, input.Length);
    compressor.Close();
    return output.ToArray();
  }

  [Benchmark]
  public byte[] CompressZLib()
  {
    var compression = Compression == "Optimal" ? CompressionLevel.Optimal : CompressionLevel.SmallestSize;
    using var output = new MemoryStream();
    using var compressor = new ZLibStream(output, compression, true);
    compressor.Write(input, 0, input.Length);
    compressor.Close();
    return output.ToArray();
  }
}

