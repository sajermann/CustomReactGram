using Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Data
{
  public class Context : IDisposable
  {
    private IMongoDatabase _database;
    private IMongoClient _client;

    public Context(IOptions<Settings> settings)
    {
      _client = new MongoClient(settings.Value.ConnectionString);
      if (_client != null)
        _database = _client.GetDatabase(settings.Value.Database);
    }

    public IMongoCollection<User> Users
    {
      get
      {
        return _database.GetCollection<User>("Users");
      }
    }

    public void Dispose()
    {
      _client = null;
      _database = null;
    }
  }

  public class Settings
  {
    public string ConnectionString;
    public string Database;
  }
}