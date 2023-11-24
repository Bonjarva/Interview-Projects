## Veyt-Project's API

This has been hosted temporaily on fly.io so for the endpoints I will be using their path hosted there.

## Endpoints

There are five endpoints in the API:

## Health Check

```
GET https://veyt-project.fly.dev/Co2Emissions/health
->
200 Ok
{
    "Healthy"
}

```

## Get top 10s

```
GET https://veyt-project.fly.dev/Co2Emissions/top10Percapita
->
200 Ok
{
    "country":"Qatar",
    "percapita":37.29
}

```

```
GET https://veyt-project.fly.dev/Co2Emissions/top10LifeExpectancy
->
200 Ok
{
    "country":"Hong Kong",
    "lifeExpectancy":84.277
}

```

## POST requests given country list

```
POST https://veyt-project.fly.dev/Co2Emissions/co2EmissionsAndYearlyChange
{
    "countries": "AFG,nzl"
}
->
200 Ok
[
    {
        "country": "Afghanistan",
        "code": "AFG",
        "cO2Emissions": 9900004,
        "yearlyChange": 7.13
    },
    {
        "country": "New Zealand",
        "code": "NZL",
        "cO2Emissions": 33276202,
        "yearlyChange": -0.14
    }
]
```

```
POST https://veyt-project.fly.dev/Co2Emissions/totalCo2Emissions
{
    "countries": "AFG,nzl"
}
->
200 Ok
{
    "totalEmissions": 43176206
}
```
