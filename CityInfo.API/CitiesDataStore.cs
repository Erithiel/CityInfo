using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
       

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                    {
                        Id =1,
                        Name = "New York",
                        Description = "rame rame ",
                        PointOfInterests = new List<PointOfInterestDto>()
                        {
                            new PointOfInterestDto()
                            {
                                Id = 1,
                                Name = "asdf",
                                Description = "dsfasfsdfsfs"
                            },
                            new PointOfInterestDto()
                            {
                                Id = 2,
                                Name = "afdfsdf",
                                Description = "dffgdsfasfsdfsfs"
                            }
                        }
                    },
                new CityDto()
                {
                    Id = 2,
                    Name = "Tbilisi",
                    Description = "tbilisoo",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "asdf",
                            Description = "dsfasfsdfsfs"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "afdfsdf",
                            Description = "dffgdsfasfsdfsfs"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Kutaisi",
                    Description = "city of Georgia",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "asdf",
                            Description = "dsfasfsdfsfs"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "afdfsdf",
                            Description = "dffgdsfasfsdfsfs"
                        }
                    }
                }

            };
        }
    }
}
