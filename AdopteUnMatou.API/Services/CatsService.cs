using AdopteUnMatou.API.DataProvider.Interfaces;
using AdopteUnMatou.API.Models.Cats;
using AdopteUnMatou.API.Services.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Services
{
    public class CatsService : ICatsService
    {
        private readonly IDPCats _dpCats;

        private readonly IFilesService _filesService;

        public CatsService(IDPCats dPCats, IFilesService filesService)
        {
            _dpCats = dPCats;

            _filesService = filesService;
        }

        public async Task<Cat> GetCat(string id)
        {
            return await _dpCats.GetCatById(id).FirstOrDefaultAsync();
        }

        public async Task<IList<Cat>> GetCats(string adoptionStatus)
        {
            IMongoQueryable<Cat> cats = _dpCats.Obtain();

            if (adoptionStatus != null)
            {
                cats = cats.Where(dbCat => dbCat.AdoptionStatus == adoptionStatus);
            }

            return await cats.ToListAsync();
        }

        public async Task<byte[]> GetCatImage(string id)
        {
            Cat cat = await _dpCats.GetCatById(id).FirstOrDefaultAsync();

            if (cat == null || cat.ImageId == null) return null;

            return (await _filesService.GetFileById(cat.ImageId)).Data;

        }

        public async Task<string> CreateCat(byte[] catImage, Cat toCreate)
        {
            if (catImage != null)
            {
                string imageId = await _filesService.UploadFile(toCreate.Name.ToLower().Trim(), catImage, false);

                toCreate.ImageId = imageId;
            }

            toCreate.AdoptionStatus = AdoptionStatus.Waiting;

            await _dpCats.GetCollection().InsertOneAsync(toCreate);

            return toCreate.Id;
        }

        public async Task UpdateCat(byte[] newImage, Cat toUpdate)
        {
            Cat oldCat = await _dpCats.GetCatById(toUpdate.Id).FirstOrDefaultAsync();

            if (oldCat == null)
            {
                throw new Exception("The cat doesn't exist");
            }

            if (newImage != null)
            {
                string imageId = await _filesService.UploadFile(toUpdate.Name.ToLower().Trim(), newImage, false);

                toUpdate.ImageId = imageId;
            }
            else
            {
                toUpdate.ImageId = oldCat.ImageId;
            }

            await _dpCats.GetCollection().ReplaceOneAsync(
                dbCat => dbCat.Id == toUpdate.Id,
                toUpdate
            );
        }
    }
}
