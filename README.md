# Introduction

This API is used to book an appointment. Bookings can be requested by passing `name` and `bookingTime`.

Example Request:
```JSON
{
    "name": "Sharmila",
    "bookingTime": "09:00"
}
```

## Build and Run

Run the solution in VisualStudio. You would see the following url will open in the browser.
`https://localhost:55716/swagger/index.html`

## Test the solution

You could test the application in the browser by using `Try it out` button or run the following curl command in the terminal

```
curl -X 'POST' \
  'https://localhost:55716/Booking' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "Sharmila",
  "bookingTime": "09:00"
}'
```