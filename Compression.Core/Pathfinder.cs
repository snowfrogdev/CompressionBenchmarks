namespace Compression.Core;

public class Pathfinder
{
  private string[] _map;
  private int?[,] _next;
  private int[,] _distances;

  // Use the Floyd-Warshall with with path reconstruction algorithm to find the shortest path between all pairs of nodes.
  // The map parameter is a string representation of a tile map. The string is split into rows with newlines as the delimiter.
  // Each tile is represented by either a space character, for a traversable tile, or a # character for a wall segment.
  public Pathfinder(string map)
  {
    _map = map.Split(Environment.NewLine);
    int height = _map.Length;
    int width = _map[0].Length;
    int numberOfTiles = height * width;

    _distances = new int[numberOfTiles, numberOfTiles];
    _next = new int?[numberOfTiles, numberOfTiles];

    for (int u = 0; u < numberOfTiles; u++)
    {
      for (int v = 0; v < numberOfTiles; v++)
      {
        if (u == v)
        {
          _distances[v, v] = 0;
          _next[v, v] = v;
          continue;
        }

        if (HasEdge(u, v))
        {
          _distances[u, v] = 1;
          _next[u, v] = v;
          continue;
        }

        _distances[u, v] = int.MaxValue;
        _next[u, v] = null;
      }
    }

    for (int k = 0; k < numberOfTiles; k++)
    {
      for (int i = 0; i < numberOfTiles; i++)
      {
        for (int j = 0; j < numberOfTiles; j++)
        {
          if (_distances[i, j] > _distances[i, k] + _distances[k, j])
          {
            _distances[i, j] = _distances[i, k] + _distances[k, j];
            _next[i, j] = _next[i, k];
          }
        }
      }
    }
  }

  private bool HasEdge(int u, int v)
  {
    (int x, int y) uCoordinates = GetCoordinates(u);
    (int x, int y) vCoordinates = GetCoordinates(v);

    if (Math.Abs(uCoordinates.x - vCoordinates.x) + Math.Abs(uCoordinates.y - vCoordinates.y) != 1)
    {
      return false;
    }

    if (_map[uCoordinates.y][uCoordinates.x] == '#' || _map[vCoordinates.y][vCoordinates.x] == '#')
    {
      return false;
    }

    return true;
  }

  private (int x, int y) GetCoordinates(int v)
  {
    int x = v % _map[0].Length;
    int y = v / _map[0].Length;
    return (x, y);
  }

  private int GetIndex((int x, int y) coordinates)
  {
    int x = coordinates.x;
    int y = coordinates.y;
    return y * _map[0].Length + x;
  }

  public IEnumerable<(int x, int y)> GetPath((int x, int y) start, (int x, int y) end)
  {
    int u = GetIndex(start);
    int v = GetIndex(end);

    if (_next[u, v] == null) return Enumerable.Empty<(int x, int y)>();

    List<(int x, int y)> path = new() { start };
    while (u != v)
    {
      u = (int)_next[u, v]!;
      path.Add(GetCoordinates(u));
    }
    return path;
  }
}