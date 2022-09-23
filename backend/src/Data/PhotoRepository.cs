using Domain;
using Microsoft.Extensions.Options;

namespace Data
{

  public interface IPhotoRepository
  {
    Task<Photo> Create(Photo photo);
  }

  public class PhotoRepository : IPhotoRepository
  {

    private readonly Context _context = null;
    public PhotoRepository(IOptions<Settings> settings)
    {
      _context = new Context(settings);
    }

    public async Task<Photo> Create(Photo photo)
    {
      await _context.Photos.InsertOneAsync(photo);
      return photo;
    }
  }
}