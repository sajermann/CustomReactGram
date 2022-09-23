using Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Data
{

  public interface IUserRepository
  {
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(string id);
    Task<User> GetByEmail(string email);
    Task<bool> Update(User user);
    Task<bool> Delete(string id);
    Task Create(User user);
  }

  public class UserRepository : IUserRepository
  {

    private readonly Context _context = null;
    public UserRepository(IOptions<Settings> settings)
    {
      _context = new Context(settings);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
      try
      {
        return await _context.Users.Find(_ => true).ToListAsync();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<User> GetById(string id)
    {
      try
      {
        return await _context.Users
                        .Find(doc => doc.Id == id)
                        .FirstOrDefaultAsync();
      }
      catch
      {
        return null;
      }
    }
    
    public async Task<User> GetByEmail(string email)
    {
      try
      {
        return await _context.Users
                        .Find(doc => doc.Email == email)
                        .FirstOrDefaultAsync();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<bool> Update(User user)
    {
      try
      {
        Expression<Func<User, bool>> filter = x => x.Id.Equals(user.Id);

        User cli = _context.Users.Find(filter).FirstOrDefault();

        if (cli != null)
        {
          cli = user;
          ReplaceOneResult result = _context.Users.ReplaceOne(filter, cli);

          return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        else return false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<bool> Delete(string id)
    {
      try
      {
        DeleteResult actionResult = await _context.Users.DeleteManyAsync(n => n.Id.Equals(id));

        return actionResult.IsAcknowledged
            && actionResult.DeletedCount > 0;
      }
      catch (Exception ex)
      {
        // log or manage the exception
        throw ex;
      }
    }

    public async Task Create(User user)
    {
      await _context.Users.InsertOneAsync(user);
    }
  }
}