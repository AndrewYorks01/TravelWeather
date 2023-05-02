__________________
| TRAVEL WEATHER |
__________________

__________________________
| HOW TO RUN THE PROGRAM |
__________________________
To run the program, open:
TravelWeather
bin
Debug
netcoreapp3.1

You should then find an Application file simply called TravelWeather. Double-click on it, and the program will run.

_________________________
| HOW THE PROGRAM WORKS |
_________________________
There are three XML data banks: one for beaches, one for national park-type places, and one for amusement parks.

The beaches database stores max temperature for each month, min temperature for each month, rain, wind speed, dew point, and water temperature.

The national/state parks database shows max temperature for each month, min temperature for each month, rain, wind speed, and dew point.

The amusement parks database shows max temperature for each month, min temperature for each month, rain, wind speed, and dew point.

Inside the program itself will be "baseline" weather information, using weather for Virginia Beach in June as a baseline for beaches and water parks, weather for Chippokes in October as a baseline for camping parks, and weather for Williamsburg in April as a basaeline for dry amusement parks.

The program will ask if the user wants to go to a beach, national/state park, or amusement park, and then ask the time of year the user wants to visit. If the user chooses to visit an amusement park, the program will also ask if they want to ride water rides.

The program will then look at the weather data for all of the chosen locations (for amusement parks, only parks that are open in the selected month) and will assign a score to each one. The highest possible score for beaches is 12, and the highest possible score for other locations is 10.

Note that for amusement parks, there's no guarantee that the park will be open for all or most days of a given month. If in doubt, check the park's website for the most accurate, up-to-date info.

Then, the program will sort the data, showing the highest-scoring locations first.

___________
| SCORING |
___________
Max and min temperatures
If the max/min/etc is within 5 degrees of optimal, +2 points
Between 5-10, +1 point
Anything else, 0 points

Rain
If less than 5, +2 points
If between 5 and 7, +1 point
Greater than 7, 0 points

Wind
If 0-8, +2 points
If 8-12, +1 point
Greater than 12, 0 points

Dew point
If less than 53, +2 points
If between 53-65, +1 points
Greater than 65, 0 points

Water temperatures
If greater than 68, +2 points
If 63-68, +1 point
Less than 63, 0 points

If the low temperature is below 30, the final score is 0, regardless of other factors.
For parks, if the high temperature is above 90, the final score is 0, regardless of other factors.

Note that the score is just a guide based on recent weather data. It's up to you to decide where you want to go.
___________
| CREDITS |
___________
Temperature, rain, wind, and dew point data from wunderground.com
Water temperatures from seatemperature.org
