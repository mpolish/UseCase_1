﻿namespace UseCase_1;

public interface ICountriesService
{
    public Task<List<Country>> GetCountries(string countryName, string sortingDirection, int population);
}