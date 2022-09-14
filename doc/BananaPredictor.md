# Name

BananaPredictor - An application to predict where bananas will be placed

# Synopsis

bnprdctr [-b file] [-p x:time[|...]]

# Description

BananaPredictor is an application made to allow for the user to predict and place Banana Shower's bananas in areas which alone couldn't be done without the utilisation of Juice Streams as they help allow the bananas to change it's RNG seed to be placed in different areas. When each Banana Shower's bananas are being evaluated on their x-axis, they modify the RNG values for the rest of the map.

Unlike Banana Showers; Juice Streams are highly controlable in their placement, high count of nested objects, and ability to easily influence the RNG factor. There are three ways in which the application uses the Juice Stream in order to generate // TODO

# Resources

## Beatmap (`-b` or `--beatmap`) - String

Uses the beatmap file as input for difficulty, timing point, and hit object values to reference from.

## Points (`-p` or `--points`)

The format for a point is `x:time|...`. The final point will always go back to the first point.

### x - integer

Is the x-axis for the point. It can go from 0 to 256 as that's how wide osu maps can be.

### time - integer

Is the time for the point relative to the spinner predictor's start time. If the point's time is bigger than the length of the spinner predictor, then the point's time will override it.

### :

Is a point specific seperator such that after we are done filling out one property of the point, we can fill out the next property.

### |

Is a point seperator such that after we are done filling out one point, we can fill out the next point.

# Authors

K 3 V R A L