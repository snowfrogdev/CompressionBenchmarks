using System;

using BenchmarkDotNet.Running;

using Compression.Benchmarks;

var cBenchmarks = new CompressBenchmarks();

cBenchmarks.Setup();

var input = cBenchmarks.input;
var inputSize = input.Length / 1024;

cBenchmarks.Compression = "Optimal";
var brotli = cBenchmarks.CompressBrotli();
var brotliSize = brotli.Length / 1024;
var brotliRatio = (double)brotliSize / inputSize;

cBenchmarks.Compression = "SmallestSize";
var brotliSmall = cBenchmarks.CompressBrotli();
var brotliSmallSize = brotliSmall.Length / 1024;
var brotliSmallRatio = (double)brotliSmallSize / inputSize;

cBenchmarks.Compression = "Optimal";
var gzip = cBenchmarks.CompressGZip();
var gzipSize = gzip.Length / 1024;
var gzipRatio = (double)gzipSize / inputSize;

cBenchmarks.Compression = "SmallestSize";
var gzipSmall = cBenchmarks.CompressGZip();
var gzipSmallSize = gzipSmall.Length / 1024;
var gzipSmallRatio = (double)gzipSmallSize / inputSize;

cBenchmarks.Compression = "Optimal";
var deflate = cBenchmarks.CompressDeflate();
var deflateSize = deflate.Length / 1024;
var deflateRatio = (double)deflateSize / inputSize;

cBenchmarks.Compression = "SmallestSize";
var deflateSmall = cBenchmarks.CompressDeflate();
var deflateSmallSize = deflateSmall.Length / 1024;
var deflateSmallRatio = (double)deflateSmallSize / inputSize;

cBenchmarks.Compression = "Optimal";
var zlib = cBenchmarks.CompressZLib();
var zlibSize = zlib.Length / 1024;
var zlibRatio = (double)zlibSize / inputSize;

cBenchmarks.Compression = "SmallestSize";
var zlibSmall = cBenchmarks.CompressZLib();
var zlibSmallSize = zlibSmall.Length / 1024;
var zlibSmallRatio = (double)zlibSmallSize / inputSize;

var dBenchmarks = new DecompressBenchmarks();
dBenchmarks.Setup();

cBenchmarks.Compression = "Optimal";
var brotliDecompressed = dBenchmarks.DecompressBrotli();
var brotliDecompressedSize = brotliDecompressed.Length / 1024;
var brotliDecompressedSameSize = brotliDecompressedSize == inputSize;

cBenchmarks.Compression = "SmallestSize";
var brotliSmallDecompressed = dBenchmarks.DecompressBrotli();
var brotliSmallDecompressedSize = brotliSmallDecompressed.Length / 1024;
var brotliSmallDecompressedSameSize = brotliSmallDecompressedSize == inputSize;

cBenchmarks.Compression = "Optimal";
var gzipDecompressed = dBenchmarks.DecompressGZip();
var gzipDecompressedSize = gzipDecompressed.Length / 1024;
var gzipDecompressedSameSize = gzipDecompressedSize == inputSize;

cBenchmarks.Compression = "SmallestSize";
var gzipSmallDecompressed = dBenchmarks.DecompressGZip();
var gzipSmallDecompressedSize = gzipSmallDecompressed.Length / 1024;
var gzipSmallDecompressedSameSize = gzipSmallDecompressedSize == inputSize;

cBenchmarks.Compression = "Optimal";
var deflateDecompressed = dBenchmarks.DecompressDeflate();
var deflateDecompressedSize = deflateDecompressed.Length / 1024;
var deflateDecompressedSameSize = deflateDecompressedSize == inputSize;

cBenchmarks.Compression = "SmallestSize";
var deflateSmallDecompressed = dBenchmarks.DecompressDeflate();
var deflateSmallDecompressedSize = deflateSmallDecompressed.Length / 1024;
var deflateSmallDecompressedSameSize = deflateSmallDecompressedSize == inputSize;

cBenchmarks.Compression = "Optimal";
var zlibDecompressed = dBenchmarks.DecompressZLib();
var zlibDecompressedSize = zlibDecompressed.Length / 1024;
var zlibDecompressedSameSize = zlibDecompressedSize == inputSize;

cBenchmarks.Compression = "SmallestSize";
var zlibSmallDecompressed = dBenchmarks.DecompressZLib();
var zlibSmallDecompressedSize = zlibSmallDecompressed.Length / 1024;
var zlibSmallDecompressedSameSize = zlibSmallDecompressedSize == inputSize;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

Console.WriteLine($"Input size: {inputSize} kb");
Console.WriteLine($"Brotli size: {brotliSize} kb ({brotliRatio:P})");
Console.WriteLine($"Brotli small size: {brotliSmallSize} kb ({brotliSmallRatio:P})");
Console.WriteLine($"GZip size: {gzipSize} kb ({gzipRatio:P})");
Console.WriteLine($"GZip small size: {gzipSmallSize} kb ({gzipSmallRatio:P})");
Console.WriteLine($"Deflate size: {deflateSize} kb ({deflateRatio:P})");
Console.WriteLine($"Deflate small size: {deflateSmallSize} kb ({deflateSmallRatio:P})");
Console.WriteLine($"ZLib size: {zlibSize} kb ({zlibRatio:P})");
Console.WriteLine($"ZLib small size: {zlibSmallSize} kb ({zlibSmallRatio:P})");

Console.WriteLine($"Brotli decompressed same size: {brotliDecompressedSameSize}");
Console.WriteLine($"Brotli small decompressed same size: {brotliSmallDecompressedSameSize}");
Console.WriteLine($"GZip decompressed same size: {gzipDecompressedSameSize}");
Console.WriteLine($"GZip small decompressed same size: {gzipSmallDecompressedSameSize}");
Console.WriteLine($"Deflate decompressed same size: {deflateDecompressedSameSize}");
Console.WriteLine($"Deflate small decompressed same size: {deflateSmallDecompressedSameSize}");
Console.WriteLine($"ZLib decompressed same size: {zlibDecompressedSameSize}");
Console.WriteLine($"ZLib small decompressed same size: {zlibSmallDecompressedSameSize}");

