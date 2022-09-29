using Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Data
{

  public interface IPhotoRepository
  {
    Task<IList<Photo>> GetAll();
    Task<IList<Photo>> GetAllByUserId(string userId);
    Task<Photo> GetById(string id);
    Task<IList<Photo>> GetByFilter(Expression<Func<Photo, bool>> filter);
    Task<Photo> Create(Photo photo);
    Task<Photo> Update(Photo photo);
    Task<bool> Delete(string id);
  }
  
  public class PhotoRepository : IPhotoRepository
  {

    private readonly Context _context = null;
    public PhotoRepository(IOptions<Settings> settings)
    {
      _context = new Context(settings);
    }

    public async Task<IList<Photo>> GetAll()
    {
      try
      {
        return await _context.Photos.Find(_ => true).ToListAsync();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<IList<Photo>> GetAllByUserId(string userId)
    {
      try
      {
        return await _context.Photos.Find(x => x.UserId == userId).ToListAsync();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<Photo> GetById(string id)
    {
      try
      {
        return await _context.Photos
                        .Find(doc => doc.Id == id)
                        .FirstOrDefaultAsync();
      }
      catch
      {
        return null;
      }
    }

    public async Task<Photo> Create(Photo photo)
    {
      await _context.Photos.InsertOneAsync(photo);
      return photo;
    }

    public async Task<Photo> Update(Photo photo)
    {
      try
      {
        Expression<Func<Photo, bool>> filter = x => x.Id.Equals(photo.Id);

        Photo cli = await _context.Photos.Find(filter).FirstOrDefaultAsync();

        if (cli != null)
        {
          cli = photo;
          ReplaceOneResult result = await _context.Photos.ReplaceOneAsync(filter, cli);
          if(result.IsAcknowledged && result.ModifiedCount > 0)
          {
            return photo;
          }
          return null;
        }
        else return null;
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
        DeleteResult actionResult = await _context.Photos.DeleteManyAsync(n => n.Id.Equals(id));

        return actionResult.IsAcknowledged
            && actionResult.DeletedCount > 0;
      }
      catch (Exception ex)
      {
        // log or manage the exception
        throw ex;
      }
    }

    public async Task<IList<Photo>> GetByFilter(Expression<Func<Photo, bool>> filter)
    {
      return await _context.Photos.Find(filter).ToListAsync();
    }
  }
}