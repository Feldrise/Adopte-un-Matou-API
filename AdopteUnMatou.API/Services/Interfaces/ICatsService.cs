using AdopteUnMatou.API.Models.Cats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Services.Interfaces
{
    public interface ICatsService
    {
        Task<Cat> GetCat(string id);
        Task<IList<Cat>> GetCats(string adoptionStatus);

        Task<byte[]> GetCatImage(string id);

        Task<string> CreateCat(byte[] catImage, Cat toCreate);
        Task UpdateCat(byte[] newImage, Cat toUpdate);
    }
}
