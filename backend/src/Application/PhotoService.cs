﻿using Application.Dtos;
using Application.Helpers;
using Application.Helpers.Interfaces;
using Application.Interfaces;
using Data;
using Domain;

namespace Application
{
  public class PhotoService : IPhotoService
  {
    private readonly IUserRepository _userRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IToken _token;


    public PhotoService(IToken token, IUserRepository userRepository, IPhotoRepository photoRepository)
    {
      _token = token;
      _userRepository = userRepository;
      _photoRepository = photoRepository;
    }

    public async Task<PhotoDtoOut> CreatePhoto(PhotoDtoIn photoDtoIn, string jwt)
    {
      var loggedUser = _token.GetClaim(jwt, "id");

      var user = await _userRepository.GetById(loggedUser);
      if (user == null) throw new Exception("User not found!");

      var completeNameFile = await photoDtoIn.Image.SuperCopyAsync("Photos");
      var photo = new Photo();
      photo.Image = completeNameFile;
      photo.Title = photoDtoIn.Title;
      photo.UserId = user.Id;
      photo.UserName = user.Name;
      var result = await _photoRepository.Create(photo);
      var photoDto = result.ToPhotoDtoOut();
      return photoDto;
    }
  }
}
