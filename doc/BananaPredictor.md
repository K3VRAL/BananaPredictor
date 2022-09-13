# Name

BananaPredictor - An application to predict where bananas will be placed for osu!ctb maps.

# Synopsis

bnprdctr [-b file] [-p x:time[|...]]

# Description

BananaPredictor is an application made to allow for the user to predict and place bananas in specific areas that they want. This is done so with the help of sliders as they help allow the spinner to change it's seed to be placed in another area. Because of how many sliders, droplets, or tiny droplets exists, we can be able to influence where the banana will end up based on how many times we can keep putting in these seed affecting objects.

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