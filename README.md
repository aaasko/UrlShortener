# How to run it

1. Install PostgreSQL.
2. Create database `url-shortener`. Username: `postgres`. Password: `postgres`.
3. `cd UrlShortener`, `dotnet ef database update`
4. `dotnet run`

# How to use it

1. `curl -X POST -H "Content-Type: application/json" \
    -d '{ "longUrl": "https://example.com" }' \
    http://localhost:5223/api/v1/data/shorten`
2. Open `localhost:5223/api/v1/shortUrl/{response}` in your browser.
