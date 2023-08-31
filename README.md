
# UseCase_1 Web App - API Documentation

Welcome to the API documentation for the **UseCase_1 Web App**. This ASP.NET Web Application provides a single endpoint to retrieve a list of countries along with advanced filtering, sorting, and pagination options. The endpoint allows you to customize your query based on population, country name, sorting direction, and pagination.

## Endpoint

**GET /countries**

This endpoint retrieves a list of countries based on the provided parameters for filtering, sorting, and pagination. The available query parameters are as follows:

- **population**: Filter countries by population. Only countries with a population lower than the specified value will be included in the response.
- **countriesCount**: Specifies the maximum number of countries to be returned in the response.
- **countryName**: Filter countries by name. Only countries with names containing the provided value will be included in the response.
- **sortingDirection**: Sort the countries in either ascending or descending order based on their names.

## Example Usage

Here are some example URLs demonstrating how to use the API to retrieve country information:

1. Retrieve a list of all countries (without any filters):
   ```
   GET /countries
   ```

2. Filter countries by population lower than 100 million:
   ```
   GET /countries?population=100
   ```

3. Retrieve the first 15 countries in alphabetical order:
   ```
   GET /countries?countriesCount=15&sortingDirection=ascend
   ```

4. Filter and sort countries by name in descending order:
   ```
   GET /countries?countryName=united&sortingDirection=descend
   ```

5. Retrieve countries with names containing "america" and a population lower than 50 million:
   ```
   GET /countries?countryName=america&population=50
   ```

6. Filter countries by name and population, sorted in ascending order:
   ```
   GET /countries?countryName=chi&population=80&sortingDirection=ascend
   ```

7. Retrieve the top 5 less populous countries:
   ```
   GET /countries?countriesCount=5&sortingDirection=descend&population=1
   ```

8. Retrieve a list of all countries, sorted in descending order of name:
   ```
   GET /countries?sortingDirection=descend
   ```

9. Retrieve the first 5 countries:
   ```
   GET /countries?countriesCount=5
   ```

10. Retrieve all countries, which contain "fI":
   ```
   GET /countries?countryName=fI
   ```

We hope this documentation helps you effectively utilize the UseCase_1 Web App API to retrieve the desired country data. If you have any further questions or require assistance, please don't hesitate to reach out.
