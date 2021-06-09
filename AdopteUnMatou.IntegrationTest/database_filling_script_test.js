function Filling_User(){
    var u = db.users;

    u.drop();

    u.insert({ _id: ObjectId("c02da7e40a2ec30b5e60dd89"), ProfilePictureId: null, FirstName: 'TestFirstName1', LastName: 'TestLastName1', Email: 'test1@gmail.com', Phone: '0606060606', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0, "+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="), Role: 'Admin' })
    u.insert({ _id: ObjectId("da021de5c0eed1ff1668c102"), ProfilePictureId: null, FirstName: 'TestFirstName2', LastName: 'TestLastName2', Email: 'test2@gmail.com', Phone: '0606060606', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0,"+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="),Role:'Adoptant' })

}

Filling_User();
