# Price Tracking for Loblaws

## More documentation coming soon.

To get this project running, simply spin up a Postgres Database in Docker and configure the connection string.

You will also need an api key for Loblaws. Hint: use your browser tools. It doesn't seem like they rotate it too often.

In order to scrape a product, simply paste the product URL and store number

To find your store number, go here https://www.loblaws.ca/store-locator?type=store
and click on "location details", and copy the number in the URL.

This API is super basic and very much untested at the moment. I'll be providing updates as fast as I can make them. 

# What needs to be done?
 - A frontend.
 - The background update service (probably) doesn't work, so I need to fix that.

Once these things are done, I'll start hosting it somewhere.
